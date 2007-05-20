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

namespace KFI_RPG_Creator.Logic {
	/// <summary>
	/// Represents a three dimensional Point (i.e. with x, y and z coordinates).
	/// </summary>
	public struct ThreeDeePoint: IEquatable<ThreeDeePoint> {
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
		
		public ThreeDeePoint Translate(int dx, int dy, int dz) {
			return new ThreeDeePoint(x+dx, y+dy, z+dz);
		}
		
		public void Offset(int dx, int dy, int dz) {
			this.x += dx;
			this.y += dy;
			this.z += dz;
		}
		
		public static explicit operator System.Drawing.Point (ThreeDeePoint p) {
			return new System.Drawing.Point(p.x, p.y);
		}
		
		public override string ToString() {
			return string.Format("ThreeDeePoint({0}; {1}; {2})", x, y, z);
		}
		
		public ThreeDeePoint(int x, int y, int z) {
			this.x = x;
			this.y = y;
			this.z = z;
		}
		
		public ThreeDeePoint(System.Drawing.Point p, int z) : this(p.X, p.Y, z) {}
		
		#region Equals and GetHashCode implementation
		// The code in this region is useful if you want to use this structure in collections.
		// If you don't need it, you can just remove the region and the ": IEquatable<ThreeDeePoint>" declaration.
		
		public override bool Equals(object obj) {
			if (obj is ThreeDeePoint)
				return Equals((ThreeDeePoint)obj); // use Equals method below
			else
				return false;
		}
		
		public bool Equals(ThreeDeePoint other) {
			// add comparisions for all members here
			return
				this.x == other.y &&
				this.y == other.y &&
				this.z == other.z;
		}
		
		public override int GetHashCode() {
			// combine the hash codes of all members here (e.g. with XOR operator ^)
			return x.GetHashCode() ^ y.GetHashCode() ^ z.GetHashCode();
		}
		
		public static bool operator ==(ThreeDeePoint lhs, ThreeDeePoint rhs) {
			return lhs.Equals(rhs);
		}
		
		public static bool operator !=(ThreeDeePoint lhs, ThreeDeePoint rhs) {
			return !(lhs.Equals(rhs)); // use operator == and negate result
		}
		#endregion
	}
}
