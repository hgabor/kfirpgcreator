using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.corelib {
	/// <summary>
	/// Objects that can have properties attached by scripts.
	/// Is not operation at the moment.
	/// </summary>
	abstract class Entity {
		Dictionary<string, object> vars = new Dictionary<string, object>();
		/// <summary>
		/// Gets a property.
		/// </summary>
		/// <param name="key">The name of the property.</param>
		/// <returns>The value of the property.</returns>
		public object GetProperty(string key) {
			return vars[key];
		}
		/// <summary>
		/// Sets a property.
		/// </summary>
		/// <param name="key">The name of the property.</param>
		/// <param name="value">The new value of the property.</param>
		public void SetProperty(string key, object value) {
			if (vars.ContainsKey(key)) vars[key] = value;
			else vars.Add(key, value);
		}
	}
}
