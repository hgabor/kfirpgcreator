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
		public int Cols {
			get { return sheet.Width / spriteWidth; ;}
		}
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
				if (sheet == null) throw new ArgumentNullException("sheet");
				this.id = id;
				this.sheet = sheet;
			}

			private Gfx() { }

			public virtual void Draw(int x, int y, Graphics g) {
				g.DrawImage(sheet.sheet, new Rectangle(x - sheet.x, y - sheet.y, sheet.spriteWidth, sheet.spriteHeight), new Rectangle(id % sheet.Cols * sheet.spriteWidth, id / sheet.Cols * sheet.spriteHeight, sheet.spriteWidth, sheet.spriteHeight), GraphicsUnit.Pixel);
			}
			public virtual int Id { get { return id + 1; } }

			internal static readonly Gfx Empty = new NullGfx();

			private class NullGfx: Gfx {
				public override void Draw(int x, int y, Graphics g) { }
				public override int Id { get { return 0; } }
			}
		}

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
