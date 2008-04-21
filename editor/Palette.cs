using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KFIRPG.editor {
	internal partial class Palette: Form {

		public class CursorEventArgs: EventArgs {
			Cursor cursor;
			public Cursor Cursor { get { return cursor; } }

			public CursorEventArgs(Cursor newCursor) {
				cursor = newCursor;
			}
		}

		public event EventHandler<CursorEventArgs> PaletteSelectionChanged;

		public Palette() {
			InitializeComponent();
		}

		Project currentProject;
		SpriteSheet sheet = null;

		public new void Load(Project project) {
			currentProject = project;
			sheet = project.sheets["tiles"];
			tilePage.Invalidate();
		}

		int selectedrow = 0;
		int selectedcol = 0;

		private void tilesPanel_Paint(object sender, PaintEventArgs e) {
			if (sheet != null) {
				e.Graphics.DrawImage(sheet.sheet, new Point(0, sheet.size));
			}
		}

		private void tilesPanel_MouseClick(object sender, MouseEventArgs e) {
			selectedrow = e.Y / sheet.size;
			selectedcol = e.X / sheet.size;
			int tileId = selectedrow == 0 ? 0 : (selectedrow - 1) * sheet.cols + selectedcol + 1;
			if (PaletteSelectionChanged != null) {
				PaletteSelectionChanged(this, new CursorEventArgs(new TileCursor(tileId, currentProject)));
			}
		}

		private void passableButton_Click(object sender, EventArgs e) {
			if (PaletteSelectionChanged != null) {
				PaletteSelectionChanged(this, new CursorEventArgs(new PassabilityCursor(true, currentProject)));
			}
		}

		private void impassableButton_Click(object sender, EventArgs e) {
			if (PaletteSelectionChanged != null) {
				PaletteSelectionChanged(this, new CursorEventArgs(new PassabilityCursor(false, currentProject)));
			}
		}
	}
}
