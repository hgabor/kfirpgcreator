using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.corelib {
	/// <summary>
	/// Abstract base class for graphics that can be drawn on screen.
	/// </summary>
	abstract class Graphics {
		/// <summary>
		/// Blits the sprite on the specified surface with the specified coordinates.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="dest"></param>
		public abstract void Blit(int x, int y, SdlDotNet.Graphics.Surface dest);

		public abstract int Width { get; }
		public abstract int Height { get; }
	}
}
