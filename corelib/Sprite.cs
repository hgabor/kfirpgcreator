using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.corelib {
	/// <summary>
	/// An object that has a representation on the map.
	/// </summary>
	class Sprite: Entity {
		AnimatedGraphics graphic;

		/// <summary>
		/// Sets the animation of the sprite's graphic.
		/// </summary>
		/// <param name="animation"></param>
		public void SetAnimation(string animation) {
			graphic.SetState(animation);
		}

		/// <summary>
		/// Loads a new sprite instance. The sprite's data must be in the "sprites" folder.
		/// </summary>
		/// <param name="spriteId">The name of the sprite.</param>
		/// <param name="game"></param>
		public Sprite(string spriteId, Game game) {
			PropertyReader sprite = game.loader.GetPropertyReader().Select("sprites/" + spriteId + ".xml");
			graphic = new AnimatedGraphics(sprite.GetString("animation"), game);
			speed = sprite.GetInt("speed");
			noclip = sprite.GetBool("noclip");

			foreach (PropertyReader ext in sprite.SelectAll("exts/ext")) {
				this.SetProperty(ext.GetString("key"), ext.GetString("value"));
			}

			this.size = game.TileSize;
		}

		/// <summary>
		/// Draws the sprite on the surface.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="surface"></param>
		public void Render(int x, int y, SdlDotNet.Graphics.Surface surface) {
			graphic.Render(x + corrX, y + corrY, surface);
		}

		int size;
		//TODO: Outfactor Height and Width - they are stored in the graphics.
		public int Height { get { return size; } }
		public int Width { get { return size; } }
		int x = 0;
		/// <summary>
		/// Gets the X coordinate of the sprite on the map.
		/// </summary>
		public int X { get { return x; } }
		int y = 0;
		/// <summary>
		/// Gets the Y coordinate of the sprite on the map.
		/// </summary>
		public int Y { get { return y; } }
		int layer = 0;
		/// <summary>
		/// Gets the Layer coordinate of the sprite on the map.
		/// </summary>
		public int Layer { get { return layer; } }
		/// <summary>
		/// Sets the coordinates of the sprite on the map.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="layer"></param>
		public void UpdateCoords(int x, int y, int layer) {
			this.x = x;
			this.y = y;
			this.layer = layer;
		}
		int speed;
		/// <summary>
		/// Gets or sets the pixels travelled per frame.
		/// </summary>
		public int Speed {
			get { return speed; }
			set { speed = value; }
		}
		bool noclip;
		/// <summary>
		/// Gets the Noclip property. The sprite is "Noclip" if it can pass through other objects.
		/// </summary>
		public bool Noclip { get { return noclip; } }

		Script action = null;
		/// <summary>
		/// Sets the script run when the player uses the action command on the sprite.
		/// </summary>
		public Script Action { set { action = value; } }
		/// <summary>
		/// Runs the action script.
		/// </summary>
		/// <see cref="Action"/>
		public void DoAction() {
			if (action != null) action.Run();
		}

		public Script Collide {
			private get;
			set;
		}
		public void OnCollide() {
			if (Collide != null) Collide.Run();
		}

		MovementAI movementAI = new NotMovingAI();
		/// <summary>
		/// Sets the controller that controls the sprites movement on the screen.
		/// Default is NotMovingAI
		/// </summary>
		/// <see cref="NotMovingAI"/>
		public MovementAI MovementAI {
			set { movementAI = value; }
		}

		protected virtual bool OnStep(Map map) { return false; }


		int corrX = 0;
		/// <summary>
		/// Gets the sprite's X offset. It is used to show the sprite moving.
		/// </summary>
		public int CorrX { get { return corrX; } }
		int corrY = 0;
		/// <summary>
		/// Gets the sprite's Y offset. It is used to show the sprite moving.
		/// </summary>
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
		/// <summary>
		/// Returns true if the sprite is currently moving.
		/// </summary>
		public bool IsMoving { get { return moving != Dir.None; } }
		Dir nextMove = Dir.None;
		Dir facing = Dir.Up;
		/// <summary>
		/// Sets the sprite's facing direction.
		/// </summary>
		/// <param name="direction"></param>
		public void Turn(Dir direction) {
			facing = direction;
			graphic.SetDirection(direction);
		}

		/// <summary>
		/// Sets the sprite's next move.
		/// Used only by MovementAIs.
		/// </summary>
		/// <param name="direction"></param>
		/// <param name="map"></param>
		/// <seealso cref="PlanAction"/>
		public void PlanMove(Dir direction, Map map) {
			nextMove = Dir.None;
			if (moving == Dir.None && direction != Dir.None) {
				facing = direction;
				graphic.SetDirection(direction);
				graphic.SetState("still");
			}
			switch (direction) {
				case Dir.Up:
					if (map.IsPassable(x, y - 1, layer)) {
						nextMove = Dir.Up;
					}
					else if (moving == Dir.None) {
						map.OnCollide(x, y - 1, layer, this);
					}
					break;
				case Dir.Down:
					if (map.IsPassable(x, y + 1, layer)) {
						nextMove = Dir.Down;
					}
					else if (moving == Dir.None) {
						map.OnCollide(x, y + 1, layer, this);
					}
					break;
				case Dir.Left:
					if (map.IsPassable(x - 1, y, layer)) {
						nextMove = Dir.Left;
					}
					else if (moving == Dir.None) {
						map.OnCollide(x - 1, y, layer, this);
					}
					break;
				case Dir.Right:
					if (map.IsPassable(x + 1, y, layer)) {
						nextMove = Dir.Right;
					}
					else if (moving == Dir.None) {
						map.OnCollide(x + 1, y, layer, this);
					}
					break;
			}
		}

		/// <summary>
		/// Plans the next movement to interact with another sprite.
		/// Used only by MovementAIs.
		/// </summary>
		/// <param name="map"></param>
		/// <seealso cref="PlanMove"/>
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

		/// <summary>
		/// Decides what to do next (move or interact) and tries to execute it.
		/// </summary>
		/// <param name="map"></param>
		public void Advance(Map map) {
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
							graphic.SetState("still");
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
							graphic.SetState("still");
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
							graphic.SetState("still");
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
							graphic.SetState("still");
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
				if (moving == Dir.None) {
					graphic.SetState("still");
				}
				else {
					graphic.SetState("move");
					graphic.SetDirection(moving);
				}
			}
			nextMove = Dir.None;
			graphic.Advance();
		}
	}
}
