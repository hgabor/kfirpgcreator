using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace KFIRPG.editor.Cursors {
	class PassabilityCursor: Cursor {
		bool passable;
		Brush brush;
		Pen pen;
		int size;

		public override string Name {
			get {
				return "Set passability";
			}
		}

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

		protected override void Edit(Map.Layer layer) {
			int tileX = this.tileX;
			int tileY = this.tileY;
			if (tileX <= layer.tiles.GetUpperBound(0) && tileY <= layer.tiles.GetUpperBound(1)) {
				if (layer.tiles[tileX, tileY].passable != passable) {
					AddCommand(
						delegate() {
							layer.tiles[tileX, tileY].passable = passable;
						},
						delegate() {
							layer.tiles[tileX, tileY].passable = !passable;
						}
					);
				}
			}
		}

		protected override void PreDraw(int x, int y, Graphics g) {
			g.FillRectangle(brush, x / size * size, y / size * size, size - 1, size - 1);
			g.DrawRectangle(pen, x / size * size, y / size * size, size - 1, size - 1);
		}

		protected override void DrawCursor(System.Drawing.Graphics g) {
			g.FillRectangle(brush, x / size * size, y / size * size, size - 1, size - 1);
			g.DrawRectangle(pen, x / size * size, y / size * size, size - 1, size - 1);
		}
	}
}
