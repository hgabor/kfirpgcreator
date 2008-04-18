using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.corelib {
	class ScriptedMovementAI: MovementAI {
		Script script;
		public ScriptedMovementAI(string scriptName, Game game) {
			script = game.vm.LoadScript(game.loader.LoadText("scripts/movement/" + scriptName));
		}

		public override void TryDoingSomething(Sprite me, Map map) {
			if (me.IsMoving) return;
			script.Owner = me;
			object result = script.Run();
			if (result != null && result.GetType() == typeof(string)) {
				string res = (string)result;
				switch (res.ToLower()) {
					case "up": me.PlanMove(Sprite.Dir.Up, map); break;
					case "down": me.PlanMove(Sprite.Dir.Down, map); break;
					case "left": me.PlanMove(Sprite.Dir.Left, map); break;
					case "right": me.PlanMove(Sprite.Dir.Right, map); break;
					default: me.PlanMove(Sprite.Dir.None, map); break;
				}
			}
		}
	}
}
