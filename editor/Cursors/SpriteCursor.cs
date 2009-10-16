using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace KFIRPG.editor.Cursors {
	class SpriteCursor: Cursor {
		private Sprite sprite;
		private bool clearScriptInfo;
		private SpriteSheet.Gfx gfx;

		int size;
		public SpriteCursor(Sprite sprite, bool clearScriptInfo, Project project) {
			this.sprite = sprite;
			this.clearScriptInfo = clearScriptInfo;
			size = project.tileSize;
			gfx = sprite.animation.sheet.GetGfxById(1);
		}

		public override void Click(Map.Layer layer) {
			if (tileX >= layer.objects.GetLength(0)|| tileY >= layer.objects.GetLength(1)) {
				return;
			}
			if (layer.objects[tileX, tileY] == null) {
				layer.objects[tileX, tileY] = new Map.Obj();
			}
			layer.objects[tileX, tileY].Sprite = sprite;
			if (clearScriptInfo) {
				layer.objects[tileX, tileY].actionScript = "";
				layer.objects[tileX, tileY].movementAIScript = "";
			}
		}
		
		Pen pen = Pens.Cyan;
		Brush brush = new SolidBrush(Color.FromArgb(128, Color.Cyan));
		public override void Draw(System.Drawing.Graphics g) {
			g.FillRectangle(brush, x / size * size, y / size * size, size - 1, size - 1);
			gfx.Draw(x / size * size, y / size * size, g);
			g.DrawRectangle(pen, x / size * size, y / size * size, size - 1, size - 1);
		}
	}
}
