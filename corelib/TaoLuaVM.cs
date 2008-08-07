using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Tao.Lua;

namespace KFIRPG.corelib {
	/// <summary>
	/// A script VM using the Tao.Lua framework - that is, the raw lua interface.
	/// </summary>
	class TaoLuaVM: ScriptVM {
		class Function: ScriptFunction {
			IntPtr luaState;
			string funcName;
			public Function(IntPtr state, string funcName) {
				this.luaState = state;
				this.funcName = funcName;
			}

			public void Run(params object[] args) {
				int stackTop = Lua.lua_gettop(luaState);
				int status = 0;
				Lua.lua_getglobal(luaState, funcName);
				foreach (object arg in args) Push(arg, luaState);
				status = Lua.lua_pcall(luaState, args.Length, 0, 0);
				if (status != 0) {
					throw new Error(Lua.lua_tostring(luaState, -1));
				}
			}
		}

		/// <summary>
		/// Thrown when an error occurs in one of the scripts.
		/// </summary>
		public class Error: Exception {
			static string format = "An error occured in a Lua script: {0}";

			public Error() : base(string.Format(format, "<unknown>")) { }
			public Error(string message) : base(string.Format(format, message)) { }
			public Error(string message, Exception innerException)
				: base(string.Format(format, message), innerException) { }
		}

		IntPtr luaState;
		ScriptLib scriptLib;

		/// <summary>
		/// Lua wrapper for blocking functions. {0} is the function name, {1} is the parameter list.
		/// </summary>
		static string coroutineStr = string.Join("\n", new string[]{
			"function {0}({1})",
			"  internal_{0}({1})",
			"  coroutine.yield()",
			"  return internal_async_return_value",
			"end"
		});

		/// <summary>
		/// Returns the parameter names of a function as a string, separated by commas.
		/// </summary>
		/// <param name="method">The method</param>
		/// <returns>The comma separated parameter names.</returns>
		private string paramList(MethodInfo method) {
			return string.Join(", ", Array.ConvertAll(method.GetParameters(), i => i.Name));
		}

		//Might be leaking memory...
		static Dictionary<IntPtr, object> lightUserData = new Dictionary<IntPtr, object>();
		/// <summary>
		/// Pushes a value on the lua stack.
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="luaState"></param>
		/// <remarks>Supported values are: null, double, int, bool, string. If any other type is pushed,
		/// it will be passed as light userdata. Lua can store and pass light userdata, but
		/// cannot use them directly.</remarks>
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

