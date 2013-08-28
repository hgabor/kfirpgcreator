using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.editor.Cursors {
	class PlaceLadderCursor: Cursor {
		int size;
		public override string Name {
			get {
				return "Place ladder";
			}
		}

		public PlaceLadderCursor(Project project) {
			this.size = project.tileSize;
		}

		protected override void Edit(LayerGroup layerGroup)
		{
			if (!(layerGroup is SimpleLayerGroup)) throw new NotSupportedException();
			Layer layer = layerGroup[0];
			Map map = layerGroup[0].Map;
			int tileX = this.tileX;
			int tileY = this.tileY;
			if (tileX >= map.width || tileY >= map.height || tileY == 0) return;
			if (map.ladders[tileX, tileY] == null && map.ladders[tileX, tileY - 1] == null && (tileY == map.height - 1 || map.ladders[tileX, tileY + 1] == null)) {
				Layer baseLayer = layer, topLayer = layer;
				if (SelectTwoLayersDialog.SelectTwoDifferentLayers(map, ref baseLayer, ref topLayer)) {
					AddCommand(
						delegate() {
							map.ladders[tileX, tileY] = new Map.Ladder(baseLayer, topLayer);
						},
						delegate() {
							map.ladders[tileX, tileY] = null;
						}
					);
					EndEdit();
				}
			}
		}

		protected override void PreDraw(int x, int y, System.Drawing.Graphics g) { }

		readonly System.Drawing.Pen ladderPen = System.Drawing.Pens.Purple;
		protected override void DrawCursor(System.Drawing.Graphics g) {
			g.DrawRectangle(ladderPen, x / size * size, (y / size - 1) * size, size - 1, size * 2 - 1);
		}
	}
}
