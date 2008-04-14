using System;
using System.Collections.Generic;
using System.Text;
using SdlDotNet.Input;

namespace KFIRPG.corelib {
	class PlayerMovementController: MovementAI {
		public override void TryDoingSomething(Sprite me, Map map) {
			if (Keyboard.IsKeyPressed(Key.UpArrow)) {
				me.PlanMove(Sprite.Dir.Up, map);
			}
			else if (Keyboard.IsKeyPressed(Key.DownArrow)) {
				me.PlanMove(Sprite.Dir.Down, map);
			}
			else if (Keyboard.IsKeyPressed(Key.LeftArrow)) {
				me.PlanMove(Sprite.Dir.Left, map);
			}
			else if (Keyboard.IsKeyPressed(Key.RightArrow)) {
				me.PlanMove(Sprite.Dir.Right, map);
			}
		}
	}
}
