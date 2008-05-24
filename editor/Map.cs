using System;
using System.Collections.Generic;
using System.Text;
using KFIRPG.corelib;
using System.Drawing;
using System.Xml;

namespace KFIRPG.editor {
	class Map {
		public class Tile {
			public SpriteSheet.Gfx gfx;
			public bool passable;
			public string onStep = "";
			public string locationName = "";
			public Tile(SpriteSheet.Gfx gfx, bool passable) {
				this.gfx = gfx;
				this.passable = passable;
			}
		}

		public class Obj {
			Sprite sprite;
			public Sprite Sprite {
				get { return sprite; }
				set {
					sprite = value;
			//		gfx = sprite.sheet.GetGfxById(1);
				}
			}

			/*
			SpriteSheet.Gfx gfx;
			public SpriteSheet.Gfx Gfx {
				get { return gfx; }
			}*/
			public SpriteSheet.Gfx Gfx {
				get { return sprite.animation.sheet.GetGfxById(1); }
			}
			public string movementAIScript;
			public string actionScript;
		}

		public class Layer {
			public readonly Map Map;
			public Tile[,] tiles;
			public Obj[,] objects;
			public string name;
			public Layer(int width, int height, string pathBase, Map map, Project project) {
				this.Map = map;
				Loader loader = project.loader;
				SpriteSheet sheet = project.sheets["tiles"];
				tiles = new Tile[width, height];
				objects = new Obj[width, height];
				string[] passLines = loader.LoadText(string.Format(pathBase, "passability")).Split('\n');
				string[] gfxLines = loader.LoadText(string.Format(pathBase, "tiles")).Split('\n');
				for (int j = 0; j < height; ++j) {
					string[] passLine = passLines[j].Split(' ');
					string[] gfxLine = gfxLines[j].Split(' ');
					for (int i = 0; i < width; ++i) {
						tiles[i, j] = new Tile(sheet.GetGfxById(int.Parse(gfxLine[i])), int.Parse(passLine[i]) == 1);
					}
				}
				name = loader.LoadText(string.Format(pathBase, "name")).Trim();
			}
			public Layer(int width, int height, string name, Map map) {
				this.Map = map;
				this.name = name;
				tiles = new Tile[width, height];
				objects = new Obj[width, height];
				for (int i = 0; i < width; ++i) {
					for (int j = 0; j < height; ++j) {
						tiles[i, j] = new Tile(SpriteSheet.Gfx.Empty, true);
					}
				}
			}

			public void Resize(int newX, int newY) {
				int oldX = tiles.GetUpperBound(0) + 1;
				int oldY = tiles.GetUpperBound(1) + 1;
				Tile[,] newTiles = new Tile[newX, newY];
				Obj[,] newObjs = new Obj[newX, newY];
				for (int i = 0; i < newX; ++i) {
					for (int j = 0; j < newY; ++j) {
						if (i < oldX && j < oldY) {
							newTiles[i, j] = tiles[i, j];
							newObjs[i, j] = objects[i, j];
						}
						else {
							newTiles[i, j] = new Tile(SpriteSheet.Gfx.Empty, false);
						}
					}
				}
				tiles = newTiles;
				objects = newObjs;
			}

			public override string ToString() {
				return name;
			}
		}

		public class Ladder {
			public Layer baseLayer;
			public Layer topLayer;
			public Ladder(Layer baseLayer, Layer topLayer) {
				this.baseLayer = baseLayer;
				this.topLayer = topLayer;
			}
		}

		public List<Layer> layers = new List<Layer>();
		public Ladder[,] ladders;
		public int width;
		public int height;
		public string name;

		public void Resize(int newX, int newY) {
			width = newX;
			height = newY;
			layers.ForEach((Layer l) => l.Resize(newX, newY));
		}

		public Map(string name, Project project) {
			this.name = name;
			Loader loader = project.loader;
			XmlDocument info = new XmlDocument();
			info.LoadXml(loader.LoadText("maps/" + name + "/info.xml"));
			int numLayers = int.Parse(info.SelectSingleNode("/map/layers").InnerText);
			width = int.Parse(info.SelectSingleNode("/map/width").InnerText);
			height = int.Parse(info.SelectSingleNode("/map/height").InnerText);
			for (int i = 0; i < numLayers; ++i) {
				Layer layer = new Layer(width, height, "maps/" + name + "/layers/{0}." + i.ToString(), this, project);
				layers.Add(layer);
			}
			this.ladders = new Ladder[width, height];

			XmlDocument objects = new XmlDocument();
			objects.LoadXml(loader.LoadText("maps/" + name + "/objects.xml"));
			foreach (XmlNode node in objects.SelectNodes("/objects/object")) {
				string spriteName = node.SelectSingleNode("sprite").InnerText.Trim();
				int x = int.Parse(node.SelectSingleNode("x").InnerText);
				int y = int.Parse(node.SelectSingleNode("y").InnerText);
				int layer = int.Parse(node.SelectSingleNode("layer").InnerText);
				string move = node.SelectSingleNode("movement").InnerText.Trim();
				string action = node.SelectSingleNode("action").InnerText.Trim();
				Obj o = new Obj();
				o.Sprite = project.sprites[spriteName];
				o.movementAIScript = move;
				o.actionScript = action;
				layers[layer].objects[x, y] = o;
			}

			XmlDocument events = new XmlDocument();
			events.LoadXml(loader.LoadText("maps/" + name + "/onstep.xml"));
			foreach (XmlNode node in events.SelectNodes("/events/event")) {
				int x = int.Parse(node.SelectSingleNode("x").InnerText);
				int y = int.Parse(node.SelectSingleNode("y").InnerText);
				int layer = int.Parse(node.SelectSingleNode("layer").InnerText);
				string script = node.SelectSingleNode("script").InnerText.Trim();
				layers[layer].tiles[x, y].onStep = script;
			}

			XmlDocument ladders = new XmlDocument();
			ladders.LoadXml(loader.LoadText("maps/" + name + "/ladders.xml"));
			foreach (XmlNode node in ladders.SelectNodes("/ladders/ladder")) {
				int x = int.Parse(node.SelectSingleNode("x").InnerText);
				int y = int.Parse(node.SelectSingleNode("y").InnerText);
				int baseLayer = int.Parse(node.SelectSingleNode("base").InnerText);
				int topLayer = int.Parse(node.SelectSingleNode("top").InnerText);
				this.ladders[x, y] = new Ladder(layers[baseLayer], layers[topLayer]);
			}
		}

		public Map(string name, Size size) {
			this.name = name;
			width = size.Width;
			height = size.Height;
			Layer layer = new Layer(size.Width, size.Height, "layer1", this);
			layers.Add(layer);
		}

		internal Layer CreateNewLayer(string name) {
			int layerId = layers.Count;
			Layer newLayer = new Layer(width, height, name, this);
			layers.Add(newLayer);
			return newLayer;
		}
	}
}
