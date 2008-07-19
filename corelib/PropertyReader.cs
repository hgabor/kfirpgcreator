using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.corelib {
	public abstract class PropertyReader {
		public abstract PropertyReader Select(string path);
		public abstract List<PropertyReader> SelectAll(string path);
		protected abstract string GetStringRaw(string path);

		public int GetInt(string path) {
			return int.Parse(GetStringRaw(path));
		}
		public bool GetBool(string path) {
			return GetString(path) == "1";
		}
		public string GetString(string path) {
			return GetStringRaw(path).Trim();
		}
	}
}
