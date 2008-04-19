using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace KFIRPG.corelib {
	public class FileLoader: Loader {
		string basePath;
		public FileLoader(string path) {
			basePath = path;
		}

		#region Loader Members

		public System.Drawing.Bitmap LoadBitmap(string path) {
			return new System.Drawing.Bitmap(Path.Combine(basePath, path));
		}

		public string LoadText(string path) {
			return File.ReadAllText(Path.Combine(basePath, path));
		}

		public byte[] LoadRaw(string path) {
			string fileName = Path.Combine(basePath, path);
			using (FileStream fs = File.OpenRead(fileName)) {
				byte[] bytes = new byte[fs.Length];
				fs.Read(bytes, 0, (int)fs.Length);
				return bytes;
			}
		}

		#endregion
	}
}
