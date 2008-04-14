using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.corelib {
	public class UserInput {
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
		internal bool IsPressed(Buttons buttons) {
			return (state & buttons) == buttons;
		}
		public void Set(Buttons buttons) {
			state = buttons;
		}
	}
}
