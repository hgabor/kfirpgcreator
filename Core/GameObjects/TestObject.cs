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

namespace Core.GameObjects {
    class TestObject : GameObject {
		string id;
		public string Id {
			get {
				return "testobject";
			}
		}
		
        bool w;
        public bool Walkable {
            get { return w; }
        }
        bool s;
        public bool Swimmable {
            get { return s; }
        }
        public TestObject() {
        }
        public TestObject(string id, ObjectLoader m) {
        	this.id = id;
            w = m.GetAttribute(id, "walkable") == "1";
            s = m.GetAttribute(id, "swimmable") == "1";
        }
    }
}
