using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.corelib {
	/// <summary>
	/// A sprite that can trigger OnStep events.
	/// </summary>
	class PlayerSprite: Sprite {
		public PlayerSprite(string spriteId, Game game) : base(spriteId, game) { }

		int lastX = -1;
		int lastY = -1;
		int lastLayer = -1;
		bool lastResult = false;
		protected override bool OnStep(Map map) {
			if (X == lastX && Y == lastY && Layer == lastLayer && lastResult) {
				return true;
			}
			else {
				lastResult = map.OnStep(X, Y, Layer);
				lastX = X;
				lastY = Y;
				lastLayer = Layer;
				return lastResult;
			}
		}
	}
}
