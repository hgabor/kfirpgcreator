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
using System.Collections.Generic;
using NUnit.Framework;


namespace Core.GameObjects {
    /// <summary>
    /// A játékmezõ implementációja
    /// </summary>
    class GameTileImpl : GameObjectImpl, GameTile {
        internal class DoesNotContainObjectException : GameException {
            public DoesNotContainObjectException(GameObject o)
                : base(String.Format("A mezõ nem tartalmazza a kért objektumot: {0}", o)) {}
        }

        List<GameObject> childObjects = new List<GameObject>();
        public void AddObject(GameObject o) {
            childObjects.Add(o);
        }
        public void RemoveObject(GameObject o) {
            if (childObjects.Contains(o)) {
                childObjects.Remove(o);
            }
            else {
                throw new DoesNotContainObjectException(o);
            }
        }

        public override bool Walkable {
            get {
                return base.Walkable && childObjects.TrueForAll(delegate(GameObject g) { return g.Walkable; });
            }
        }

        public override bool Swimmable {
            get {
                return base.Swimmable && childObjects.TrueForAll(delegate(GameObject g) { return g.Swimmable; });
            }
        }

        public GameTileImpl(string id, ObjectLoader manager) : base(id, manager) { }
    }

    #if DEBUG
    
    [TestFixture]
    public class GameTile_Test {
        private ObjectLoader manager;

        [SetUp]
        public void SetUp() {
            manager = new TestLoader();
        }

        //Normál adatokkal ne legyen hiba a konstruktálás közben
        [Test]
        public void BasicCreation() {
            GameTileImpl t = new GameTileImpl("valami", manager);
        }

        [Test]
        [ExpectedException(typeof(ObjectNotFoundException))]
        public void NonExistantCreation() {
            GameTileImpl t = new GameTileImpl("nonexistant", manager);
        }

        [Test]
        public void AddAndRemoveObject() {
            GameTileImpl t = new GameTileImpl("valami", manager);
            GameObject o = new TestObject();
            t.AddObject(o);
            t.RemoveObject(o);
        }

        [Test]
        [ExpectedException(typeof(GameTileImpl.DoesNotContainObjectException))]
        public void AddAndRemoveTwice() {
            GameTileImpl t = new GameTileImpl("valami", manager);
            GameObject o = new TestObject();
            t.AddObject(o);
            t.RemoveObject(o);
            t.RemoveObject(o);
        }

        [Test]
        [ExpectedException(typeof(GameTileImpl.DoesNotContainObjectException))]
        public void RemoveNonExistant() {
            GameTileImpl t = new GameTileImpl("valami", manager);
            GameObject o = new TestObject();
            t.RemoveObject(o);
        }

        [Test]
        public void Walkability_TileWalkable_ObjectToo() {
            GameTileImpl t = new GameTileImpl("járható", manager);
            GameObject o = new TestObject("járható", manager);
            t.AddObject(o);
            Assert.IsTrue(t.Walkable);
        }
        [Test]
        public void Walkablility_TileWalkable_ObjectNot() {
            GameTileImpl t = new GameTileImpl("járható", manager);
            GameObject o = new TestObject("valami", manager);
            t.AddObject(o);
            Assert.IsFalse(t.Walkable);
        }
        [Test]
        public void Walkability_TileWalkable() {
            GameTileImpl t = new GameTileImpl("járható", manager);
            Assert.IsTrue(t.Walkable);
        }
        [Test]
        public void Walkability_TileNotWalkable() {
            GameTileImpl t = new GameTileImpl("valami", manager);
            Assert.IsFalse(t.Walkable);
        }

        [Test]
        public void Swimmability_TileSwimmable_ObjectToo() {
            GameTileImpl t = new GameTileImpl("úszható", manager);
            GameObject o = new TestObject("úszható", manager);
            t.AddObject(o);
            Assert.IsTrue(t.Swimmable);
        }
        [Test]
        public void Swimmability_TileSwimmable_ObjectNot() {
            GameTileImpl t = new GameTileImpl("úszható", manager);
            GameObject o = new TestObject("valami", manager);
            t.AddObject(o);
            Assert.IsFalse(t.Swimmable);
        }
        [Test]
        public void Swimmability_TileSwimmable() {
            GameTileImpl t = new GameTileImpl("úszható", manager);
            Assert.IsTrue(t.Swimmable);
        }
        [Test]
        public void Swimmability_TileNotSwimmable() {
            GameTileImpl t = new GameTileImpl("valami", manager);
            Assert.IsFalse(t.Swimmable);
        }
    }
    
    #endif
}
