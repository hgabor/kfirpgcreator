﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml;
using SdlDotNet.Graphics;

namespace KFIRPG.corelib {
	class AnimatedGraphics: Graphics {
		Surface sheet;
		class State {
			public int start;
			public int count;
			public int interval;
			public int timeout;
		}
		Dictionary<string, State> states = new Dictionary<string, State>();
		State currentState;
		int subState;
		int frame;
		int width;
		int height;
		int x;
		int y;
		int columnsInRow;

		public AnimatedGraphics(string sheetName, Game game) {
			//sheet = new Surface(game.loader.LoadBitmap("img/" + sheetName + ".png"));
			sheet = game.loader.LoadSurface("img/" + sheetName + ".png");
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(game.loader.LoadText("img/" + sheetName + ".xml"));
			width = int.Parse(doc.SelectSingleNode("spritesheet/width").InnerText);
			height = int.Parse(doc.SelectSingleNode("spritesheet/height").InnerText);
			x = int.Parse(doc.SelectSingleNode("spritesheet/x").InnerText);
			y = int.Parse(doc.SelectSingleNode("spritesheet/y").InnerText);
			columnsInRow = sheet.Width / width;
			foreach (XmlNode node in doc.SelectNodes("spritesheet/image")) {
				State current = new State();
				string name = node.Attributes["type"].InnerText;
				current.start = int.Parse(node.SelectSingleNode("start").InnerText);
				current.count = int.Parse(node.SelectSingleNode("count").InnerText);
				current.interval = int.Parse(node.SelectSingleNode("interval").InnerText);
				current.timeout = int.Parse(node.SelectSingleNode("timeout").InnerText);
				states.Add(name, current);
			}
			currentState = states["still"];
			subState = 0;
			frame = 0;
			CalculateRows();
		}

		public void SetState(string stateName, int subState) {
			currentState = states[stateName];
			this.subState = subState;
			frame = 0;
			time = 0;
			CalculateRows();
		}

		int time = 0;
		public void Advance() {
			++time;
			if (time == currentState.timeout) {
				time = 0;
				if (frame == currentState.count - 1) {
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
			int id = currentState.start + subState * currentState.interval + frame;
			row = id / columnsInRow;
			col = id % columnsInRow;
		}

		public override void Blit(int x, int y, Surface dest) {
			dest.Blit(sheet, new Point(x - this.x, y - this.y), new Rectangle(col * width, row * height, width, height));
		}
	}
}
