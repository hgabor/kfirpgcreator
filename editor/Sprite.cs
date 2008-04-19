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

		public Sprite(string spriteName, Project project) {
			Loader loader = project.loader;
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(loader.LoadText("sprites/" + spriteName + ".xml"));
			sheet = new SpriteSheet(doc.SelectSingleNode("/sprite/img").InnerText, project);
			speed = int.Parse(doc.SelectSingleNode("/sprite/speed").InnerText);
			noclip = int.Parse(doc.SelectSingleNode("/sprite/noclip").InnerText) == 1;
		}
	}
}
