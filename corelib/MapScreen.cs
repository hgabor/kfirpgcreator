using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.corelib {
	class MapScreen: Screen {
		Map map;
		Game game;

		public MapScreen(Map map, int startX, int startY, int startL, Game game) {
			this.map = map;
			this.game = game;
			map.Place(game.Party.Leader, startX, startY, startL);
			//map.Place(player, startX, startY);
		}

		public override void Draw(SdlDotNet.Graphics.Surface surface) {
			//Draw map
			//TODO: center on player
			Sprite leader = game.Party.Leader;
			surface.Fill(System.Drawing.Color.Black);
			int offsetX = -(game.Width - leader.Width) / 2 + leader.X * game.TileSize + leader.CorrX;
			int offsetY = -(game.Height - leader.Height) / 2 + leader.Y * game.TileSize + leader.CorrY;
			map.Draw(offsetX, offsetY, surface);
			//Draw stats/info
		}

		public override void Think() {
			map.ThinkAll();
		}
	}
}
