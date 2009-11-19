using System;
using System.Collections.Generic;
using System.Text;
using KFIRPG.editor.Commands;

namespace KFIRPG.editor.Cursors {
	abstract class Cursor {
		public Cursor() {
			commandList = new CommandList(Name);
		}

		public void Draw(System.Drawing.Graphics g) {
			commandList.ForEach(c => PreDraw(c.X, c.Y, g));
			DrawCursor(g);
		}

		protected abstract void DrawCursor(System.Drawing.Graphics g);
		protected virtual void PreDraw(int x, int y, System.Drawing.Graphics g) { }
		protected abstract void Edit(Map.Layer layer);

		public abstract string Name { get; }

		public EventHandler<CommandEventArgs> CommandReady;
		private void OnCommand(CommandEventArgs e) {
			if (CommandReady != null) CommandReady(this, e);
		}

		CommandList commandList;

		protected void AddCommand(Command.Function doCmd, Command.Function redoCmd) {
			Command c = new Command(x, y, tileX, tileY, doCmd, redoCmd);
			AddCommand(c);
		}

		private void AddCommand(Command c) {
			if (commandList.TrueForAll(o => !(o.TileX == c.TileX && o.TileY == c.TileY))) {
				commandList.Add(c);
			}
		}

		public void DoEdit(Map.Layer layer) {
			this.Edit(layer);
		}
		public void EndEdit() {
			if (commandList.IsEmpty) return;
			OnCommand(new CommandEventArgs(commandList));
			commandList = new CommandList(Name);
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
