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

		[AsyncScript]
		public void Message(string message) {
			dialogs.Message(message);
		}

		[Script]
		public void StartMusic(string fileName) {
			game.audio.StartMusic(fileName);
		}

		public ScriptLib(Game game) {
			this.game = game;
			dialogs = new Dialogs(game);
		}
	}
}
