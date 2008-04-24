using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.editor {
	class DeleteSpriteCursor: Cursor {
		public override void Click(Map.Layer layer) {
			layer.objects[tileX, tileY] = null;
		}

		public override void Draw(System.Drawing.Graphics g) { }
	}
}
