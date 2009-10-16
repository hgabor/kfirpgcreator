using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.corelib {
	public abstract class PropertyWriter {
		protected abstract void SetStringRaw(string val, string path);
		public abstract PropertyWriter Create(string path);

		public void Set(string path, int val) {
			SetStringRaw(val.ToString(), path);
		}
		public void Set(string path, bool val) {
			SetStringRaw(val ? "1" : "0", path);
		}
		public void Set(string path, string val) {
			SetStringRaw(val.Trim(), path);
		}
	}
}
