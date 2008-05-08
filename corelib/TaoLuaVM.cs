using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Tao.Lua;

namespace KFIRPG.corelib {
	class TaoLuaVM: ScriptVM {
		public class Error: Exception {
			static string format = "An error occured in a Lua script: {0}";

			public Error() : base(string.Format(format, "<unknown>")) { }
			public Error(string message) : base(string.Format(format, message)) { }
			public Error(string message, Exception innerException)
				: base(string.Format(format, message), innerException) { }
		}

		IntPtr luaState;
		ScriptLib scriptLib;

		static string coroutineStr = string.Join("\n", new string[]{
			"function {0}({1})",
			"  internal_{0}({1})",
			"  coroutine.yield()",
			"  return internal_async_return_value",
			"end"
		});

		public string paramList(MethodInfo method) {
			return string.Join(", ", Array.ConvertAll(method.GetParameters(), i => i.Name));
		}

		//Might be leaking memory...
		static Dictionary<IntPtr, object> lightUserData = new Dictionary<IntPtr, object>();
		public static void Push(object obj, IntPtr luaState) {
			if (obj == null) {
				Lua.lua_pushnil(luaState);
			}
			else if (obj is double) {
				Lua.lua_pushnumber(luaState, (double)obj);
			}
			else if (obj is int) {
				Lua.lua_pushinteger(luaState, (int)obj);
			}
			else if (obj is bool) {
				Lua.lua_pushboolean(luaState, (bool)obj ? 1 : 0);
			}
			else if (obj is string) {
				Lua.lua_pushstring(luaState, (string)obj);
			}
			else {
				IntPtr ptr = new IntPtr(lightUserData.Count);
				lightUserData.Add(ptr, obj);
				Lua.lua_pushlightuserdata(luaState, ptr);
			}
		}
		public static object Pop(IntPtr luaState) {
			object ret;
			switch (Lua.lua_type(luaState, -1)) {
				case Lua.LUA_TBOOLEAN:
					ret = Lua.lua_toboolean(luaState, -1) == 1;
					break;
				case Lua.LUA_TNIL:
				case Lua.LUA_TNONE:
					ret = null;
					break;
				case Lua.LUA_TNUMBER:
					ret = Lua.lua_tonumber(luaState, -1);
					break;
				case Lua.LUA_TSTRING:
					ret = Lua.lua_tostring(luaState, -1);
					break;
				case Lua.LUA_TLIGHTUSERDATA:
					ret = lightUserData[Lua.lua_touserdata(luaState, -1)];
					break;
				default:
					throw new TaoLuaVM.Error("Type of return value is not suported.");
			}
			Lua.lua_pop(luaState, 1);
			return ret;
		}

		static List<Lua.lua_CFunction> functionList = new List<Lua.lua_CFunction>();
		public static Lua.lua_CFunction ToLuaFunction(MethodInfo method, object self) {
			Lua.lua_CFunction func = state => {
				List<object> paramList = new List<object>();
				ParameterInfo[] paramInfos = method.GetParameters();
				int paramCount = Lua.lua_gettop(state);
				for (int i = 0; i < paramCount; ++i) {
					paramList.Add(Pop(state));
				}
				paramList.Reverse();
				object returnValue;
				try {
					returnValue = method.Invoke(self, paramList.ToArray());
				}
				catch (TargetParameterCountException ex) {
					throw new Error("Parameter count does not match!", ex);
				}


				if (method.ReturnType == typeof(void)) return 0;
				else {
					Push(returnValue, state);
					return 1;
				}
			};
			functionList.Add(func);
			return func;
		}

		public TaoLuaVM(Game game) {
			luaState = Lua.lua_open();
			Lua.lua_atpanic(luaState, state => {
				throw new Error(Lua.lua_tostring(luaState, -1));
			});
			Lua.luaL_openlibs(luaState);
			scriptLib = new ScriptLib(game);
			foreach (MethodInfo method in scriptLib.GetType().GetMethods()) {
				if (method.GetCustomAttributes(typeof(BlockingScriptAttribute), true).Length != 0) {
					Lua.lua_register(luaState, "internal_" + method.Name, ToLuaFunction(method, scriptLib));
					LoadScript(string.Format(coroutineStr, method.Name, paramList(method))).Run();
				}
				else if (method.GetCustomAttributes(typeof(ScriptAttribute), true).Length != 0) {
					Lua.lua_register(luaState, method.Name, ToLuaFunction(method, scriptLib));
				}
			}
			Lua.lua_register(luaState, "internal_addcoroutine", ToLuaFunction(this.GetType().GetMethod("AddCoroutine"), this));
		}

		public Script LoadNonBlockingScript(string script) {
			return new TaoLuaScript(script, this, luaState, false);
		}

		public Script LoadScript(string script) {
			return new TaoLuaScript(script, this, luaState, true);
		}

		static string continueBase =
			"coroutine.resume({0})\n" +
			"if coroutine.status({0}) ~= \"dead\" then\n" +
			"  internal_addcoroutine(\"{0}\")\n" +
			"end";

		Stack<string> runningCoroutines = new Stack<string>();
		public void ContinueWithValue(object value) {
			if (runningCoroutines.Count == 0) {
				throw new InvalidOperationException("LuaVM: Cannot continue - no coroutines are running");
			}
			Push(value, luaState);
			Lua.lua_setglobal(luaState, "internal_async_return_value");
			string coroutineName = runningCoroutines.Pop();
			string resume = string.Format(continueBase, coroutineName);
			LoadScript(resume).Run();
		}

		public void AddCoroutine(string cr) {
			runningCoroutines.Push(cr);
		}

		public object this[string var] {
			get {
				Lua.lua_getglobal(luaState, var);
				return Pop(luaState);
			}
			set {
				Push(value, luaState);
				Lua.lua_setglobal(luaState, var);
			}
		}
	}
}
