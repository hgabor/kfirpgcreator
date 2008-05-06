﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace KFIRPG.corelib {
	class Map {
		class Layer {
			public List<Sprite>[,] objects;
			public Graphics[,] tiles;
			public bool[,] passable;
			public List<Script>[,] onStep;
			public Layer[,] ladderMove;
			public Layer(int width, int height, string path, Game game) {
				string[] tileLines = game.loader.LoadText(string.Format(path, "tiles")).Split('\n');
				string[] passLines = game.loader.LoadText(string.Format(path, "passability")).Split('\n');
				objects = new List<Sprite>[width, height];
				tiles = new Graphics[width, height];
				passable = new bool[width, height];
				onStep = new List<Script>[width, height];
				ladderMove = new Layer[width, height];

				for (int j = 0; j < height; ++j) {
					string[] tileLine = tileLines[j].Split(' ');
					string[] passLine = passLines[j].Split(' ');
					for (int i = 0; i < width; ++i) {
						objects[i, j] = new List<Sprite>();
						onStep[i, j] = new List<Script>();
						int tileID = int.Parse(tileLine[i]);
						if (tileID == 0) {
							tiles[i, j] = new NoGraphics();
						}
						else {
							AnimatedGraphics anim = new AnimatedGraphics("tiles", game, tileID - 1);
							tiles[i, j] = anim;
						}
						passable[i, j] = int.Parse(passLine[i]) == 1;
					}
				}
			}
		}

		List<Sprite> objects = new List<Sprite>();
		List<Sprite> addList = new List<Sprite>();
		List<Sprite> removeList = new List<Sprite>();
		Layer[] layers;
		int rows;
		int cols;
		int tileSize;

		string mapName;
		public string Name { get { return mapName; } }

		public Map(string mapName, Game game) {
			this.mapName = mapName;
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
			XmlDocument objects = new XmlDocument();
			objects.LoadXml(game.loader.LoadText("maps/" + mapName + "/objects.xml"));
			foreach (XmlNode node in objects.SelectNodes("/objects/object")) {
				string name = node.SelectSingleNode("sprite").InnerText.Trim();
				int x = int.Parse(node.SelectSingleNode("x").InnerText);
				int y = int.Parse(node.SelectSingleNode("y").InnerText);
				int l = int.Parse(node.SelectSingleNode("layer").InnerText);
				string action = node.SelectSingleNode("action").InnerText.Trim();
				string movement = node.SelectSingleNode("movement").InnerText.Trim();
				Sprite sp = new Sprite(name, game);
				this.Place(sp, x, y, l);
				if (action != "") {
					Script script = game.vm.LoadScript(game.loader.LoadText("scripts/" + action));
					script.Owner = sp;
					sp.Action = script;
				}
				if (movement != "") {
					MovementAI ai = new ScriptedMovementAI(movement, game);
					sp.MovementAI = ai;
				}
			}

			XmlDocument onstep = new XmlDocument();
			onstep.LoadXml(game.loader.LoadText("maps/" + mapName + "/onstep.xml"));
			foreach (XmlNode node in onstep.SelectNodes("/events/event")) {
				int x = int.Parse(node.SelectSingleNode("x").InnerText);
				int y = int.Parse(node.SelectSingleNode("y").InnerText);
				int l = int.Parse(node.SelectSingleNode("layer").InnerText);
				string name = node.SelectSingleNode("script").InnerText.Trim();
				Script script = game.vm.LoadScript(game.loader.LoadText("scripts/" + name));
				layers[l].onStep[x, y].Add(script);
			}
			XmlDocument ladders = new XmlDocument();
			ladders.LoadXml(game.loader.LoadText("maps/" + mapName + "/ladders.xml"));
			foreach (XmlNode node in ladders.SelectNodes("/ladders/ladder")) {
				int x = int.Parse(node.SelectSingleNode("x").InnerText);
				int y = int.Parse(node.SelectSingleNode("y").InnerText);
				int baseLayer = int.Parse(node.SelectSingleNode("base").InnerText);
				int topLayer = int.Parse(node.SelectSingleNode("top").InnerText);
				layers[baseLayer].ladderMove[x, y - 1] = layers[topLayer];
				layers[topLayer].ladderMove[x, y] = layers[baseLayer];
			}
		}

		internal void Place(Sprite sprite, int x, int y, int layer) {
			layers[layer].objects[x, y].Add(sprite);
			addList.Add(sprite);
			sprite.UpdateCoords(x, y, layer);
		}
		internal void Remove(Sprite sprite, int x, int y, int layer) {
			layers[layer].objects[x, y].Remove(sprite);
			removeList.Add(sprite);
		}
		internal void Move(Sprite sprite, int fromX, int fromY, int fromLayer, int toX, int toY, int toLayer) {
			layers[fromLayer].objects[fromX, fromY].Remove(sprite);
			if (layers[toLayer].ladderMove[toX, toY] != null) {
				layers[toLayer].ladderMove[toX, toY].objects[toX, toY].Add(sprite);
				sprite.UpdateCoords(toX, toY, Array.IndexOf(layers, layers[toLayer].ladderMove[toX, toY]));
			}
			else {
				layers[toLayer].objects[toX, toY].Add(sprite);
				sprite.UpdateCoords(toX, toY, toLayer);
			}
		}
		internal bool OnStep(int x, int y, int layer) {
			if (layers[layer].onStep[x, y].Count == 0) return false;
			layers[layer].onStep[x, y].ForEach(script => script.Run());
			return true;
		}
		internal void OnAction(int x, int y, int layer) {
			if (x < 0 || x >= cols || y < 0 || y >= rows || layer < 0 || layer >= layers.Length) return;
			layers[layer].objects[x, y].ForEach(sprite => sprite.DoAction());
		}

		private bool ObjectPassable(Sprite obj) {
			return obj.Noclip;
		}

		internal bool IsPassable(int x, int y, int layer) {
			return x >= 0 && y >= 0 && x < cols && y < rows && layers[layer].passable[x, y] &&
				layers[layer].objects[x, y].TrueForAll(ObjectPassable);
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
			if (removeList.Count != 0) {
				foreach (Sprite sp in removeList) objects.Remove(sp);
				removeList.Clear();
			}
			if (addList.Count != 0) {
				objects.AddRange(addList);
				addList.Clear();
			}
		}
	}
}
