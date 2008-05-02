﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
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
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(game.loader.LoadText("dialog/dialog.xml"));
			Border = int.Parse(doc.SelectSingleNode("/dialog/border").InnerText);
			int red = int.Parse(doc.SelectSingleNode("/dialog/background/red").InnerText);
			int green = int.Parse(doc.SelectSingleNode("/dialog/background/green").InnerText);
			int blue = int.Parse(doc.SelectSingleNode("/dialog/background/blue").InnerText);
			int alpha = int.Parse(doc.SelectSingleNode("/dialog/background/alpha").InnerText);
			string fontName = doc.SelectSingleNode("/dialog/font").InnerText.Trim();
			int fontSize = int.Parse(doc.SelectSingleNode("/dialog/fontsize").InnerText);
			Font = new SdlDotNet.Graphics.Font(game.loader.LoadRaw("dialog/" + fontName), fontSize);
			bgColor = Color.FromArgb(alpha, red, green, blue);

			selectedBorder = Color.FromArgb(
				int.Parse(doc.SelectSingleNode("/dialog/selectedborder/alpha").InnerText),
				int.Parse(doc.SelectSingleNode("/dialog/selectedborder/red").InnerText),
				int.Parse(doc.SelectSingleNode("/dialog/selectedborder/green").InnerText),
				int.Parse(doc.SelectSingleNode("/dialog/selectedborder/blue").InnerText));
			selectedBg = Color.FromArgb(
				int.Parse(doc.SelectSingleNode("/dialog/selectedbackground/alpha").InnerText),
				int.Parse(doc.SelectSingleNode("/dialog/selectedbackground/red").InnerText),
				int.Parse(doc.SelectSingleNode("/dialog/selectedbackground/green").InnerText),
				int.Parse(doc.SelectSingleNode("/dialog/selectedbackground/blue").InnerText));

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
