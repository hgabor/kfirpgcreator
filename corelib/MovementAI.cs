using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.corelib {
	/// <summary>
	/// Abstract base class for controlling sprites on the map.
	/// </summary>
	abstract class MovementAI {
		/// <summary>
		/// Decides the next action of the sprite.
		/// </summary>
		/// <param name="me">The sprite whose action is to be decided.</param>
		/// <param name="map">The map the sprite is on.</param>
		public abstract void TryDoingSomething(Sprite me, Map map);
	}
}
