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

			hScrollBar.Enabled = true;
			hScrollBar.Minimum = 0;
			hScrollBar.Maximum = sheet.cols * sheet.size;
			hScrollBar.LargeChange = tilesPanel.Width;
			hScrollBar.SmallChange = sheet.size;
			hScrollBar.ValueChanged += (sender, args) => {
				this.offsetX = hScrollBar.Value;
				tilesPanel.Invalidate();
			};
			vScrollBar.Enabled = true;
			vScrollBar.Minimum = 0;
			vScrollBar.Maximum = sheet.sheet.Height + sheet.size;
			vScrollBar.LargeChange = tilesPanel.Height;
			vScrollBar.SmallChange = sheet.size;
			vScrollBar.ValueChanged += (sender, args) => {
				this.offsetY = vScrollBar.Value;
				tilesPanel.Invalidate();
			};
			tilesPanel.Resize += (sender, args) => {
				hScrollBar.LargeChange = tilesPanel.Width;
				vScrollBar.LargeChange = tilesPanel.Height;
			};

			tilePage.Invalidate();
		}

		int selectedrow = 0;
		int selectedcol = 0;
		int offsetX = 0;
		int offsetY = 0;

		private void tilesPanel_Paint(object sender, PaintEventArgs e) {
			if (sheet != null) {
				e.Graphics.DrawImage(sheet.sheet, new Point(-offsetX, sheet.size - offsetY));
			}
			if (selectedrow != 0) {
				e.Graphics.DrawRectangle(Pens.Red, selectedcol * sheet.size - offsetX, selectedrow * sheet.size - offsetY, sheet.size - 1, sheet.size - 1);
			}
			else {
				e.Graphics.DrawRectangle(Pens.Red, -offsetX, -offsetY, sheet.size - 1, sheet.size - 1);
			}
		}

		private void tilesPanel_MouseClick(object sender, MouseEventArgs e) {
			selectedrow = (e.Y + offsetY) / sheet.size;
			selectedcol = (e.X + offsetX) / sheet.size;
			int tileId = selectedrow == 0 || selectedcol >= sheet.cols ? 0 : (selectedrow - 1) * sheet.cols + selectedcol + 1;
			if (PaletteSelectionChanged != null) {
				PaletteSelectionChanged(this, new CursorEventArgs(new TileCursor(tileId, currentProject)));
			}
			tilesPanel.Invalidate();
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
