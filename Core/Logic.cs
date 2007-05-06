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
	public enum ScreenType {
		MAP,
		BATTLE
	}
	
	/// <summary>
	/// A screen of the game (battle, overworld, map etc.).
	/// </summary>
	public interface Screen {
		/// <summary>
		/// Gets the currently visible objects on the screen.
		/// </summary>
		Sprite[] VisibleSprites {get;}
		
		ScreenType Type {get;}
	}
	
	/// <summary>
	/// The object that encapsulates all game logic related objects.
	/// </summary>
	public interface Logic {
		/// <summary>
		/// Gets the currently visible screen.
		/// </summary>
		Screen VisibleScreen {get;}
		
		int Width {get;}
		int Height {get;}
	}
	
	/// <summary>
	/// A factory that creates game logic realted objects.
	/// </summary>
	public interface LogicFactory {
		/// <summary>
		/// Creates Logic objects.
		/// </summary>
		/// <param name="loader">An ObjectLoader to load the objects from.</param>
		/// <returns>A Logic object.</returns>
		Logic CreateLogic(ObjectLoader loader);
	}
}
