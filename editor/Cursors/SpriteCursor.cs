using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace KFIRPG.editor.Cursors {
	class SpriteCursor: Cursor {
		private Sprite sprite;
		private bool clearScriptInfo;
		private SpriteSheet.Gfx gfx;

		public override string Name {
			get {
				return "Place sprite(s)";
			}
		}

		int size;
		public SpriteCursor(Sprite sprite, bool clearScriptInfo, Project project) {
			this.sprite = sprite;
			this.clearScriptInfo = clearScriptInfo;
			size = project.tileSize;
			gfx = sprite.animation.sheet.GetGfxById(1);
		}

		protected override void Edit(Map.Layer layer) {
			int tileX = this.tileX;
			int tileY = this.tileY;
			if (tileX >= layer.objects.GetLength(0)|| tileY >= layer.objects.GetLength(1)) {
				return;
			}
			if (layer.objects[tileX, tileY] == null) {
				AddCommand(
					delegate() {
						layer.objects[tileX, tileY] = new Map.Obj();
						layer.objects[tileX, tileY].Sprite = sprite;
					},
					delegate() {
						layer.objects[tileX, tileY] = null;
					});
			}
			else {
				Sprite oldSprite = layer.objects[tileX, tileY].Sprite;
				AddCommand(
					delegate() {
						layer.objects[tileX, tileY].Sprite = sprite;
					},
					delegate() {
						layer.objects[tileX, tileY].Sprite = oldSprite;
					});
			}
			if (clearScriptInfo) {
				string oldAction = layer.objects[tileX, tileY].actionScript;
				string oldCollide = layer.objects[tileX,tileY].collideScript;
				string oldMovement = layer.objects[tileX, tileY].movementAIScript;
				AddCommand(
					delegate() {
						layer.objects[tileX, tileY].actionScript = "";
						layer.objects[tileX, tileY].movementAIScript = "";
						layer.objects[tileX, tileY].collideScript = "";
					},
					delegate() {
						layer.objects[tileX, tileY].actionScript = oldAction;
						layer.objects[tileX, tileY].movementAIScript = oldMovement;
						layer.objects[tileX, tileY].collideScript = oldCollide;
					});
			}
		}
		
		Pen pen = Pens.Cyan;
		Brush brush = new SolidBrush(Color.FromArgb(128, Color.Cyan));

		protected override void PreDraw(int x, int y, Graphics g) {
			g.FillRectangle(brush, x / size * size, y / size * size, size - 1, size - 1);
			gfx.Draw(x / size * size, y / size * size, g);
			g.DrawRectangle(pen, x / size * size, y / size * size, size - 1, size - 1);
		}

		protected override void DrawCursor(System.Drawing.Graphics g) {
			g.FillRectangle(brush, x / size * size, y / size * size, size - 1, size - 1);
			gfx.Draw(x / size * size, y / size * size, g);
			g.DrawRectangle(pen, x / size * size, y / size * size, size - 1, size - 1);
		}
	}
}
