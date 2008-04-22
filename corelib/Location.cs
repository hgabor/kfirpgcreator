using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.corelib {
	class Location {
		int x;
		public int X { get { return x; } }
		int y;
		public int Y { get { return y; } }
		int layer;
		public int Layer { get { return layer; } }
		string mapName;
		public string MapName { get { return mapName; } }
		public Location(int x, int y, int layer, string mapName) {
			this.x = x;
			this.y = y;
			this.layer = layer;
			this.mapName = mapName;
		}
	}
}
