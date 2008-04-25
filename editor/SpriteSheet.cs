using System;
using System.Collections.Generic;
using System.Text;
using KFIRPG.corelib;
using System.Drawing;
using System.Xml;

namespace KFIRPG.editor {
	class SpriteSheet {
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

		public Dictionary<string, Animation> states = new Dictionary<string, Animation>();

		public SpriteSheet(string name, Project project) {
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
			foreach (XmlNode node in doc.SelectNodes("/spritesheet/image")) {
				string type = node.Attributes["type"].InnerText;
				int start = int.Parse(node.SelectSingleNode("start").InnerText);
				int count = int.Parse(node.SelectSingleNode("count").InnerText);
				int interval = int.Parse(node.SelectSingleNode("interval").InnerText);
				int timeout = int.Parse(node.SelectSingleNode("timeout").InnerText);
				states.Add(type, new Animation(start, count, interval, timeout));
			}
			this.spriteWidth = project.tileSize;
		}

		public Gfx GetGfxById(int id) {
			if (id == 0) return Gfx.Empty;
			else return new Gfx(id - 1, this);
		}
	}
}
