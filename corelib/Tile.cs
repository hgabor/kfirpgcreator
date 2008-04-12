using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.corelib {
	class Tile: Object {
		//TODO: Outfactor Tile from project
		Graphic graphic;

		public Tile(int id, Game game) {
			graphic = new Graphic("img/tiles.png", id, game.TileSize, game);
		}

		public override void Draw(int x, int y, SdlDotNet.Graphics.Surface surface) {
			graphic.Blit(x, y, surface);
		}
	}
}
