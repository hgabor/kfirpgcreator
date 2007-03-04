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
using NUnit.Framework;
using KFI_Game_Core.GameObjects;

namespace KFI_Game_Core {
    /// <summary>
    /// A térkép, amin a mezõk találhatók
    /// </summary>
    class GameMapImpl: GameMap {
        GameTile[,] tiles;

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
        public GameTile GetTile(int x, int y) {
            return tiles[x, y];
        }

        /// <summary>
        /// Új térképet készít
        /// </summary>
        /// <param name="tiles">A térkép mezõit tartalmazó mátrix</param>
        internal GameMapImpl(GameTile[,] tiles) {
            width = tiles.GetLength(0);
            height = tiles.GetLength(1);
            this.tiles = tiles;
        }
    }

    [TestFixture]
    public class GameMap_Test {
        ObjectLoader loader;

        [SetUp]
        public void SetUp() {
            loader = new TestLoader();
        }

        GameTile[,] CreateTileArray(int width, int height, string id) {
            GameTile[,] tiles = new GameTileImpl[width, height];
            for (int i = 0; i < width; ++i) {
                for (int j = 0; j < height; ++j) {
                    tiles[i, j] = new GameTileImpl(id, loader);
                }
            }
            return tiles;
        }

        [Test]
        public void Create1x1Map() {
            GameTile[,] tiles = CreateTileArray(1, 1, "járható");
            GameMapImpl m = new GameMapImpl(tiles);
            Assert.AreEqual(m.Width, 1);
            Assert.AreEqual(m.Height, 1);
        }

        [Test]
        public void Create3x5Map() {
            GameTile[,] tiles = CreateTileArray(3, 5, "járható");
            GameMapImpl m = new GameMapImpl(tiles);
            Assert.AreEqual(3, m.Width);
            Assert.AreEqual(5, m.Height);
        }

        [Test]
        public void GetTile() {
            GameTile[,] tiles = CreateTileArray(1,1,"járható");
            GameMapImpl m = new GameMapImpl(tiles);
            Assert.IsTrue(m.GetTile(0, 0).Walkable);
        }
    }
}
