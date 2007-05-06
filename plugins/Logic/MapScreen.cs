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
using KFI_RPG_Creator.Core;

namespace KFI_RPG_Creator.Logic {
	/// <summary>
	/// Description of MapScreen.
	/// </summary>
	class MapScreen: KFI_RPG_Creator.Core.Screen {
		Map currentMap;
		
		public MapScreen(Map currentMap) {
			this.currentMap = currentMap;
		}
		
		public Sprite[] VisibleSprites {
			get {
				GameObject[] objects = currentMap.GetAllObjects();
				Sprite[] sprites = new Sprite[objects.Length];
				objects.CopyTo(sprites, 0);
				return sprites;
			}
		}
		
		public ScreenType Type {
			get {
				throw new NotImplementedException();
			}
		}
	}
}
