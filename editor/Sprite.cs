using System;
using System.Collections.Generic;
using System.Text;
using KFIRPG.corelib;
using System.Xml;

namespace KFIRPG.editor {
	class Sprite {
		Project project;
		public string Name {
			get {
				foreach (KeyValuePair<string, Sprite> sprite in project.sprites) {
					if (sprite.Value == this) {
						return sprite.Key;
					}
				}
				throw new Exception("Project does not contain sprite!");
			}
			set {
				project.sprites.Remove(Name);
				project.sprites.Add(value, this);
			}
		}

		public SpriteSheet sheet;
		public int speed;
		public bool noclip;
		public Dictionary<string, string> ext = new Dictionary<string, string>();

		public Sprite(string spriteName, Project project) {
			this.project = project;
			Loader loader = project.loader;
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(loader.LoadText("sprites/" + spriteName + ".xml"));
			sheet = project.sheets[doc.SelectSingleNode("/sprite/img").InnerText];
			speed = int.Parse(doc.SelectSingleNode("/sprite/speed").InnerText);
			noclip = int.Parse(doc.SelectSingleNode("/sprite/noclip").InnerText) == 1;
			foreach (XmlNode node in doc.SelectSingleNode("/sprite/ext").ChildNodes) {
				ext.Add(node.Name, node.InnerText.Trim());
			}
		}

		public Sprite(Project project) {
			this.project = project;
		}
	}
}
