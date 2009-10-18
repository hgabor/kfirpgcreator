using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.editor.Commands {
	class CommandList: List<Command> {
		public CommandList(string name) {
			this.Name = name;
		}

		public bool IsEmpty {
			get { return this.Count == 0; }
		}

		public string Name { get; private set; }

		public void Do() {
			for (int i = 0; i < this.Count; ++i) {
				this[i].Do();
			}
		}

		public void Undo() {
			for (int i = this.Count - 1; i >= 0; --i) {
				this[i].Undo();
			}
		}
	}
}
