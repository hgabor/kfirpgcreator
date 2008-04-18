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
			noclip = int.Parse(doc.SelectSingleNode("sprite/noclip").InnerText) == 1;

			foreach (XmlNode node in doc.SelectNodes("sprite/ext")) {
				this[node.LocalName] = node.InnerText;
			}

			this.size = game.TileSize;
		}

		public void Draw(int x, int y, SdlDotNet.Graphics.Surface surface) {
			graphic.Blit(x + corrX, y + corrY, surface);
		}

		int size;
		public int Height { get { return size; } }
		public int Width { get { return size; } }
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
		bool noclip;
		public bool Noclip { get { return noclip; } }

		Script action = null;
		public Script Action { set { action = value; } }
		public void DoAction() {
			if (action != null) action.Run();
		}

		MovementAI movementAI = new NotMovingAI();
		public MovementAI MovementAI {
			set { movementAI = value; }
		}

		protected virtual bool OnStep(Map map) { return false; }


		int corrX = 0;
		public int CorrX { get { return corrX; } }
		int corrY = 0;
		public int CorrY { get { return corrY; } }
		//TODO: Remove magic numbers
		public enum Dir: int {
			None = -1,
			Down = 0,
			Up = 1,
			Left = 2,
			Right = 3
		}

		Dir moving = Dir.None;
		Dir nextMove = Dir.None;
		Dir facing = Dir.Up;
		public void Turn(Dir direction) {
			facing = direction;
			switch (moving) {
				case Dir.Up: graphic.SetState("moveup", 0); break;
				case Dir.Down: graphic.SetState("movedown", 0); break;
				case Dir.Left: graphic.SetState("moveleft", 0); break;
				case Dir.Right: graphic.SetState("moveright", 0); break;
				case Dir.None: graphic.SetState("still", (int)facing); break;
			}
		}

		public void PlanMove(Dir direction, Map map) {
			nextMove = Dir.None;
			if (moving == Dir.None && direction != Dir.None) {
				facing = direction;
				graphic.SetState("still", (int)facing);
			}
			switch (direction) {
				case Dir.Up:
					if (map.IsPassable(x, y - 1, layer)) {
						nextMove = Dir.Up;
					}
					break;
				case Dir.Down:
					if (map.IsPassable(x, y + 1, layer)) {
						nextMove = Dir.Down;
					}
					break;
				case Dir.Left:
					if (map.IsPassable(x - 1, y, layer)) {
						nextMove = Dir.Left;
					}
					break;
				case Dir.Right:
					if (map.IsPassable(x + 1, y, layer)) {
						nextMove = Dir.Right;
					}
					break;
			}
		}

		public void PlanAction(Map map) {
			if (moving != Dir.None) return;
			switch (facing) {
				case Dir.Up: map.OnAction(x, y - 1, layer); break;
				case Dir.Down: map.OnAction(x, y + 1, layer); break;
				case Dir.Left: map.OnAction(x - 1, y, layer); break;
				case Dir.Right: map.OnAction(x + 1, y, layer); break;
			}
			map.OnAction(x, y, layer);
		}

		public void Think(Map map) {
			movementAI.TryDoingSomething(this, map);
			Dir oldMoving = moving;

			if (moving == Dir.None) {
				moving = nextMove;
				if (moving != Dir.None) facing = moving;
			}

			switch (moving) {
				case Dir.Left:
					if (corrX > 0) {
						corrX -= speed;
					}
					else if (corrX <= 0) {
						if (oldMoving == moving && OnStep(map)) {
							corrX = 0;
							moving = Dir.None;
							graphic.SetState("still", (int)facing);
						}
						else {
							if (nextMove != Dir.Left) {
								corrX = 0;
								moving = Dir.None;
							}
							else {
								corrX += size;
								map.Move(this, x, y, layer, x - 1, y, layer);
							}
						}
					}
					break;
				case Dir.Right:
					if (corrX < 0) {
						corrX += speed;
					}
					else if (corrX >= 0) {
						if (oldMoving == moving && OnStep(map)) {
							corrX = 0;
							moving = Dir.None;
							graphic.SetState("still", (int)facing);
						}
						else {
							if (nextMove != Dir.Right) {
								corrX = 0;
								moving = Dir.None;
							}
							else {
								corrX -= size;
								map.Move(this, x, y, layer, x + 1, y, layer);
							}
						}
					}
					break;
				case Dir.Up:
					if (corrY > 0) {
						corrY -= speed;
					}
					else if (corrY <= 0) {
						if (oldMoving == moving && OnStep(map)) {
							corrY = 0;
							moving = Dir.None;
							graphic.SetState("still", (int)facing);
						}
						else {
							if (nextMove != Dir.Up) {
								corrX = 0;
								moving = Dir.None;
							}
							else {
								corrY += size;
								map.Move(this, x, y, layer, x, y - 1, layer);
							}
						}
					}
					break;
				case Dir.Down:
					if (corrY < 0) {
						corrY += speed;
					}
					else if (corrY >= 0) {
						if (oldMoving == moving && OnStep(map)) {
							corrY = 0;
							moving = Dir.None;
							graphic.SetState("still", (int)facing);
						}
						else {
							if (nextMove != Dir.Down) {
								corrX = 0;
								moving = Dir.None;
							}
							else {
								corrY -= size;
								map.Move(this, x, y, layer, x, y + 1, layer);
							}
						}
					}
					break;
			}

			if (moving == Dir.None) {
				moving = nextMove;
				if (moving != Dir.None) facing = moving;
			}

			if (moving != oldMoving) {
				switch (moving) {
					case Dir.Up: graphic.SetState("moveup", 0); break;
					case Dir.Down: graphic.SetState("movedown", 0); break;
					case Dir.Left: graphic.SetState("moveleft", 0); break;
					case Dir.Right: graphic.SetState("moveright", 0); break;
					case Dir.None: graphic.SetState("still", (int)facing); break;
				}
			}
			nextMove = Dir.None;
			graphic.Advance();
		}
	}
}
