using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using SdlDotNet.Graphics.Primitives;

namespace KFIRPG.corelib {
	class PanelGraphics: Graphics {
		Size size;
		Color background;
		Color border;
		public PanelGraphics(int width, int height, Color background, Color border) {
			this.size = new Size(width, height);
			this.background = background;
			this.border = border;
		}

		public override void Render(int x, int y, SdlDotNet.Graphics.Surface dest) {
			Box box = new SdlDotNet.Graphics.Primitives.Box(new Point(x, y), size);
			dest.Draw(box, background, false, true);
			dest.Draw(box, border);
		}

		public override int Width {
			get { return size.Width; }
		}

		public override int Height {
			get { return size.Height; }
		}
	}
}
