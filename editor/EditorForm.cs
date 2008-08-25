using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using KFIRPG.corelib;

namespace KFIRPG.editor {
	public partial class EditorForm: Form {
		Project currentProject;
		Map currentMap;
		int CurrentLayerIndex {
			get {
				return currentMap.layers.Count - layers.checkedListBox.SelectedIndex - 1;
			}
		}
		Map.Layer CurrentLayer {
			get {
				return currentMap.layers[CurrentLayerIndex];
			}
		}
		string savePath = null;
		MRU mru = new MRU(6, "recent");

		LayersToolbar layers;
		AudioLibrary audio;
		ImageLibrary images;
		Palette palette;
		AnimationLibrary animations;
		DoubleBufferedPanel mainPanel;
		HScrollBar hScrollBar;
		VScrollBar vScrollBar;

		Cursor cursor;

		private void BindFormWithMenuItem(DockableForm form, ToolStripMenuItem menuitem) {
			form.DockHandler.HideOnClose = true;
			form.DockHandler.DockStateChanged += (sender, args) => {
				if (form.DockHandler.IsHidden) {
					menuitem.Checked = false;
				}
			};
			menuitem.Click += (sender, args) => {
				if (menuitem.Checked) {
					form.DockHandler.Hide();
					menuitem.Checked = false;
				}
				else {
					form.DockHandler.Show(dockPanel, WeifenLuo.WinFormsUI.Docking.DockState.DockRight);
					menuitem.Checked = true;
				}
			};
		}

