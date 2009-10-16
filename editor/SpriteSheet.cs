using System;
using System.Collections.Generic;
using System.Text;
using KFIRPG.corelib;
using System.Drawing;

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

		public SpriteSheet(string name, Project project) {
			this.project = project;
			Loader loader = project.loader;
			using (Bitmap bm = loader.LoadBitmap("img/" + name + ".png")) {
				sheet = new Bitmap(bm);
			}
			PropertyReader props = loader.GetPropertyReader().Select("img/" + name + ".xml");
			spriteWidth = props.GetInt("width");
			spriteHeight = props.GetInt("height");
			x = props.GetInt("x");
			y = props.GetInt("y");
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
