using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.corelib {
	/// <summary>
	/// Caches the resources of another loader to reduce the access to the local hard disk.
	/// </summary>
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

		public PropertyReader GetPropertyReader() {
			return new PropertyCacheAccessor(source.GetPropertyReader(), propertyCache);
		}

		#endregion

		Dictionary<string, string> propertyCache = new Dictionary<string, string>();

		class PropertyCacheAccessor: PropertyReader {
			PropertyReader baseReader;
			Dictionary<string, string> propertyCache;
			string basePath;

			public PropertyCacheAccessor(PropertyReader baseReader, Dictionary<string, string> propertyCache)
				: this(baseReader, propertyCache, "") { }

			public PropertyCacheAccessor(PropertyReader baseReader, Dictionary<string, string> propertyCache, string basePath) {
				this.baseReader = baseReader;
				this.propertyCache = propertyCache;
				this.basePath = basePath;
			}

			public override PropertyReader Select(string path) {
				return new PropertyCacheAccessor(baseReader.Select(path), propertyCache, basePath + "/" + path);
			}

			//TODO: SelectAll is not cached
			public override List<PropertyReader> SelectAll(string path) {
				return baseReader.SelectAll(path);
			}

			protected override string GetStringRaw(string path) {
				string key = basePath + "/" + path;
				if (propertyCache.ContainsKey(key)) {
					return propertyCache[key];
				}
				else {
					string data = baseReader.GetString(path);
					propertyCache.Add(key, data);
					return data;
				}
			}
		}
	}
}
