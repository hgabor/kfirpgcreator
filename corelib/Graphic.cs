using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using SdlDotNet.Graphics;

namespace KFIRPG.corelib {
	public class Graphic {
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
		public Graphic(string sheet, int id, int size, Game game) {
			if (id == 0) state = new Empty();
			else state = new Normal(sheet, id - 1, size, game);
		}

		private Graphic(State prevState, int offset) {
			state = (State)prevState.Clone();
			state.Offset(offset);
		}

		public Graphic getState(int offset) {
			return new Graphic(this.state, offset);
		}

		public void Blit(int x, int y, Surface dest) {
			state.Blit(x, y, dest);
		}
	}
}
