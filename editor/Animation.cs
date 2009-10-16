using System;
using System.Collections.Generic;
using System.Text;
using KFIRPG.corelib;

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
			PropertyReader props = loader.GetPropertyReader().Select("animations/" + animationName + ".xml");
			foreach (PropertyReader animation in props.SelectAll("group")) {
				string groupName = animation.GetString("name");
				Group group = new Group();
				foreach (PropertyReader frame in animation.SelectAll("frame")) {
					int id = frame.GetInt("sheetid");
					int time = frame.GetInt("time");
					group.frames.Add(new Frame(id, time));
				}
				groups.Add(groupName, group);
			}
			this.sheet = project.sheets[props.GetString("sheet")];
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
