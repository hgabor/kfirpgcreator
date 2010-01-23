
using System;
using KFIRPG.corelib;

namespace KFIRPG.editor {
	/// <summary>
	/// Defers loading of large files until it's necessary to load them.
	/// </summary>
	class BigFile {
		Loader loader;
		byte[] content;
		string fileName;
		
		public BigFile(string fileName, Loader loader) {
			this.loader = loader;
			this.fileName = fileName;
			this.NeedsSaving = false;
		}
		
		public BigFile(byte[] content) {
			this.content = content;
			this.NeedsSaving = true;
		}
		
		public byte[] Content {
			get {
				if (content == null) content = loader.LoadRaw(fileName);
				return content;
			}
			set {
				content = value;
				loader = null;
				fileName = null;
				NeedsSaving = true;
			}
		}
		
		public bool NeedsSaving { get; private set; }
		
		public void ContentSaved() {
			NeedsSaving = false;
		}
	}
}
