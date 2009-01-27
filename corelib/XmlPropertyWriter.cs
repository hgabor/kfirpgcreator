using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace KFIRPG.corelib {
	class XmlPropertyWriter: PropertyWriter {
		XmlNode baseNode;
		public XmlPropertyWriter(XmlNode baseNode) {
			this.baseNode = baseNode;
		}

		protected override void SetStringRaw(string val, string path) {
			baseNode.AppendChild(baseNode.OwnerDocument.CreateElement(path)).InnerText = val;
		}

		public override PropertyWriter Create(string path) {
			return new XmlPropertyWriter(baseNode.AppendChild(baseNode.OwnerDocument.CreateElement(path)));
		}
	}
}
