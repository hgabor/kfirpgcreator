using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.corelib {
	/// <summary>
	/// Contains the tiles, sprites and OnStep events of the map.
	/// </summary>
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
		/// <summary>
		/// Gets the name of the map.
		/// </summary>
		public string Name { get { return mapName; } }

		/// <summary>
		/// Loads a new map. All map data must be in the "maps/mapName" folder.
		/// </summary>
		/// <param name="mapName">Name of the map.</param>
		/// <param name="game"></param>
		public Map(string mapName, Game game) {
			this.mapName = mapName;
			PropertyReader baseReader = game.loader.GetPropertyReader();
			PropertyReader info = baseReader.Select("maps/" + mapName + "/info.xml");
			cols = info.GetInt("width");
			rows = info.GetInt("height");
			int numLayers = info.GetInt("layers");
			tileSize = game.TileSize;

			layers = new Layer[numLayers];
			for (int i = 0; i < numLayers; ++i) {
				layers[i] = new Layer(cols, rows, "maps/" + mapName + "/layers/{0}." + i.ToString(), game);
			}
			PropertyReader objects = baseReader.Select("maps/" + mapName + "/objects.xml");
			foreach (PropertyReader obj in objects.SelectAll("object")) {
				string name = obj.GetString("sprite");
				int x = obj.GetInt("x");
				int y = obj.GetInt("y");
				int l = obj.GetInt("layer");
				string action = obj.GetString("action");
				string movement = obj.GetString("movement");
				Sprite sp = new Sprite(name, game);
				this.Place(sp, x, y, l);
				if (action != "") {
					Script script = game.vm.LoadResumableScript(game.loader.LoadText("scripts/" + action));
					script.Owner = sp;
					sp.Action = script;
				}
				if (movement != "") {
					MovementAI ai = new ScriptedMovementAI(movement, game);
					sp.MovementAI = ai;
				}
			}

			PropertyReader onstep = baseReader.Select("maps/" + mapName + "/onstep.xml");
			foreach (PropertyReader stepEvent in onstep.SelectAll("event")) {
				int x = stepEvent.GetInt("x");
				int y = stepEvent.GetInt("y");
				int l = stepEvent.GetInt("layer");
				string name = stepEvent.GetString("script");
				Script script = game.vm.LoadResumableScript(game.loader.LoadText("scripts/" + name));
				layers[l].onStep[x, y].Add(script);
			}

			PropertyReader ladders = baseReader.Select("maps/" + mapName + "/ladders.xml");
			foreach (PropertyReader ladder in ladders.SelectAll("ladder")) {
				int x = ladder.GetInt("x");
				int y = ladder.GetInt("y");
				int baseLayer = ladder.GetInt("base");
				int topLayer = ladder.GetInt("top");
				layers[baseLayer].ladderMove[x, y - 1] = layers[topLayer];
				layers[topLayer].ladderMove[x, y] = layers[baseLayer];
			}
		}

		/// <summary>
		/// Places a new sprite on the map. The sprite's Think() method is not executed
		/// until next frame.
		/// </summary>
		/// <param name="sprite"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="layer"></param>
		internal void Place(Sprite sprite, int x, int y, int layer) {
			layers[layer].objects[x, y].Add(sprite);
			addList.Add(sprite);
			sprite.UpdateCoords(x, y, layer);
		}
		//TODO: Might be the cause of yet unknown bugs.
		/// <summary>
		/// Removes a sprite from the map with specified coordinates. The sprite's Think() method
		/// is still executed this frame.
		/// </summary>
		/// <param name="sprite"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="layer"></param>
		internal void Remove(Sprite sprite, int x, int y, int layer) {
			layers[layer].objects[x, y].Remove(sprite);
			removeList.Add(sprite);
		}
		/// <summary>
		/// Moves a sprite from one place to another.
		/// </summary>
		/// <param name="sprite"></param>
		/// <param name="fromX"></param>
		/// <param name="fromY"></param>
		/// <param name="fromLayer"></param>
		/// <param name="toX"></param>
		/// <param name="toY"></param>
		/// <param name="toLayer"></param>
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
		/// <summary>
		/// Executes the OnStep event at the specified location.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="layer"></param>
		/// <returns>True if there was at least one event, otherwise false.</returns>
		internal bool OnStep(int x, int y, int layer) {
			if (layers[layer].onStep[x, y].Count == 0) return false;
			layers[layer].onStep[x, y].ForEach(script => script.Run());
			return true;
		}
		/// <summary>
		/// Executes the OnAction event of the sprites at the specified location.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="layer"></param>
		internal void OnAction(int x, int y, int layer) {
			if (x < 0 || x >= cols || y < 0 || y >= rows || layer < 0 || layer >= layers.Length) return;
			layers[layer].objects[x, y].ForEach(sprite => sprite.DoAction());
			if (layers[layer].ladderMove[x, y] != null) {
				layers[layer].ladderMove[x, y].objects[x, y].ForEach(sprite => sprite.DoAction());
			}
		}

		private static bool ObjectPassable(Sprite obj) {
			return obj.Noclip;
		}

		/// <summary>
		/// Returns the passability of the location. A location is passable if the tile
		/// is passable and if there are no sprites with Noclip = false.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="layer"></param>
		/// <returns></returns>
		internal bool IsPassable(int x, int y, int layer) {
			if (x < 0 || y < 0 || x >= cols || y >= rows) return false;
			if (layers[layer].ladderMove[x, y] == null) {
				return layers[layer].passable[x, y] &&
					layers[layer].objects[x, y].TrueForAll(ObjectPassable);
			}
			else {
				return layers[layer].ladderMove[x, y].passable[x, y] &&
					layers[layer].ladderMove[x, y].objects[x, y].TrueForAll(ObjectPassable);
			}
		}

		/// <summary>
		/// Draws the tiles and objects on the specified surface.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="surface"></param>
		internal void Draw(int x, int y, SdlDotNet.Graphics.Surface surface) {
			for (int layer = 0; layer < layers.Length; ++layer) {
				for (int j = 0; j < rows; ++j) {
					for (int i = 0; i < cols; ++i) {
						layers[layer].tiles[i, j].Blit(i * tileSize - x, j * tileSize - y, surface);
					}
				}
				for (int j = 0; j < rows; ++j) {
					for (int i = 0; i < cols; ++i) {
						layers[layer].objects[i, j].ForEach((a) => a.Draw(i * tileSize - x, j * tileSize - y, surface));
					}
				}
			}
		}

		/// <summary>
		/// Executes the Think() method for all sprites on the map.
		/// Should be called once per frame.
		/// </summary>
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
