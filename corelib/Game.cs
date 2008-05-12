﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace KFIRPG.corelib {

	/// <summary>
	/// Contains the core modules of the game.
	/// </summary>
	public class Game {

		/// <summary>
		/// Loads a new game using the selected loader.
		/// </summary>
		/// <param name="loader">The loader to load the game with.</param>
		private Game(Loader loader) {
			this.loader = new CachedLoader(loader);
			XmlDocument globalSettings = new XmlDocument();
			globalSettings.LoadXml(loader.LoadText("global.xml"));

			string scriptvm = globalSettings.SelectSingleNode("/settings/scriptvm").InnerText;
			switch (scriptvm) {
				case "lua":
					vm = new LuaVM(this);
					break;
				case "taolua":
					vm = new TaoLuaVM(this);
					break;
				default:
					throw new InvalidValueException("Script VM", scriptvm);
			}

			audio = new Audio(this);

			width = int.Parse(globalSettings.SelectSingleNode("/settings/screenwidth").InnerText);
			height = int.Parse(globalSettings.SelectSingleNode("/settings/screenheight").InnerText);
			tileSize = int.Parse(globalSettings.SelectSingleNode("/settings/tilesize").InnerText);

			Map defaultMap = new Map(globalSettings.SelectSingleNode("/settings/defaultmap").InnerText, this);
			int startX = int.Parse(globalSettings.SelectSingleNode("/settings/startx").InnerText);
			int startY = int.Parse(globalSettings.SelectSingleNode("/settings/starty").InnerText);
			int startL = int.Parse(globalSettings.SelectSingleNode("/settings/startl").InnerText);
			currentMap = defaultMap;

			party = new Party(this);
			foreach (XmlNode node in globalSettings.SelectNodes("/settings/party")) {
				party.Add(new PlayerSprite(node.InnerText, this));
			}
			party.Leader.MovementAI = new PlayerMovementController(this);

			foreach(XmlNode loc in globalSettings.SelectNodes("/settings/locations/location")) {
				locations.Add(loc.Attributes["name"].InnerText.Trim(),
					new Location(int.Parse(loc.SelectSingleNode("x").InnerText),
						int.Parse(loc.SelectSingleNode("y").InnerText),
						int.Parse(loc.SelectSingleNode("layer").InnerText),
						loc.SelectSingleNode("map").InnerText.Trim()));
			}

			currentMap.Place(party.Leader, startX, startY, startL);

			PushScreen(new MapScreen(this));

			startupScript = vm.LoadResumableScript(loader.LoadText("scripts/" + globalSettings.SelectSingleNode("/settings/startscript").InnerText));
			startupScript.Run();
		}

		/// <summary>
		/// The scripting engine.
		/// </summary>
		internal ScriptVM vm;

		/// <summary>
		/// The loader used to load all game data.
		/// </summary>
		internal Loader loader;

		/// <summary>
		/// The audio subsystem.
		/// </summary>
		internal Audio audio;

		int width;
		/// <summary>
		/// Gets the width of the screen.
		/// </summary>
		public int Width { get { return width; } }

		int height;
		/// <summary>
		/// Gets the height of the screen.
		/// </summary>
		public int Height { get { return height; } }

		int tileSize;
		/// <summary>
		/// Gets the size of the tile's side in pixels.
		/// </summary>
		public int TileSize { get { return tileSize; } }

		List<Screen> screens = new List<Screen>();
		/// <summary>
		/// Deactivates the current screen and activates a new one on top of the current.
		/// </summary>
		/// <param name="sc">The new screen to activate.</param>
		internal void PushScreen(Screen sc) {
			screens.Add(sc);
		}
		/// <summary>
		/// Deactivates the current screen and activates the one below.
		/// </summary>
		internal void PopScreen() {
			screens.RemoveAt(screens.Count - 1);
		}
		/// <summary>
		/// The current map.
		/// </summary>
		internal Map currentMap;

		Dictionary<string, Location> locations = new Dictionary<string, Location>();
		/// <summary>
		/// Gets a named location.
		/// </summary>
		/// <param name="name">The name of the location.</param>
		/// <returns>The location</returns>
		internal Location GetLocation(string name) {
			return locations[name];
		}

		Party party;
		/// <summary>
		/// Gets the player's current party.
		/// </summary>
		internal Party Party { get { return party; } }

		Script startupScript;

		/// <summary>
		/// Thrown when a settings file's structure is malformed.
		/// </summary>
		public class SettingsException: Exception {
			public SettingsException(string msg) : base("The settings file is malformed: " + msg) { }
		}
		/// <summary>
		/// Thrown when a settings file contains invalid data.
		/// </summary>
		public class InvalidValueException: SettingsException {
			public InvalidValueException(string key, string value) : base(string.Format("\"{0}\" cannot have value \"{1}\"", key, value)) { }
		}

		/// <summary>
		/// Loads a game from the hard disk.
		/// </summary>
		/// <param name="fileName">The name of the file or directory to load from.</param>
		/// <returns>A new game</returns>
		public static Game LoadFromFile(string fileName) {
			Loader loader;
			XmlDocument gameInfo = new XmlDocument();
			gameInfo.Load(fileName);
			string loadPath = gameInfo.SelectSingleNode("/settings/loadpath").InnerText;
			string loaderType = gameInfo.SelectSingleNode("/settings/loader").InnerText;
			switch (loaderType) {
				case "file":
					loader = new FileLoader(loadPath);
					break;
				case "zip":
					loader = new ZipLoader(loadPath);
					break;
				default:
					throw new InvalidValueException("Loader", loaderType);
			}

			Game game = new Game(loader);

			return game;
		}

		int currentScreenAdvancing;
		/// <summary>
		/// Advances the screen below the current one by one frame.
		/// </summary>
		internal void AdvanceScreenBelow() {
			if (currentScreenAdvancing < 0) return;
			Input.Disable();
			--currentScreenAdvancing;
			screens[currentScreenAdvancing].Think();
			++currentScreenAdvancing;
			if (currentScreenAdvancing == screens.Count - 1) Input.Enable();
		}

		/// <summary>
		/// Advances the game by one frame.
		/// </summary>
		public void Advance() {
			currentScreenAdvancing = screens.Count - 1;
			screens[currentScreenAdvancing].Think();
		}

		/// <summary>
		/// Renders the current state of the game.
		/// </summary>
		/// <param name="surface">The surface to render the game to.</param>
		public void Render(SdlDotNet.Graphics.Surface surface) {
			foreach (Screen screen in screens) {
				screen.Draw(surface);
			}
		}

		/// <summary>
		/// Renders the game to a new surface.
		/// </summary>
		/// <returns>A new surface containing the current state of the game.</returns>
		public SdlDotNet.Graphics.Surface TakeScreenshot() {
			SdlDotNet.Graphics.Surface surface = new SdlDotNet.Graphics.Surface(width, height);
			Render(surface);
			return surface;
		}

		/// <summary>
		/// The input source of the game.
		/// </summary>
		public readonly UserInput Input = new UserInput();
	}
}
