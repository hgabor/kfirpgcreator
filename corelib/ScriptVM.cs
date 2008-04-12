using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.corelib {
	public interface ScriptVM {
		Script LoadScript(string script);
	}
}
