using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.corelib {
	class FadeAnimation: ScreenAnimation {
		byte alphaCounter = 255;

		public FadeAnimation(Game game) : base(game) { }

		public override void Draw(SdlDotNet.Graphics.Surface surface) {
			from.Alpha = alphaCounter;
			from.AlphaBlending = true;
			surface.Blit(from);
		}

		public override void Think() {
			alphaCounter -= 15;
			if (alphaCounter == 0) {
				game.PopScreen();
			}
		}
	}
}
