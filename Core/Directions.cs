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

using System.Drawing;

namespace KFI_RPG_Creator.Core {
	
	public abstract class Direction {
		protected const double SQRT2DIV2 = 0.7071;
		public abstract Point CalculateNewCoords(Point oldPoint, int distance);
		
		
		private class NorthD: Direction {
			public override Point CalculateNewCoords(Point oldPoint, int distance) {
				return new Point(oldPoint.X, oldPoint.Y - distance);
			}
			public override string ToString() {
				return "N";
			}
		}
		private class SouthD: Direction {
			public override Point CalculateNewCoords(Point oldPoint, int distance) {
				return new Point(oldPoint.X, oldPoint.Y + distance);
			}
			public override string ToString() {
				return "S";
			}
		}
		private class WestD: Direction {
			public override Point CalculateNewCoords(Point oldPoint, int distance) {
				return new Point(oldPoint.X - distance, oldPoint.Y);
			}
			public override string ToString() {
				return "W";
			}
		}
		private class EastD: Direction {
			public override Point CalculateNewCoords(Point oldPoint, int distance) {
				return new Point(oldPoint.X + distance, oldPoint.Y);
			}
			public override string ToString() {
				return "E";
			}
		}
		private class NorthWestD: Direction {
			public override Point CalculateNewCoords(Point oldPoint, int distance) {
				double d;
				d = distance * SQRT2DIV2;
				System.Console.WriteLine(d);
				d = oldPoint.X - d;
				System.Console.WriteLine(d);
				System.Console.WriteLine((int)System.Math.Round(d));
				return new Point((int)System.Math.Round(oldPoint.X - distance * SQRT2DIV2), (int)System.Math.Round(oldPoint.Y - distance * SQRT2DIV2));
			}
			public override string ToString() {
				return "NW";
			}
		}
		private class NorthEastD: Direction {
			public override Point CalculateNewCoords(Point oldPoint, int distance) {
				return new Point((int)System.Math.Round(oldPoint.X + distance * SQRT2DIV2), (int)System.Math.Round(oldPoint.Y - distance * SQRT2DIV2));
			}
			public override string ToString() {
				return "NE";
			}
		}
		private class SouthWestD: Direction {
			public override Point CalculateNewCoords(Point oldPoint, int distance) {
				return new Point((int)System.Math.Round(oldPoint.X - distance * SQRT2DIV2), (int)System.Math.Round(oldPoint.Y + distance * SQRT2DIV2));
			}
			public override string ToString() {
				return "SW";
			}
		}
		private class SouthEastD: Direction {
			public override Point CalculateNewCoords(Point oldPoint, int distance) {
				return new Point((int)System.Math.Round(oldPoint.X + distance * SQRT2DIV2), (int)System.Math.Round(oldPoint.Y + distance * SQRT2DIV2));
			}
			public override string ToString() {
				return "SE";
			}
		}

		
		public static readonly Direction North = new NorthD();
		public static readonly Direction South = new SouthD();
		public static readonly Direction West = new WestD();
		public static readonly Direction East = new EastD();
		public static readonly Direction NorthEast = new NorthEastD();
		public static readonly Direction NorthWest = new NorthWestD();
		public static readonly Direction SouthEast = new SouthEastD();
		public static readonly Direction SouthWest = new SouthWestD();
	}
}
