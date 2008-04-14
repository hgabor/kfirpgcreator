using System;
using System.Collections.Generic;
using System.Text;
using SdlDotNet.Input;

namespace KFIRPG.corelib {
	class PlayerMovementController: MovementAI {
		public override void TryDoingSomething(Sprite me, Map map) {
			int x = me.X;
			int y = me.Y;
			int l = me.Layer;
			if (Keyboard.IsKeyPressed(Key.UpArrow)) {
				if (map.IsPassable(x, y - 1, l)) {
					me.PlanMove(Sprite.Dir.Up);
				}
				else me.PlanMove(Sprite.Dir.None);
			}
			else if (Keyboard.IsKeyPressed(Key.DownArrow)) {
				if (map.IsPassable(x, y + 1, l)) {
					me.PlanMove(Sprite.Dir.Down);
				}
				else me.PlanMove(Sprite.Dir.None);
			}
			else if (Keyboard.IsKeyPressed(Key.LeftArrow)) {
				if (map.IsPassable(x - 1, y, l)) {
					me.PlanMove(Sprite.Dir.Left);
				}
				else me.PlanMove(Sprite.Dir.None);
			}
			else if (Keyboard.IsKeyPressed(Key.RightArrow)) {
				if (map.IsPassable(x + 1, y, l)) {
					me.PlanMove(Sprite.Dir.Right);
				}
				else me.PlanMove(Sprite.Dir.None);
			}
		}
	}
}
