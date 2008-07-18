using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace KFIRPG.corelib {
	class FilePropertyReader: PropertyReader {
		string basePath;

		public FilePropertyReader(string path) {
			this.basePath = path;
		}

		const string PartXmlRegex = @"^(.*\.xml)/(.+)$";
		const string FullXmlRegex = @"^(.*\.xml)$";

		#region PropertyReader Members

		public PropertyReader Select(string path) {
			string fullPath = System.IO.Path.Combine(basePath, path);
			Match matchFull = Regex.Match(fullPath, FullXmlRegex);
			if (matchFull.Success) {
				XmlDocument doc = new XmlDocument();
				doc.Load(fullPath);
				//This relies on all XML documents having a declaration,
				//like <?xml version="1.0"?>
				return new XmlPropertyReader(doc.ChildNodes[1]);
			}
			Match matchPart = Regex.Match(fullPath, PartXmlRegex);
			//Console.WriteLine(matchPart.Success);
			return null;
		}

		public List<PropertyReader> SelectAll(string path) {
			throw new NotImplementedException();
		}

		public int GetInt(string path) {
			throw new NotImplementedException();
		}

		public bool GetBool(string path) {
			throw new NotImplementedException();
		}

		public string GetString(string path) {
			throw new NotImplementedException();
		}

		#endregion
	}
}
