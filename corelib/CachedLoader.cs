﻿using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.corelib {
	class CachedLoader: Loader {
		Loader source;
		public CachedLoader(Loader source) {
			this.source = source;
		}

		#region Loader Members

		Dictionary<string, System.Drawing.Bitmap> bitmaps = new Dictionary<string, System.Drawing.Bitmap>();
		public System.Drawing.Bitmap LoadBitmap(string path) {
			if (!bitmaps.ContainsKey(path)) {
				bitmaps.Add(path, source.LoadBitmap(path));
			}
			return bitmaps[path];
		}

		Dictionary<string, SdlDotNet.Graphics.Surface> surfaces = new Dictionary<string, SdlDotNet.Graphics.Surface>();
		public SdlDotNet.Graphics.Surface LoadSurface(string path) {
			if (!surfaces.ContainsKey(path)) {
				surfaces.Add(path, new SdlDotNet.Graphics.Surface(LoadBitmap(path)));
			}
			return surfaces[path];
		}

		Dictionary<string, string> texts = new Dictionary<string, string>();
		public string LoadText(string path) {
			if (!texts.ContainsKey(path)) {
				texts.Add(path, source.LoadText(path));
			}
			return texts[path];
		}

		Dictionary<string, byte[]> rawData = new Dictionary<string, byte[]>();
		public byte[] LoadRaw(string path) {
			if (!rawData.ContainsKey(path)) {
				rawData.Add(path, source.LoadRaw(path));
			}
			return rawData[path];
		}

		#endregion
	}
}