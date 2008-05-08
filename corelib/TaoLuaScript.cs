using System;
using System.Collections.Generic;
using System.Text;
using Tao.Lua;

namespace KFIRPG.corelib {
	class TaoLuaScript: Script {
		string script;
		IntPtr luaState;
		TaoLuaVM vm;

		static string scriptBase =
			"{0} = coroutine.create( function() \n {1} \n end )\n" +
			"local ok, retval = coroutine.resume({0})\n" +
			"if coroutine.status({0}) ~= \"dead\" then\n" +
			"  internal_addcoroutine(\"{0}\")\n" +
			"end\n" +
			"return retval\n";
		static string crNameBase = "internal_coroutine_{0}";
		static ulong coroutineId = 0;

		bool coroutine;
		public TaoLuaScript(string script, TaoLuaVM vm, IntPtr luaState, bool createCoroutine) {
			this.coroutine = createCoroutine;
			if (coroutine) {
				this.script = string.Format(scriptBase, crNameBase, script.Replace("{", "{{").Replace("}", "}}"));
			}
			else {
				this.script = script;
			}
			this.luaState = luaState;
			this.vm = vm;
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

		public object Run() {
			string scriptStr;
			if (coroutine) {
				scriptStr = string.Format(script, coroutineId++).Replace("{{", "{").Replace("}}", "}");
			}
			else {
				scriptStr = script;
			}

			int stackTop = Lua.lua_gettop(luaState);
			int status = 0;

			if (owner != null) {
				TaoLuaVM.Push(owner, luaState);
				Lua.lua_setglobal(luaState, "self");
			}

			status = Lua.luaL_loadstring(luaState, scriptStr);
			if (status != 0) {
				throw new TaoLuaVM.Error(Lua.lua_tostring(luaState, -1));
			}
			status = Lua.lua_pcall(luaState, 0, Lua.LUA_MULTRET, 0);
			if (status != 0) {
				throw new TaoLuaVM.Error(Lua.lua_tostring(luaState, -1));
			}
			else {
				List<object> ret = new List<object>();
				while (Lua.lua_gettop(luaState) != stackTop) {
					ret.Add(TaoLuaVM.Pop(luaState));
				}
				if (ret.Count == 0) return null;
				else if (ret.Count == 1) return ret[0];
				else {
					ret.Reverse();
					return ret.ToArray();
				}
			}
		}
	}
}
