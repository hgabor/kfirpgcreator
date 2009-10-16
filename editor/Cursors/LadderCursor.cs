using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.editor.Cursors {
	class PlaceLadderCursor: Cursor {
		class PlaceLadderCommand: Commands.Command {
			public override string Name {
				get { return "Place ladder"; }
			}

			public override void Do() {
				map.ladders[tileX, tileY] = new Map.Ladder(baseLayer, topLayer);
			}

			public override void Undo() {
				map.ladders[tileX, tileY] = null;
			}

			Map map;
			int tileX, tileY;
			Map.Layer baseLayer, topLayer;
			public PlaceLadderCommand(Map map, int tileX, int tileY, Map.Layer baseLayer, Map.Layer topLayer) {
				this.map = map;
				this.tileX = tileX;
				this.tileY = tileY;
				this.baseLayer = baseLayer;
				this.topLayer = topLayer;
			}
		}


		int size;

		public PlaceLadderCursor(Project project) {
			this.size = project.tileSize;
		}

		public override void Click(Map.Layer layer) {
			Map map = layer.Map;
			if (tileX >= map.width || tileY >= map.height || tileY == 0) return;
			if (map.ladders[tileX, tileY] == null && map.ladders[tileX, tileY - 1] == null && (tileY == map.height - 1 || map.ladders[tileX, tileY + 1] == null)) {
				Map.Layer baseLayer = layer, topLayer = layer;
				if (SelectTwoLayersDialog.SelectTwoDifferentLayers(map, ref baseLayer, ref topLayer)) {
					map.ladders[tileX, tileY] = new Map.Ladder(baseLayer, topLayer);
				}
			}
		}

		protected override void Edit(Map.Layer layer) {
			Map map = layer.Map;
			if (tileX >= map.width || tileY >= map.height || tileY == 0) return;
			if (map.ladders[tileX, tileY] == null && map.ladders[tileX, tileY - 1] == null && (tileY == map.height - 1 || map.ladders[tileX, tileY + 1] == null)) {
				Map.Layer baseLayer = layer, topLayer = layer;
				if (SelectTwoLayersDialog.SelectTwoDifferentLayers(map, ref baseLayer, ref topLayer)) {
					commandList.Add(new PlaceLadderCommand(map, tileX, tileY, baseLayer, topLayer));
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
