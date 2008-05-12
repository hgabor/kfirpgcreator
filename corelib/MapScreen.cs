using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.corelib {
	/// <summary>
	/// The screen where the terrain and objects are drawn.
	/// </summary>
	class MapScreen: Screen {
		Game game;
		//CustomScreen stats;

		public MapScreen(Game game) {
			this.game = game;
			//map.Place(game.Party.Leader, startX, startY, startL);
			//PlaceMap(map, startX, startY, startL);
			//map.Place(player, startX, startY);
		}

		public override void Draw(SdlDotNet.Graphics.Surface surface) {
			//Draw map
			Sprite leader = game.Party.Leader;
			surface.Fill(System.Drawing.Color.Black);
			int offsetX = -(game.Width - leader.Width) / 2 + leader.X * game.TileSize + leader.CorrX;
			int offsetY = -(game.Height - leader.Height) / 2 + leader.Y * game.TileSize + leader.CorrY;
			game.currentMap.Draw(offsetX, offsetY, surface);
			//Draw stats/info
		}

		public override void Think() {
			game.currentMap.ThinkAll();
		}
	}
}
