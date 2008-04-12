using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using SdlDotNet.Graphics;

namespace KFIRPG.corelib {
	public class Graphic {
		int id;
		int row;
		int column;
		const int ColumnsInRow = 8;
		int size;
		Surface surface;

		public Graphic(string sheet, int id, int size, Game game) {
			this.id = id;
			this.row = id / ColumnsInRow;
			this.column = id % ColumnsInRow;
			this.size = size;
			Bitmap bm = game.loader.LoadBitmap(sheet);
			surface = new Surface(bm);
		}

		private Graphic(Graphic prevState, int offset) {
			id = prevState.id + offset;
		}

		public Graphic getState(int state) {
			return new Graphic(this, id + state);
		}

		public void Blit(int x, int y, Surface dest) {
			dest.Blit(surface, new Point(x, y), new Rectangle(column * size, row * size, size, size));
		}
	}
}
