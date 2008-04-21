using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace KFIRPG.editor {
	class TileCursor: Cursor {
		int size;
		int id;
		SpriteSheet tileSheet;
		readonly Pen pen = new Pen(Color.Red);

		public TileCursor() {
			id = 0;
			tileSheet = null;
			size = 32;
		}

		public TileCursor(int tileId, Project project) {
			id = tileId;
			size = project.tileSize;
			tileSheet = project.sheets["tiles"];
		}

		public override void Click(Map.Layer currentLayer) {
			if (tileSheet != null) {
				if (id == 0) {
					currentLayer.tiles[tileX, tileY].gfx = SpriteSheet.Gfx.Empty;
				}
				else if (tileX <= currentLayer.tiles.GetUpperBound(0) && tileY <= currentLayer.tiles.GetUpperBound(1)) {
					currentLayer.tiles[tileX, tileY].gfx = new SpriteSheet.Gfx(id - 1, tileSheet.cols, size, size, tileSheet);
				}
				
			}
		}

		public override void Draw(Graphics g) {
			g.DrawRectangle(pen, x / size * size, y / size * size, size - 1, size - 1);
		}
	}
}
