﻿// Copyright (C) 2007 Gábor Halász
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

namespace KFI_RPG_Creator.Core {
	public interface Sprite {
		int X {get;}
		int Y {get;}
		int Z {get;}
		string TypeID {get;}
		Direction Facing {get;}
	}
	
	/// <summary>
	/// Description of GraphicsPlugin.
	/// </summary>
	public interface GraphicsPlugin {
		void Render();
	}
	
	public interface GraphicsPluginFactory {
		GraphicsPlugin Create(Game g);
	}
}
