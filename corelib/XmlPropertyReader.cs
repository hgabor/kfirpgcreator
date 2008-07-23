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

		public override PropertyReader Select(string path) {
			return new XmlPropertyReader(baseNode.SelectSingleNode(path));
		}

		public override List<PropertyReader> SelectAll(string path) {
			List<PropertyReader> list = new List<PropertyReader>();
			foreach (XmlNode node in baseNode.SelectNodes(path)) {
				list.Add(new XmlPropertyReader(node));
			}
			return list;
		}

		protected override string GetStringRaw(string path) {
			if (path == "") return baseNode.InnerText;
			else {
				XmlNode node;
				if ((node = baseNode.SelectSingleNode(path)) != null) {
					return node.InnerText;
				}
				else {
					throw new KFIRPG.corelib.Game.SettingsException(string.Format("Setting {0} no found!", path));
				}
			}
		}
	}
}
