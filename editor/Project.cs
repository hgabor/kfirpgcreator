using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace KFIRPG.editor {
	class Project {
		public Dictionary<string, SpriteSheet> sheets = new Dictionary<string, SpriteSheet>();
		public Dictionary<string, Map> maps = new Dictionary<string, Map>();
		public int tileSize;
		public KFIRPG.corelib.Loader loader;
		public string startupMapName;

		private Project(KFIRPG.corelib.Loader loader) {
			this.loader = loader;
			XmlDocument global = new XmlDocument();
			global.LoadXml(loader.LoadText("global.xml"));

			tileSize = int.Parse(global.SelectSingleNode("settings/tilesize").InnerText);
			startupMapName = global.SelectSingleNode("settings/defaultmap").InnerText.Trim();

			foreach (string strImg in loader.LoadText("img.list").Split('\n')) {
				string img = strImg.Trim();
				if (img == "") continue;
				sheets.Add(img, new SpriteSheet(img, this));
			}

			foreach (string strMap in loader.LoadText("maps.list").Split('\n')) {
				string map = strMap.Trim();
				if (map == "") continue;
				maps.Add(map, new Map(map, this));
			}
		}

		public static Project FromFiles(string path) {
			return new Project(new KFIRPG.corelib.FileLoader(path));
		}
	}
}
