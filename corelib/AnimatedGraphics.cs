using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml;
using SdlDotNet.Graphics;

namespace KFIRPG.corelib {
	/// <summary>
	/// A graphic that can have multiple frames and/or states.
	/// </summary>
	class AnimatedGraphics: Graphics {
		Surface sheet;

		class Animation {
			public int start;
			public int count;
			public int timeout;
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
		int frame;
		int width;
		int height;
		int x;
		int y;
		int columnsInRow;

		/// <summary>
		/// Loads an animated graphics from a sprite sheet. The sheet must be in the "img" folder.
		/// </summary>
		/// <param name="sheetName">The name of the sprite sheet.</param>
		/// <param name="game"></param>
		public AnimatedGraphics(string sheetName, Game game) {
			sheet = game.loader.LoadSurface("img/" + sheetName + ".png");
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(game.loader.LoadText("img/" + sheetName + ".xml"));
			width = int.Parse(doc.SelectSingleNode("spritesheet/width").InnerText);
			height = int.Parse(doc.SelectSingleNode("spritesheet/height").InnerText);
			x = int.Parse(doc.SelectSingleNode("spritesheet/x").InnerText);
			y = int.Parse(doc.SelectSingleNode("spritesheet/y").InnerText);
			columnsInRow = sheet.Width / width;
			foreach (XmlNode node in doc.SelectNodes("spritesheet/animation")) {
				Animation current = new Animation();
				string type = node.Attributes["type"].InnerText;
				Sprite.Dir dir = (Sprite.Dir)Enum.Parse(typeof(Sprite.Dir), node.Attributes["dir"].InnerText, true);
				current.start = int.Parse(node.SelectSingleNode("start").InnerText);
				current.count = int.Parse(node.SelectSingleNode("count").InnerText);
				current.timeout = int.Parse(node.SelectSingleNode("timeout").InnerText);
				AnimationType animType = new AnimationType(type, dir);
				animations.Add(animType, current);
				if (this.currentAnimation == null) {
					this.currentAnimation = current;
					this.currentType = animType;
				}
			}
			if (currentAnimation == null) {
				currentAnimation = new Animation();
				currentAnimation.start = 0;
				currentAnimation.timeout = 1;
				currentAnimation.count = 1;
			}
			frame = 0;
			CalculateRows();
		}

		/// <summary>
		/// Loads a still image from a sprite sheet. The sheet must be in the "img" folder.
		/// </summary>
		/// <param name="sheetName">The name of the sprite sheet.</param>
		/// <param name="game"></param>
		/// <param name="imageId">The zero-based index of the image in the sprite sheet.</param>
		public AnimatedGraphics(string sheetName, Game game, int imageId)
			: this(sheetName, game) {
			currentAnimation = new Animation();
			currentAnimation.start = imageId;
			currentAnimation.timeout = 1;
			currentAnimation.count = 1;
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
			if (time >= currentAnimation.timeout) {
				time = 0;
				if (frame >= currentAnimation.count - 1) {
					frame = 0;
				}
				else {
					++frame;
				}
			}
			CalculateRows();
		}

		int row, col;
		void CalculateRows() {
			int id = currentAnimation.start + frame;
			row = id / columnsInRow;
			col = id % columnsInRow;
		}

		public override void Blit(int x, int y, Surface dest) {
			dest.Blit(sheet, new Point(x - this.x, y - this.y), new Rectangle(col * width, row * height, width, height));
		}
	}
}
