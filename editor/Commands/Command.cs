using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.editor.Commands {
	class Command {
		public delegate void Function();

		public int X { get; private set; }
		public int Y { get; private set; }
		public int TileX { get; private set; }
		public int TileY { get; private set; }

		Function doFunc, undoFunc;
		public Command(int x, int y, int tileX, int tileY, Function doFunc, Function undoFunc) {
			this.X = x;
			this.Y = y;
			this.TileX = tileX;
			this.TileY = tileY;
			this.doFunc = doFunc;
			this.undoFunc = undoFunc;
		}

		public Command(int x, int y, Function doFunc, Function undoFunc)
			: this(x, y, 0, 0, doFunc, undoFunc) { }
		
		public Command(Function doFunc, Function undoFunc)
			: this(0, 0, 0, 0, doFunc, undoFunc) { }

		public void Do() {
			doFunc();
		}
		public void Undo() {
			undoFunc();
		}
	}
}
