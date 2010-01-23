using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.editor.Undo {
	public class UndoEventArgs: EventArgs {
		public UndoCommandList Command { get; set; }
		public UndoEventArgs(UndoCommandList c) {
			this.Command = c;
		}
	}
}