		static int functionCount = 0;
		/// <summary>
		/// Pops the topmost value from the lua stack.
		/// </summary>
		/// <param name="luaState"></param>
		/// <returns></returns>
		/// <remarks>Supported Lua types and their .NET counterparts:
		/// TNIL, TNONE: null;
		/// TBOOLEAN: bool;
		/// TNUMBER: double;
		/// TSTRING: string;
		/// TLIGHTUSERDATA: any object other than the above.
		/// Other lua types (functions, coroutines, tables) are not supported and will generate
		/// an error when popped.
		/// </remarks>
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
				case Lua.LUA_TFUNCTION:
					++functionCount;
					string funcName = "internal_function_" + functionCount;
					Lua.lua_setglobal(luaState, funcName);
					return new Function(luaState, funcName);
				default:
					throw new TaoLuaVM.Error("Type of return value is not suported.");
			}
			Lua.lua_pop(luaState, 1);
			return ret;
		}

		static List<Lua.lua_CFunction> functionList = new List<Lua.lua_CFunction>();
		/// <summary>
		/// Wraps a .NET function in another that ensures proper communication
		/// between lua scripts and the .NET framework.
		/// </summary>
		/// <param name="method">The function to encapsulate.</param>
		/// <param name="self">If the function is an instance method, the "this" object, otherwise null.</param>
		/// <returns>The lua-compatible function.</returns>
		/// <remarks>This conversion ensures the correct parameter and result passing using
		/// the lua stack, and some basic error handling.</remarks>
		public static Lua.lua_CFunction ToLuaFunction(MethodInfo method, object self) {
			Lua.lua_CFunction func = state => {
				List<object> paramList = new List<object>();
				ParameterInfo[] paramInfos = method.GetParameters();
				int paramCount = Lua.lua_gettop(state);
				if (paramInfos.Length != paramCount) throw new Error("Parameter count does not match!");
				for (int i = 0; i < paramCount; ++i) {
					if (paramInfos[i].ParameterType == typeof(int)) {
						paramList.Add(Convert.ToInt32(Pop(state)));
					}
					else {
						paramList.Add(Pop(state));
					}
				}
				paramList.Reverse();
				object returnValue;
				returnValue = method.Invoke(self, paramList.ToArray());

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
					LoadResumableScript(string.Format(coroutineStr, method.Name, paramList(method))).Run();
				}
				else if (method.GetCustomAttributes(typeof(ScriptAttribute), true).Length != 0) {
					Lua.lua_register(luaState, method.Name, ToLuaFunction(method, scriptLib));
				}
			}
			Lua.lua_register(luaState, "internal_addcoroutine", ToLuaFunction(runningCoroutines.GetType().GetMethod("Push"), runningCoroutines));
		}

		/// <summary>
		/// Creates a non-resumable script object.
		/// </summary>
		/// <param name="script">The source code of the script.</param>
		/// <returns>The script object.</returns>
		/// <remarks>Non-resumable spripts finish executing immediately after they are called.
		/// They include initializing and AI scripts.
		/// If you are planning to use waiting functions, use the <see cref="LoadResumableScript"/> method instead.</remarks>
		public Script LoadNonResumableScript(string script) {
			return new TaoLuaScript(script, this, luaState, false);
		}

		/// <summary>
		/// Creates a potentially resumable script object.
		/// </summary>
		/// <param name="script">The source code of the script.</param>
		/// <returns>The script object.</returns>
		/// <remarks>Resumable scripts return after they are called, but they do not finish
		/// executing most of the time. They can be resumed using the ContinueWithValue() method.
		/// The cause for stopping are blocking functions. The return value for
		/// these functions can also be set with ContinueWithValue().
		/// They include map events and battle scripts.
		/// Only use this instead of <see cref="LoadNonResumableScript"/> if there is a possibility
		/// for the script waiting for more than one frame (e.g. waiting for user input,
		/// using the <see cref="ScriptLib.Wait"/> function, opening other screens).</remarks>
		/// <seealso cref="ContinueWithValue"/>
		public Script LoadResumableScript(string script) {
			return new TaoLuaScript(script, this, luaState, true);
		}

		/// <summary>
		/// Wrapper for resumable scripts. {0} is the internal name of the script's coroutine.
		/// </summary>
		static string continueBase =
			"local ok, retval = coroutine.resume({0})\n" +
			"if coroutine.status({0}) ~= \"dead\" then\n" +
			"  internal_addcoroutine(\"{0}\")\n" +
			"end\n" +
			"return retval";

		Stack<string> runningCoroutines = new Stack<string>();
		/// <summary>
		/// Continues the most recent resumable script, optionally specifying a countinue value.
		/// </summary>
		/// <param name="value">The continue value.</param>
		/// <returns>The return or yield value of the script.</returns>
		/// <exception cref="InvalidOperationException">There are no resumable scripts running.</exception>
		public object ContinueWithValue(object value) {
			if (runningCoroutines.Count == 0) {
				throw new InvalidOperationException("LuaVM: Cannot continue - no coroutines are running");
			}
			Push(value, luaState);
			Lua.lua_setglobal(luaState, "internal_async_return_value");
			string coroutineName = runningCoroutines.Pop();
			string resume = string.Format(continueBase, coroutineName);
			return LoadResumableScript(resume).Run();
		}

		/// <summary>
		/// Gets or sets a public lua variable.
		/// </summary>
		/// <param name="var">The name of the variable.</param>
		/// <returns>The value of the variable.</returns>
		/// <remarks>Only supports values that are supported by the Push() and Pop() methods.</remarks>
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
