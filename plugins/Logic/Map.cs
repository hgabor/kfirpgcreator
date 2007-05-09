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
using System.Collections.Generic;
using NUnit.Framework;
using KFI_RPG_Creator.Core;

namespace KFI_RPG_Creator.Logic {
	/// <summary>
	/// A térkép, amin a mezõk találhatók
	/// </summary>
	class Map {
		int width;
		/// <summary>
		/// A térkép szélessége
		/// </summary>
		public int Width {
		get { return width; }
		}
		int height;
		/// <summary>
		/// A térkép magassága
		/// </summary>
		public int Height {
			get { return height; }
		}

		internal Map(int width, int height) {
			this.width = width;
			this.height = height;
		}
		
		Dictionary<Point, List<GameObject>> objectsByCoords = new Dictionary<Point, List<GameObject>>();
		List<GameObject> objects = new List<GameObject>();
		
		public void AddObject(GameObject o) {
			if (o.X < 0 || o.X >= this.width || o.Y < 0 || o.Y >= this.Height) {
				throw new ArgumentOutOfRangeException("Tried to place object outside the map");
			}
			Point p = new Point(o.X, o.Y);
			if (!(objectsByCoords.ContainsKey(p))) {
				objectsByCoords.Add(p, new List<GameObject>());
			}
			objectsByCoords[p].Add(o);
			objects.Add(o);
		}
		
		internal GameObject[] GetObjects(int x, int y) {
			Point p = new Point(x, y);
			if (objectsByCoords.ContainsKey(p)) {
				return objectsByCoords[new Point(x, y)].ToArray();
			}
			else {
				return null;
			}
		}
		
		public GameObject[] GetAllObjects() {
			return objects.ToArray();
		}
		
		internal void RemoveObjects(int x, int y) {
			Point p = new Point(x, y);
			if (objectsByCoords.ContainsKey(p)) {
				foreach(GameObject o in objectsByCoords[p]) {
					objects.Remove(o);
				}
				objectsByCoords.Remove(p);
			}
		}
		
		internal void RemoveObject(int x, int y, string id) {
			Point p = new Point(x, y);
			if (objectsByCoords.ContainsKey(p)) {
				GameObject toRemove = objectsByCoords[p].Find(
					delegate(GameObject o) { return o.TypeID == id; }
				);
				objectsByCoords[p].Remove(toRemove);
				objects.Remove(toRemove);
			}
		}
	}

	#if DEBUG
	
	[TestFixture]
	public class GameMap_Test {
		KFI_RPG_Creator.Core.ObjectLoader loader;

		[SetUp]
		public void SetUp() {
			loader = new TestLoader();
		}

		Map CreateMap(int width, int height, string id) {
			Map map = new Map(width * 100, height * 100);
			
			for (int i = 0; i < width; ++i) {
				for (int j = 0; j < height; ++j) {
					map.AddObject(new GameObject(id, i*100, j*100, 0, loader));
				}
			}
			return map;
		}

		[Test]
		public void Create100x100Map() {
			Map m = CreateMap(1, 1, "walkable");
			Assert.AreEqual(m.Width, 100);
			Assert.AreEqual(m.Height, 100);
		}

		[Test]
		public void Create300x500Map() {
			Map m = CreateMap(3, 5, "walkable");
			Assert.AreEqual(300, m.Width);
			Assert.AreEqual(500, m.Height);
		}

		[Test]
		public void GetObjectAtSpecificPosition() {
			Map m = CreateMap(2, 2, "walkable");
			m.AddObject(new GameObject("walkable", 1, 1, 1, loader));
			Assert.IsTrue(m.GetObjects(1, 1)[0].TypeID == "walkable");
		}
		
		[Test]
		public void GetNonexistantObject() {
			Map m = CreateMap(2, 2, "walkable");
			Assert.IsNull(m.GetObjects(0,1));
		}
		
		[Test]
		public void GetListOfObjectsAndCheckIfTheyAreTheOnlyObjectsOnMap() {
			Map m = CreateMap(2, 2, "walkable");
			m.AddObject(new GameObject("obj(1,1)", 1, 1, 1, loader));
			m.AddObject(new GameObject("obj(2,1)", 2, 1, 1, loader));
			m.AddObject(new GameObject("obj(10,10)", 10, 10, 1, loader));
			GameObject[] list = m.GetAllObjects();
			bool firstFound, secondFound, thirdFound, otherFound;
			firstFound = secondFound = thirdFound = otherFound = false;
			foreach(GameObject o in list) {
				if (o.TypeID == "walkable") continue; // do not count tiles
				
				if (o.TypeID == "obj(1,1)") firstFound = true;
				else if (o.TypeID == "obj(2,1)") secondFound = true;
				else if (o.TypeID == "obj(10,10)") thirdFound = true;
				else otherFound = true;
			}
			Assert.IsTrue(firstFound && secondFound && thirdFound && !otherFound);
		}
		
		[Test]
		public void RemoveAllObjectsFromPosition() {
			Map m = CreateMap(2, 2, "walkable");
			m.AddObject(new GameObject("obj", 2, 2, 1, loader));
			m.RemoveObjects(2, 2);
			Assert.IsNull(m.GetObjects(2, 2));
		}
		
		[Test]
		public void RemoveSpecificObjectFromPosition() {
			Map m = CreateMap(2, 2, "walkable");
			m.AddObject(new GameObject("obj", 2, 2, 1, loader));
			m.AddObject(new GameObject("obj2", 2, 2, 1, loader));
			m.RemoveObject(2, 2, "obj");
			Assert.IsTrue(m.GetObjects(2, 2).Length == 1);
		}
		
		[Test]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void PlaceObjectOutOfBounds() {
			Map m = CreateMap(2, 2, "walkable");
			m.AddObject(new GameObject("obj", 300, 200, 1, loader));
		}
		
	}
	
	#endif
}
