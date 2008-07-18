using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace KFIRPG.corelib {
	class XmlPropertyReader: PropertyReader {
		XmlNode baseNode;
		public XmlPropertyReader(XmlNode baseNode) {
			this.baseNode = baseNode;
		}

		#region PropertyReader Members

		public PropertyReader Select(string path) {
			return new XmlPropertyReader(baseNode.SelectSingleNode(path));
		}

		public List<PropertyReader> SelectAll(string path) {
			List<PropertyReader> list = new List<PropertyReader>();
			foreach (XmlNode node in baseNode.SelectNodes(path)) {
				list.Add(new XmlPropertyReader(node));
			}
			return list;
		}

		public int GetInt(string path) {
			return int.Parse(GetString(path));
		}

		public bool GetBool(string path) {
			return GetString(path) == "1";
		}

		public string GetString(string path) {
			if (path == "") return baseNode.InnerText;
			else return baseNode.SelectSingleNode(path).InnerText;
		}

		#endregion
	}
}
