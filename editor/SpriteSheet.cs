using System;
using System.Collections.Generic;
using System.Text;
using KFIRPG.corelib;
using System.Drawing;
using System.Xml;

namespace KFIRPG.editor {
	class SpriteSheet {
		Project project;
		public string Name {
			get {
				foreach (KeyValuePair<string, SpriteSheet> sheet in project.sheets) {
					if (sheet.Value == this) return sheet.Key;
				}
				throw new Exception("Project does not contain spritesheet!");
			}
		}
		public int cols;
		public Bitmap sheet;
		public int spriteWidth, spriteHeight;
		public int x, y;
		public class Animation {
			public int startFrame;
			public int frameCount;
			public int interval;
			public int timeOut;
			public Animation(int startFrame, int frameCount, int interval, int timeOut) {
				this.startFrame = startFrame;
				this.frameCount = frameCount;
				this.interval = interval;
				this.timeOut = timeOut;
			}
			public Animation(int startFrame, int frameCount, int timeOut) {
				this.startFrame = startFrame;
				this.frameCount = frameCount;
				this.timeOut = timeOut;
			}
		}

		public class Gfx {
			SpriteSheet sheet;
			int id;
			public Gfx(int id, SpriteSheet sheet) {
				this.id = id;
				this.sheet = sheet;
			}

			private Gfx() { }

			public virtual void Draw(int x, int y, Graphics g) {
				g.DrawImage(sheet.sheet, new Rectangle(x - sheet.x, y - sheet.y, sheet.spriteWidth, sheet.spriteHeight), new Rectangle(id % sheet.cols * sheet.spriteWidth, id / sheet.cols * sheet.spriteHeight, sheet.spriteWidth, sheet.spriteHeight), GraphicsUnit.Pixel);
			}
			public virtual int Id { get { return id + 1; } }

			internal static readonly Gfx Empty = new NullGfx();

			private class NullGfx: Gfx {
				public override void Draw(int x, int y, Graphics g) { }
				public override int Id { get { return 0; } }
			}
		}

		#region Old Format

		//public Dictionary<string, Animation> states = new Dictionary<string, Animation>();

		public void LoadOldFormat(XmlDocument doc) {
			//throw new InvalidOperationException("Support for old format is not enabled!");
			foreach (XmlNode node in doc.SelectNodes("/spritesheet/image")) {
				string type = node.Attributes["type"].InnerText;
				int start = int.Parse(node.SelectSingleNode("start").InnerText);
				int count = int.Parse(node.SelectSingleNode("count").InnerText);
				int interval = int.Parse(node.SelectSingleNode("interval").InnerText);
				int timeout = int.Parse(node.SelectSingleNode("timeout").InnerText);
				//states.Add(type, new Animation(start, count, interval, timeout));
				if (type == "still") {
					animations.Add(new AnimationType("still", "down"), new Animation(start, count, timeout));
					animations.Add(new AnimationType("still", "up"), new Animation(start + interval, count, timeout));
					animations.Add(new AnimationType("still", "left"), new Animation(start + 2 * interval, count, timeout));
					animations.Add(new AnimationType("still", "right"), new Animation(start + 3 * interval, count, timeout));
				}
				else if (type.Contains("down")) {
					animations.Add(new AnimationType(type.Replace("down", ""), "down"), new Animation(start, count, timeout));
				}
				else if (type.Contains("up")) {
					animations.Add(new AnimationType(type.Replace("up", ""), "up"), new Animation(start, count, timeout));
				}
				else if (type.Contains("left")) {
					animations.Add(new AnimationType(type.Replace("left", ""), "left"), new Animation(start, count, timeout));
				}
				else if (type.Contains("right")) {
					animations.Add(new AnimationType(type.Replace("right", ""), "right"), new Animation(start, count, timeout));
				}
			}
		}

		#endregion

		public struct AnimationType {
			public readonly string type;
			public readonly string dir;
			public AnimationType(string type, string dir) {
				this.type = type;
				this.dir = dir;
			}

			public override string ToString() {
				return type + " " + dir;
			}
		}

		public Dictionary<AnimationType, Animation> animations = new Dictionary<AnimationType, Animation>();

		public SpriteSheet(string name, Project project) {
			this.project = project;
			Loader loader = project.loader;
			using (Bitmap bm = loader.LoadBitmap("img/" + name + ".png")) {
				sheet = new Bitmap(bm);
			}
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(loader.LoadText("img/" + name + ".xml"));
			spriteWidth = int.Parse(doc.SelectSingleNode("/spritesheet/width").InnerText);
			spriteHeight = int.Parse(doc.SelectSingleNode("/spritesheet/height").InnerText);
			x = int.Parse(doc.SelectSingleNode("/spritesheet/x").InnerText);
			y = int.Parse(doc.SelectSingleNode("/spritesheet/y").InnerText);
			cols = sheet.Width / spriteWidth;
			//cols = int.Parse(doc.SelectSingleNode("/spritesheet/cols").InnerText);
			XmlNodeList nodes = doc.SelectNodes("/spritesheet/animation");
			if (nodes != null && nodes.Count > 0) {
				foreach (XmlNode node in nodes) {
					string type = node.Attributes["type"].InnerText;
					string dir = node.Attributes["dir"].InnerText;
					int start = int.Parse(node.SelectSingleNode("start").InnerText);
					int count = int.Parse(node.SelectSingleNode("count").InnerText);
					int timeout = int.Parse(node.SelectSingleNode("timeout").InnerText);
					animations.Add(new AnimationType(type, dir), new Animation(start, count, timeout));
				}
			}
		}

		public SpriteSheet(Project project) {
			this.project = project;
		}

		public Gfx GetGfxById(int id) {
			if (id == 0) return Gfx.Empty;
			else return new Gfx(id - 1, this);
		}
	}
}