		public EditorForm() {
			InitializeComponent();

			MainPanelForm mainPanelForm = new MainPanelForm();
			mainPanel = mainPanelForm.mainPanel;
			hScrollBar = mainPanelForm.hScrollBar;
			vScrollBar = mainPanelForm.vScrollBar;
			mainPanelForm.DockHandler.Show(dockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
			mainPanel.MouseClick += this.mainPanel_MouseClick;
			mainPanel.Paint += this.mainPanel_Paint;
			mainPanel.MouseDown += this.mainPanel_MouseDown;
			mainPanel.MouseUp += this.mainPanel_MouseUp;
			mainPanel.MouseMove += this.mainPanel_MouseMove;

			layers = new LayersToolbar();
			BindFormWithMenuItem(layers, layersToolStripMenuItem);
			layers.checkedListBox.ItemCheck += UpdateEventHandler;
			layers.checkedListBox.SelectedIndexChanged += UpdateEventHandler;

			audio = new AudioLibrary();
			BindFormWithMenuItem(audio, audioLibraryToolStripMenuItem);

			images = new ImageLibrary();
			BindFormWithMenuItem(images, imageLibraryToolStripMenuItem);

			palette = new Palette();
			BindFormWithMenuItem(palette, paletteToolStripMenuItem);
			palette.PaletteSelectionChanged += (sender, args) => {
				this.cursor = args.Cursor;
			};

			animations = new AnimationLibrary();
			BindFormWithMenuItem(animations, animationLibraryToolStripMenuItem);

			specialViewComboBox.SelectedIndex = 0;

			mainPanel.Resize += (sender, args) => CalculateScrollbars();
			CalculateScrollbars();
			vScrollBar.ValueChanged += UpdateEventHandler;
			hScrollBar.ValueChanged += UpdateEventHandler;

			RecreateMRUList();

			cursor = new TileCursor();
		}

		public void CalculateScrollbars() {
			if (currentMap == null) {
				vScrollBar.Enabled = false;
				hScrollBar.Enabled = false;
				return;
			}
			vScrollBar.Enabled = true;
			vScrollBar.Minimum = 0;
			vScrollBar.Maximum = currentMap.height - 1;
			vScrollBar.LargeChange = mainPanel.Height / currentProject.tileSize;
			if (vScrollBar.Maximum >= vScrollBar.LargeChange && vScrollBar.Value >= vScrollBar.Maximum - vScrollBar.LargeChange) {
				vScrollBar.Value = vScrollBar.Maximum - vScrollBar.LargeChange;
			}
			vScrollBar.Enabled = vScrollBar.LargeChange <= vScrollBar.Maximum;
			hScrollBar.Enabled = true;
			hScrollBar.Minimum = 0;
			hScrollBar.Maximum = currentMap.width - 1;
			hScrollBar.LargeChange = mainPanel.Width / currentProject.tileSize;
			if (hScrollBar.Maximum >= hScrollBar.LargeChange && hScrollBar.Value >= hScrollBar.Maximum - hScrollBar.LargeChange) {
				hScrollBar.Value = hScrollBar.Maximum - hScrollBar.LargeChange;
			}
			hScrollBar.Enabled = hScrollBar.LargeChange <= hScrollBar.Maximum;
		}

		private void EnableControls() {
			saveProjectToolStripMenuItem.Enabled = true;
			saveProjectAsToolStripMenuItem.Enabled = true;
			foreach (ToolStripItem item in menuStrip.Items) {
				item.Enabled = true;
			}
			//layers.Show();
			//audio.Show();
			//images.Show();
			//animations.Show();
			//palette.Show();
		}

		private void RecreateMRUList() {
			mruToolStripMenuItem.DropDownItems.Clear();
			foreach (string item in mru) {
				ToolStripMenuItem menuItem = new ToolStripMenuItem();
				menuItem.Text = item;
				menuItem.Click += (sender, args) => {
					switch (MessageBox.Show(this, "Your project might have unsaved changes. Click yes to save the changes, no to continue without saving, or cancel if you do not want to load the project.",
	"Exit", MessageBoxButtons.YesNoCancel)) {
						case DialogResult.Yes:
							if (this.savePath == null) {
								if (this.SetSaveLocation()) Save();
							}
							else {
								Save();
							}
							goto case DialogResult.No;
						case DialogResult.No:
							string path = menuItem.Text;
							if (!Directory.Exists(path)) {
								if (MessageBox.Show(this, "The specified directory does not exist!\nDo you want to remove it from the list?",
									"Invalid Directory", MessageBoxButtons.YesNo) == DialogResult.Yes) {
									mru.Remove(path);
									RecreateMRUList();
								}
							}
							else {
								savePath = path;
								if (!Load()) {
									if (MessageBox.Show(this, "Do you want to remove it from the list?",
										"Invalid Directory", MessageBoxButtons.YesNo) == DialogResult.Yes) {
										mru.Remove(path);
										RecreateMRUList();
										savePath = null;
									}
								}
							}
							break;
					}
				};
				mruToolStripMenuItem.DropDownItems.Add(menuItem);
			}
		}

		private new bool Load() {
			try {
				currentProject = Project.FromFiles(savePath);

			}
			catch (Project.LoadException) {
				MessageBox.Show(this, "The selected folder is not a project, or it is corrupted.", "Invalid folder");
				savePath = null;
				return false;
			}

			currentMap = currentProject.maps[currentProject.startupMapName];

			audio.Load(currentProject);
			images.Load(currentProject);
			palette.Load(currentProject);
			layers.Load(currentMap);
			animations.Load(currentProject);

			layers.addbutton.Click += (sender, args) => {
				using (ComposedForm form = new ComposedForm("New layer", ComposedForm.Parts.Name)) {
					if (form.ShowDialog() == DialogResult.OK) {
						currentMap.CreateNewLayer(form.GetName());
						layers.Load(currentMap);
						mainPanel.Invalidate();
					}
				}
			};
			layers.deletebutton.Click += (sender, args) => {
				if (currentMap.layers.Count == 1) {
					MessageBox.Show("Cannot remove last layer!", "Last layer");
				}
				else {
					int count = 0;
					for (int i = 0; i < currentMap.ladders.GetLength(0); ++i) {
						for (int j = 0; j < currentMap.ladders.GetLength(1); ++j) {
							Map.Ladder ladder = currentMap.ladders[i, j];
							if (ladder != null && (ladder.baseLayer == CurrentLayer || ladder.topLayer == CurrentLayer)) ++count;
						}
					}
					string message;
					if (count == 0) {
						message = string.Format("Do you want to remove layer \"{0}\"?", layers.checkedListBox.SelectedItem);
					}
					else {
						message = string.Format("Do you want to remove layer \"{0}\"? The layer contains {1} ladder(s), they will be removed!", layers.checkedListBox.SelectedItem, count);
					}
					if (MessageBox.Show(layers, message, "Remove layer", MessageBoxButtons.YesNo) == DialogResult.Yes) {
						for (int i = 0; i < currentMap.ladders.GetLength(0); ++i) {
							for (int j = 0; j < currentMap.ladders.GetLength(1); ++j) {
								Map.Ladder ladder = currentMap.ladders[i, j];
								if (ladder != null && (ladder.baseLayer == CurrentLayer || ladder.topLayer == CurrentLayer)) currentMap.ladders[i, j] = null;
							}
						}
						currentMap.layers.Remove(CurrentLayer);
						layers.Load(currentMap);
						mainPanel.Invalidate();
					}
				}
			};
			layers.upbutton.Click += (sender, args) => {
				int index = layers.checkedListBox.SelectedIndex;
				if (index == 0) return;
				int lindex = currentMap.layers.Count - index - 1;
				Map.Layer layer = currentMap.layers[lindex];
				currentMap.layers.RemoveAt(lindex);
				currentMap.layers.Insert(lindex + 1, layer);
				layers.Load(currentMap);
				layers.checkedListBox.SelectedIndex = index - 1;
				mainPanel.Invalidate();
			};
			layers.downbutton.Click += (sender, args) => {
				int index = layers.checkedListBox.SelectedIndex;
				if (index == currentMap.layers.Count - 1) return;
				int lindex = currentMap.layers.Count - index - 1;
				Map.Layer layer = currentMap.layers[lindex];
				currentMap.layers.RemoveAt(lindex);
				currentMap.layers.Insert(lindex - 1, layer);
				layers.Load(currentMap);
				layers.checkedListBox.SelectedIndex = index + 1;
				mainPanel.Invalidate();
			};

			foreach(string mapName in currentProject.maps.Keys) {
				mapComboBox.Items.Add(mapName);
			}
			mapComboBox.SelectedIndex = mapComboBox.Items.IndexOf(currentMap.name);
			mapComboBox.SelectedIndexChanged += (sender, args) => {
				ChangeCurrentMap((string)mapComboBox.SelectedItem);
			};

			mru.Add(savePath);
			RecreateMRUList();
			EnableControls();
			mainPanel.Invalidate();
			return true;
		}

		private void Save() {
			if (this.savePath == null && !SetSaveLocation()) {
				return;
			}
			Saver saver = new FileSaver(this.savePath);
			this.currentProject.Save(saver);
		}

		private bool SetSaveLocation() {
			if (folderDialog.ShowDialog() == DialogResult.OK) {
				this.savePath = folderDialog.SelectedPath;
				return true;
			}
			else return false;
		}

		private void ChangeCurrentMap(string mapName) {
			currentMap = currentProject.maps[mapName];
			mapComboBox.SelectedIndex = mapComboBox.Items.IndexOf(mapName);
			layers.Load(currentMap);
		}

		readonly Brush stepEventBrush = new SolidBrush(Color.FromArgb(128, Color.Orange));
		readonly Pen stepEventPen = Pens.Orange;
		readonly Brush actionBrush = new SolidBrush(Color.FromArgb(128, Color.Yellow));
		readonly Pen actionPen = Pens.Yellow;
		readonly Brush passBrush = new SolidBrush(Color.FromArgb(128, Color.Red));
		readonly Pen passPen = Pens.Red;
		readonly Pen mapFramePen = Pens.White;
		readonly Brush locationBrush = new SolidBrush(Color.FromArgb(128, Color.Blue));
		readonly Pen locationPen = Pens.Blue;
		readonly Brush LadderBrush = new System.Drawing.Drawing2D.HatchBrush(System.Drawing.Drawing2D.HatchStyle.Horizontal, Color.Purple, Color.FromArgb(128, Color.Purple));
		readonly Pen ladderPen = Pens.Purple;

		private void DrawBox(int x, int y, int width, int height, Brush bg, Pen frame, Graphics g) {
			g.FillRectangle(bg, x, y, width, height);
			g.DrawRectangle(frame, x, y, width, height);
		}

		private void mainPanel_Paint(object sender, PaintEventArgs e) {
			if (currentProject == null) return;
			int x = -hScrollBar.Value * currentProject.tileSize;
			int y = -vScrollBar.Value * currentProject.tileSize;
			int size = currentProject.tileSize;
			for (int l = 0; l < currentMap.layers.Count; ++l) {
				if (!layers.checkedListBox.CheckedIndices.Contains(currentMap.layers.Count - l - 1)) continue;
				bool selectedLayer = layers.checkedListBox.SelectedIndex == currentMap.layers.Count - l - 1;
				Map.Layer layer = currentMap.layers[l];
				for (int i = 0; i < currentMap.width; ++i) {
					for (int j = 0; j < currentMap.height; ++j) {
						layer.tiles[i, j].gfx.Draw(x + i * size, y + j * size, e.Graphics);
						if (layer.objects[i, j] != null) {
							layer.objects[i, j].Gfx.Draw(x + i * size, y + j * size, e.Graphics);
						}

						if (specialViewComboBox.SelectedIndex == 1 || selectedLayer) {

							if (!layer.tiles[i, j].passable && passabilityButton.Checked) {
								DrawBox(x + i * size, y + j * size, size - 1, size - 1, passBrush, passPen, e.Graphics);
							}

							if (!string.IsNullOrEmpty(layer.tiles[i, j].onStep)) {
								DrawBox(x + i * size, y + j * size, size - 1, size - 1, stepEventBrush, stepEventPen, e.Graphics);
							}
							if (!string.IsNullOrEmpty(layer.tiles[i, j].locationName)) {
								DrawBox(x + i * size, y + j * size, size - 1, size - 1, locationBrush, locationPen, e.Graphics);
							}
							if (layer.objects[i, j] != null && !string.IsNullOrEmpty(layer.objects[i, j].actionScript)) {
								DrawBox(x + i * size, y + j * size, size - 1, size - 1, actionBrush, actionPen, e.Graphics);
							}
						}
					}
				}
			}
			for (int i = 0; i < currentMap.width; ++i) {
				for (int j = 0; j < currentMap.height; ++j) {
					if (currentMap.ladders[i, j] != null) {
						DrawBox(x + i * size, y + (j - 1) * size, size - 1, 2 * size - 1, LadderBrush, ladderPen, e.Graphics);
					}
				}
			}
			e.Graphics.DrawRectangle(mapFramePen, new Rectangle(x, y, currentMap.width * size - 1, currentMap.height * size - 1));
			cursor.Draw(e.Graphics);
		}

		private void UpdateEventHandler(object sender, EventArgs args) {
			mainPanel.Invalidate();
		}

		private void saveProjectToolStripMenuItem_Click(object sender, EventArgs e) {
			Save();
		}

		private void saveProjectAsToolStripMenuItem_Click(object sender, EventArgs e) {
			if (SetSaveLocation()) {
				Save();
			}
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
			this.Close();
		}

		//TODO: Refactor duplicate MessageBox code
		private void EditorForm_FormClosing(object sender, FormClosingEventArgs e) {
			switch (MessageBox.Show(this, "Your project might have unsaved changes. Click yes to save the changes, no to exit without saving, or cancel if you do not want to exit.",
				"Exit", MessageBoxButtons.YesNoCancel)) {
				case DialogResult.Yes:
					if (this.savePath == null) {
						if (this.SetSaveLocation()) Save();
						else e.Cancel = true;
					}
					else {
						Save();
					}
					break;
				case DialogResult.Cancel:
					e.Cancel = true;
					break;
				case DialogResult.No:
					break;
			}
		}

		private void loadProjectToolStripMenuItem_Click(object sender, EventArgs e) {
			if (SetSaveLocation()) {
				Load();
			}
		}

		private void resizeToolStripMenuItem_Click(object sender, EventArgs e) {
			Size thisSize = new Size(currentMap.width, currentMap.height);
			using (ComposedForm form = new ComposedForm("Resize map", ComposedForm.Parts.Size)) {
				form.SetSize(thisSize);
				if (form.ShowDialog() == DialogResult.OK) {
					Size newSize = form.GetSize();
					if (thisSize != newSize) {
						currentMap.Resize(newSize.Width, newSize.Height);
					}
					mainPanel.Invalidate();
				}
			}
		}

		private void newMapToolStripMenuItem_Click(object sender, EventArgs e) {
			using (ComposedForm form = new ComposedForm("New map", ComposedForm.Parts.Name | ComposedForm.Parts.Size)) {
				if (form.ShowDialog() == DialogResult.OK) {
					string name = form.GetName();
					Size size = form.GetSize();
					Map map = new Map(name, size);
					currentProject.maps.Add(name, map);
					mapComboBox.Items.Add(name);
					ChangeCurrentMap(name);
				}
			}
		}

		bool dragging = false;

		Point? tileLocation = null;
		private void mainPanel_MouseClick(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.Right && currentProject != null && !IsOutOfBounds(e.Location)) {
				tileLocation = new Point(-hScrollBar.Value * currentProject.tileSize + e.X / currentProject.tileSize,
					-vScrollBar.Value * currentProject.tileSize + e.Y / currentProject.tileSize);
				contextMenu.Show(mainPanel, e.Location);
			}
		}

