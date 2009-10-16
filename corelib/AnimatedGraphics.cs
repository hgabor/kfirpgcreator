using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using SdlDotNet.Graphics;

namespace KFIRPG.corelib {
	/// <summary>
	/// A graphic that can have multiple frames and/or states.
	/// </summary>
	class AnimatedGraphics: Graphics {
		Surface sheet;

		class Frame {
			public int id;
			public int time;
			public Frame() { }
			public Frame(int id, int time) { this.id = id; this.time = time; }
		}

		class Animation {
			public Frame[] frames;
		}

		struct AnimationType {
			public string type;
			public Sprite.Dir dir;
			public AnimationType(string type, Sprite.Dir dir) {
				this.type = type;
				this.dir = dir;
			}
		}

		Dictionary<AnimationType, Animation> animations = new Dictionary<AnimationType, Animation>();
		Animation currentAnimation;
		AnimationType currentType;
		Frame currentFrame;
		int frame;
		int width;
		int height;
		int x;
		int y;
		int columnsInRow;

		public override int Width {
			get { return width; }
		}

		public override int Height {
			get { return height; }
		}

		public int FrameCount {
			get { return currentAnimation.frames.Length; }
		}

		private void LoadSpriteSheet(string sheetName, Game game) {
			sheet = game.loader.LoadSurface("img/" + sheetName + ".png");
			PropertyReader props = game.loader.GetPropertyReader().Select("img/" + sheetName + ".xml");
			width = props.GetInt("width");
			height = props.GetInt("height");
			x = props.GetInt("x");
			y = props.GetInt("y");
			columnsInRow = sheet.Width / width;
		}

		/// <summary>
		/// Loads an animated graphics. The descriptor file for the animation must be
		/// in the "animations" folder.
		/// </summary>
		/// <param name="animationName">The name of the animation.</param>
		/// <param name="game"></param>
		public AnimatedGraphics(string animationName, Game game) {
			PropertyReader props = game.loader.GetPropertyReader().Select("animations/" + animationName + ".xml");
			foreach (PropertyReader group in props.SelectAll("group")) {
				Animation current = new Animation();
				string[] animName = group.GetString("name").Split('.');
				string type = animName[0];
				Sprite.Dir dir = (Sprite.Dir)Enum.Parse(typeof(Sprite.Dir), animName[1], true);
				AnimationType animType = new AnimationType(type, dir);
				List<Frame> frames = new List<Frame>();
				foreach (PropertyReader frameProp in group.SelectAll("frame")) {
					Frame frame = new Frame();
					frame.id = frameProp.GetInt("sheetid");
					frame.time = frameProp.GetInt("time");
					frames.Add(frame);
				}
				current.frames = frames.ToArray();
				animations.Add(animType, current);
				if (this.currentAnimation == null) {
					this.currentAnimation = current;
					this.currentType = animType;
				}
			}
			LoadSpriteSheet(props.GetString("sheet"), game);

			if (currentAnimation == null) {
				throw new Game.SettingsException(string.Format("Animation descriptor file \"{0}.xml\" does not contain animation!", animationName));
			}
			this.frame = 0;
			CalculateRows();
		}

		/// <summary>
		/// Loads a still image from a sprite sheet. The sheet must be in the "img" folder.
		/// </summary>
		/// <param name="sheetName">The name of the sprite sheet.</param>
		/// <param name="game"></param>
		/// <param name="imageId">The zero-based index of the image in the sprite sheet.</param>
		public AnimatedGraphics(string sheetName, Game game, int imageId) {
			LoadSpriteSheet(sheetName, game);
			currentAnimation = new Animation();
			Frame frame = new Frame(imageId, 1);
			currentAnimation.frames = new Frame[] { frame };
			this.frame = 0;
			CalculateRows();
		}

		/// <summary>
		/// Selects a new animation to display, and sets it to display from the first frame.
		/// </summary>
		/// <param name="stateName">The name of the animation.</param>
		public void SetState(string stateName) {
			currentType.type = stateName;
			currentAnimation = animations[currentType];
			frame = 0;
			time = 0;
			CalculateRows();
		}

		/// <summary>
		/// Selects the direction for the animation.
		/// </summary>
		/// <param name="dir">The new direction.</param>
		public void SetDirection(Sprite.Dir dir) {
			currentType.dir = dir;
			currentAnimation = animations[currentType];
			CalculateRows();
		}

		int time = 0;
		/// <summary>
		/// Advances the animation by one frame.
		/// </summary>
		public void Advance() {
			++time;
			if (time >= currentFrame.time) {
				time = 0;
				if (frame >= currentAnimation.frames.Length - 1) {
					frame = 0;
				}
				else {
					++frame;
				}
			}
			CalculateRows();
		}

		/// <summary>
		/// Resets the animation to its default frame.
		/// </summary>
		public void Reset() {
			frame = 0;
			time = 0;
			CalculateRows();
		}

		int row, col;
		void CalculateRows() {
			currentFrame = currentAnimation.frames[frame];
			int id = currentFrame.id;
			row = id / columnsInRow;
			col = id % columnsInRow;
		}

		public override void Render(int x, int y, Surface dest) {
			dest.Blit(sheet, new Point(x - this.x, y - this.y), new Rectangle(col * width, row * height, width, height));
		}
	}
}
