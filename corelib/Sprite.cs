using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace KFIRPG.corelib {
	public class Sprite: Object {
		Graphic graphic;

		public Sprite(string spriteId, Game game) {
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(game.loader.LoadText(string.Concat("sprites/", spriteId, ".xml")));
			graphic = new Graphic(doc.SelectSingleNode("sprite/img").InnerText, 0, game.TileSize, game);

			foreach (XmlNode node in doc.SelectNodes("sprite/ext")) {
				this[node.LocalName] = node.InnerText;
			}
		}

		public override void Draw(int x, int y, SdlDotNet.Graphics.Surface surface) {
			graphic.Blit(x, y, surface);
		}
	}
}
