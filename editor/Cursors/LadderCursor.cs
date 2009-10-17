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

		protected override void Edit(Map.Layer layer) {
			Map map = layer.Map;
			int tileX = this.tileX;
			int tileY = this.tileY;
			if (tileX >= map.width || tileY >= map.height || tileY == 0) return;
			if (map.ladders[tileX, tileY] == null && map.ladders[tileX, tileY - 1] == null && (tileY == map.height - 1 || map.ladders[tileX, tileY + 1] == null)) {
				Map.Layer baseLayer = layer, topLayer = layer;
				if (SelectTwoLayersDialog.SelectTwoDifferentLayers(map, ref baseLayer, ref topLayer)) {
					//commandList.Add(new PlaceLadderCommand(map, tileX, tileY, baseLayer, topLayer));
					Commands.Command c = new Commands.Command(
						delegate() {
							map.ladders[tileX, tileY] = new Map.Ladder(baseLayer, topLayer);
						},
						delegate() {
							map.ladders[tileX, tileY] = null;
						}
					);
					commandList.Add(c);


					EndEdit();
					//map.ladders[tileX, tileY] = new Map.Ladder(baseLayer, topLayer);
				}
			}
		}

		readonly System.Drawing.Pen ladderPen = System.Drawing.Pens.Purple;
		public override void Draw(System.Drawing.Graphics g) {
			g.DrawRectangle(ladderPen, x / size * size, (y / size - 1) * size, size - 1, size * 2 - 1);
		}
	}

	class RemoveLadderCursor: Cursor {
		Project project;

		public RemoveLadderCursor(Project project) {
			this.project = project;
		}

		public override void Click(Map.Layer layer) {
			Map map = layer.Map;
			if (tileX >= map.ladders.GetLength(0) || tileY >= map.ladders.GetLength(1)) return;
			map.ladders[tileX, tileY] = null;
			if (tileY < map.height - 1) map.ladders[tileX, tileY + 1] = null;
		}

		public override void Draw(System.Drawing.Graphics g) { }
	}
}
