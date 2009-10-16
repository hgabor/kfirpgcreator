using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.editor.Commands {
	class CommandList: Command {
		List<Command> list = new List<Command>();

		public void Add(Command command) {
			list.Add(command);
		}
		public bool Contains(Command command) {
			return list.Contains(command);
		}
		public bool IsEmpty {
			get { return list.Count == 0; }
		}

		public override string Name {
			get {
				return string.Join(" ", list.ConvertAll<string>(c => c.Name).ToArray());
			}
		}

		public override void Do() {
			for (int i = 0; i < list.Count; ++i) {
				list[i].Do();
			}
		}

		public override void Undo() {
			for (int i = list.Count - 1; i >= 0; --i) {
				list[i].Undo();
			}
		}
	}
}
