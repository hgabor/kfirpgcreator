using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.editor.Undo {
	public class UndoCommand {
		public delegate void Function();

		public int X { get; private set; }
		public int Y { get; private set; }
		public int TileX { get; private set; }
		public int TileY { get; private set; }

		Function doFunc, undoFunc;
		public UndoCommand(int x, int y, int tileX, int tileY, Function doFunc, Function undoFunc) {
			this.X = x;
			this.Y = y;
			this.TileX = tileX;
			this.TileY = tileY;
			this.doFunc = doFunc;
			this.undoFunc = undoFunc;
		}

		public UndoCommand(int x, int y, Function doFunc, Function undoFunc)
			: this(x, y, 0, 0, doFunc, undoFunc) { }
		
		public UndoCommand(Function doFunc, Function undoFunc)
			: this(0, 0, 0, 0, doFunc, undoFunc) { }

		public void Do() {
			doFunc();
		}
		public void Undo() {
			undoFunc();
		}
	}
}
