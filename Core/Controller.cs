// Copyright (C) 2007 Gábor Halász
// 
// This program is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either version 2
// of the License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.

using System;

namespace KFI_RPG_Creator.Core {
	
	/// <summary>
	/// The buttons the game can use.
	/// </summary>
	public enum Button {
		SELECT,
		BACK,
		UP,
		DOWN,
		LEFT,
		RIGHT
	}

	/// <summary>
	/// Represents a game controller (keyboard, joystic etc.).
	/// </summary>
	public interface Controller {
		/// <summary>
		/// Check if a button is activated.
		/// </summary>
		/// <param name="b">The button to check</param>
		/// <returns>The state of the button</returns>
		bool Poll(Button b);
	}
	
	/// <summary>
	/// A factory that creates game controller-related objects.
	/// </summary>
	public interface ControllerFactory {
		/// <summary>
		/// Creates Controlles objects.
		/// </summary>
		Controller Create();
	}
}
