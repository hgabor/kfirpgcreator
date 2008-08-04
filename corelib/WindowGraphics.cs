using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace KFIRPG.corelib {
	class WindowGraphics: Graphics {
		SdlDotNet.Graphics.Primitives.Box box;
		Dialogs dialogs;
		Size size;

		public WindowGraphics(int width, int height, Dialogs dialogs) {
			size = new Size(width + dialogs.Margin, height + dialogs.Margin);
			this.dialogs = dialogs;
		}

		public override void Blit(int x, int y, SdlDotNet.Graphics.Surface dest) {
			//Code copied from Dialogs.DrawWindow
			int Border = dialogs.Border;
			Color bgColor = dialogs.bgColor;
			SdlDotNet.Graphics.Surface surface = dialogs.surface;
			SdlDotNet.Graphics.Primitives.Box box = new SdlDotNet.Graphics.Primitives.Box(
				new Point(x - dialogs.Margin / 2, y - dialogs.Margin / 2),
				size);
			dest.Draw(box, bgColor, false, true);
			for (int i = Border; i < box.Width - Border; i += Border) {
				dest.Blit(surface, new Point(box.XPosition1 + i, box.YPosition1), new Rectangle(2 * Border, 0, Border, Border));
				dest.Blit(surface, new Point(box.XPosition1 + i, box.YPosition2 - Border + 1), new Rectangle(2 * Border, Border, Border, Border));
			}
			for (int i = Border; i < box.Height - Border; i += Border) {
				dest.Blit(surface, new Point(box.XPosition1, box.YPosition1 + i), new Rectangle(3 * Border, 0, Border, Border));
				dest.Blit(surface, new Point(box.XPosition2 - Border + 1, box.YPosition1 + i), new Rectangle(3 * Border, Border, Border, Border));
			}
			dest.Blit(surface, new Point(box.XPosition1, box.YPosition1), new Rectangle(0, 0, Border, Border));
			dest.Blit(surface, new Point(box.XPosition2 - Border + 1, box.YPosition1), new Rectangle(Border, 0, Border, Border));
			dest.Blit(surface, new Point(box.XPosition1, box.YPosition2 - Border + 1), new Rectangle(0, Border, Border, Border));
			dest.Blit(surface, new Point(box.XPosition2 - Border + 1, box.YPosition2 - Border + 1), new Rectangle(Border, Border, Border, Border));
		}

		public override int Width {
			get { return size.Width; }
		}

		public override int Height {
			get { return size.Height; }
		}
	}
}