		private void mainPanel_MouseMove(object sender, MouseEventArgs e) {
			if (currentProject == null) return;
			int x = -hScrollBar.Value * currentProject.tileSize;
			int y = -vScrollBar.Value * currentProject.tileSize;

			cursor.UpdateCoords(e.X, e.Y, e.X / currentProject.tileSize + x, e.Y / currentProject.tileSize + y);

			if (dragging && e.Button == MouseButtons.Left) {
				if (layers.checkedListBox.CheckedIndices.Contains(layers.checkedListBox.SelectedIndex)) {
					cursor.Click(currentMap.layers[currentMap.layers.Count - layers.checkedListBox.SelectedIndex - 1]);
				}
			}

			mainPanel.Invalidate();
		}

		private void EditorForm_Deactivate(object sender, EventArgs e) {
			dragging = false;
		}

		private void mainPanel_MouseDown(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.Left) {
				dragging = true;
				if (layers.checkedListBox.CheckedIndices.Contains(layers.checkedListBox.SelectedIndex)) {
					cursor.Click(currentMap.layers[currentMap.layers.Count - layers.checkedListBox.SelectedIndex - 1]);
				}
			}
		}

		private void mainPanel_MouseUp(object sender, MouseEventArgs e) {
			dragging = false;
		}

		private bool IsOutOfBounds(Point point) {
			int x = -hScrollBar.Value * currentProject.tileSize;
			int y = -vScrollBar.Value * currentProject.tileSize;
			return (point.X + x >= currentMap.width * currentProject.tileSize) || (point.Y + y >= currentMap.height * currentProject.tileSize);
		}

