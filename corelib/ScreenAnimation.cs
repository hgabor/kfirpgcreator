using System;
using System.Collections.Generic;
using System.Text;
using SdlDotNet.Graphics;

namespace KFIRPG.corelib {
	abstract class ScreenAnimation:Screen {
		protected Game game;
		protected Surface from;
		public Surface FromImage { set { from = value; } }

		public ScreenAnimation(Game game) {
			this.game = game;
		}
	}
}
