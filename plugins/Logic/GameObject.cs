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
using System.Drawing;
using KFI_RPG_Creator.Core;
using NUnit.Framework;

namespace KFI_RPG_Creator.Logic {
	/// <summary>
	/// Játékban használt objektum implementációja
	/// </summary>
	class GameObject : Sprite {
		string id;
		public string TypeID {
			get {
				return id;
			}
		}

		bool walkable;
		public virtual bool Walkable {
			get { return walkable; }
		}

		bool swimmable;
		public virtual bool Swimmable {
			get { return swimmable; }
		}
		
		int x;
		public int X {
			get { return x; }
			set { x = value; }
		}
		int y;
		public int Y {
			get { return y; }
			set { y = value; }
		}
		int z;
		public int Z {
			get { return z; }
			set { z = value; }
		}

		internal GameObject(string id, int x, int y, int z, ObjectLoader loader) {
			this.id = id;
			walkable = (loader.GetAttribute(id, "walkable") == "true");
			swimmable = (loader.GetAttribute(id, "swimmable") == "true");
			this.x = x;
			this.y = y;
			this.z = z;
		}
		
		internal GameObject(string id, ObjectLoader loader) : this(id, 0, 0, 0, loader) {}
		
		Direction facing = Direction.West;
		public Direction Facing {
			get {
				return facing;
			}
			set {
				facing = value;
			}
		}
		
		int movementSpeed = 0;
		public int MovementSpeed {
			get { return movementSpeed; }
			set { movementSpeed = value; }
		}
		
		int fallingSpeed = 0;
		public int FallingSpeed {
			get { return fallingSpeed; }
			set { fallingSpeed = value; }
		}
		
		public void Move() {
			Move(facing);
		}
		
		public void Move(Direction d) {
			if (movementSpeed != 0) {
				Point p = d.CalculateNewCoords(new Point(x,y), movementSpeed);
				x = p.X;
				y = p.Y;
			}
		}
		public void Fall() {
			if (fallingSpeed != 0) {
				z -= fallingSpeed;
			}
		}
	}

	#if DEBUG

	[TestFixture]
	public class GameObject_Test {
		ObjectLoader loader;

		[SetUp]
		public void SetUp() {
			loader = new TestLoader();
		}

		//Normál elkészítés közben nincs-e hiba (kivétel)
		[Test]
		public void CreatingNormallySucceeds() {
			GameObject g = new GameObject("something", loader);
			Assert.IsTrue(g.TypeID == "something");
		}

		[Test]
		[ExpectedException(typeof(ObjectNotFoundException))]
		public void CreatingNonexistantThrows() {
			GameObject g = new GameObject("nonexistant", loader);
		}

		[Test]
		public void WalkableProperty() {
			GameObject walk = new GameObject("walkable", loader);
			GameObject nonwalk = new GameObject("swimmable", loader);
			Assert.IsTrue(walk.Walkable);
			Assert.IsFalse(nonwalk.Walkable);
		}

		[Test]
		public void SwimmableProperty() {
			GameObject swim = new GameObject("swimmable", loader);
			GameObject nonswim = new GameObject("walkable", loader);
			Assert.IsTrue(swim.Swimmable);
			Assert.IsFalse(nonswim.Swimmable);
		}
		
		[Test]
		public void MovesStraight() {
			GameObject o = new GameObject("something", 0, 0, 0, loader);
			o.MovementSpeed = 20;
			o.Move(Direction.South);
			Assert.IsTrue(o.X == 0);
			Assert.IsTrue(o.Y == 20);
			Assert.IsTrue(o.Z == 0);
		}
		
		[Test]
		public void MovesDiagonally() {
			GameObject o = new GameObject("something", 100, 100, 10, loader);
			o.MovementSpeed = 50;
			o.Move(Direction.NorthWest);
			// SQRT(2) / 2 * 50 = 35.355339059327376220042218105242
			Assert.IsTrue(o.X == 65);
			Assert.IsTrue(o.Y == 65);
			Assert.IsTrue(o.Z == 10);
		}
		
		[Test]
		public void Falls() {
			GameObject o = new GameObject("something", 0, 0, 0, loader);
			o.FallingSpeed = 10;
			o.Fall();
			Assert.IsTrue(o.X == 0);
			Assert.IsTrue(o.Y == 0);
			Assert.IsTrue(o.Z == -10);
		}
	}

	#endif
}
