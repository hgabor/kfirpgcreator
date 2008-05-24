﻿using System;
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

		private void LoadSpriteSheet(string sheetName, Game game) {
			sheet = game.loader.LoadSurface("img/" + sheetName + ".png");
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(game.loader.LoadText("img/" + sheetName + ".xml"));
			width = int.Parse(doc.SelectSingleNode("spritesheet/width").InnerText);
			height = int.Parse(doc.SelectSingleNode("spritesheet/height").InnerText);
			x = int.Parse(doc.SelectSingleNode("spritesheet/x").InnerText);
			y = int.Parse(doc.SelectSingleNode("spritesheet/y").InnerText);
			columnsInRow = sheet.Width / width;
		}

		/// <summary>
		/// Loads an animated graphics. The descriptor file for the animation must be
		/// in the "animations" folder.
		/// </summary>
		/// <param name="animationName">The name of the animation.</param>
		/// <param name="game"></param>
		public AnimatedGraphics(string animationName, Game game) {
			XmlDocument docAnim = new XmlDocument();
			docAnim.LoadXml(game.loader.LoadText("animations/" + animationName + ".xml"));
			foreach (XmlNode groupNode in docAnim.SelectNodes("/animation/group")) {
				Animation current = new Animation();
				string[] animName = groupNode.Attributes["name"].InnerText.Split('.');
				string type = animName[0];
				Sprite.Dir dir = (Sprite.Dir)Enum.Parse(typeof(Sprite.Dir), animName[1], true);
				AnimationType animType = new AnimationType(type, dir);
				List<Frame> frames = new List<Frame>();
				foreach (XmlNode frameNode in groupNode.SelectNodes("frame")) {
					Frame frame = new Frame();
					frame.id = int.Parse(frameNode.SelectSingleNode("sheetid").InnerText);
					frame.time = int.Parse(frameNode.SelectSingleNode("time").InnerText);
					frames.Add(frame);
				}
				current.frames = frames.ToArray();
				animations.Add(animType, current);
				if (this.currentAnimation == null) {
					this.currentAnimation = current;
					this.currentType = animType;
				}
			}
			LoadSpriteSheet(docAnim.SelectSingleNode("/animation/sheet").InnerText, game);

			/*foreach (XmlNode node in doc.SelectNodes("spritesheet/animation")) {
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
			}*/
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

		int row, col;
		void CalculateRows() {
			currentFrame = currentAnimation.frames[frame];
			int id = currentFrame.id;
			row = id / columnsInRow;
			col = id % columnsInRow;
		}

		public override void Blit(int x, int y, Surface dest) {
			dest.Blit(sheet, new Point(x - this.x, y - this.y), new Rectangle(col * width, row * height, width, height));
		}
	}
}
