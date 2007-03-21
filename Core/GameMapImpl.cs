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
using Core.GameObjects;

namespace Core {
	/// <summary>
	/// A térkép, amin a mezõk találhatók
	/// </summary>
	class GameMapImpl: GameMap {
		GameObject[,] tiles;

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

		/// <summary>
		/// A megadott koordinátájú mezõt adja vissza
		/// </summary>
		/// <param name="x">A mezõ x koordinátája</param>
		/// <param name="y">A mezõ y koordinátája</param>
		/// <returns>A mezõ</returns>
		public GameObject GetTile(int x, int y) {
			return tiles[x, y];
		}

		/// <summary>
		/// Új térképet készít
		/// </summary>
		/// <param name="tiles">A térkép mezõit tartalmazó mátrix</param>
		internal GameMapImpl(GameObject[,] tiles) {
			width = tiles.GetLength(0);
			height = tiles.GetLength(1);
			this.tiles = tiles;
		}
		
		
		Dictionary<Point, List<GameObject>> objects = new Dictionary<Point, List<GameObject>>();
		
		internal void AddObject(int x, int y, GameObject o) {
			Point p = new Point(x, y);
			if (!(objects.ContainsKey(p))) {
				objects.Add(p, new List<GameObject>());
			}
			objects[p].Add(o);
		}
		
		internal GameObject[] GetObjects(int x, int y) {
			Point p = new Point(x, y);
			if (objects.ContainsKey(p)) {
				return objects[new Point(x, y)].ToArray();
			}
			else {
				return null;
			}
		}

	}

	#if DEBUG

	[TestFixture]
	public class GameMap_Test {
		ObjectLoader loader;

		[SetUp]
		public void SetUp() {
			loader = new TestLoader();
		}

		GameObject[,] CreateTileArray(int width, int height, string id) {
			GameObject[,] tiles = new GameObjectImpl[width, height];
			for (int i = 0; i < width; ++i) {
				for (int j = 0; j < height; ++j) {
					tiles[i, j] = new GameObjectImpl(id, loader);
				}
			}
			return tiles;
		}

		[Test]
		public void Create1x1Map() {
			GameObject[,] tiles = CreateTileArray(1, 1, "walkable");
			GameMapImpl m = new GameMapImpl(tiles);
			Assert.AreEqual(m.Width, 1);
			Assert.AreEqual(m.Height, 1);
		}

		[Test]
		public void Create3x5Map() {
			GameObject[,] tiles = CreateTileArray(3, 5, "walkable");
			GameMapImpl m = new GameMapImpl(tiles);
			Assert.AreEqual(3, m.Width);
			Assert.AreEqual(5, m.Height);
		}

		[Test]
		public void GetTile() {
			GameObject[,] tiles = CreateTileArray(1, 1, "walkable");
			GameMapImpl m = new GameMapImpl(tiles);
			Assert.IsTrue(m.GetTile(0, 0).Id == "walkable");
		}

		[Test]
		public void GetObjectAtSpecificPosition() {
			GameObject[,] tiles = CreateTileArray(2, 2, "walkable");
			GameMapImpl m = new GameMapImpl(tiles);
			m.AddObject(1, 1, new GameObjectImpl("walkable", loader));
			Assert.IsTrue(m.GetObjects(1, 1)[0].Id == "walkable");
		}
		
		[Test]
		public void GetNonexistantObject() {
			GameObject[,] tiles = CreateTileArray(2, 2, "walkable");
			GameMapImpl m = new GameMapImpl(tiles);
			Assert.IsNull(m.GetObjects(0,1));
		}
	}
	
	#endif
}
