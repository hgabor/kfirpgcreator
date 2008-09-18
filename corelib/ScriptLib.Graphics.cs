using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.corelib {
	partial class ScriptLib {
		[Script]
		public CustomScreen CustomScreen_New() {
			return new CustomScreen(game);
		}
		[Script]
		public void CustomScreen_Place(CustomScreen screen, int x, int y, Graphics gfx) {
			screen.Place(gfx, new System.Drawing.Point(x, y));
		}
		[Script]
		public void CustomScreen_Remove(CustomScreen screen, Graphics gfx) {
			screen.Remove(gfx);
		}
		[BlockingScript]
		public void CustomScreen_Show(CustomScreen screen) {
			FadeAnimation anim = new FadeAnimation(game);
			anim.FromImage = game.TakeScreenshot();
			game.PushScreen(screen);
			game.PushScreen(anim);
		}
		[Script]
		public void CustomScreen_Hide(CustomScreen screen) {
			screen.Hide();
		}
		[Script]
		public void CustomScreen_OnKey_Add(CustomScreen screen, ScriptFunction script) {
			screen.KeyPressed += (sender, args) => script.Run((int)args.Button);
		}
		[Script]
		public void CustomScreen_Delete() {
			//No-op for now...
		}

		[Script]
		public TextGraphics TextGraphics_New(string text, string align) {
			return new TextGraphics(
				text,
				(TextGraphics.Align)Enum.Parse(typeof(TextGraphics.Align), align, true),
				dialogs,
				game);
		}

		[Script]
		public int Graphics_GetWidth(Graphics graphics) {
			return graphics.Width;
		}

		[Script]
		public int Graphics_GetHeight(Graphics graphics) {
			return graphics.Height;
		}

		[Script]
		public WindowGraphics WindowGraphics_New(int width, int height) {
			return new WindowGraphics(width, height, dialogs);
		}

		[Script]
		public PanelGraphics MenuItemBackground_New(int width, int height) {
			return new PanelGraphics(width, height, dialogs.selectedBg, dialogs.selectedBorder);
		}

		[Script]
		public AnimatedGraphics AnimatedGraphics_New(string name) {
			return new AnimatedGraphics(name, game);
		}

		[Script]
		public void AnimatedGraphics_SetState(AnimatedGraphics gfx, string state) {
			gfx.SetState(state);
		}

		[Script]
		public void AnimatedGraphics_SetDir(AnimatedGraphics gfx, string dir) {
			gfx.SetDirection((Sprite.Dir)Enum.Parse(typeof(Sprite.Dir), dir, true));
		}

		[Script]
		public ImageGraphics ImageGraphics_New(string name) {
			return new ImageGraphics(name, game);
		}

		[Script]
		public int RGB(int r, int g, int b) {
			return System.Drawing.Color.FromArgb(r, g, b).ToArgb();
		}
		[Script]
		public int ARGB(int a, int r, int g, int b) {
			return System.Drawing.Color.FromArgb(a, r, g, b).ToArgb();
		}

		[Script]
		public CounterBarGraphics CounterBarGraphics_New(int bg, int border, int height, int width) {
			return new CounterBarGraphics(
				System.Drawing.Color.FromArgb(bg),
				System.Drawing.Color.FromArgb(border),
				height, width);
		}

		[Script]
		public void CounterBarGraphics_SetValue(CounterBarGraphics cbg, int val) {
			cbg.Value = val;
		}
		[Script]
		public void CounterBarGraphics_SetMaxValue(CounterBarGraphics cbg, int maxval) {
			cbg.MaxValue = maxval;
		}

		[Script]
		public int GetScreenWidth() {
			return game.Width;
		}
		[Script]
		public int GetScreenHeight() {
			return game.Height;
		}
	}
}
