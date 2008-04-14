using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace KFIRPG.corelib {
	public class Map {
		class Layer {
			public List<Sprite>[,] objects;
			public Graphics[,] tiles;
			public bool[,] passable;
			public Layer(int width, int height, string path, Game game) {
				string[] tileLines = game.loader.LoadText(string.Format(path, "tiles")).Split('\n');
				string[] passLines = game.loader.LoadText(string.Format(path, "passability")).Split('\n');
				//TODO: Sprites
				objects = new List<Sprite>[width, height];
				tiles = new Graphics[width, height];
				passable = new bool[width, height];

				for (int j = 0; j < height; ++j) {
					string[] tileLine = tileLines[j].Split(' ');
					string[] passLine = passLines[j].Split(' ');
					for (int i = 0; i < width; ++i) {
						objects[i, j] = new List<Sprite>();
						int tileID = int.Parse(tileLine[i]);
						if (tileID == 0) {
							tiles[i, j] = new NoGraphics();
						}
						else {
							Animation anim = new Animation("tiles", game.TileSize, game);
							anim.SetState("still", tileID - 1);
							tiles[i, j] = anim;
						}
						passable[i, j] = int.Parse(passLine[i]) == 1;
					}
				}
			}
		}

		List<Sprite> objects = new List<Sprite>();
		Script onEnter;
		Script onLeave;
		Layer[] layers;
		int rows;
		int cols;
		int tileSize;

		public Map(string mapName, Game game) {
			onEnter = game.vm.LoadScript(game.loader.LoadText(string.Concat("maps/", mapName ,"/onenter")));
			onLeave = game.vm.LoadScript(game.loader.LoadText(string.Concat("maps/", mapName, "/onleave")));
			XmlDocument info = new XmlDocument();
			info.LoadXml(game.loader.LoadText(string.Concat("maps/", mapName, "/info.xml")));
			cols = int.Parse(info.SelectSingleNode("/map/width").InnerText);
			rows = int.Parse(info.SelectSingleNode("/map/height").InnerText);
			int numLayers = int.Parse(info.SelectSingleNode("/map/layers").InnerText);
			tileSize = game.TileSize;

			layers = new Layer[numLayers];
			for (int i = 0; i < numLayers; ++i) {
				layers[i] = new Layer(cols, rows, "maps/" + mapName + "/layers/{0}." + i.ToString(), game);
			}
		}

		internal void Place(Sprite sprite, int x, int y, int layer) {
			layers[layer].objects[x, y].Add(sprite);
			objects.Add(sprite);
			sprite.UpdateCoords(x, y, layer);
		}
		internal void Remove(Sprite sprite, int x, int y, int layer) {
			layers[layer].objects[x, y].Remove(sprite);
			objects.Remove(sprite);
		}
		internal void Move(Sprite sprite, int fromX, int fromY, int fromLayer, int toX, int toY, int toLayer) {
			layers[fromLayer].objects[fromX, fromY].Remove(sprite);
			layers[toLayer].objects[toX, toY].Add(sprite);
			sprite.UpdateCoords(toX, toY, toLayer);
		}

		internal bool IsPassable(int x, int y, int layer) {
			//Check for collision
			return x >= 0 && y >= 0 && x < cols && y < rows && layers[layer].passable[x, y];
		}

		internal void Draw(int x, int y, SdlDotNet.Graphics.Surface surface) {
			for (int layer = 0; layer < layers.Length; ++layer) {
				for (int i = 0; i < cols; ++i) {
					for (int j = 0; j < rows; ++j) {
						layers[layer].tiles[i, j].Blit(i * tileSize - x, j * tileSize - y, surface);
					}
				}
				for (int i = 0; i < cols; ++i) {
					for (int j = 0; j < rows; ++j) {
						layers[layer].objects[i, j].ForEach((a) => a.Draw(i * tileSize - x, j * tileSize - y, surface));
					}
				}
			}
		}

		internal void ThinkAll() {
			foreach (Sprite sp in objects) {
				sp.Think(this);
			}
		}
	}
}
