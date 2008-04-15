using System;
using System.Collections.Generic;
using System.Text;
using SdlDotNet.Graphics;
using System.Drawing;

namespace KFIRPG.corelib {
	class MessageBox: Screen {
		Game game;
		Dialogs dialogs;
		int width;
		int height;
		Surface textSurface;

		//TODO: Loader should load the Fonts
		public MessageBox(string text, Dialogs dialogs, Game game) {
			this.game = game;
			this.dialogs = dialogs;
			SdlDotNet.Graphics.Font font = new SdlDotNet.Graphics.Font("TestGame\\dialog\\DejaVuSans.ttf", 12);
			textSurface = font.Render(text, Color.White, true);
		}

		public override void Draw(SdlDotNet.Graphics.Surface surface) {
			dialogs.DrawWindow(599, 399, surface);
			surface.Blit(textSurface, new Point(100, 100));
		}

		public override void Think() {
			if (game.Input.IsPressed(UserInput.Buttons.Action)) {
				game.PopScreen();
				game.vm.ContinueWithValue(null);
			}
		}
	}
}
