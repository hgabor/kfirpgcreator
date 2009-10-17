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

		public override string Name {
			get {
				return "Delete ladder(s)";
			}
		}

		public RemoveLadderCursor(Project project) {
			this.project = project;
		}

		protected override void Edit(Map.Layer layer) {
			Map map = layer.Map;
			int tileX = this.tileX;
			int tileY = this.tileY;
			if (tileX >= map.ladders.GetLength(0) || tileY >= map.ladders.GetLength(1)) return;

			Map.Ladder oldLadder = map.ladders[tileX, tileY];
			if (oldLadder != null) {
				Commands.Command c = new Commands.Command(
					delegate() {
						map.ladders[tileX, tileY] = null;
					},
					delegate() {
						map.ladders[tileX, tileY] = oldLadder;
					}
				);
				commandList.Add(c);
			}
			if (tileY < map.height - 1) {
				Map.Ladder oldLadder2 = map.ladders[tileX, tileY + 1];
				if (oldLadder2 != null) {
					Commands.Command c = new Commands.Command(
						delegate() {
							map.ladders[tileX, tileY + 1] = null;
						},
						delegate() {
							map.ladders[tileX, tileY + 1] = oldLadder2;
						}
					);
					commandList.Add(c);
				}
			}
		}

		public override void Draw(System.Drawing.Graphics g) { }
	}
}
