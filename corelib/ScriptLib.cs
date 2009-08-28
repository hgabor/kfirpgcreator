using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.corelib {
	/// <summary>
	/// A script function that returns immediately.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method)]
	class ScriptAttribute: Attribute { }
	/// <summary>
	/// A script function that does not return immediately, but waits for
	/// events (e.g. user interaction, time etc.)
	/// </summary>
	[AttributeUsage(AttributeTargets.Method)]
	class BlockingScriptAttribute: Attribute { }

	/// <summary>
	/// A library of common script functions.
	/// </summary>
	partial class ScriptLib {
		Dialogs dialogs;
		Game game;
		Random random = new Random();

		/// <summary>
		/// Starts playing a music. The music file must be in the "music" folder.
		/// </summary>
		/// <param name="fileName"></param>
		[Script]
		public void StartMusic(string fileName) {
			game.audio.StartMusic(fileName);
		}

		[Script]
		public void StopMusic() {
			game.audio.StopMusic();
		}

		/// <summary>
		/// Makes a sprite turn in the direction of another sprite.
		/// </summary>
		/// <param name="her"></param>
		/// <param name="to"></param>
		[Script]
		public void Turn(Sprite her, Sprite to) {
			int x = to.X - her.X;
			int y = to.Y - her.Y;
			if (x + y > 0) {
				if (y - x > 0) her.Turn(Sprite.Dir.Down);
				else her.Turn(Sprite.Dir.Right);
			}
			else {
				if (y - x > 0) her.Turn(Sprite.Dir.Left);
				else her.Turn(Sprite.Dir.Up);
			}
		}

		/// <summary>
		/// Writes a string on the Console.
		/// Debug only.
		/// </summary>
		/// <param name="text"></param>
		[Script]
		public void WriteLine(object text) {
			Console.WriteLine(text.ToString());
		}

		/// <summary>
		/// Moves the player to a location on the current map.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="layer"></param>
		public void ShortJump(int x, int y, int layer) {
			FadeAnimation anim = new FadeAnimation(game);
			anim.FromImage = game.TakeScreenshot();
			Sprite leader = game.Party.Leader;
			game.currentMap.Move(leader, leader.X, leader.Y, leader.Layer, x, y, layer);
			game.currentMap.OnStep(x, y, layer);
			game.PushScreen(anim);
		}

		/// <summary>
		/// Moves the player to a location on another map.
		/// </summary>
		/// <param name="loc"></param>
		public void LongJump(Location loc) {
			FadeAnimation anim = new FadeAnimation(game);
			anim.FromImage = game.TakeScreenshot();
			Sprite leader = game.Party.Leader;
			game.currentMap.Remove(leader, leader.X, leader.Y, leader.Layer);
			game.currentMap = new Map(loc.MapName, game);
			game.currentMap.Place(leader, loc.X, loc.Y, loc.Layer);
			game.PushScreen(anim);
		}

		/// <summary>
		/// Moves the player to a named location with a fade animation.
		/// </summary>
		/// <param name="locationStr"></param>
		[Script]
		public void MoveTo(string locationStr) {
			Location location = game.GetLocation(locationStr);
			if (location.MapName == game.currentMap.Name) {
				ShortJump(location.X, location.Y, location.Layer);
			}
			else {
				LongJump(location);
			}
		}

		[Script]
		public void QuitGame() {
			game.AskToQuit();
		}

		[Script]
		public void NewGame() {
			game.NewGame();
		}

		/// <summary>
		/// Saves game data to the specified save slot.
		/// </summary>
		/// <param name="saveData">The string to be saved</param>
		/// <param name="saveSlot">Number of the save slot</param>
		[Script]
		public void SaveGame(string saveData, int saveSlot) {
			using (FileSaver saver = new FileSaver("save")) {
				saver.Save(string.Format("save{0}", saveSlot), saveData);
				PropertyWriter map = saver.CreatePropertyFile(string.Format("savemap{0}.xml", saveSlot));
				this.game.currentMap.SaveToSaveFile(map);
			}
		}

		/// <summary>
		/// Loads game data from the specified save slot.
		/// </summary>
		/// <param name="saveSlot">Number of the save slot</param>
		/// <returns>The loaded string</returns>
		[Script]
		public void LoadGame(int saveSlot) {
			FileLoader loader = new FileLoader("save");

			game.LoadGame(saveSlot, loader);
		}

		[Script]
		public string[] GetSaveSlots() {
			FileLoader loader = new FileLoader("save");
			string[] s = new string[20];
			for (int i = 1; i < 20; ++i) {
				string fileName = string.Format("savemap{0}.xml", i);
				if (!loader.Exists(fileName)) {
					s[i - 1] = "";
				}
				else {
					PropertyReader r = loader.GetPropertyReader().Select(fileName);
					s[i - 1] = r.GetString("mapname");
				}
			}
			return s;
		}

		List<string> includedScripts = new List<string>();
		/// <summary>
		/// Includes another script. The script must be in the "scripts" folder.
		/// Every file is included at most once per game.
		/// </summary>
		/// <param name="scriptName"></param>
		[Script]
		public void include(string scriptName) {
			if (!includedScripts.Contains(scriptName)) {
				includedScripts.Add(scriptName);
				game.vm.LoadNonResumableScript(game.loader.LoadText("scripts/" + scriptName)).Run();
			}
		}

		[Script]
		public void run(string scriptName) {
			game.vm.LoadResumableScript(game.loader.LoadText("scripts/" + scriptName)).Run();
		}

		/// <summary>
		/// Changes the animation of a sprite.
		/// </summary>
		/// <param name="sprite"></param>
		/// <param name="animation"></param>
		[Script]
		public void SetAnimation(Sprite sprite, string animation) {
			sprite.SetAnimation(animation.ToLower().Trim());
		}

		/// <summary>
		/// Blocks player input for a number of milliseconds.
		/// </summary>
		/// <param name="milliseconds"></param>
		/// <remarks>This is approximate, the waiting time is rounded to the number of
		/// frames to wait.</remarks>
		[BlockingScript]
		public void Wait(int milliseconds) {
			int frames = milliseconds / 20;
			game.PushScreen(new WaitingScreen(frames, game));
		}

		public ScriptLib(Game game) {
			this.game = game;
			dialogs = new Dialogs(game);
		}
	}
}
