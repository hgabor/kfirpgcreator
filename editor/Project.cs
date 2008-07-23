using System;
using System.Collections.Generic;
using System.Text;
using KFIRPG.corelib;

namespace KFIRPG.editor {
	class Project {
		public Dictionary<string, SpriteSheet> sheets = new Dictionary<string, SpriteSheet>();
		public Dictionary<string, Map> maps = new Dictionary<string, Map>();
		public Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();
		public Dictionary<string, Animation> animations = new Dictionary<string, Animation>();
		public List<Script> scripts = new List<Script>();
		public int tileSize;
		public KFIRPG.corelib.Loader loader;
		public string startupMapName;
		public string startupScriptName;
		public List<Sprite> party = new List<Sprite>();

		public string scriptvm;
		public int screenWidth;
		public int screenHeight;
		public int startX;
		public int startY;
		public int startLayer;

		public Project() { }

		private Project(KFIRPG.corelib.Loader loader) {
			this.loader = loader;
			PropertyReader global = loader.GetPropertyReader().Select("global.xml");

			tileSize = global.GetInt("tilesize");
			startupMapName = global.GetString("defaultmap");
			startupScriptName = global.GetString("startscript");
			scriptvm = global.GetString("scriptvm");
			screenWidth = global.GetInt("screenwidth");
			screenHeight = global.GetInt("screenheight");

			//Create LoadList<T>(listName, loader, Converter<string, T> adder);
			foreach (string strImg in loader.LoadText("img.list").Split('\n')) {
				string img = strImg.Trim();
				if (img == "") continue;
				sheets.Add(img, new SpriteSheet(img, this));
			}

			foreach (string strAnim in loader.LoadText("animations.list").Split('\n')) {
				string anim = strAnim.Trim();
				if (anim == "") continue;
				animations.Add(anim, new Animation(anim, this));
			}

			foreach (string strSprite in loader.LoadText("sprites.list").Split('\n')) {
				string sprite = strSprite.Trim();
				if (sprite == "") continue;
				sprites.Add(sprite, new Sprite(sprite, this));
			}

			foreach (string strMap in loader.LoadText("maps.list").Split('\n')) {
				string map = strMap.Trim();
				if (map == "") continue;
				maps.Add(map, new Map(map, this));
			}

			foreach (string strScript in loader.LoadText("scripts.list").Split('\n')) {
				string script = strScript.Trim();
				if (script == "") continue;
				scripts.Add(new Script(script, loader.LoadText("scripts/" + script)));
			}

			startX = global.GetInt("startx");
			startY = global.GetInt("starty");
			startLayer = global.GetInt("startl");
			foreach (PropertyReader character in global.SelectAll("party/character")) {
				party.Add(sprites[character.GetString("")]);
			}
			foreach (PropertyReader loc in global.SelectAll("locations/location")) {
				string locName = loc.GetString("name");
				int x = loc.GetInt("x");
				int y = loc.GetInt("y");
				int l = loc.GetInt("layer");
				string mapName = loc.GetString("map");
				maps[mapName].layers[l].tiles[x, y].locationName = locName;
			}
		}

		public static Project FromFiles(string path) {
			return new Project(new KFIRPG.corelib.FileLoader(path));
		}
	}
}
