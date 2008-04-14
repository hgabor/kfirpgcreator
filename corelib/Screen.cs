using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.corelib {
	abstract class Screen {
		public abstract void Draw(SdlDotNet.Graphics.Surface surface);
		public abstract void Think();
	}
}
