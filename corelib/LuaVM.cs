using System;
using System.Collections.Generic;
using System.Text;
using LuaInterface;
using System.Reflection;

namespace KFIRPG.corelib {
	class LuaVM: ScriptVM {
		Lua vm = new Lua();
		ScriptLib scriptLib;

		public string paramList(MethodInfo method) {
			return string.Join(", ", Array.ConvertAll(method.GetParameters(), i => i.Name));
		}

		static string coroutineStr = string.Join("\n", new string[]{
			"function {0}({1})",
			"  internal_{0}({1})",
			"  coroutine.yield()",
			"  return internal_async_return_value",
			"end"
		});

		public LuaVM(Game game) {
			scriptLib = new ScriptLib(game);
			foreach (MethodInfo method in scriptLib.GetType().GetMethods()) {
				if (method.GetCustomAttributes(typeof(BlockingScriptAttribute), true).Length != 0) {
					vm.RegisterFunction("internal_" + method.Name, scriptLib, method);
					vm.DoString(string.Format(coroutineStr, method.Name, paramList(method)));
				}
				else if (method.GetCustomAttributes(typeof(ScriptAttribute), true).Length != 0) {
					vm.RegisterFunction(method.Name, scriptLib, method);
				}
			}
			vm.RegisterFunction("internal_addcoroutine", this, this.GetType().GetMethod("AddCoroutine"));
		}

		Stack<string> runningCoroutines = new Stack<string>();

		public void AddCoroutine(string cr) {
			runningCoroutines.Push(cr);
		}

		#region ScriptVM Members

		public Script LoadNonBlockingScript(string script) {
			return new LuaScript(script, vm);
		}
		public Script LoadScript(string script) {
			return new LuaScript(script, vm);
		}

		static string continueBase =
			"coroutine.resume({0})\n" +
			"if coroutine.status({0}) ~= \"dead\" then\n" +
			"  internal_addcoroutine(\"{0}\")\n" +
			"end";

		public void ContinueWithValue(object value) {
			if (runningCoroutines.Count == 0) {
				throw new InvalidOperationException("LuaVM: Cannot continue - no coroutines are running");
			}
			vm["internal_async_return_value"] = value;
			string coroutineName = runningCoroutines.Pop();
			string resume = string.Format(continueBase, coroutineName);
			vm.DoString(resume);
		}

		public object this[string var] {
			get {
				return vm[var];
			}
			set {
				vm[var] = value;
			}
		}

		#endregion
	}
}
