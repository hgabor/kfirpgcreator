using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.editor {
	class DoubleBufferedPanel:System.Windows.Forms.Panel {
		public DoubleBufferedPanel() {
			this.DoubleBuffered = true;
		}
	}
}
