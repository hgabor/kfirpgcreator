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
	class Logic: KFI_RPG_Creator.Core.Logic {
		ObjectLoader loader;
		MapScreen mapScreen;
		Map currentMap;
		
		public Logic(ObjectLoader loader) {
			this.loader = loader;
			currentMap = new Map(300, 400);
			currentMap.AddObject(new GameObject("testtile", 0, 0, loader));
			currentMap.AddObject(new GameObject("testtile", 100, 0, loader));
			currentMap.AddObject(new GameObject("testtile", 200, 0, loader));
			currentMap.AddObject(new GameObject("testtile", 0, 100, loader));
			currentMap.AddObject(new GameObject("testtile", 100, 100, loader));
			currentMap.AddObject(new GameObject("testtile2", 200, 100, loader));
			currentMap.AddObject(new GameObject("testtile", 0, 200, loader));
			currentMap.AddObject(new GameObject("testtile2", 100, 200, loader));
			currentMap.AddObject(new GameObject("testtile2", 200, 200, loader));
			currentMap.AddObject(new GameObject("testtile2", 0, 300, loader));
			currentMap.AddObject(new GameObject("testtile2", 100, 300, loader));
			currentMap.AddObject(new GameObject("testtile2", 200, 300, loader));
			currentMap.AddObject(new GameObject("Gray ball", 20, 20, loader));
			mapScreen = new MapScreen(currentMap);
		}
		
		public Screen VisibleScreen {
			get {
				return mapScreen;
			}
		}
		
		public int Width {
			get {
				return currentMap.Width;
			}
		}
		
		public int Height {
			get {
				return currentMap.Height;
			}
		}
	}
	
	public class LogicFactory: KFI_RPG_Creator.Core.LogicFactory {
		public KFI_RPG_Creator.Core.Logic CreateLogic(ObjectLoader loader) {
			return new Logic(loader);
		}
	}
}
