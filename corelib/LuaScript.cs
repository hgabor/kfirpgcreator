using System;
using System.Collections.Generic;
using System.Text;
using LuaInterface;

namespace KFIRPG.corelib {
	class LuaScript: Script {
		Lua vm;

		static string scriptBase =
			"{0} = coroutine.create( function() \n {1} \n end )\n" +
			"local ok, retval = coroutine.resume({0})\n" +
			"if coroutine.status({0}) ~= \"dead\" then\n" +
			"  internal_addcoroutine(\"{0}\")\n" +
			"end\n" +
			"return retval\n";
		static string crNameBase = "internal_coroutine_{0}";
		static ulong coroutineId = 0;
		string script;

		public LuaScript(string script, Lua vm) {
			this.vm = vm;
			this.script = string.Format(scriptBase, crNameBase, script);
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

		public object Run() {
			string scriptStr = string.Format(script, coroutineId++);
			if (owner != null) {
				vm["self"] = owner;
			}
			object[] src = vm.DoString(scriptStr);
			if (src.Length != 1) return null;
			else return src[0];
		}

		#endregion
	}
}
