
using System;
using System.Collections.Generic;

namespace KFIRPG.editor.Undo {
	/// <summary>
	/// Description of UndoStack.
	/// </summary>
	public class UndoStack {
		public UndoStack() {
			
		}
		
		Stack<UndoCommandList> undoStack = new Stack<UndoCommandList>();
		Stack<UndoCommandList> redoStack = new Stack<UndoCommandList>();

		public event EventHandler Command;
		void OnCommand(EventArgs e) {
			if (Command != null) Command(this, e);
		}
		public void DoCommand(UndoCommandList c) {
			c.Do();
			undoStack.Push(c);
			redoStack.Clear();
			OnCommand(new EventArgs());
		}
		public bool CanUndo {
			get { return undoStack.Count != 0; }
		}
		public string UndoName {
			get { return undoStack.Peek().Name; }
		}
		public void Undo() {
			var c = undoStack.Pop();
			c.Undo();
			redoStack.Push(c);
			OnCommand(new EventArgs());
		}
		public bool CanRedo {
			get { return redoStack.Count != 0; }
		}
		public string RedoName {
			get { return redoStack.Peek().Name; }
		}
		public void Redo() {
			var c = redoStack.Pop();
			c.Do();
			undoStack.Push(c);
			OnCommand(new EventArgs());
		}
	}
}
