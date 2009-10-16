using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.corelib {
	/// <summary>
	/// A screen that accepts no input for a specified number of frames.
	/// </summary>
	class WaitingScreen: Screen {
		int remaining;
		Game game;
		public WaitingScreen(int frames, Game game) {
			this.game = game;
			remaining = frames;
		}

		public override void Render(SdlDotNet.Graphics.Surface surface) { }

		public override void Advance() {
			if (--remaining == 0) {
				game.PopScreen();
				game.vm.ContinueWithValue(null);
			}
			else {
				game.AdvanceScreenBelow();
			}
		}
	}
}
