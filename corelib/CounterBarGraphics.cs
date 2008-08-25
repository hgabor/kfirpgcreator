using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using SdlDotNet.Graphics.Primitives;

namespace KFIRPG.corelib {
	class CounterBarGraphics: Graphics {
		Color border;
		Color color;
		Size size;

		public CounterBarGraphics(Color color, Color border, int width, int height) {
			this.border = border;
			this.color = color;
			this.size = new Size(width, height);
		}

		public int MaxValue { get; set; }
		int value;
		public int Value {
			get {
				return this.value;
			}
			set {
				this.value = value > MaxValue ? MaxValue : value;
			}
		}

		public override void Blit(int x, int y, SdlDotNet.Graphics.Surface dest) {
			double ratio = (double)value / MaxValue;
			Rectangle fullRect = new Rectangle(new Point(x, y), size);
			Rectangle rect = new Rectangle(new Point(x, y), new Size(Convert.ToInt32(size.Width * ratio), Height));
			Box box = new Box(new Point(x, y), size);
			dest.Fill(fullRect, Color.Black);
			dest.Fill(rect, color);
			dest.Draw(box, border);
		}

		public override int Width {
			get { return size.Width; }
		}

		public override int Height {
			get { return size.Height; }
		}
	}
}
