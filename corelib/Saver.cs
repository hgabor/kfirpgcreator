﻿using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.corelib {
	public interface Saver {
		void Save(string path, System.Drawing.Bitmap bitmap);
		void Save(string path, string text);
		void Save(string path, byte[] raw);

		PropertyWriter CreatePropertyFile(string path);

		void SavePropertyFiles();

		void RestoreCurrentDirectory();
	}
}