using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.corelib {
	abstract class Graphics {
		public abstract void Blit(int x, int y, SdlDotNet.Graphics.Surface dest);
	}
}
