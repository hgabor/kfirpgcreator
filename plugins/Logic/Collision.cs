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

/*
 * Created by SharpDevelop.
 * User: KILLER
 * Date: 2007.05.20.
 * Time: 12:05
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Drawing;
using System.Collections.Generic;
using NUnit.Framework;

namespace KFI_RPG_Creator.Logic {
	/// <summary>
	/// Description of Collision.
	/// </summary>
	abstract class Collision {
		private class BoxCollision: Collision {
			ThreeDeePoint center;
			int size;
			int height;
			public BoxCollision(ThreeDeePoint center, int size, int height) {
				this.center = center;
				this.size = size;
				this.height = height;
				SetUpKeyPoints();
			}
			
			void SetUpKeyPoints() {
				KeyPoints.Clear();
				KeyPoints.Add(new ThreeDeePoint(center.X-size, center.Y-size, center.Z));
				KeyPoints.Add(new ThreeDeePoint(center.X+size, center.Y+size, center.Z));
				KeyPoints.Add(new ThreeDeePoint(center.X-size, center.Y+size, center.Z));
				KeyPoints.Add(new ThreeDeePoint(center.X+size, center.Y-size, center.Z));
			}
			
			public override ThreeDeePoint Center {
				get { return center; }
				set {
					center = value;
					SetUpKeyPoints();
				}
			}
			
			protected override bool Contains(ThreeDeePoint p) {
				return
					p.Z >= center.Z &&
					p.Z <= center.Z+height &&
					p.X <= center.X+size &&
					p.X >= center.X-size &&
					p.Y <= center.Y+size &&
					p.Y >= center.Y-size;
			}
		}
		
		private class CylinderCollision: Collision {
			ThreeDeePoint center;
			int size;
			int height;
			
			public CylinderCollision(ThreeDeePoint center, int size, int height) {
				this.center = center;
				this.size = size;
				this.height = height;
				SetUpKeyPoints();
			}
			
			void SetUpKeyPoints() {
				KeyPoints.Clear();
				KeyPoints.Add(new ThreeDeePoint(center.X, center.Y-size, center.Z));
				KeyPoints.Add(new ThreeDeePoint(center.X, center.Y+size, center.Z));
				KeyPoints.Add(new ThreeDeePoint(center.X-size, center.Y, center.Z));
				KeyPoints.Add(new ThreeDeePoint(center.X+size, center.Y, center.Z));
			}
			
			public override ThreeDeePoint Center {
				get { return center; }
				set {
					center = value;
					SetUpKeyPoints();
				}
			}
			
			protected override bool Contains(ThreeDeePoint p) {
				return
					p.Z >= center.Z &&
					p.Z <= center.Z + height &&
					((p.X-center.X)*(p.X-center.X) + (p.Y-center.Y)*(p.Y-center.Y)) <= size*size;
			}
		}
		
		public static Collision Create(string type, ThreeDeePoint center, int size, int height) {
			switch(type) {
				case "box":
					return new BoxCollision(center, size, height);
				case "cylinder":
					return new CylinderCollision(center, size, height);
				default:
					throw new ArgumentOutOfRangeException("Collision.Create: type must be one of \"box\", \"cylinder\".");
			}
		}
		
		public abstract ThreeDeePoint Center {
			get;
			set;
		}
		
		private List<ThreeDeePoint> keyPoints = new List<ThreeDeePoint>();
		
		protected List<ThreeDeePoint> KeyPoints {
			get { return keyPoints; }
		}
		
		protected abstract bool Contains(ThreeDeePoint p);
		
		public bool CollidesWith(Collision that) {
			return
				(! this.KeyPoints.TrueForAll(
					delegate(ThreeDeePoint p) {
						return !that.Contains(p);
					}
				))
				||
				(! that.KeyPoints.TrueForAll(
					delegate(ThreeDeePoint p) {
						return !this.Contains(p);
					}
				));
		}
		
		public void Offset(int dx, int dy, int dz) {
			Center = Center.Translate(dx, dy, dz);
		}

		public override string ToString() {
			System.Text.StringBuilder b = new System.Text.StringBuilder();
			for (int i = 0; i < keyPoints.Count; ++i) {
				b.Append(string.Format("{0}, {1}, {2}\n", keyPoints[i].X, keyPoints[i].Y, keyPoints[i].Z));
			}
			return b.ToString();
		}
	}
	
	
	//NB: These test did not guide me...
	
	#if DEBUG
	[TestFixture]
	public class CollisionTest {
		const int size = 50;
		const int height = 70;
		
		[Test]
		public void DistantObjectsDoNotCollide() {
			Collision one = Collision.Create("box", new ThreeDeePoint(0, 0, 0), size, height);
			Collision two = Collision.Create("box", new ThreeDeePoint(200, 200, 0), size, height);
			Assert.IsFalse(one.CollidesWith(two));
			Assert.IsFalse(two.CollidesWith(one));
		}
		
		[Test]
		public void BoxesCollideOnCorners() {
			Collision one = Collision.Create("box", new ThreeDeePoint(0, 0, 0), size, height);
			Collision two = Collision.Create("box", new ThreeDeePoint(100, 100, 0), size, height);
			Assert.IsTrue(one.CollidesWith(two));
			Assert.IsTrue(two.CollidesWith(one));
		}
		
		[Test]
		public void CylindersDontCollideOnCorners() {
			Collision one = Collision.Create("cylinder", new ThreeDeePoint(0, 0, 0), size, height);
			Collision two = Collision.Create("cylinder", new ThreeDeePoint(100, 100, 0), size, height);
			Assert.IsFalse(one.CollidesWith(two));
			Assert.IsFalse(two.CollidesWith(one));
		}
		
		[Test]
		public void CylinderCollidesWithBox() {
			Collision box = Collision.Create("box", new ThreeDeePoint(0, 0, 0), size, height);
			Collision sphere = Collision.Create("cylinder", new ThreeDeePoint(90, 0, 0), size, height);
			Assert.IsTrue(box.CollidesWith(sphere));
			Assert.IsTrue(sphere.CollidesWith(box));
		}
		
		[Test]
		public void ObjectsAboveEachOtherDoNotCollide() {
			Collision o1 = Collision.Create("box", new ThreeDeePoint(0, 0, 0), size, height);
			Collision o2 = Collision.Create("box", new ThreeDeePoint(0, 0, 100), size, height);
			Assert.IsFalse(o1.CollidesWith(o2));
			Assert.IsFalse(o2.CollidesWith(o1));
		}

		[Test]
		public void ObjectsOnTopOfEachOtherDoNotCollide() {
			Collision o1 = Collision.Create("box", new ThreeDeePoint(0, 0, 0), size, height);
			Collision o2 = Collision.Create("box", new ThreeDeePoint(0, 0, 71), size, height);
			Assert.IsFalse(o1.CollidesWith(o2));
			Assert.IsFalse(o2.CollidesWith(o1));
		}
		
		[Test]
		public void ObjectsWithDifferentZCollide() {
			Collision o1 = Collision.Create("box", new ThreeDeePoint(0, 0, 0), size, height);
			Collision o2 = Collision.Create("box", new ThreeDeePoint(0, 0, 70), size, height);
			Assert.IsTrue(o1.CollidesWith(o2));
			Assert.IsTrue(o2.CollidesWith(o1));
		}
		
		[Test]
		public void EqualSizedObjectsWithTwoCommonPointsCollide() {
			Collision o1 = Collision.Create("box", new ThreeDeePoint(0, 0, 0), size, height);
			Collision o2 = Collision.Create("box", new ThreeDeePoint(50, 0, 0), size, height);
			Assert.IsTrue(o1.CollidesWith(o2));
			Assert.IsTrue(o2.CollidesWith(o1));
		}
		
		[Test]
		public void TranslateCollision() {
			Collision o1 = Collision.Create("box", new ThreeDeePoint(0, 0, 0), size, height);
			Collision o2 = Collision.Create("box", new ThreeDeePoint(200, 200, 0), size, height);
			Assert.IsFalse(o1.CollidesWith(o2));
			Assert.IsFalse(o2.CollidesWith(o1));
			o2.Offset(-100, -100, 0);
			Assert.IsTrue(o1.CollidesWith(o2));
			Assert.IsTrue(o2.CollidesWith(o1));
		}
		
		[Test]
		public void TranslateAlongZAxis() {
			Collision o1 = Collision.Create("box", new ThreeDeePoint(0, 0, 0), size, height);
			Collision o2 = Collision.Create("box", new ThreeDeePoint(0, 0, 170), size, height);
			Assert.IsFalse(o1.CollidesWith(o2));
			Assert.IsFalse(o2.CollidesWith(o1));
			o2.Offset(0, 0, -100);
			Assert.IsTrue(o1.CollidesWith(o2));
			Assert.IsTrue(o2.CollidesWith(o1));
		}
		
		[Test]
		public void SetCenter() {
			Collision o1 = Collision.Create("box", new ThreeDeePoint(0, 0, 0), size, height);
			Collision o2 = Collision.Create("box", new ThreeDeePoint(200, 200, 0), size, height);
			o2.Center = new ThreeDeePoint(100, 100, 0);
			Assert.IsTrue(o1.CollidesWith(o2));
			Assert.IsTrue(o2.CollidesWith(o1));
		}
		
		
	}
	#endif

}
