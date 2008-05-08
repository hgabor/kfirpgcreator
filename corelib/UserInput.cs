using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.corelib {
	public class UserInput {
		public class ButtonEventArgs: EventArgs {
			private Buttons button;
			public Buttons Button { get { return button; } }

			public ButtonEventArgs(Buttons button) {
				this.button = button;
			}
		}
		public event EventHandler<ButtonEventArgs> ButtonStateChanged;

		private void OnButtonStateChanged(ButtonEventArgs args) {
			if (ButtonStateChanged != null) ButtonStateChanged(this, args);
		}

		[Flags]
		public enum Buttons: uint {
			None = 0,
			Down = 1,
			Up = 2,
			Left = 4,
			Right = 8,
			Action = 16,
			Back = 32
		}
		Buttons state = Buttons.None;
		internal Buttons State { get { return state; } }
		bool waitingForKeyUp = false;

		internal bool IsPressed(Buttons buttons) {
			return (state & buttons) == buttons;
		}

		public void Set(Buttons buttons) {
			if (waitingForKeyUp) {
				if (buttons == Buttons.None) {
					waitingForKeyUp = false;
					state = buttons;
					OnButtonStateChanged(new ButtonEventArgs(buttons));
				}
			}
			else {
				if (state != buttons) {
					state = buttons;
					OnButtonStateChanged(new ButtonEventArgs(buttons));
				}
			}
		}

		internal void WaitForKeyUp() {
			waitingForKeyUp = true;
			state = Buttons.None;
		}
	}
}
