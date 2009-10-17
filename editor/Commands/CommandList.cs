using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.editor.Commands {
	class CommandList {
		List<Command> list = new List<Command>();

		public CommandList(string name) {
			this.Name = name;
		}

		public void Add(Command command) {
			list.Add(command);
		}
		public bool Contains(Command command) {
			return list.Contains(command);
		}
		public bool IsEmpty {
			get { return list.Count == 0; }
		}

		public string Name { get; private set; }

		public void Do() {
			for (int i = 0; i < list.Count; ++i) {
				list[i].Do();
			}
		}

		public void Undo() {
			for (int i = list.Count - 1; i >= 0; --i) {
				list[i].Undo();
			}
		}
	}
}
