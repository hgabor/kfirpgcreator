using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml;
using SdlDotNet.Graphics;

namespace KFIRPG.corelib {
	public class Animation {
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
		int size;
		const int columnsInRow = 8;

		public Animation(string sheetName, int size, Game game) {
			this.size = size;
			sheet = new Surface(game.loader.LoadBitmap("img/"+sheetName+".png"));
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(game.loader.LoadText("img/"+sheetName+".xml"));
			foreach (XmlNode node in doc.SelectNodes("spritesheet/image")) {
				State current = new State();
				string name = node.Attributes["type"].InnerText;
				current.start = int.Parse(node.SelectSingleNode("start").InnerText) - 1;
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
		}

		int time = 0;
		void Advance() {
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
		}

		int row, col;
		void CalculateRows() {
			int id = currentState.start + subState * currentState.interval + frame;
			row = id / columnsInRow;
			col = id % columnsInRow;
		}

		public void Blit(int x, int y, Surface dest) {
			dest.Blit(sheet, new Point(x, y), new Rectangle(col * size, row * size, size, size));
			Advance();
			CalculateRows();
		}



		/*
		abstract class State: ICloneable {
			public abstract void Offset(int offset);
			public abstract void Blit(int x, int y, Surface dest);
			public abstract object Clone();
		}
		class Normal: State {
			int id;
			int row;
			int column;
			const int ColumnsInRow = 8;
			int size;
			Surface surface;
			public Normal(string sheet, int id, int size, Game game) {
				this.id = id;
				this.row = id / ColumnsInRow;
				this.column = id % ColumnsInRow;
				this.size = size;
				Bitmap bm = game.loader.LoadBitmap(sheet);
				surface = new Surface(bm);
			}
			public override void Offset(int offset) {
				id += offset;
				this.row = id / ColumnsInRow;
				this.column = id % ColumnsInRow;
			}
			public override void Blit(int x, int y, Surface dest) {
				dest.Blit(surface, new Point(x, y), new Rectangle(column * size, row * size, size, size));
			}
			private Normal() { }
			public override object Clone() {
				Normal ret = new Normal();
				ret.id = this.id;
				ret.row = this.row;
				ret.column = this.column;
				ret.size = this.size;
				ret.surface = this.surface;
				return ret;
			}
		}
		class Empty: State {
			public override void Offset(int offset) { }
			public override void Blit(int x, int y, Surface dest) { }
			public override object Clone() { return this; }
		}

		State state;
		public Animation(string sheet, int id, int size, Game game) {
			if (id == 0) state = new Empty();
			else state = new Normal(sheet, id - 1, size, game);
		}

		private Animation(State prevState, int offset) {
			state = (State)prevState.Clone();
			state.Offset(offset);
		}

		public Animation getState(int offset) {
			return new Animation(this.state, offset);
		}

		public void Blit(int x, int y, Surface dest) {
			state.Blit(x, y, dest);
		}*/
		 
	}
}
