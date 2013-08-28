using System;
using System.Collections.Generic;
using System.Text;
using KFIRPG.editor.Undo;

namespace KFIRPG.editor.Cursors {
	abstract class Cursor {
		public Cursor() {
			commandList = new UndoCommandList(Name);
		}

		/// <summary>
		/// Paints all content associated with the cursor, including the frame and preview content.
		/// </summary>
		/// <param name="g"></param>
		public void Draw(System.Drawing.Graphics g) {
			commandList.ForEach(c => PreDraw(c.X, c.Y, g));
			DrawCursor(g);
		}

		/// <summary>
		/// Paints the cursor frame.
		/// </summary>
		/// <param name="g"></param>
		protected abstract void DrawCursor(System.Drawing.Graphics g);
		/// <summary>
		/// Draws a preview of the content associated with the cursor.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="g"></param>
		protected virtual void PreDraw(int x, int y, System.Drawing.Graphics g) { }
		/// <summary>
		/// Adds the cursors actions to the current command list.
		/// </summary>
		/// <param name="layer">The layer the actions are performed on.</param>
		protected abstract void Edit(Map.Layer layer);

		public abstract string Name { get; }

		/// <summary>
		/// Fires when an action was performed with this cursor.
		/// </summary>
		public EventHandler<UndoEventArgs> CommandReady;
		private void OnCommand(UndoEventArgs e) {
			if (CommandReady != null) CommandReady(this, e);
		}

		UndoCommandList commandList;

		/// <summary>
		/// Creates a command from the passed undo and redo functions, and adds it to the undo list.
		/// </summary>
		/// <param name="doCmd"></param>
		/// <param name="redoCmd"></param>
		protected void AddCommand(UndoCommand.Function doCmd, UndoCommand.Function redoCmd) {
			UndoCommand c = new UndoCommand(x, y, tileX, tileY, doCmd, redoCmd);
			AddCommand(c);
		}

		/// <summary>
		/// Adds a command to the undo list.
		/// </summary>
		/// <param name="c"></param>
		private void AddCommand(UndoCommand c) {
			if (commandList.TrueForAll(o => !(o.TileX == c.TileX && o.TileY == c.TileY))) {
				commandList.Add(c);
			}
		}

		/// <summary>
		/// Starts performing the actions associated with the cursor.
		/// Some cursors can painted, thus the undo action will be finalized when the
		/// <see cref="EndEdit()"/> function is called.
		/// </summary>
		/// <param name="layer">The layer the cursor should act on.</param>
		public void DoEdit(Map.Layer layer) {
			this.Edit(layer);
		}
		/// <summary>
		/// Ends the action associated with the cursor. If it had any effect, a CommandReady
		/// event is fired.
		/// </summary>
		public void EndEdit() {
			if (commandList.IsEmpty) return;
			OnCommand(new UndoEventArgs(commandList));
			commandList = new UndoCommandList(Name);
		}

		protected int x = 0;
		protected int y = 0;
		protected int tileX = 0;
		protected int tileY = 0;
		/// <summary>
		/// Sets the coordinates of the cursor, usually due to mouse movement.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="tileX"></param>
		/// <param name="tileY"></param>
		public void UpdateCoords(int x, int y, int tileX, int tileY) {
			this.x = x;
			this.y = y;
			this.tileX = tileX;
			this.tileY = tileY;
		}
	}
}
