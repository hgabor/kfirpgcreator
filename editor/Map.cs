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
					gfx = sprite.sheet.GetGfxById(1);
				}
			}

			SpriteSheet.Gfx gfx;
			public SpriteSheet.Gfx Gfx {
				get { return gfx; }
			}
			public string movementAIScript;
			public string actionScript;
		}

		public class Layer {
			public Tile[,] tiles;
			public Obj[,] objects;
			public Layer(int width, int height, string pathBase, Project project) {
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
		}

		public List<Layer> layers = new List<Layer>();
		public int width;
		public int height;

		public void Resize(int newX, int newY) {
			width = newX;
			height = newY;
			layers.ForEach((Layer l) => l.Resize(newX, newY));
		}

		public Map(string name, Project project) {
			Loader loader = project.loader;
			XmlDocument info = new XmlDocument();
			info.LoadXml(loader.LoadText("maps/" + name + "/info.xml"));
			int numLayers = int.Parse(info.SelectSingleNode("/map/layers").InnerText);
			width = int.Parse(info.SelectSingleNode("/map/width").InnerText);
			height = int.Parse(info.SelectSingleNode("/map/height").InnerText);
			for (int i = 0; i < numLayers; ++i) {
				Layer layer = new Layer(width, height, "maps/" + name + "/layers/{0}." + i.ToString(), project);
				layers.Add(layer);
			}

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
		}
	}
}
