using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.corelib {
	/// <summary>
	/// Provides input handling for the game.
	/// </summary>
	public class UserInput {
		/// <summary>
		/// Contains the event data for the ButtonStateChange event.
		/// </summary>
		public class ButtonEventArgs: EventArgs {
			private Buttons button;
			/// <summary>
			/// Gets the pressed buttons.
			/// </summary>
			public Buttons Button { get { return button; } }

			public ButtonEventArgs(Buttons button) {
				this.button = button;
			}
		}
		/// <summary>
		/// Occurs when a button is set or unset.
		/// </summary>
		public event EventHandler<ButtonEventArgs> ButtonStateChanged;

		private void OnButtonStateChanged(ButtonEventArgs args) {
			if (enabled && ButtonStateChanged != null) ButtonStateChanged(this, args);
		}

		/// <summary>
		/// The buttons the game can handle.
		/// </summary>
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
		/// <summary>
		/// Gets the currently pressed buttons.
		/// </summary>
		internal Buttons State { get { return enabled ? state : Buttons.None; } }
		bool waitingForKeyUp = false;

		/// <summary>
		/// Returns true if the selected button (or combination) is pressed.
		/// </summary>
		/// <param name="buttons">The button or combination.</param>
		/// <returns>True if the button is pressed, otherwise false.</returns>
		internal bool IsPressed(Buttons buttons) {
			return enabled && (state & buttons) == buttons;
		}

		/// <summary>
		/// Sets the current state of the buttons.
		/// </summary>
		/// <param name="buttons"></param>
		/// <remarks>Use this method the report the changes to the controller.
		/// Should not be used by internal code.</remarks>
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

		/// <summary>
		/// Disables input handling until all buttons are released.
		/// </summary>
		/// <remarks>Reports Buttons.None until the Set() method is called with Buttons.None.
		/// After that it returns to normal operation.</remarks>
		internal void WaitForKeyUp() {
			waitingForKeyUp = true;
			state = Buttons.None;
		}

		bool enabled = true;
		/// <summary>
		/// Disables input handling. Default is enabled.
		/// </summary>
		/// <remarks>Reports Buttons.None as the state until the Enable() method is called.</remarks>
		internal void Disable() {
			enabled = false;
		}
		/// <summary>
		/// Enables input handling. Default is enabled.
		/// </summary>
		/// <remarks>Cancels the effects of the Disable() method.</remarks>
		internal void Enable() {
			enabled = true;
		}
	}
}
