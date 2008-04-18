using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.corelib {
	interface ScriptVM {
		Script LoadScript(string script);
		void ContinueWithValue(object value);
		object this[string var] { get; set; }
	}
}
