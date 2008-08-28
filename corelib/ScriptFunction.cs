using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.corelib {
	interface ScriptFunction {
		void Run(params object[] args);
	}
}
