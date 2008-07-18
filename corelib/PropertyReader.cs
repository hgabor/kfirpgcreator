using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.corelib {
	public interface PropertyReader {
		PropertyReader Select(string path);
		List<PropertyReader> SelectAll(string path);

		int GetInt(string path);
		bool GetBool(string path);
		string GetString(string path);
	}
}
