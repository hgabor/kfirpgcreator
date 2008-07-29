using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.editor {
	interface Saver {
		void Save(string path, System.Drawing.Bitmap bitmap);
		void Save(string path, string text);

		PropertyWriter CreatePropertyFile(string path);

		void SavePropertyFiles();
	}
}
