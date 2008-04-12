using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace KFIRPG.corelib {
	//TODO: Reconsider using the common base class (Object/Entity)
	public abstract class Object: Entity {
		public Object() {}

		public abstract void Draw(int x, int y, SdlDotNet.Graphics.Surface surface);
	}
}
