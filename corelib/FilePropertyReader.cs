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

		public override PropertyReader Select(string path) {
			string fullPath = System.IO.Path.Combine(basePath, path);
			Match matchFull = Regex.Match(fullPath, FullXmlRegex);
			if (matchFull.Success) {
				XmlDocument doc = new XmlDocument();
				try {
					doc.Load(fullPath);
				}
				catch (System.IO.FileNotFoundException ex) {
					throw new ResourceNotFoundException(path, ex);
				}
				//This relies on all XML documents having a declaration,
				//like <?xml version="1.0"?>
				return new XmlPropertyReader(doc.ChildNodes[1]);
			}
			Match matchPart = Regex.Match(fullPath, PartXmlRegex);
			//Console.WriteLine(matchPart.Success);
			return null;
		}

		public override List<PropertyReader> SelectAll(string path) {
			throw new NotImplementedException();
		}

		protected override string GetStringRaw(string path) {
			throw new NotImplementedException();
		}
	}
}
