using System;
using System.Collections.Generic;
using System.Text;
using SdlDotNet.Core;
using SdlDotNet.Graphics;
using KFIRPG.corelib;
using System.Xml;

namespace KFIRPG.runner {
	public class Program {
		public static void Main() {
			//I'd like to get exception data from the IDE, not from logging.
			//Should be turned on for production code.
			//try {
			Game game = Game.LoadFromFile("game.xml");

			XmlDocument settings = new XmlDocument();
			try {
				settings.Load("game_settings.xml");
			}
			catch (System.IO.FileNotFoundException) {
				//Create settings file
				settings.AppendChild(settings.CreateXmlDeclaration("1.0", null, null));
				XmlElement root = settings.CreateElement("settings");
				settings.AppendChild(root);
				root.AppendChild(settings.CreateElement("fullscreen")).InnerText = "0";
			}


			bool fullScreen = settings.SelectSingleNode("/settings/fullscreen").InnerText == "1";
			Surface screen = SdlDotNet.Graphics.Video.SetVideoMode(game.Width, game.Height, false, false, fullScreen);

			SdlDotNet.Input.Joystick joy = null;
			if (SdlDotNet.Input.Joysticks.NumberOfJoysticks > 0) {
				joy = SdlDotNet.Input.Joysticks.OpenJoystick(0);
				if (joy.NumberOfAxes < 2 || joy.NumberOfButtons < 2) {
					joy.Close();
					joy = null;
				}
			}

			bool quit = false;
			Events.Quit += (sender, args) => { quit = true; };

			int counter = 0;
			DateTime lastMeasure = DateTime.Now;
			int lastMovement = SdlDotNet.Core.Timer.TicksElapsed;
			int ticks = 0;
			int tpfLastTick = 0;
			int advanced = 0;
			int targetMSpM = 20; // ms/movement
			bool AltPressed = false;
			bool F4Pressed = false;
			bool ReturnPressed = false;
			while (!quit) {
				Events.Poll();
				UserInput.Buttons buttons = UserInput.Buttons.None;
				if (SdlDotNet.Input.Keyboard.IsKeyPressed(SdlDotNet.Input.Key.UpArrow)) buttons |= UserInput.Buttons.Up;
				if (SdlDotNet.Input.Keyboard.IsKeyPressed(SdlDotNet.Input.Key.DownArrow)) buttons |= UserInput.Buttons.Down;
				if (SdlDotNet.Input.Keyboard.IsKeyPressed(SdlDotNet.Input.Key.LeftArrow)) buttons |= UserInput.Buttons.Left;
				if (SdlDotNet.Input.Keyboard.IsKeyPressed(SdlDotNet.Input.Key.RightArrow)) buttons |= UserInput.Buttons.Right;
				if (SdlDotNet.Input.Keyboard.IsKeyPressed(SdlDotNet.Input.Key.Space)) buttons |= UserInput.Buttons.Action;
				if (SdlDotNet.Input.Keyboard.IsKeyPressed(SdlDotNet.Input.Key.Escape)) buttons |= UserInput.Buttons.Back;
				if (joy != null) {
					if (joy.GetAxisPosition(SdlDotNet.Input.JoystickAxis.Vertical) == 0) buttons |= UserInput.Buttons.Up;
					if (joy.GetAxisPosition(SdlDotNet.Input.JoystickAxis.Vertical) == 1) buttons |= UserInput.Buttons.Down;
					if (joy.GetAxisPosition(SdlDotNet.Input.JoystickAxis.Horizontal) == 0) buttons |= UserInput.Buttons.Left;
					if (joy.GetAxisPosition(SdlDotNet.Input.JoystickAxis.Horizontal) == 1) buttons |= UserInput.Buttons.Right;
					if (joy.GetButtonState(0) == SdlDotNet.Input.ButtonKeyState.Pressed) buttons |= UserInput.Buttons.Action;
					if (joy.GetButtonState(1) == SdlDotNet.Input.ButtonKeyState.Pressed) buttons |= UserInput.Buttons.Back;
				}

				game.Input.Set(buttons);
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
					// If the game is too slow or too fast, this little trick adjusts the speed
					// between 47~53 game frames / sec (MPS).
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


				AltPressed = SdlDotNet.Input.Keyboard.IsKeyPressed(SdlDotNet.Input.Key.RightAlt) || SdlDotNet.Input.Keyboard.IsKeyPressed(SdlDotNet.Input.Key.LeftAlt);

				//Check for Alt+Enter, but not for Enter+Alt
				if (AltPressed && !ReturnPressed && SdlDotNet.Input.Keyboard.IsKeyPressed(SdlDotNet.Input.Key.Return)) {
					fullScreen = !fullScreen;
					screen = SdlDotNet.Graphics.Video.SetVideoMode(game.Width, game.Height, false, false, fullScreen);
				}
				ReturnPressed = SdlDotNet.Input.Keyboard.IsKeyPressed(SdlDotNet.Input.Key.Return);

				//Check for Alt+F4, but not for F4+Alt
				if (AltPressed && !F4Pressed && SdlDotNet.Input.Keyboard.IsKeyPressed(SdlDotNet.Input.Key.F4)) {
					quit = true;
				}
				F4Pressed = SdlDotNet.Input.Keyboard.IsKeyPressed(SdlDotNet.Input.Key.F4);
			}

			settings.SelectSingleNode("/settings/fullscreen").InnerText = fullScreen ? "1" : "0";
			settings.Save("game_settings.xml");

			//}
			//catch(Exception ex) {
			//	System.Windows.Forms.MessageBox.Show(ex.ToString(), "Exception");
			//}
		}
	}
}
