using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.corelib {
	[AttributeUsage(AttributeTargets.Method)]
	class RegisterScriptAttribute: Attribute { }

	class ScriptLib {
		Dialogs dialogs;
		Game game;

		[RegisterScript]
		public void Message(string message) {
			dialogs.Message(message);
		}

		public ScriptLib(Game game) {
			dialogs = new Dialogs(game);
		}
	}
}