		private void locationMenuItem_Click(object sender, EventArgs e) {
			using (ComposedForm form = new ComposedForm("Location name", ComposedForm.Parts.Name, ComposedForm.Parts.None)) {
				form.SetName(CurrentLayer.tiles[tileLocation.Value.X, tileLocation.Value.Y].locationName);
				if (form.ShowDialog(this) == DialogResult.OK) {
					CurrentLayer.tiles[tileLocation.Value.X, tileLocation.Value.Y].locationName = form.GetName();
				}
			}
		}

		private void scriptsToolStripMenuItem_Click(object sender, EventArgs e) {
			using (ScriptEditor form = new ScriptEditor(currentProject)) {
				form.ShowDialog(this);
			}
		}

		private void onstepMenuItem_Click(object sender, EventArgs e) {
			string currentScript = CurrentLayer.tiles[tileLocation.Value.X,tileLocation.Value.Y].onStep;
			using (ScriptSelector selector = new ScriptSelector(currentScript, currentProject)) {
				if (selector.ShowDialog(this) == DialogResult.OK) {
					CurrentLayer.tiles[tileLocation.Value.X, tileLocation.Value.Y].onStep = selector.Script;
				}
			}
		}

		private void onActionToolStripMenuItem_Click(object sender, EventArgs e) {
			Map.Obj obj = CurrentLayer.objects[tileLocation.Value.X, tileLocation.Value.Y];
			string currentScript = obj.actionScript;
			using (ScriptSelector selector = new ScriptSelector(currentScript, currentProject)) {
				if (selector.ShowDialog(this) == DialogResult.OK) {
					obj.actionScript = selector.Script;
				}
			}
		}

