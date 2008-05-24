using System;
using System.Collections.Generic;
using System.Text;
using KFIRPG.corelib;
using System.Xml;

namespace KFIRPG.editor {
	class Animation: ICloneable {
		Project project;
		public string Name {
			get {
				foreach (KeyValuePair<string, Animation> kvp in project.animations) {
					if (kvp.Value == this) return kvp.Key;
				}
				throw new InvalidOperationException("Project does not contain animation!");
			}
		}

		public SpriteSheet sheet;

		public Dictionary<string, Group> groups = new Dictionary<string, Group>();

		public class Group {
			public List<Frame> frames = new List<Frame>();
		}

		public struct Frame {
			public int sheetId;
			public int time;
			public Frame(int sheetId, int time) {
				this.sheetId = sheetId;
				this.time = time;
			}
		}

		public Animation(string animationName, Project project) {
			this.project = project;
			Loader loader = project.loader;
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(loader.LoadText("animations/" + animationName + ".xml"));
			foreach (XmlNode animationNode in doc.SelectNodes("/animation/group")) {
				string groupName = animationNode.Attributes["name"].InnerText.Trim();
				Group group = new Group();
				foreach (XmlNode frameNode in animationNode.SelectNodes("frame")) {
					int id = int.Parse(frameNode.SelectSingleNode("sheetid").InnerText);
					int time = int.Parse(frameNode.SelectSingleNode("time").InnerText);
					group.frames.Add(new Frame(id, time));
				}
				groups.Add(groupName, group);
			}
			this.sheet = project.sheets[doc.SelectSingleNode("/animation/sheet").InnerText.Trim()];
		}

		public Animation(SpriteSheet sheet, Project project) {
			this.project = project;
			this.sheet = sheet;
		}

		#region ICloneable Members

		/// <summary>
		/// Creates a copy of this Animation, but does not add it to the current project.
		/// Do not call the Name property until you added it to the project!
		/// </summary>
		/// <returns>A new Animation instance.</returns>
		public Animation Clone() {
			Animation cloned = new Animation(this.sheet, this.project);
			foreach (KeyValuePair<string, Group> oldGroup in this.groups) {
				Group newGroup = new Group();
				foreach (Frame oldFrame in oldGroup.Value.frames) {
					newGroup.frames.Add(new Frame(oldFrame.sheetId, oldFrame.time));
				}
				cloned.groups.Add(oldGroup.Key, newGroup);
			}
			return cloned;
		}

		object ICloneable.Clone() {
			return this.Clone();
		}

		#endregion
	}
}
