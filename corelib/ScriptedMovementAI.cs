using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.corelib {
	/// <summary>
	/// A controller where the sprite is controlled by a script.
	/// </summary>
	class ScriptedMovementAI: MovementAI {
		Script script;
		/// <summary>
		/// Creates a new instance. The code for the script must be inside the "scripts" folder.
		/// </summary>
		/// <param name="scriptName">The name of the script.</param>
		/// <param name="game"></param>
		public ScriptedMovementAI(string scriptName, Game game) {
			script = game.vm.LoadNonResumableScript(game.loader.LoadText("scripts/" + scriptName));
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
