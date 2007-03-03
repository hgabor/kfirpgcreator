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

using NUnit.Framework;

namespace KFI_Game_Core.GameObjects {
    /// <summary>
    /// Játékban használt objektum implementációja
    /// </summary>
    class GameObjectImpl : GameObject {

        bool walkable;
        public virtual bool Walkable {
            get { return walkable; }
        }

        bool swimmable;
        public virtual bool Swimmable {
            get { return swimmable; }
        }

        internal GameObjectImpl(string id, ObjectLoader manager) {
            walkable = (manager.GetAttribute(id, "walkable") == "1");
            swimmable = (manager.GetAttribute(id, "swimmable") == "1");
        }
    }

    [TestFixture]
    public class GameObject_Test {
        ObjectLoader manager;

        [SetUp]
        public void SetUp() {
            manager = new TestLoader();
        }

        //Normál elkészítés közben nincs-e hiba (kivétel)
        [Test]
        public void BasicCreation() {
            GameObjectImpl g = new GameObjectImpl("valami", manager);
        }

        [Test]
        [ExpectedException(typeof(ObjectNotFoundException))]
        public void CreateDoesNotExist() {
            GameObjectImpl g = new GameObjectImpl("nonexistant", manager);
        }

        [Test]
        public void Walkability() {
            GameObjectImpl g = new GameObjectImpl("járható", manager);
            Assert.IsTrue(g.Walkable);
        }

        [Test]
        public void NotWalkability() {
            GameObjectImpl g = new GameObjectImpl("úszható", manager);
            Assert.IsFalse(g.Walkable);
        }

        [Test]
        public void Swimmability() {
            GameObjectImpl g = new GameObjectImpl("úszható", manager);
            Assert.IsTrue(g.Swimmable);
        }

        [Test]
        public void NotSwimmability() {
            GameObjectImpl g = new GameObjectImpl("járható", manager);
            Assert.IsFalse(g.Swimmable);
        }
    }
}
