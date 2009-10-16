using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.editor.Commands {
	abstract class Command {
		private class NoneCommand: Command {
			public override string Name {
				get { return "None"; }
			}
			public override void Do() { }
			public override void Undo() { }
		}
		public static readonly Command None = new NoneCommand();


		public abstract string Name { get; }

		public abstract void Do();
		public abstract void Undo();
	}
}
