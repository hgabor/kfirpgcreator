using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace KFIRPG.editor {
	class TileCursor: Cursor {
		int size;
		readonly Pen pen = new Pen(Color.Red);

		public TileCursor(int tileSize) {
			size = tileSize;
		}

		public override void Click() {
			throw new NotImplementedException();
		}

		public override void Draw(Graphics g) {
			g.DrawRectangle(pen, x / size * size, y / size * size, size - 1, size - 1);
		}
	}
}
