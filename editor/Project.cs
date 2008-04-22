using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace KFIRPG.editor {
	class Project {
		public Dictionary<string, SpriteSheet> sheets = new Dictionary<string, SpriteSheet>();
		public Dictionary<string, Map> maps = new Dictionary<string, Map>();
		public Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();
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
			XmlDocument global = new XmlDocument();
			global.LoadXml(loader.LoadText("global.xml"));

			tileSize = int.Parse(global.SelectSingleNode("/settings/tilesize").InnerText);
			startupMapName = global.SelectSingleNode("/settings/defaultmap").InnerText.Trim();
			startupScriptName = global.SelectSingleNode("/settings/startscript").InnerText.Trim();
			scriptvm = global.SelectSingleNode("/settings/scriptvm").InnerText.Trim();
			screenWidth = int.Parse(global.SelectSingleNode("/settings/screenwidth").InnerText);
			screenHeight = int.Parse(global.SelectSingleNode("/settings/screenheight").InnerText);

			foreach (string strImg in loader.LoadText("img.list").Split('\n')) {
				string img = strImg.Trim();
				if (img == "") continue;
				sheets.Add(img, new SpriteSheet(img, this));
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

			startX = int.Parse(global.SelectSingleNode("/settings/startx").InnerText);
			startY = int.Parse(global.SelectSingleNode("/settings/starty").InnerText);
			startLayer = int.Parse(global.SelectSingleNode("/settings/startl").InnerText);
			foreach (XmlNode node in global.SelectNodes("/settings/party/character")) {
				party.Add(sprites[node.InnerText.Trim()]);
			}
			foreach (XmlNode node in global.SelectNodes("/settings/locations/location")) {
				string locName = node.Attributes["name"].InnerText.Trim();
				int x = int.Parse(node.SelectSingleNode("x").InnerText);
				int y = int.Parse(node.SelectSingleNode("y").InnerText);
				int l = int.Parse(node.SelectSingleNode("layer").InnerText);
				string mapName = node.SelectSingleNode("map").InnerText.Trim();
				maps[mapName].layers[l].tiles[x, y].locationName = locName;
			}
		}

		public static Project FromFiles(string path) {
			return new Project(new KFIRPG.corelib.FileLoader(path));
		}
	}
}
