using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.corelib {
	/// <summary>
	/// Represents a class that loads the game data.
	/// </summary>
	public interface Loader {
		/// <summary>
		/// Loads a bitmap from an image.
		/// </summary>
		/// <exception cref="ResourceNotFoundException">The image does not exist.</exception>
		/// <param name="path">Path to the image.</param>
		/// <returns>The bitmap.</returns>
		System.Drawing.Bitmap LoadBitmap(string path);
		/// <summary>
		/// Loads a surface from an image.
		/// </summary>
		/// <exception cref="ResourceNotFoundException">The image does not exist.</exception>
		/// <param name="path">Path to the image.</param>
		/// <returns>The surface.</returns>
		SdlDotNet.Graphics.Surface LoadSurface(string path);
		/// <summary>
		/// Loads the contents of a file as text.
		/// </summary>
		/// <exception cref="ResourceNotFoundException">The file does not exist.</exception>
		/// <param name="path">Path to the file.</param>
		/// <returns>The string with the contents of the file.</returns>
		string LoadText(string path);
		/// <summary>
		/// Loads the contents of a file as raw binary data.
		/// </summary>
		/// <exception cref="ResourceNotFoundException">The file does not exist.</exception>
		/// <param name="path">Path to the file.</param>
		/// <returns>Array of bytes with the contents of the file.</returns>
		byte[] LoadRaw(string path);

		PropertyReader GetPropertyReader();
	}
	/// <summary>
	/// Thrown when a resource is not found by a loader.
	/// </summary>
	public class ResourceNotFoundException: Exception {
		public ResourceNotFoundException(string resource) :
			base(string.Format("Resource \"{0}\" is not found.", resource)) { }
		public ResourceNotFoundException(string resource, Exception innerException) :
			base(string.Format("Resource \"{0}\" is not found.", resource), innerException) { }
	}
}