		private void onCollideToolStripMenuItem_Click(object sender, EventArgs e) {
			Map.Obj obj = CurrentLayer.objects[tileLocation.Value.X, tileLocation.Value.Y];
			string currentScript = obj.collideScript;
			using (ScriptSelector selector = new ScriptSelector(currentScript, currentProject)) {
				if (selector.ShowDialog(this) == DialogResult.OK) {
					obj.collideScript = selector.Script;
				}
			}
		}

		private void movementScriptToolStripMenuItem_Click(object sender, EventArgs e) {
			Map.Obj obj = CurrentLayer.objects[tileLocation.Value.X, tileLocation.Value.Y];
			string currentScript = obj.movementAIScript;
			using (ScriptSelector selector = new ScriptSelector(currentScript, currentProject)) {
				if (selector.ShowDialog(this) == DialogResult.OK) {
					obj.movementAIScript = selector.Script;
				}
			}
		}

		private void contextMenu_Opened(object sender, EventArgs e) {
			bool isObject = CurrentLayer.objects[tileLocation.Value.X, tileLocation.Value.Y] != null;
			onActionToolStripMenuItem.Enabled = isObject;
			movementScriptToolStripMenuItem.Enabled = isObject;
			onCollideToolStripMenuItem.Enabled = isObject;
		}
	}
}
