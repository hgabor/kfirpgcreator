using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.corelib {
	/// <summary>
	/// Represents a location on the map.
	/// Common use is for named locations.
	/// </summary>
	class Location {
		int x;
		/// <summary>
		/// Gets the X coordinate of the Location.
		/// </summary>
		public int X { get { return x; } }
		int y;
		/// <summary>
		/// Gets the Y coordinate of the Location.
		/// </summary>
		public int Y { get { return y; } }
		int layer;
		/// <summary>
		/// Gets the Layer coordinate of the Location.
		/// </summary>
		public int Layer { get { return layer; } }
		string mapName;
		/// <summary>
		/// Gets the name of the map where the Location is.
		/// </summary>
		public string MapName { get { return mapName; } }
		public Location(int x, int y, int layer, string mapName) {
			this.x = x;
			this.y = y;
			this.layer = layer;
			this.mapName = mapName;
		}
	}
}
