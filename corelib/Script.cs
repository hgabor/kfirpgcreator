using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.corelib {
	/// <summary>
	/// Represents a script that can be modified without recompiling the game engine.
	/// </summary>
	interface Script {
		/// <summary>
		/// Sets the owner of the script.
		/// </summary>
		Entity Owner { get; set; }
		/// <summary>
		/// Runs the script and returns its result value.
		/// </summary>
		/// <returns></returns>
		object Run();
	}
}
