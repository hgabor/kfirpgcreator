﻿using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.corelib {
	public class ZipLoader: Loader {
		public ZipLoader(string fileName) { }

		#region Loader Members

		public System.Drawing.Bitmap LoadBitmap(string path) {
			throw new NotImplementedException();
		}

		public SdlDotNet.Graphics.Surface LoadSurface(string path) {
			throw new NotImplementedException();
		}

		public string LoadText(string path) {
			throw new NotImplementedException();
		}

		public byte[] LoadRaw(string path) {
			throw new NotImplementedException();
		}

		public PropertyReader GetPropertyReader() {
			throw new NotImplementedException();
		}

		public bool Exists(string path) {
			throw new NotImplementedException();
		}

		#endregion
	}
}
