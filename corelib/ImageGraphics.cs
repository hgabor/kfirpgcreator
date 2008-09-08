using System;
using System.Collections.Generic;
using System.Text;
using SdlDotNet.Graphics;

namespace KFIRPG.corelib {
	class ImageGraphics: Graphics {
		Surface surface;

		public ImageGraphics(string imgName, Game game) {
			this.surface = game.loader.LoadSurface("img/" + imgName + ".png");
		}

		public override void Render(int x, int y, Surface dest) {
			dest.Blit(surface, new System.Drawing.Point(x, y));
		}

		public override int Width {
			get { return surface.Width; }
		}

		public override int Height {
			get { return surface.Height; }
		}
	}
}
