using System;
using System.Collections.Generic;
using System.Text;
using LuaInterface;

namespace KFIRPG.corelib {
	public class LuaVM: ScriptVM {
		Lua vm = new Lua();

		#region ScriptVM Members

		public Script LoadScript(string script) {
			return new LuaScript(script, vm);
		}

		#endregion
	}
}
