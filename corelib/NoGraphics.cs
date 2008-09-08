using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.corelib {
	/// <summary>
	/// Transparent graphics.
	/// </summary>
	class NoGraphics: Graphics {
		public override void Render(int x, int y, SdlDotNet.Graphics.Surface dest) { }

		public override int Width {
			get { return 0; }
		}
		public override int Height {
			get { return 0; }
		}
	}
}
