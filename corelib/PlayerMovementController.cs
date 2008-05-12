using System;
using System.Collections.Generic;
using System.Text;
using SdlDotNet.Input;

namespace KFIRPG.corelib {
	/// <summary>
	/// Controls the sprite with user input.
	/// </summary>
	class PlayerMovementController: MovementAI {
		Game game;
		public PlayerMovementController(Game game) { this.game = game; }

		public override void TryDoingSomething(Sprite me, Map map) {
			if (game.Input.IsPressed(UserInput.Buttons.Up)) {
				me.PlanMove(Sprite.Dir.Up, map);
			}
			else if (game.Input.IsPressed(UserInput.Buttons.Down)) {
				me.PlanMove(Sprite.Dir.Down, map);
			}
			else if (game.Input.IsPressed(UserInput.Buttons.Left)) {
				me.PlanMove(Sprite.Dir.Left, map);
			}
			else if (game.Input.IsPressed(UserInput.Buttons.Right)) {
				me.PlanMove(Sprite.Dir.Right, map);
			}
			if (game.Input.IsPressed(UserInput.Buttons.Action)) {
				me.PlanAction(map);
			}
		}
	}
}
