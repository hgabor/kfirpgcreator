using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.corelib {
	interface ScriptVM {
		Script LoadResumableScript(string script);
		Script LoadNonResumableScript(string script);
		object ContinueWithValue(object value);
		object this[string var] { get; set; }
	}
}
