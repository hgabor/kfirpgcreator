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
			int tpfLastTick = 0;
			int advanced = 0;
			int targetMSpM = 20; // ms/movement
			while (!quit) {
				Events.Poll();
				ticks = SdlDotNet.Core.Timer.TicksElapsed;
				if ((ticks - lastMovement) > targetMSpM) {
					lastMovement = ticks;
					game.Advance();
					++advanced;
				}

				++counter;
				if (counter == 250) {
					DateTime measure = DateTime.Now;
					TimeSpan span = measure - lastMeasure;
					int tickMeasure = advanced;
					double mps = (tickMeasure - tpfLastTick) / span.TotalSeconds;
					if (mps > 53) ++targetMSpM;
					if (mps < 47) --targetMSpM;
					Video.WindowCaption = string.Format("FPS: {0} FPS - MPS: {1} - TargetMSpM: {2}",
						250 / span.TotalSeconds,
						mps,
						targetMSpM);
					counter = 0;
					lastMeasure = measure;
					tpfLastTick = tickMeasure;
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
