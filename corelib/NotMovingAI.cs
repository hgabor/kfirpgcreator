using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.corelib {
	/// <summary>
	/// The sprites does not move, it stands still.
	/// </summary>
	class NotMovingAI: MovementAI {
		public override void TryDoingSomething(Sprite me, Map map) { }
	}
}
