using System;
using System.Collections.Generic;
using System.Text;
using SdlDotNet.Core;
using SdlDotNet.Graphics;
using KFIRPG.corelib;

namespace KFIRPG.runner {
	public class Program {
		public static void Main() {
			try {
				Game game = Game.LoadFromFile("config.xml");
				Surface screen = SdlDotNet.Graphics.Video.SetVideoMode(800, 600);
				bool quit = false;
				Events.Quit += (sender, args) => { quit = true; };
				int counter = 0;
				DateTime lastMeasure = DateTime.Now;
				while (!quit) {
					Events.Poll();
					game.Render(screen);
					Video.Update();
					++counter;
					if (counter == 250) {
						DateTime measure = DateTime.Now;
						Video.WindowCaption = (250 / (measure - lastMeasure).TotalSeconds).ToString();
						counter = 0;
						lastMeasure = measure;
					}
				}
			}
			catch(Exception ex) {
				System.Windows.Forms.MessageBox.Show(ex.ToString(), "Exception");
			}
		}
	}
}
