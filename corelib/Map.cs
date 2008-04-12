using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.corelib {
	public class Map {
		Script onEnter;
		Script onLeave;
		Tile[,] tiles;
		List<Object>[,] contents;
		int rows;
		int cols;
		int tileSize;

		public Map(string mapName, Game game) {
			onEnter = game.vm.LoadScript(game.loader.LoadText(string.Concat("maps/", mapName ,"/onenter")));
			onLeave = game.vm.LoadScript(game.loader.LoadText(string.Concat("maps/", mapName, "/onleave")));
			tileSize = game.TileSize;
			string[] tileLines = game.loader.LoadText(string.Concat("maps/",mapName,"/tiles")).Split('\n');
			string[] line = tileLines[0].Split(' ');
			cols = int.Parse(line[0]);
			rows = int.Parse(line[1]);
			tiles = new Tile[cols, rows];
			contents = new List<Object>[cols, rows];
			for (int j = 0; j < rows; ++j) {
				line = tileLines[j+1].Split(' ');
				for (int i = 0; i < cols; ++i) {
					tiles[i, j] = new Tile(int.Parse(line[i]), game);
					contents[i, j] = new List<Object>();
				}
			}
		}

		internal void Place(Object sprite, int x, int y) {
			contents[x, y].Add(sprite);
		}
		internal void Remove(Sprite sprite, int x, int y) {
			contents[x, y].Remove(sprite);
		}

		internal void Draw(int x, int y, SdlDotNet.Graphics.Surface surface) {
			for (int i = 0; i < cols; ++i) {
				for (int j = 0; j < rows; ++j) {
					tiles[i, j].Draw((i - x) * tileSize, (j - y) * tileSize, surface);
				}
			}
			for (int i = 0; i < cols; ++i) {
				for (int j = 0; j < rows; ++j) {
					contents[i, j].ForEach((a) => a.Draw((i - x) * tileSize, (j - y) * tileSize, surface));
				}
			}
		}
	}
}
