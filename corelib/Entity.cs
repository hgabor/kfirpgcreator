using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace KFIRPG.corelib {
	abstract class Entity {
		StringDictionary vars = new StringDictionary();
		public string this[string key] {
			get {
				return vars[key];
			}
			set {
				if (vars.ContainsKey(key)) vars[key] = value;
				else vars.Add(key, value);
			}
		}
	}
}
