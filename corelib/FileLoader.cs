using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace KFIRPG.corelib {
	class FileLoader: Loader {
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

		#endregion
	}
}
