using System;
using System.Collections.Generic;
using System.Text;
using SdlDotNet.Graphics;
using System.Drawing;

namespace KFIRPG.corelib {
	class MessageBox: Screen {
		Game game;
		Dialogs dialogs;
		const int width = 500;
		int height;
		int textX;
		int textY;
		List<Surface> textSurfaces;
		bool wasPressed;

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

		public MessageBox(string text, Dialogs dialogs, Game game) {
			this.game = game;
			this.dialogs = dialogs;
			SdlDotNet.Graphics.Font font = dialogs.Font;

			//HACK: Font.Render only returns a Surface with transparent background when called
			//      with textWidth=0 and maxLines=0, and when the text contains no newline characters.
			//      Otherwise it renders on a black background.
			List<string> lines = SplitAndRejoin(text, font);
			textSurfaces = lines.ConvertAll<Surface>(s => font.Render(s, Color.White, true, 0, 0));

			if (textSurfaces.Count == 0) {
				textX = 0;
				textY = 0;
				height = dialogs.Border * 2 - 1;
			}
			else {
				int sumHeight = 0;
				foreach (Surface s in textSurfaces) {
					sumHeight += s.Height;
				}
				if (textSurfaces.Count == 1) {
					textX = (game.Width - (textSurfaces[0].Width)) / 2;
				}
				else {
					textX = (game.Width - (width - dialogs.Margin * 2)) / 2;
				}
				height = sumHeight + dialogs.Margin * 2;
				while (height % dialogs.Border != 0) ++height;
				textY = (game.Height - sumHeight) / 2;
			}
		}

		public override void Draw(SdlDotNet.Graphics.Surface surface) {
			dialogs.DrawWindow(width - 1, height - 1, surface);
			int y = textY;
			foreach (Surface surf in textSurfaces) {
				surface.Blit(surf, new Point(textX, y));
				y += surf.Height;
			}
		}

		public override void Think() {
			if (game.Input.IsPressed(UserInput.Buttons.Action)) {
				FadeAnimation anim = new FadeAnimation(game);
				anim.FromImage = game.TakeScreenshot();
				game.PopScreen();
				game.vm.ContinueWithValue(null);
				game.PushScreen(anim);
			}
		}
	}
}
