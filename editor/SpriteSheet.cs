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
		public int size;
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
			int id, cols, width, height;
			public Gfx(int id, int cols, int width, int height, SpriteSheet sheet) {
				this.id = id;
				this.cols = cols;
				this.width = width;
				this.height = height;
				this.sheet = sheet;
			}

			private Gfx() { }

			public virtual void Draw(int x, int y, Graphics g) {
				g.DrawImage(sheet.sheet, new Rectangle(x, y, width, height), new Rectangle(id % cols * sheet.size, id / cols * sheet.size, width, height), GraphicsUnit.Pixel);
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
			cols = int.Parse(doc.SelectSingleNode("/spritesheet/cols").InnerText);
			foreach (XmlNode node in doc.SelectNodes("/spritesheet/image")) {
				string type = node.Attributes["type"].InnerText;
				int start = int.Parse(node.SelectSingleNode("start").InnerText);
				int count = int.Parse(node.SelectSingleNode("count").InnerText);
				int interval = int.Parse(node.SelectSingleNode("interval").InnerText);
				int timeout = int.Parse(node.SelectSingleNode("timeout").InnerText);
				states.Add(type, new Animation(start, count, interval, timeout));
			}
			this.size = project.tileSize;
		}

		public Gfx GetGfxById(int id) {
			if (id == 0) return Gfx.Empty;
			else return new Gfx(id - 1, cols, size, size, this);
		}
	}
}
