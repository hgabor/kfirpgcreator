using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.corelib {
	/// <summary>
	/// Transparent graphics.
	/// </summary>
	class NoGraphics: Graphics {
		public override void Blit(int x, int y, SdlDotNet.Graphics.Surface dest) { }
	}
}
