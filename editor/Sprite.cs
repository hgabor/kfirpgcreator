using System;
using System.Collections.Generic;
using System.Text;
using KFIRPG.corelib;
using System.Xml;

namespace KFIRPG.editor {
	class Sprite {
		public SpriteSheet sheet;
		public int speed;
		public bool noclip;
		public string name;
		public Dictionary<string, string> ext = new Dictionary<string, string>();

		public Sprite(string spriteName, Project project) {
			this.name = spriteName;
			Loader loader = project.loader;
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(loader.LoadText("sprites/" + spriteName + ".xml"));
			sheet = project.sheets[doc.SelectSingleNode("/sprite/img").InnerText];
			speed = int.Parse(doc.SelectSingleNode("/sprite/speed").InnerText);
			noclip = int.Parse(doc.SelectSingleNode("/sprite/noclip").InnerText) == 1;
			foreach (XmlNode node in doc.SelectNodes("/sprite/ext")) {
				ext.Add(node.Name, node.InnerText.Trim());
			}
		}
	}
}
