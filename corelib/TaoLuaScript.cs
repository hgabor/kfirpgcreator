using System;
using System.Collections.Generic;
using System.Text;
using Tao.Lua;

namespace KFIRPG.corelib {
	class TaoLuaScript: Script {
		string script;
		public string Raw { get; private set; }
		IntPtr luaState;
		TaoLuaVM vm;

		/// <summary>
		/// Wrapper for resumable scripts.
		/// </summary>
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
		/// <summary>
		/// Creates a new script.
		/// </summary>
		/// <param name="script">The code for the script.</param>
		/// <param name="vm">The lua virtual machine to create the script in.</param>
		/// <param name="luaState"></param>
		/// <param name="createCoroutine">Create a resumable script.</param>
		public TaoLuaScript(string script, TaoLuaVM vm, IntPtr luaState, bool createCoroutine) {
			this.coroutine = createCoroutine;
			this.Raw = script;
			if (coroutine) {
				this.script = string.Format(scriptBase, crNameBase, script.Replace("{", "{{").Replace("}", "}}"));
			}
			else {
				this.script = script;
			}
			this.luaState = luaState;
			this.vm = vm;
		}

		/// <summary>
		/// Gets or sets the owner of the script. When the script is run, it will be available under
		/// the global "self" variable.
		/// </summary>
		/// <remarks>The supported values are the same as the supported values of the
		/// TaoLuaVM.Push() and Pop() methods, and the same restrictions apply.</remarks>
		/// <see cref="TaoLuaVM.Push"/>
		/// <see cref="TaoLuaVM.Pop"/>
		public Sprite Owner { get; set; }

		/// <summary>
		/// Runs the script.
		/// </summary>
		/// <returns>The return or yield value of the script.</returns>
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

			if (Owner != null) {
				TaoLuaVM.Push(Owner, luaState);
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
