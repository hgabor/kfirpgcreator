using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace KFIRPG.corelib {
	public class Game {
		private Game(Loader loader) {
			this.loader = loader;
			XmlDocument globalSettings = new XmlDocument();
			globalSettings.LoadXml(loader.LoadText("global.xml"));

			string scriptvm = globalSettings.SelectSingleNode("/settings/scriptvm").InnerText;
			switch (scriptvm) {
				case "lua":
					vm = new LuaVM();
					break;
				default:
					throw new InvalidValueException("Script VM", scriptvm);
			}
			width = int.Parse(globalSettings.SelectSingleNode("/settings/screenwidth").InnerText);
			height = int.Parse(globalSettings.SelectSingleNode("/settings/screenheight").InnerText);
			tileSize = int.Parse(globalSettings.SelectSingleNode("/settings/tilesize").InnerText);

			Map defaultMap = new Map(globalSettings.SelectSingleNode("/settings/defaultmap").InnerText, this);
			int startX = int.Parse(globalSettings.SelectSingleNode("/settings/startx").InnerText);
			int startY = int.Parse(globalSettings.SelectSingleNode("/settings/starty").InnerText);
			int startL = int.Parse(globalSettings.SelectSingleNode("/settings/startl").InnerText);

			party = new Party();
			foreach (XmlNode node in globalSettings.SelectNodes("/settings/party")) {
				party.Add(new Sprite(node.InnerText, this));
			}
			party.Leader.MovementAI = new PlayerMovementController();

			screen = new MapScreen(defaultMap, startX, startY, startL, this);
		}

		internal ScriptVM vm;
		internal Loader loader;
		int width;
		public int Width { get { return width; } }
		int height;
		public int Height { get { return height; } }
		int tileSize;
		public int TileSize { get { return tileSize; } }
		Screen screen;
		Party party;
		internal Party Party { get { return party; } }

		public class SettingsException: Exception {
			public SettingsException(string msg) : base("The settings file is malformed: " + msg) { }
		}
		public class InvalidValueException: SettingsException {
			public InvalidValueException(string key, string value) : base(string.Format("\"{0}\" cannot have value \"{1}\"", key, value)) { }
		}

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

		public void Advance() {
			screen.Think();
		}

		public void Render(SdlDotNet.Graphics.Surface surface) {
			screen.Draw(surface);
		}
	}
}
