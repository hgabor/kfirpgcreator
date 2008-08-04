using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace KFIRPG.corelib {
	/// <summary>
	/// This screen's contents can be customized by scripts.
	/// </summary>
	class CustomScreen: Screen {
		Game game;
		public CustomScreen(Game game) {
			this.game = game;
		}

		class ScreenGraphics {
			public Graphics graphics;
			public Point coords;
			public ScreenGraphics(Graphics graphics, Point coords) {
				this.graphics = graphics;
				this.coords = coords;
			}
		}
		List<ScreenGraphics> graphics = new List<ScreenGraphics>();

		public void UpdateEventHandler() {
			graphics = new List<ScreenGraphics>();
		}

		public void Place(Graphics gfx, Point coords) {
			ScreenGraphics sg = graphics.Find(s => s.graphics == gfx);
			if (sg == null) {
				graphics.Add(new ScreenGraphics(gfx, coords));
			}
			else {
				sg.coords = coords;
			}
		}

		bool hide = false;
		internal void Hide() {
			hide = true;
		}

		public event EventHandler<UserInput.ButtonEventArgs> KeyPressed;

		public override void Draw(SdlDotNet.Graphics.Surface surface) {
			foreach (ScreenGraphics gfx in graphics) {
				gfx.graphics.Blit(gfx.coords.X, gfx.coords.Y, surface);
			}
		}

		public override void Think() {
			if (hide) {
				FadeAnimation animation = new FadeAnimation(game);
				animation.FromImage = game.TakeScreenshot();
				game.PopScreen();
				game.vm.ContinueWithValue(null);
				game.PushScreen(animation);
			}
			else {
				UserInput.Buttons keyState = game.Input.State;
				if (keyState != UserInput.Buttons.None) {
					if (KeyPressed != null) KeyPressed(this, new UserInput.ButtonEventArgs(keyState));
				}
				game.Input.WaitForKeyUp();
			}
		}
	}
}
