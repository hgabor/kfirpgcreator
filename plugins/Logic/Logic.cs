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
		//ObjectcurrentGame.Loader currentGame.Loader;
		Game currentGame;
		MapScreen mapScreen;
		Map currentMap;
		
		GameObject protagonist;
		
		public Logic(Game currentGame) {
			this.currentGame = currentGame;
			protagonist = new GameObject("Redarrow", 200, 200, 10, currentGame.Loader);
			
			currentMap = new Map(300, 400);
			currentMap.AddObject(new GameObject("Grass", 0, 0, 0, currentGame.Loader));
			currentMap.AddObject(new GameObject("Grass", 100, 0, 0, currentGame.Loader));
			currentMap.AddObject(new GameObject("Grass", 200, 0, 0, currentGame.Loader));
			currentMap.AddObject(new GameObject("Grass", 0, 100, 0, currentGame.Loader));
			currentMap.AddObject(new GameObject("Grass", 100, 100, 0, currentGame.Loader));
			currentMap.AddObject(new GameObject("Lush Grass", 200, 100, 0, currentGame.Loader));
			currentMap.AddObject(new GameObject("Grass", 0, 200, 0, currentGame.Loader));
			currentMap.AddObject(new GameObject("Lush Grass", 100, 200, 0, currentGame.Loader));
			currentMap.AddObject(new GameObject("Lush Grass", 200, 200, 0, currentGame.Loader));
			currentMap.AddObject(new GameObject("Lush Grass", 0, 300, 0, currentGame.Loader));
			currentMap.AddObject(new GameObject("Lush Grass", 100, 300, 0, currentGame.Loader));
			currentMap.AddObject(new GameObject("Lush Grass", 200, 300, 0, currentGame.Loader));
			currentMap.AddObject(new GameObject("Gray ball", 20, 20, 1, currentGame.Loader));
			currentMap.AddObject(protagonist);
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
		
		public int CenterX {
			get {
				return protagonist.X;
			}
		}
		
		public int CenterY {
			get {
				return protagonist.Y;
			}
		}
		
		public int CenterZ {
			get {
				return protagonist.Z;
			}
		}
		
		//const int StraightSpeed = 7;
		//const int DiagonalSpeed = 10;
		const int speed = 10;
		
		public void Work() {
			bool up = currentGame.Controller.Poll(Button.UP);
			bool down = currentGame.Controller.Poll(Button.DOWN);
			bool left = currentGame.Controller.Poll(Button.LEFT);
			bool right = currentGame.Controller.Poll(Button.RIGHT);
			if (up || down || left || right) {
				if (up && down) up = down = false;
				if (left && right) left = right = false;
				if (up && left) {
					protagonist.Facing = Direction.West;
				}
				else if (up && right) {
					protagonist.Facing = Direction.North;
				}
				else if (down && left) {
					protagonist.Facing = Direction.South;
				}
				else if (down && right) {
					protagonist.Facing = Direction.East;
				}
				else if (up) {
					protagonist.Facing = Direction.NorthWest;
				}
				else if (down) {
					protagonist.Facing = Direction.SouthEast;
				}
				else if (left) {
					protagonist.Facing = Direction.SouthWest;
				}
				else if (right) {
					protagonist.Facing = Direction.NorthEast;
				}
				protagonist.MovementSpeed = speed;
			}
			else {
				protagonist.MovementSpeed = 0;
			}
			protagonist.Move();
			
			foreach(GameObject o in currentMap.GetAllObjects()) {
				if (o.AffectedByGravity) {
					o.FallingSpeed = 1;
					o.Fall();
					foreach(GameObject o2 in currentMap.GetAllObjects()) {
						if (o != o2 && o.CollidesWith(o2)) {
							o.AvoidFallingCollision(o2);
						}
					}
				}
				if (o != protagonist && o.CollidesWith(protagonist)) {
					protagonist.AvoidMovementCollision(o);
				}
			}
			
			//TODO: If cannot move, try diagonally...
		}
	}
	
	public class LogicFactory: KFI_RPG_Creator.Core.LogicFactory {
		public KFI_RPG_Creator.Core.Logic CreateLogic(Game game) {
			return new Logic(game);
		}
	}
}
