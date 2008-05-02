using System;
using System.Collections.Generic;
using System.Text;
using SdlDotNet.Graphics;
using System.Drawing;

namespace KFIRPG.corelib {
	class TextGraphics: Graphics {
		int x, y;
		Game game;
		Dialogs dialogs;
		const int width = 500;
		List<Surface> textSurfaces;
		public int Height {
			get {
				return textSurfaces.Count * dialogs.TextHeight;
			}
		}

		public enum Align { Left, Right, Center }
		Align align;
		public TextGraphics(string text, int x, int y, Align align, Dialogs dialogs, Game game) {
			this.x = x;
			this.y = y;
			this.game = game;
			this.dialogs = dialogs;
			this.align = align;
			SdlDotNet.Graphics.Font font = dialogs.Font;

			//HACK: Font.Render only returns a Surface with transparent background when called
			//      with textWidth=0 and maxLines=0, and when the text contains no newline characters.
			//      Otherwise it renders on a black background.
			//      So we need to break up the string into lines by hand.
			List<string> lines = SplitAndRejoin(text, font);
			textSurfaces = lines.ConvertAll<Surface>(s => font.Render(s, Color.White, true, 0, 0));
		}

		List<string> SplitAndRejoin(string text, SdlDotNet.Graphics.Font font) {
			List<string> retLines = new List<string>();
			string[] lines = text.Split('\n');
			foreach (string line in lines) {
				string[] words = line.Trim().Split(' ');
				int i = 0;
				List<string> currentLine = new List<string>();
				while (i < words.Length) {
					currentLine.Add(words[i]);
					if (currentLine.Count != 1 && font.SizeText(string.Join(" ", currentLine.ToArray())).Width > width - 100) {
						currentLine.RemoveAt(currentLine.Count - 1);
						retLines.Add(string.Join(" ", currentLine.ToArray()));
						currentLine = new List<string>();
					}
					else {
						++i;
					}
				}
				if (currentLine.Count != 0) {
					retLines.Add(string.Join(" ", currentLine.ToArray()));
					currentLine = new List<string>();
				}
			}
			return retLines;
		}



		public override void Blit(int x, int y, SdlDotNet.Graphics.Surface dest) {
			int yLocal = 0;
			if (align == Align.Left) {
				foreach (Surface surf in textSurfaces) {
					dest.Blit(surf, new Point(x + this.x, yLocal + y + this.y));
					yLocal += surf.Height;
				}
			}
			else if (align == Align.Right) {
				foreach (Surface surf in textSurfaces) {
					dest.Blit(surf, new Point(x + this.x + width - surf.Width, yLocal + y + this.y));
					yLocal += surf.Height;
				}
			}
			else {
				foreach (Surface surf in textSurfaces) {
					dest.Blit(surf, new Point(x + this.x + (width - surf.Width) / 2, yLocal + y + this.y));
					yLocal += surf.Height;
				}
			}
		}
	}
}
