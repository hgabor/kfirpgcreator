﻿using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.corelib {
	class ZipLoader: Loader {
		public ZipLoader(string fileName) { }

		#region Loader Members

		public System.Drawing.Bitmap LoadBitmap(string path) {
			throw new NotImplementedException();
		}

		public string LoadText(string path) {
			throw new NotImplementedException();
		}

		#endregion
	}
}
