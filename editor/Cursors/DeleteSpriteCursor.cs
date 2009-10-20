using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.editor.Cursors {
	class DeleteSpriteCursor: Cursor {
		int size;

		public DeleteSpriteCursor(Project project) {
			this.size = project.tileSize;
			this.pen = new System.Drawing.Pen(System.Drawing.Color.Red);
			this.pen.Width = 2;
		}

		public override string Name {
			get {
				return "Delete sprite(s)";
			}
		}

		protected override void Edit(Map.Layer layer) {
			int tileX = this.tileX;
			int tileY = this.tileY;
			if (tileX >= layer.objects.GetLength(0) || tileY >= layer.objects.GetLength(1)) {
				return;
			}
			Map.Obj oldObj = layer.objects[tileX, tileY];
			if (oldObj == null) return;
			AddCommand(
				delegate() {
					layer.objects[tileX, tileY] = null;
				},
				delegate() {
					layer.objects[tileX, tileY] = oldObj;
				});
		}

		System.Drawing.Pen pen;
		protected override void PreDraw(int x, int y, System.Drawing.Graphics g) {
			int realX = x / size * size;
			int realY = y / size * size;
			//Draw a red X
			g.DrawLine(pen, realX, realY, realX + size, realY + size);
			g.DrawLine(pen, realX + size, realY, realX, realY + size);
		}

		protected override void DrawCursor(System.Drawing.Graphics g) { }
	}
}
