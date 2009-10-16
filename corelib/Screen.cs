using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.corelib {
	/// <summary>
	/// Abstract base class for screens.
	/// </summary>
	abstract class Screen {
		/// <summary>
		/// Draws the contents of a screen to the spcified surface.
		/// </summary>
		/// <param name="surface">The surface.</param>
		public abstract void Render(SdlDotNet.Graphics.Surface surface);
		/// <summary>
		/// Executes one step of logic of the screen.
		/// </summary>
		public abstract void Advance();
	}
}
