using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using KFIRPG.corelib;

namespace KFIRPG.editor {
	public partial class EditorForm: Form {
		Project currentProject;
		Map currentMap;

		LayersToolbar layers;

		public EditorForm() {
			InitializeComponent();
			currentProject = Project.FromFiles("TestGame");
			currentMap = currentProject.maps[currentProject.startupMapName];

			layers = new LayersToolbar();
			layers.FormClosing += (sender, args) => {
				args.Cancel = true;
				layers.Hide();
				layersToolStripMenuItem.Checked = false;
			};
			layersToolStripMenuItem.Click += (sender, args) => {
				if (layersToolStripMenuItem.Checked) {
					layers.Hide();
					layersToolStripMenuItem.Checked = false;
				}
				else {
					layers.Show();
					layersToolStripMenuItem.Checked =true;
				}
			};

			for (int l = currentMap.layers.Count - 1; l >= 0; --l) {
				layers.checkedListBox.Items.Add("layer " + l.ToString(), true);
			}
			layers.checkedListBox.SelectedIndex = 0;

			layers.addbutton.Click += (sender, args) => {
			};
			layers.deletebutton.Click += (sender, args) => {
			};
			layers.upbutton.Click += (sender, args) => {
			};
			layers.downbutton.Click += (sender, args) => {
			};
			layers.checkedListBox.ItemCheck += UpdateEventHandler;
			layers.Show();

			this.Resize += (sender, args) => CalculateScrollbars();
			CalculateScrollbars();
			vScrollBar.ValueChanged += UpdateEventHandler;
			hScrollBar.ValueChanged += UpdateEventHandler;
		}

		public void CalculateScrollbars() {
			vScrollBar.Minimum = 0;
			vScrollBar.Maximum = currentMap.height;
			vScrollBar.LargeChange = mainPanel.Height / currentProject.tileSize;
			if (vScrollBar.Maximum >= vScrollBar.LargeChange && vScrollBar.Value >= vScrollBar.Maximum - vScrollBar.LargeChange) {
				vScrollBar.Value = vScrollBar.Maximum - vScrollBar.LargeChange;
			}
			hScrollBar.Minimum = 0;
			hScrollBar.Maximum = currentMap.width;
			hScrollBar.LargeChange = mainPanel.Width / currentProject.tileSize;
			if (hScrollBar.Maximum >= hScrollBar.LargeChange && hScrollBar.Value >= hScrollBar.Maximum - hScrollBar.LargeChange) {
				hScrollBar.Value = hScrollBar.Maximum - hScrollBar.LargeChange;
			}
		}

		private void mainPanel_Paint(object sender, PaintEventArgs e) {
			int x = -hScrollBar.Value * currentProject.tileSize;
			int y = -vScrollBar.Value * currentProject.tileSize;
			int size = currentProject.tileSize;
			for (int l = 0; l < currentMap.layers.Count;++l) {
				if (!layers.checkedListBox.CheckedIndices.Contains(currentMap.layers.Count - l - 1)) continue;
				Map.Layer layer = currentMap.layers[l];
				for (int i = 0; i < currentMap.width; ++i) {
					for (int j = 0; j < currentMap.height; ++j) {
						layer.tiles[i, j].gfx.Draw(x + i * size, y + j * size, e.Graphics);
						if (layer.objects[i, j] != null) {
							layer.objects[i, j].Gfx.Draw(x + i * size, y + j * size, e.Graphics);
						}
					}
				}
			}
		}

		private void UpdateEventHandler(object sender, EventArgs args) {
			mainPanel.Invalidate();
		}
	}
}
