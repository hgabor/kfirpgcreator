using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace KFIRPG.corelib {
	class Sprite: Entity {
		Animation baseGraphic;
		Animation graphic;

		public Sprite(string spriteId, Game game) {
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(game.loader.LoadText(string.Concat("sprites/", spriteId, ".xml")));
			graphic = baseGraphic = new Animation(doc.SelectSingleNode("sprite/img").InnerText, game.TileSize, game);
			speed = int.Parse(doc.SelectSingleNode("sprite/speed").InnerText);

			foreach (XmlNode node in doc.SelectNodes("sprite/ext")) {
				this[node.LocalName] = node.InnerText;
			}

			this.size = game.TileSize;
		}

		public void Draw(int x, int y, SdlDotNet.Graphics.Surface surface) {
			graphic.Blit(x + movingX, y + movingY, surface);
		}

		int size;
		int x = 0;
		public int X { get { return x; } }
		int y = 0;
		public int Y { get { return y; } }
		int layer = 0;
		public int Layer { get { return layer; } }
		public void UpdateCoords(int x, int y, int layer) {
			this.x = x;
			this.y = y;
			this.layer = layer;
		}
		int speed;
		public int Speed {
			get { return speed; }
			set { speed = value; }
		}

		MovementAI movementAI = new NotMovingAI();
		public MovementAI MovementAI {
			set { movementAI = value; }
		}

		int movingX = 0;
		int movingY = 0;
		//TODO: Remove magic numbers
		public void MoveDown() {
			movingY = -size;
			graphic.SetState("movedown", 0);
		}
		public void MoveUp() {
			movingY = size;
			graphic.SetState("moveup", 1);
		}
		public void MoveLeft() {
			movingX = size;
			graphic.SetState("moveleft", 2);
		}
		public void MoveRight() {
			movingX = -size;
			graphic.SetState("moveright", 3);
		}

		public void Think(Map map) {
			if (movingX == 0 && movingY == 0) {
				movementAI.TryDoingSomething(this, map);
			}
			if (movingX > 0) {
				movingX -= speed;
				if (movingX < 0) movingX = 0;
			}
			if (movingX < 0) {
				movingX += speed;
				if (movingX > 0) movingX = 0;
			}
			if (movingY > 0) {
				movingY -= speed;
				if (movingY < 0) movingY = 0;
			}
			if (movingY < 0) {
				movingY += speed;
				if (movingY > 0) movingY = 0;
			}
		}
	}
}
