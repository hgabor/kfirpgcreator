using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

namespace KFIRPG.editor {
	class FileSaver: Saver {
		string path;
		string originalPath;

		public FileSaver(string path) {
			this.path = path;
			this.originalPath = Directory.GetCurrentDirectory();
			Directory.SetCurrentDirectory(path);
		}

		private static void CreateDir(string path) {
			string dir = Path.GetDirectoryName(path);
			//Directory.CreateDirectory() fails if argument is an empty string
			if (dir != "") {
				Directory.CreateDirectory(dir);
			}
		}

		#region Saver Members

		public void Save(string path, System.Drawing.Bitmap bitmap) {
			CreateDir(path);
			string fullPath = Path.Combine(this.path, path);
			if (File.Exists(fullPath)) {
				File.Delete(fullPath);
			}
			bitmap.Save(fullPath);
		}

		public void Save(string path, string text) {
			CreateDir(path);
			File.WriteAllText(Path.Combine(this.path, path), text);
		}

		public void Save(string path, byte[] raw) {
			CreateDir(path);
			File.WriteAllBytes(Path.Combine(this.path, path), raw);
		}

		private Dictionary<string, XmlDocument> docs = new Dictionary<string, XmlDocument>();
		public PropertyWriter CreatePropertyFile(string path) {
			XmlDocument doc = new XmlDocument();
			docs.Add(path, doc);
			doc.AppendChild(doc.CreateXmlDeclaration("1.0", null, null));
			string fileName = Path.GetFileNameWithoutExtension(path);
			XmlElement root = doc.CreateElement(fileName);
			doc.AppendChild(root);
			return new XmlPropertyWriter(root);
		}

		public void SavePropertyFiles() {
			foreach (var kvp in docs) {
				CreateDir(kvp.Key);
				kvp.Value.Save(kvp.Key);
			}
		}

		public void RestoreCurrentDirectory() {
			Directory.SetCurrentDirectory(this.originalPath);
		}

		#endregion
	}
}
