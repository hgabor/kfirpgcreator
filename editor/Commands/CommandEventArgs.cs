using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.editor.Commands {
	class CommandEventArgs: EventArgs {
		public Command Command { get; set; }
		public CommandEventArgs(Command c) {
			this.Command = c;
		}
	}
}
