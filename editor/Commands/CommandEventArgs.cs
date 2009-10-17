using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.editor.Commands {
	class CommandEventArgs: EventArgs {
		public CommandList Command { get; set; }
		public CommandEventArgs(CommandList c) {
			this.Command = c;
		}
	}
}
