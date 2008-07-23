using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using SdlDotNet.Graphics;

namespace KFIRPG.corelib {
	class Dialogs {
		Surface surface;
		public readonly Color bgColor;
		public readonly Color selectedBorder;
		public readonly Color selectedBg;
		public readonly int Border;
		public readonly int Margin = 50;
		public readonly int TextHeight;
		Game game;
		public readonly SdlDotNet.Graphics.Font Font;

		public Dialogs(Game game) {
			this.game = game;
			PropertyReader props = game.loader.GetPropertyReader().Select("dialog/dialog.xml");
			Border = props.GetInt("border");
			string fontName = props.GetString("font");
			int fontSize = props.GetInt("fontsize");
			Font = new SdlDotNet.Graphics.Font(game.loader.LoadRaw("dialog/" + fontName), fontSize);
			bgColor = Color.FromArgb(
				props.GetInt("background/alpha"),
				props.GetInt("background/red"),
				props.GetInt("background/green"),
				props.GetInt("background/blue"));
			selectedBorder = Color.FromArgb(
				props.GetInt("selectedborder/alpha"),
				props.GetInt("selectedborder/red"),
				props.GetInt("selectedborder/green"),
				props.GetInt("selectedborder/blue"));
			selectedBg = Color.FromArgb(
				props.GetInt("selectedbackground/alpha"),
				props.GetInt("selectedbackground/red"),
				props.GetInt("selectedbackground/green"),
				props.GetInt("selectedbackground/blue"));

			TextHeight = Font.SizeText(" ").Height;

			surface = new Surface(game.loader.LoadBitmap("dialog/windowborder.png"));
		}

		//TODO: Check for border size and width/height
		public void DrawCenteredWindow(int width, int height, Surface dest) {
			SdlDotNet.Graphics.Primitives.Box box = new SdlDotNet.Graphics.Primitives.Box(new Point((game.Width - width) / 2, (game.Height - height) / 2), new Size(width, height));
			dest.Draw(box, bgColor, false, true);
			for (int i = Border; i < width - Border; i += Border) {
				dest.Blit(surface, new Point(box.XPosition1 + i, box.YPosition1), new Rectangle(2 * Border, 0, Border, Border));
				dest.Blit(surface, new Point(box.XPosition1 + i, box.YPosition2 - Border + 1), new Rectangle(2 * Border, Border, Border, Border));
			}
			for (int i = Border; i < height - Border; i += Border) {
				dest.Blit(surface, new Point(box.XPosition1, box.YPosition1 + i), new Rectangle(3 * Border, 0, Border, Border));
				dest.Blit(surface, new Point(box.XPosition2 - Border + 1, box.YPosition1 + i), new Rectangle(3 * Border, Border, Border, Border));
			}
			dest.Blit(surface, new Point(box.XPosition1, box.YPosition1), new Rectangle(0, 0, Border, Border));
			dest.Blit(surface, new Point(box.XPosition2 - Border + 1, box.YPosition1), new Rectangle(Border, 0, Border, Border));
			dest.Blit(surface, new Point(box.XPosition1, box.YPosition2 - Border + 1), new Rectangle(0, Border, Border, Border));
			dest.Blit(surface, new Point(box.XPosition2 - Border + 1, box.YPosition2 - Border + 1), new Rectangle(Border, Border, Border, Border));
		}

		public void DrawWindow(SdlDotNet.Graphics.Primitives.Box box, Surface dest) {
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
	}
}
