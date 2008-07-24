using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KFIRPG.editor {
	internal partial class Palette: DockableForm {

		public class CursorEventArgs: EventArgs {
			Cursor cursor;
			public Cursor Cursor { get { return cursor; } }

			public CursorEventArgs(Cursor newCursor) {
				cursor = newCursor;
			}
		}

		public event EventHandler<CursorEventArgs> PaletteSelectionChanged;

		private void OnPaletteSelectionChanged(CursorEventArgs args) {
			if (PaletteSelectionChanged != null) {
				PaletteSelectionChanged(this, args);
			}
		}

		public Palette() {
			InitializeComponent();
		}

		Project currentProject;
		SpriteSheet sheet = null;

		internal new void Load(Project project) {
			currentProject = project;
			sheet = project.sheets["tiles"];

			hScrollBar.Enabled = true;
			hScrollBar.Minimum = 0;
			hScrollBar.Maximum = sheet.Cols * sheet.spriteWidth;
			hScrollBar.LargeChange = tilesPanel.Width;
			hScrollBar.SmallChange = sheet.spriteWidth;
			hScrollBar.ValueChanged += (sender, args) => {
				this.offsetX = hScrollBar.Value;
				tilesPanel.Invalidate();
			};
			vScrollBar.Enabled = true;
			vScrollBar.Minimum = 0;
			vScrollBar.Maximum = sheet.sheet.Height + sheet.spriteHeight;
			vScrollBar.LargeChange = tilesPanel.Height;
			vScrollBar.SmallChange = sheet.spriteHeight;
			vScrollBar.ValueChanged += (sender, args) => {
				this.offsetY = vScrollBar.Value;
				tilesPanel.Invalidate();
			};
			tilesPanel.Resize += (sender, args) => {
				hScrollBar.LargeChange = tilesPanel.Width;
				vScrollBar.LargeChange = tilesPanel.Height;
			};

			LoadObjects();

			tilePage.Invalidate();
		}

		public void LoadObjects() {
			objectsListBox.Items.Clear();
			objectsListBox.Items.Add("DELETE OBJECT");
			foreach (Sprite sprite in currentProject.sprites.Values) {
				objectsListBox.Items.Add(sprite);
			}
			objectsListBox.SelectedIndex = -1;
		}

		int selectedrow = 0;
		int selectedcol = 0;
		int offsetX = 0;
		int offsetY = 0;
		bool tileSelected = false;

		private void tilesPanel_Paint(object sender, PaintEventArgs e) {
			if (sheet != null) {
				e.Graphics.DrawImage(sheet.sheet, new Point(-offsetX, sheet.spriteHeight - offsetY));
			}
			if (tileSelected) {
				if (selectedrow != 0) {
					e.Graphics.DrawRectangle(Pens.Red, selectedcol * sheet.spriteWidth - offsetX, selectedrow * sheet.spriteHeight - offsetY, sheet.spriteWidth - 1, sheet.spriteHeight - 1);
				}
				else {
					e.Graphics.DrawRectangle(Pens.Red, -offsetX, -offsetY, sheet.spriteWidth - 1, sheet.spriteHeight - 1);
				}
			}
		}

		private void tilesPanel_MouseClick(object sender, MouseEventArgs e) {
			selectedrow = (e.Y + offsetY) / sheet.spriteHeight;
			selectedcol = (e.X + offsetX) / sheet.spriteWidth;
			int tileId = selectedrow == 0 || selectedcol >= sheet.Cols ? 0 : (selectedrow - 1) * sheet.Cols + selectedcol + 1;
			OnPaletteSelectionChanged(new CursorEventArgs(new TileCursor(tileId, currentProject)));

			objectsListBox.SelectedIndex = -1;
			tileSelected = true;
			tilesPanel.Invalidate();
		}

		private void passableButton_Click(object sender, EventArgs e) {
			OnPaletteSelectionChanged(new CursorEventArgs(new PassabilityCursor(true, currentProject)));

			objectsListBox.SelectedIndex = -1;
			tileSelected = false;
		}

		private void impassableButton_Click(object sender, EventArgs e) {
			OnPaletteSelectionChanged(new CursorEventArgs(new PassabilityCursor(false, currentProject)));

			objectsListBox.SelectedIndex = -1;
			tileSelected = false;
		}

		private void SelectObjectCursor_Handler(object sender, EventArgs e) {
			if (objectsListBox.SelectedIndex == 0) {
				Cursor cursor = new DeleteSpriteCursor();
				OnPaletteSelectionChanged(new CursorEventArgs(cursor));

				tileSelected = false;
			}
			else if (objectsListBox.SelectedIndex != -1) {
				Sprite sprite = (Sprite)objectsListBox.SelectedItem;
				Cursor cursor = new SpriteCursor(sprite, clearScriptCheckBox.Checked, currentProject);
				OnPaletteSelectionChanged(new CursorEventArgs(cursor));

				tileSelected = false;
			}
		}

		private void ladderButton_Click(object sender, EventArgs e) {
			OnPaletteSelectionChanged(new CursorEventArgs(new PlaceLadderCursor(currentProject)));

			objectsListBox.SelectedIndex = -1;
			tileSelected = false;
		}

		private void removeLadderButton_Click(object sender, EventArgs e) {
			OnPaletteSelectionChanged(new CursorEventArgs(new RemoveLadderCursor(currentProject)));

			objectsListBox.SelectedIndex = -1;
			tileSelected = false;
		}

		private void addObjectButton_Click(object sender, EventArgs e) {
			using (SpriteDialog sd = new SpriteDialog(currentProject, null)) {
				sd.ShowDialog(this);
			}
			LoadObjects();
		}

		private void objectsListBox_DoubleClick(object sender, EventArgs e) {
			if (objectsListBox.SelectedIndex == -1 || objectsListBox.SelectedIndex == 0) return;
			using (SpriteDialog sd = new SpriteDialog(currentProject, (Sprite)objectsListBox.SelectedItem)) {
				sd.ShowDialog(this);
			}
			LoadObjects();
		}
	}
}
