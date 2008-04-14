using System;
using System.Collections.Generic;
using System.Text;
using SdlDotNet.Core;
using SdlDotNet.Graphics;
using KFIRPG.corelib;

namespace KFIRPG.runner {
	public class Program {
		public static void Main() {
			//try {
				Game game = Game.LoadFromFile("game.xml");
				Surface screen = SdlDotNet.Graphics.Video.SetVideoMode(800, 600);
				bool quit = false;
				Events.Quit += (sender, args) => { quit = true; };
				int counter = 0;
				DateTime lastMeasure = DateTime.Now;
				int lastMovement = SdlDotNet.Core.Timer.TicksElapsed;
				int ticks = 0;
				while (!quit) {
					Events.Poll();
					ticks = SdlDotNet.Core.Timer.TicksElapsed;
					if ((ticks - lastMovement) > 20) {
						lastMovement = ticks;
						game.Advance();
					}

					++counter;
					if (counter == 250) {
						DateTime measure = DateTime.Now;
						Video.WindowCaption = (250 / (measure - lastMeasure).TotalSeconds).ToString();
						counter = 0;
						lastMeasure = measure;
					}
					game.Render(screen);
					Video.Update();
				}
			//}
			//catch(Exception ex) {
			//	System.Windows.Forms.MessageBox.Show(ex.ToString(), "Exception");
			//}
		}
	}
}
