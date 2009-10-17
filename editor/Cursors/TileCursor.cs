using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace KFIRPG.editor.Cursors {
	class TileCursor: Cursor {
		int size;
		int id;
		SpriteSheet tileSheet;
		readonly Pen pen = new Pen(Color.Red);

		public override string Name {
			get {
				return "Draw tiles";
			}
		}

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

		protected override void Edit(Map.Layer currentLayer) {
			int tileX = this.tileX;
			int tileY = this.tileY;
			if (tileSheet != null) {
				if (tileX <= currentLayer.tiles.GetUpperBound(0) && tileY <= currentLayer.tiles.GetUpperBound(1)) {
					SpriteSheet.Gfx oldGfx = currentLayer.tiles[tileX, tileY].gfx;
					SpriteSheet.Gfx newGfx = (id == 0) ?
						SpriteSheet.Gfx.Empty :
						new SpriteSheet.Gfx(id - 1, tileSheet);
					if (oldGfx != newGfx) {
						Commands.Command c = new Commands.Command(
							delegate() {
								currentLayer.tiles[tileX, tileY].gfx = newGfx;
							},
							delegate() {
								currentLayer.tiles[tileX, tileY].gfx = oldGfx;
							}
						);
						commandList.Add(c);
					}
				}
				
			}
		}

		public override void Draw(Graphics g) {
			g.DrawRectangle(pen, x / size * size, y / size * size, size - 1, size - 1);
		}
	}
}
