using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.editor.Commands {
	class Command {
		public delegate void Function();

		Function doFunc, undoFunc;
		public Command(Function doFunc, Function undoFunc) {
			this.doFunc = doFunc;
			this.undoFunc = undoFunc;
		}

		public void Do() {
			doFunc();
		}
		public void Undo() {
			undoFunc();
		}
	}
}
