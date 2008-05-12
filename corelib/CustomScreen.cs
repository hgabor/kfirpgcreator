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

		public void Add(Graphics gfx, Point coords) {
			graphics.Add(new ScreenGraphics(gfx, coords));
		}

		public override void Draw(SdlDotNet.Graphics.Surface surface) {
			foreach (ScreenGraphics gfx in graphics) {
				gfx.graphics.Blit(gfx.coords.X, gfx.coords.Y, surface);
			}
		}

		public override void Think() {
			UserInput.Buttons buttons = game.Input.State;
			if (buttons != UserInput.Buttons.None) {
				game.vm.ContinueWithValue((int)buttons);
			}
		}
	}
}
