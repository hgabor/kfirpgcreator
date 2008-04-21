using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace KFIRPG.editor {
	class PassabilityCursor: Cursor {
		bool passable;
		Brush brush;
		Pen pen;
		int size;

		public PassabilityCursor(bool passable, Project project) {
			this.passable = passable;
			this.size = project.tileSize;
			if (passable) {
				brush = new SolidBrush(Color.FromArgb(128, Color.LightGreen));
				pen = Pens.LightGreen;
			}
			else {
				brush = new SolidBrush(Color.FromArgb(128, Color.Red));
				pen = Pens.Red;
			}
		}

		public override void Click(Map.Layer layer) {
			if (tileX <= layer.tiles.GetUpperBound(0) && tileY <= layer.tiles.GetUpperBound(1)) {
				layer.tiles[tileX, tileY].passable = passable;
			}
		}

		public override void Draw(System.Drawing.Graphics g) {
			g.FillRectangle(brush, x / size * size, y / size * size, size - 1, size - 1);
			g.DrawRectangle(pen, x / size * size, y / size * size, size - 1, size - 1);
		}
	}
}
