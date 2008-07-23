using System;
using System.Collections.Generic;
using System.Text;
using KFIRPG.corelib;

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

		public Animation animation;
		public int speed;
		public bool noclip;
		public Dictionary<string, string> ext = new Dictionary<string, string>();

		public Sprite(string spriteName, Project project) {
			this.project = project;
			PropertyReader props = project.loader.GetPropertyReader().Select("sprites/" + spriteName + ".xml");
			animation = project.animations[props.GetString("animation")];
			speed = props.GetInt("speed");
			noclip = props.GetBool("noclip");
			foreach (PropertyReader extNode in props.SelectAll("exts/ext")) {
				ext.Add(extNode.GetString("key"), extNode.GetString("value"));
			}
		}

		public Sprite(Project project) {
			this.project = project;
		}
	}
}
