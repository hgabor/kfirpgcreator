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
using Core.Controller;
using SdlDotNet.Input;

namespace SDLPlugin {
	
	public class KeyboardFactory: ControllerFactory {
		public Controller Create() {
			return new Keyboard();
		}
	}
	
	/// <summary>
	/// Description of Keyboard.
	/// </summary>
	public class Keyboard: Controller {
		internal Keyboard() {
		}
		
		public bool Poll(Button b) {
			KeyboardState keys = new KeyboardState();
			switch(b) {
				case Button.SELECT:
					return keys.IsKeyPressed(Key.Space);
				case Button.BACK:
					return keys.IsKeyPressed(Key.Escape);
				case Button.UP:
					return keys.IsKeyPressed(Key.UpArrow);
				case Button.DOWN:
					return keys.IsKeyPressed(Key.DownArrow);
				case Button.LEFT:
					return keys.IsKeyPressed(Key.LeftArrow);
				case Button.RIGHT:
					return keys.IsKeyPressed(Key.RightArrow);
				default:
					return false;
			}
		}
	}
	
	
}
