using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.editor.Cursors {
	abstract class Cursor {
		public abstract void Click(Map.Layer layer);
		public abstract void Draw(System.Drawing.Graphics g);

		protected int x = 0;
		protected int y = 0;
		protected int tileX = 0;
		protected int tileY = 0;
		public void UpdateCoords(int x, int y, int tileX, int tileY) {
			this.x = x;
			this.y = y;
			this.tileX = tileX;
			this.tileY = tileY;
		}
	}
}
