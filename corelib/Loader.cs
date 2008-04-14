﻿using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.corelib {
	interface Loader {
		System.Drawing.Bitmap LoadBitmap(string path);
		string LoadText(string path);

	}
	public class ResourceNotFoundException: Exception {
		public ResourceNotFoundException(string resource) :
			base(string.Format("Resource \"{0}\" is not found.", resource)) { }
		public ResourceNotFoundException(string resource, Exception innerException) :
			base(string.Format("Resource \"{0}\" is not found.", resource), innerException) { }
	}
}