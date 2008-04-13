using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.corelib {
	abstract class MovementAI {
		public abstract void TryDoingSomething(Sprite me, Map map);
	}
}
