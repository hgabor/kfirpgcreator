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
		
		ThreeDeePoint prevCenter;
		Direction prevDirection;
		
		public int X {
			get { return collision.Center.X; }
		}
		public int Y {
			get { return collision.Center.Y; }
		}
		public int Z {
			get { return collision.Center.Z; }
		}
		
		bool ethereal;
		Collision collision;

		int size;
		int height;
		
		bool affectedByGravity;
		
		public bool AffectedByGravity {
			get { return affectedByGravity; }
		}
		
		internal GameObject(string id, int x, int y, int z, ObjectLoader loader) {
			this.id = id;
			ThreeDeePoint center = new ThreeDeePoint(x, y, z);
			this.prevCenter = center;
			this.ethereal = (loader.GetAttribute(id, "ethereal") == "true");
			int collideSize = Convert.ToInt32(loader.GetAttribute(id, "collision.size"));
			size = collideSize;
			string type = loader.GetAttribute(id, "collision.type");
			height = Convert.ToInt32(loader.GetAttribute(id, "collision.height"));
			this.collision = Collision.Create(type, center, collideSize, height);
			this.affectedByGravity = (loader.GetAttribute(id, "affectedbygravity") == "true");
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
		
		int movementSpeed;
		public int MovementSpeed {
			//get { return movementSpeed; }
			set { movementSpeed = value; }
		}
		
		int fallingSpeed;
		public int FallingSpeed {
			//get { return fallingSpeed; }
			set { fallingSpeed = value; }
		}
		
		public void Move() {
			Move(facing, movementSpeed);
		}
		public void Move(Direction d) {
			Move(d, movementSpeed);
		}
		private void Move(Direction d, int speed) {
			prevCenter = collision.Center;
			prevDirection = d;
			if (movementSpeed != 0) {
				Point p = d.CalculateNewCoords((Point)collision.Center, speed);
				collision.Center = new ThreeDeePoint(p, collision.Center.Z);
			}
		}
		
		public void Fall() {
			Fall(fallingSpeed);
		}
		public void Fall(int speed) {
			prevCenter = collision.Center;
			if (fallingSpeed != 0) {
				Collision c = Collision.Create("box", collision.Center, this.size, this.height);
				Collision d = Collision.Create("box", collision.Center.Translate(0, 0, -speed), this.size, this.height);
				collision.Offset(0, 0, -speed);
			}
		}
		
		public override string ToString() {
			return string.Format(
				"({0};{1};{2}) mspeed={3} facing={4} size={5} height={6}",
				collision.Center.X, collision.Center.Y, collision.Center.Z, movementSpeed, facing, size, height
			);
		}
		
		
		public void AvoidMovementCollision(GameObject other) {
			if (movementSpeed == 0) return; //throw new GameException("Called AvoidMovementCollision on a still object.");
			int speed = movementSpeed;
			do {
				--speed;
				collision.Center = prevCenter;
				Move(prevDirection, speed);
			} while(speed > 0 && this.CollidesWith(other));
		}
		
		public void AvoidFallingCollision(GameObject other) {
			if (fallingSpeed == 0) return; //throw new GameException("Called AvoidFallingCollision on a still object.");
			int speed = fallingSpeed;
			do {
				--speed;
				collision.Center = prevCenter;
				Fall(speed);
			} while(speed > 0 && this.CollidesWith(other));
		}
	
		public bool CollidesWith(GameObject that) {
			if (this.ethereal || that.ethereal) return false;
			else return this.collision.CollidesWith(that.collision);
		}
	}

	#if DEBUG

	[TestFixture]
	public class GameObjectTest {
		ObjectLoader loader;
		const int size = 50;
		const int height = 70;

		[SetUp]
		public void SetUp() {
			loader = new TestLoader();
		}

		#region Creation
		
		//Sanity Checks
		
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
		
		#endregion
		#region Movement
		
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
		
		#endregion
		#region Collision
		
		[Test]
		public void EtherealObjectDoesNotCollide() {
			GameObject one = new GameObject("ethereal", 0, 0, 0, loader);
			GameObject two = new GameObject("something", 0, 0, 0, loader);
			Assert.IsFalse(one.CollidesWith(two));
			Assert.IsFalse(two.CollidesWith(one));
		}
		
		[Test]
		public void NonEtherealObjectCollides() {
			GameObject one = new GameObject("something", 0, 0, 0, loader);
			GameObject two = new GameObject("something", 0, 0, 0, loader);
			Assert.IsTrue(one.CollidesWith(two));
			Assert.IsTrue(two.CollidesWith(one));
		}
		
		[Test]
		public void BackTrackToAvoidCollisionDuringMovement() {
			GameObject moving = new GameObject("something", 0, 0, 0, loader);
			GameObject standing = new GameObject("something", size*2 + 10, 0, 0, loader);
			moving.MovementSpeed = 30;
			Assert.IsFalse(moving.CollidesWith(standing));
			moving.Move(Direction.East);
			Assert.IsTrue(moving.CollidesWith(standing));
			moving.AvoidMovementCollision(standing);
			Assert.IsTrue(moving.X == 9);
			Assert.IsTrue(moving.Y == 0);
			Assert.IsTrue(moving.Z == 0);
		}
		
		[Test]
		public void BackTrackMovementDiagonally() {
			GameObject moving = new GameObject("something", 0, 0, 0, loader);
			GameObject standing = new GameObject("something", size*2 + 10, size*2 + 10, 0, loader);
			moving.MovementSpeed = 30;
			moving.Move(Direction.SouthEast);
			moving.AvoidMovementCollision(standing);
			Assert.IsTrue(moving.X == 9);
			Assert.IsTrue(moving.Y == 9);
			Assert.IsTrue(moving.Z == 0);
		}
		
		[Test]
		public void BackTrackToAvoidCollisionDuringFall() {
			GameObject falling = new GameObject("something", 0, 0, height + 10, loader);
			GameObject standing = new GameObject("something", 0, 0, 0, loader);
			falling.FallingSpeed = 30;
			Assert.IsFalse(falling.CollidesWith(standing));
			falling.Fall();
			Assert.IsTrue(falling.CollidesWith(standing));
			falling.AvoidFallingCollision(standing);
			Assert.IsTrue(falling.X == 0);
			Assert.IsTrue(falling.Y == 0);
			Console.WriteLine(falling.Z);
			Assert.IsTrue(falling.Z == height+1);
		}
		
		#endregion
	}

	#endif
}
