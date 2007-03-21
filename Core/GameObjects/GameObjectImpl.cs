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

namespace Core.GameObjects {
	/// <summary>
	/// Játékban használt objektum implementációja
	/// </summary>
	class GameObjectImpl : GameObject {
		string id;
		public string Id {
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

		internal GameObjectImpl(string id, ObjectLoader manager) {
			this.id = id;
			walkable = (manager.GetAttribute(id, "walkable") == "true");
			swimmable = (manager.GetAttribute(id, "swimmable") == "true");
		}
	}

	#if DEBUG

	[TestFixture]
	public class GameObject_Test {
		ObjectLoader manager;

		[SetUp]
		public void SetUp() {
			manager = new TestLoader();
		}

		//Normál elkészítés közben nincs-e hiba (kivétel)
		[Test]
		public void CreatingNormallySucceeds() {
			GameObjectImpl g = new GameObjectImpl("something", manager);
			Assert.IsTrue(g.Id == "something");
		}

		[Test]
		[ExpectedException(typeof(ObjectNotFoundException))]
		public void CreatingNonexistantThrows() {
			GameObjectImpl g = new GameObjectImpl("nonexistant", manager);
		}

		[Test]
		public void WalkableProperty() {
			GameObjectImpl walk = new GameObjectImpl("walkable", manager);
			GameObjectImpl nonwalk = new GameObjectImpl("swimmable", manager);
			Assert.IsTrue(walk.Walkable);
			Assert.IsFalse(nonwalk.Walkable);
		}

		[Test]
		public void SwimmableProperty() {
			GameObjectImpl swim = new GameObjectImpl("swimmable", manager);
			GameObjectImpl nonswim = new GameObjectImpl("walkable", manager);
			Assert.IsTrue(swim.Swimmable);
			Assert.IsFalse(nonswim.Swimmable);
		}
	}

	#endif
}
