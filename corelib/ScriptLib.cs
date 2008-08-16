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
	class ScriptLib {
		Dialogs dialogs;
		Game game;
		Random random = new Random();

		[Script]
		public CustomScreen CustomScreen_New() {
			return new CustomScreen(game);
		}
		[Script]
		public void CustomScreen_Place(CustomScreen screen, int x, int y, Graphics gfx) {
			screen.Place(gfx, new System.Drawing.Point(x, y));
		}
		[BlockingScript]
		public void CustomScreen_Show(CustomScreen screen) {
			FadeAnimation anim = new FadeAnimation(game);
			anim.FromImage = game.TakeScreenshot();
			game.PushScreen(screen);
			game.PushScreen(anim);
		}
		[Script]
		public void CustomScreen_Hide(CustomScreen screen) {
			screen.Hide();
		}
		[Script]
		public void CustomScreen_OnKey_Add(CustomScreen screen, ScriptFunction script) {
			screen.KeyPressed += (sender, args) => script.Run((int)args.Button);
		}
		[Script]
		public void CustomScreen_Delete() {
			//No-op for now...
		}

		[Script]
		public TextGraphics TextGraphics_New(string text, string align) {
			return new TextGraphics(
				text,
				(TextGraphics.Align)Enum.Parse(typeof(TextGraphics.Align), align, true),
				dialogs,
				game);
		}

		[Script]
		public int Graphics_GetWidth(Graphics graphics) {
			return graphics.Width;
		}

		[Script]
		public int Graphics_GetHeight(Graphics graphics) {
			return graphics.Height;
		}

		[Script]
		public WindowGraphics WindowGraphics_New(int width, int height) {
			return new WindowGraphics(width, height, dialogs);
		}

		[Script]
		public PanelGraphics MenuItemBackground_New(int width, int height) {
			return new PanelGraphics(width, height, dialogs.selectedBg, dialogs.selectedBorder);
		}

		[Script]
		public AnimatedGraphics AnimatedGraphics_New(string name) {
			return new AnimatedGraphics(name, game);
		}

		[Script]
		public void AnimatedGraphics_SetState(AnimatedGraphics gfx, string state) {
			gfx.SetState(state);
		}

		[Script]
		public void AnimatedGraphics_SetDir(AnimatedGraphics gfx, string dir) {
			gfx.SetDirection((Sprite.Dir)Enum.Parse(typeof(Sprite.Dir), dir, true));
		}

		[Script]
		public ImageGraphics ImageGraphics_New(string name) {
			return new ImageGraphics(name, game);
		}

		[Script]
		public int RGB(int r, int g, int b) {
			return System.Drawing.Color.FromArgb(r, g, b).ToArgb();
		}
		[Script]
		public int ARGB(int a, int r, int g, int b) {
			return System.Drawing.Color.FromArgb(a, r, g, b).ToArgb();
		}

		[Script]
		public CounterBarGraphics CounterBarGraphics_New(int bg, int border, int height, int width) {
			return new CounterBarGraphics(
				System.Drawing.Color.FromArgb(bg),
				System.Drawing.Color.FromArgb(border),
				height, width);
		}

		[Script]
		public void CounterBarGraphics_SetValue(CounterBarGraphics cbg, int val) {
			cbg.Value = val;
		}
		[Script]
		public void CounterBarGraphics_SetMaxValue(CounterBarGraphics cbg, int maxval) {
			cbg.MaxValue = maxval;
		}

		/// <summary>
		/// Starts playing a music. The music file must be in the "music" folder.
		/// </summary>
		/// <param name="fileName"></param>
		[Script]
		public void StartMusic(string fileName) {
			game.audio.StartMusic(fileName);
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
