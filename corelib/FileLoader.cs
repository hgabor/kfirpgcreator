using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace KFIRPG.corelib {
	/// <summary>
	/// Loads the game from the local hard disk.
	/// </summary>
	public class FileLoader: Loader {
		string basePath;
		public FileLoader(string path) {
			basePath = path;
		}

		#region Loader Members

		public System.Drawing.Bitmap LoadBitmap(string path) {
			try {
				return new System.Drawing.Bitmap(Path.Combine(basePath, path));
			}
			catch (FileNotFoundException ex) {
				throw new ResourceNotFoundException(path, ex);
			}
		}

		public SdlDotNet.Graphics.Surface LoadSurface(string path) {
			return new SdlDotNet.Graphics.Surface(LoadBitmap(path));
		}

		public string LoadText(string path) {
			try {
				return File.ReadAllText(Path.Combine(basePath, path));
			}
			catch (FileNotFoundException ex) {
				throw new ResourceNotFoundException(path, ex);
			}
		}

		public byte[] LoadRaw(string path) {
			try {
				string fileName = Path.Combine(basePath, path);
				using (FileStream fs = File.OpenRead(fileName)) {
					byte[] bytes = new byte[fs.Length];
					fs.Read(bytes, 0, (int)fs.Length);
					return bytes;
				}
			}
			catch (FileNotFoundException ex) {
				throw new ResourceNotFoundException(path, ex);
			}
		}

		public PropertyReader GetPropertyReader() {
			return new FilePropertyReader(basePath);
		}

		public bool Exists(string path) {
			return File.Exists(Path.Combine(basePath, path));
		}

		#endregion
	}
}
