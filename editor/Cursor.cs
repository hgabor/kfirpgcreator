using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.editor {
	abstract class Cursor {
		public abstract void Click();
		public abstract void Draw(System.Drawing.Graphics g);

		protected int x = 0;
		protected int y = 0;
		public void UpdateCoords(int x, int y) {
			this.x = x;
			this.y = y;
		}
	}
}
