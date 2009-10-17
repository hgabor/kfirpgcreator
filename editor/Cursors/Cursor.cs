using System;
using System.Collections.Generic;
using System.Text;
using KFIRPG.editor.Commands;

namespace KFIRPG.editor.Cursors {
	abstract class Cursor {
		public Cursor() {
			commandList = new CommandList(Name);
		}

		public virtual void Click(Map.Layer layer) { }
		public abstract void Draw(System.Drawing.Graphics g);

		/// <summary>
		/// Returns a command containing the action to be performed
		/// </summary>
		/// <param name="layer"></param>
		/// <returns></returns>
		protected virtual void Edit(Map.Layer layer) { }

		public virtual string Name { get { return ""; } }

		public EventHandler<CommandEventArgs> CommandReady;
		private void OnCommand(CommandEventArgs e) {
			if (CommandReady != null) CommandReady(this, e);
		}

		protected CommandList commandList;



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
