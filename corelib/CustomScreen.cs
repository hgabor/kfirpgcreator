using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace KFIRPG.corelib {
	/// <summary>
	/// This screen's contents can be customized by scripts.
	/// </summary>
	class CustomScreen: Screen {
		Game game;
		public CustomScreen(Game game) {
			this.game = game;
		}

		class ScreenGraphics {
			public Graphics graphics;
			public Point coords;
			public ScreenGraphics(Graphics graphics, Point coords) {
				this.graphics = graphics;
				this.coords = coords;
			}
			public virtual void Advance() { }
		}
		class AnimatedScreenGraphics: ScreenGraphics {
			public AnimatedGraphics animatedGraphics;
			public AnimatedScreenGraphics(AnimatedGraphics graphics, Point coords)
				: base(graphics, coords) {
				this.animatedGraphics = graphics;
			}
			public override void Advance() {
				animatedGraphics.Advance();
			}
		}
		List<ScreenGraphics> graphics = new List<ScreenGraphics>();

		public void UpdateEventHandler() {
			graphics = new List<ScreenGraphics>();
		}

		public void Place(Graphics gfx, Point coords) {
			ScreenGraphics sg = graphics.Find(s => s.graphics == gfx);
			if (sg == null) {
				if (gfx is AnimatedGraphics) {
					graphics.Add(new AnimatedScreenGraphics((AnimatedGraphics)gfx, coords));
				}
				else {
					graphics.Add(new ScreenGraphics(gfx, coords));
				}
			}
			else {
				sg.coords = coords;
			}
		}

		public void Remove(Graphics gfx) {
			graphics.RemoveAll(sgfx => sgfx.graphics == gfx);
		}

		bool hide = false;
		internal void Hide() {
			hide = true;
		}

		public event EventHandler<UserInput.ButtonEventArgs> KeyPressed;


		class Timer {
			public int framesLeft;
			public EventHandler callback;
		}
		List<Timer> timers = new List<Timer>();

		public void SetTimer(int milliseconds, EventHandler callback) {
			timers.Add(new Timer { framesLeft = (milliseconds / 20), callback = callback });
		}

		public override void Render(SdlDotNet.Graphics.Surface surface) {
			foreach (ScreenGraphics gfx in graphics) {
				gfx.graphics.Render(gfx.coords.X, gfx.coords.Y, surface);
			}
		}

		public override void Advance() {
			if (hide) {
				FadeAnimation animation = new FadeAnimation(game);
				animation.FromImage = game.TakeScreenshot();
				game.PopScreen();
				game.vm.ContinueWithValue(null);
				game.PushScreen(animation);
			}
			else {
				UserInput.Buttons keyState = game.Input.State;
				if (keyState != UserInput.Buttons.None) {
					if (KeyPressed != null) KeyPressed(this, new UserInput.ButtonEventArgs(keyState));
				}
				game.Input.WaitForKeyUp();
			}
			foreach (var gfx in graphics) {
				gfx.Advance();
			}
			foreach (Timer timer in timers) {
				--timer.framesLeft;
				if (timer.framesLeft == 0) {
					timer.callback(this, new EventArgs());
				}
			}
			timers.RemoveAll(t => t.framesLeft == 0);
		}
	}
}
