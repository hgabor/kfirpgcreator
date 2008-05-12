using System;
using System.Collections.Generic;
using System.Text;
using SdlDotNet.Graphics;
using System.Drawing;

namespace KFIRPG.corelib {
	/// <summary>
	/// Displays text on the screen.
	/// </summary>
	class TextGraphics: Graphics {
		Point location;
		/// <summary>
		/// Gets or sets the coordinates the text should appear at.
		/// </summary>
		public Point Location {
			get { return location; }
			set { location = value; }
		}
		Game game;
		Dialogs dialogs;
		List<Surface> textSurfaces;
		const int width = 500;
		/// <summary>
		/// Gets the width of the text.
		/// </summary>
		public int Width { get { return width; } }
		/// <summary>
		/// Gets the height of the text.
		/// </summary>
		public int Height {
			get {
				return textSurfaces.Count * dialogs.TextHeight;
			}
		}

		public enum Align { Left, Right, Center }
		Align align;
		/// <summary>
		/// Creates a new text to be shown on screen.
		/// </summary>
		/// <param name="text">The text to be shown.</param>
		/// <param name="align">The alignment of the text.</param>
		/// <param name="dialogs"></param>
		/// <param name="game"></param>
		/// <remarks>Use to Coords property to set the location.</remarks>
		public TextGraphics(string text, Align align, Dialogs dialogs, Game game) {
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

		/// <summary>
		/// Splits the text into lines to fit inside the specified width.
		/// </summary>
		/// <param name="text">The text to be splitted.</param>
		/// <param name="font">The font to use for the width calculation.</param>
		/// <returns></returns>
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

		//TODO: Candidate for optimization: two much Point creation
		public override void Blit(int x, int y, SdlDotNet.Graphics.Surface dest) {
			int yLocal = 0;
			if (align == Align.Left) {
				foreach (Surface surf in textSurfaces) {
					dest.Blit(surf, new Point(x + location.X, yLocal + y + location.Y));
					yLocal += surf.Height;
				}
			}
			else if (align == Align.Right) {
				foreach (Surface surf in textSurfaces) {
					dest.Blit(surf, new Point(x + location.X + width - surf.Width, yLocal + y + location.Y));
					yLocal += surf.Height;
				}
			}
			else {
				foreach (Surface surf in textSurfaces) {
					dest.Blit(surf, new Point(x + location.X + (width - surf.Width) / 2, yLocal + y + location.Y));
					yLocal += surf.Height;
				}
			}
		}
	}
}
