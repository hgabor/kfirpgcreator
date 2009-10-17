using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.editor.Cursors {
	class DeleteSpriteCursor: Cursor {
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
			commandList.Add(new Commands.Command(
				delegate() {
					layer.objects[tileX, tileY] = null;
				},
				delegate() {
					layer.objects[tileX, tileY] = oldObj;
				}));
		}

		public override void Draw(System.Drawing.Graphics g) { }
	}
}
