using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.corelib {
	class MapScreen: Screen {
		Map map;

		public MapScreen(Map map, int startX, int startY, Game game) {
			this.map = map;
			map.Place(game.Party, startX, startY);
			//map.Place(player, startX, startY);
		}

		public override void Draw(SdlDotNet.Graphics.Surface surface) {
			//Draw map
			//TODO: center on player
			map.Draw(0, 0, surface);
			//Draw stats/info
		}
	}
}
