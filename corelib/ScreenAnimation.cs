using System;
using System.Collections.Generic;
using System.Text;
using SdlDotNet.Graphics;

namespace KFIRPG.corelib {
	/// <summary>
	/// Abstract base class for transition effects from one screen to another.
	/// </summary>
	abstract class ScreenAnimation: Screen {
		protected Game game;
		/// <summary>
		/// The surface the transition starts from.
		/// </summary>
		protected Surface from;
		/// <summary>
		/// Sets the image the transition should start from.
		/// </summary>
		public Surface FromImage { set { from = value; } }

		public ScreenAnimation(Game game) {
			this.game = game;
		}
	}
}
