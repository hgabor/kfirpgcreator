using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.corelib {
	[AttributeUsage(AttributeTargets.Method)]
	class ScriptAttribute: Attribute { }
	[AttributeUsage(AttributeTargets.Method)]
	class AsyncScriptAttribute: Attribute { }

	class ScriptLib {
		Dialogs dialogs;
		Game game;
		Random random = new Random();

		[AsyncScript]
		public void Message(string message) {
			dialogs.Message(message);
		}

		[Script]
		public void StartMusic(string fileName) {
			game.audio.StartMusic(fileName);
		}

		[Script]
		public void Turn(Sprite her, Sprite to) {
			int x = to.X - her.X;
			int y = to.Y - her.Y;
			if (x + y > 0) {
				if (y - x > 0) her.Turn(Sprite.Dir.Down);
				else her.Turn(Sprite.Dir.Right);
			}
			else {
				if (y - x > 0) her.Turn(Sprite.Dir.Left);
				else her.Turn(Sprite.Dir.Up);
			}
		}

		public ScriptLib(Game game) {
			this.game = game;
			dialogs = new Dialogs(game);
		}
	}
}
