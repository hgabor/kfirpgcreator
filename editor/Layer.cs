using KFIRPG.corelib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KFIRPG.editor
{
	class Layer
	{
		public readonly Map Map;
		public Map.Tile[,] tiles;
		public Map.Obj[,] objects;
		public Layer(int width, int height, string pathBase, Map map, Project project)
		{
			this.Map = map;
			Loader loader = project.loader;
			SpriteSheet sheet = project.sheets["tiles"];
			tiles = new Map.Tile[width, height];
			objects = new Map.Obj[width, height];
			string[] passLines = loader.LoadText(string.Format(pathBase, "passability")).Split('\n');
			string[] gfxLines = loader.LoadText(string.Format(pathBase, "tiles")).Split('\n');
			for (int j = 0; j < height; ++j)
			{
				string[] passLine = passLines[j].Split(' ');
				string[] gfxLine = gfxLines[j].Split(' ');
				for (int i = 0; i < width; ++i)
				{
					tiles[i, j] = new Map.Tile(sheet.GetGfxById(int.Parse(gfxLine[i])), int.Parse(passLine[i]) == 1);
				}
			}
		}
		public Layer(int width, int height, Map map)
		{
			this.Map = map;
			tiles = new Map.Tile[width, height];
			objects = new Map.Obj[width, height];
			for (int i = 0; i < width; ++i)
			{
				for (int j = 0; j < height; ++j)
				{
					tiles[i, j] = new Map.Tile(SpriteSheet.Gfx.Empty, true);
				}
			}
		}

		public void Resize(int newX, int newY)
		{
			int oldX = tiles.GetUpperBound(0) + 1;
			int oldY = tiles.GetUpperBound(1) + 1;
			Map.Tile[,] newTiles = new Map.Tile[newX, newY];
			Map.Obj[,] newObjs = new Map.Obj[newX, newY];
			for (int i = 0; i < newX; ++i)
			{
				for (int j = 0; j < newY; ++j)
				{
					if (i < oldX && j < oldY)
					{
						newTiles[i, j] = tiles[i, j];
						newObjs[i, j] = objects[i, j];
					}
					else
					{
						newTiles[i, j] = new Map.Tile(SpriteSheet.Gfx.Empty, false);
					}
				}
			}
			tiles = newTiles;
			objects = newObjs;
		}

		public override string ToString()
		{
			return "<Layer>";
		}
	}
}
