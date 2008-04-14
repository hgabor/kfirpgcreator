using System;
using System.Collections.Generic;
using System.Text;
using LuaInterface;

namespace KFIRPG.corelib {
	class LuaScript: Script {
		Lua vm;
		string script;

		public LuaScript(string script, Lua vm) {
			this.vm = vm;
			this.script = script;
		}

		Entity owner = null;
		public Entity Owner {
			get {
				return owner;
			}
			set {
				owner = value;
			}
		}

		#region Script Members

		public object[] Run() {
			if (owner == null) {
				return vm.DoString(script);
			}
			else {
				vm["this"] = owner;
				object[] ret = vm.DoString(script);
				vm["this"] = null;
				return ret;
			}
		}

		#endregion
	}
}
