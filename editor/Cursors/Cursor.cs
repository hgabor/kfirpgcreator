using System;
using System.Collections.Generic;
using System.Text;
using KFIRPG.editor.Undo;

namespace KFIRPG.editor.Cursors {
	abstract class Cursor {
		public Cursor() {
			commandList = new UndoCommandList(Name);
		}

		public void Draw(System.Drawing.Graphics g) {
			commandList.ForEach(c => PreDraw(c.X, c.Y, g));
			DrawCursor(g);
		}

		protected abstract void DrawCursor(System.Drawing.Graphics g);
		protected virtual void PreDraw(int x, int y, System.Drawing.Graphics g) { }
		protected abstract void Edit(Map.Layer layer);

		public abstract string Name { get; }

		public EventHandler<UndoEventArgs> CommandReady;
		private void OnCommand(UndoEventArgs e) {
			if (CommandReady != null) CommandReady(this, e);
		}

		UndoCommandList commandList;

		protected void AddCommand(UndoCommand.Function doCmd, UndoCommand.Function redoCmd) {
			UndoCommand c = new UndoCommand(x, y, tileX, tileY, doCmd, redoCmd);
			AddCommand(c);
		}

		private void AddCommand(UndoCommand c) {
			if (commandList.TrueForAll(o => !(o.TileX == c.TileX && o.TileY == c.TileY))) {
				commandList.Add(c);
			}
		}

		public void DoEdit(Map.Layer layer) {
			this.Edit(layer);
		}
		public void EndEdit() {
			if (commandList.IsEmpty) return;
			OnCommand(new UndoEventArgs(commandList));
			commandList = new UndoCommandList(Name);
		}

		protected int x = 0;
		protected int y = 0;
		protected int tileX = 0;
		protected int tileY = 0;
		public void UpdateCoords(int x, int y, int tileX, int tileY) {
			this.x = x;
			this.y = y;
			this.tileX = tileX;
			this.tileY = tileY;
		}
	}
}
