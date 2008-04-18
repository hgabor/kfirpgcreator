﻿using System;
using System.Collections.Generic;
using System.Text;
using LuaInterface;

namespace KFIRPG.corelib {
	class LuaScript: Script {
		Lua vm;

		static string scriptBase = 
			"{0} = coroutine.create( function() \n {1} \n end )\n"+
			"coroutine.resume({0})\n"+
			"if coroutine.status({0}) ~= \"dead\" then\n"+
			"  internal_addcoroutine(\"{0}\")\n" +
			"end";
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

		public object[] Run() {
			string scriptStr = string.Format(script, coroutineId++);
			if (owner == null) {
				return vm.DoString(scriptStr);
			}
			else {
				vm["self"] = owner;
				object[] ret = vm.DoString(scriptStr);
				return ret;
			}
		}

		#endregion
	}
}
