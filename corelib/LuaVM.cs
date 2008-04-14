using System;
using System.Collections.Generic;
using System.Text;
using LuaInterface;
using System.Reflection;

namespace KFIRPG.corelib {
	class LuaVM: ScriptVM {
		Lua vm = new Lua();
		ScriptLib scriptLib;

		public LuaVM(Game game) {
			scriptLib = new ScriptLib(game);
			foreach(MethodInfo method in scriptLib.GetType().GetMethods()) {
				if (method.GetCustomAttributes(typeof(RegisterScriptAttribute), true).Length != 0) {
					vm.RegisterFunction("Message", scriptLib, method);
				}
			}
		}

		#region ScriptVM Members

		public Script LoadScript(string script) {
			return new LuaScript(script, vm);
		}

		#endregion
	}
}
