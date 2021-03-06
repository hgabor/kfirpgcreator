﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using KFIRPG.corelib;
using KFIRPG.editor.Cursors;
using KFIRPG.editor.Undo;

namespace KFIRPG.editor {
	public partial class EditorForm: Form {
		Project currentProject;
		Map currentMap;
		int CurrentLayerIndex {
			get {
				return currentMap.layerGroups.Count - layers.checkedListBox.SelectedIndex - 1;
			}
		}
		LayerGroup CurrentLayer {
			get {
				return currentMap.layerGroups[CurrentLayerIndex];
			}
		}
		string savePath = null;
		Locker locker = new Locker();
		MRU mru = new MRU(6, "recent");


		LayersToolbar layers;

		List<Project.Loadable> projectLoadables = new List<Project.Loadable>();
		DoubleBufferedPanel mainPanel;
		HScrollBar hScrollBar;
		VScrollBar vScrollBar;

		Cursors.Cursor _cursor;
		Cursors.Cursor cursor {
			get { return _cursor; }
			set {
				_cursor = value;
				_cursor.CommandReady += (sender, args) => currentProject.Undo.DoCommand(args.Command);
			}
		}

		private void BindFormWithMenuItem(DockableForm form, ToolStripMenuItem menuitem) {
			form.Closing += (sender, args) => {
				args.Cancel = true;
				form.DockHandler.Hide();
				menuitem.Checked = false;
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
			Application.ThreadException += this.UnhandledExceptionHandler;

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
			layers.addbutton.Click += (sender, args) => {
				using(ComposedForm form = new ComposedForm("New layer", ComposedForm.Parts.Name)) {
					if (form.ShowDialog() == DialogResult.OK) {
						currentMap.CreateNewLayer(form.GetName());
						layers.Load(currentMap);
						mainPanel.Invalidate();
					}
				}
			};
			layers.deletebutton.Click += (sender, args) => {
				if (currentMap.layerGroups.Count == 1) {
					MessageBox.Show("Cannot remove last layer!", "Last layer");
				}
				else {
					int count = 0;
					for (int i = 0; i < currentMap.ladders.GetLength(0); ++i) {
						for (int j = 0; j < currentMap.ladders.GetLength(1); ++j) {
							Map.Ladder ladder = currentMap.ladders[i, j];
							if (ladder != null && (ladder.baseLayer == CurrentLayer[0] || ladder.topLayer == CurrentLayer[0])) ++count;
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
								if (ladder != null && (ladder.baseLayer == CurrentLayer[0] || ladder.topLayer == CurrentLayer[0])) currentMap.ladders[i, j] = null;
							}
						}
						currentMap.layerGroups.Remove(CurrentLayer);
						layers.Load(currentMap);
						mainPanel.Invalidate();
					}
				}
			};
			layers.upbutton.Click += (sender, args) => {
				int index = layers.checkedListBox.SelectedIndex;
				if (index == 0) return;
				int lindex = currentMap.layerGroups.Count - index - 1;
				LayerGroup layer = currentMap.layerGroups[lindex];
				currentMap.layerGroups.RemoveAt(lindex);
				currentMap.layerGroups.Insert(lindex + 1, layer);
				layers.Load(currentMap);
				layers.checkedListBox.SelectedIndex = index - 1;
				mainPanel.Invalidate();
			};
			layers.downbutton.Click += (sender, args) => {
				int index = layers.checkedListBox.SelectedIndex;
				if (index == currentMap.layerGroups.Count - 1) return;
				int lindex = currentMap.layerGroups.Count - index - 1;
				LayerGroup layer = currentMap.layerGroups[lindex];
				currentMap.layerGroups.RemoveAt(lindex);
				currentMap.layerGroups.Insert(lindex - 1, layer);
				layers.Load(currentMap);
				layers.checkedListBox.SelectedIndex = index + 1;
				mainPanel.Invalidate();
			};


			var audio = new AudioLibrary();
			projectLoadables.Add(audio);
			BindFormWithMenuItem(audio, audioLibraryToolStripMenuItem);

			var images = new ImageLibrary();
			projectLoadables.Add(images);
			BindFormWithMenuItem(images, imageLibraryToolStripMenuItem);

			var palette = new Palette();
			projectLoadables.Add(palette);
			BindFormWithMenuItem(palette, paletteToolStripMenuItem);
			palette.PaletteSelectionChanged += (sender, args) => {
				this.cursor = args.Cursor;
			};

			var animations = new AnimationLibrary();
			projectLoadables.Add(animations);
			BindFormWithMenuItem(animations, animationLibraryToolStripMenuItem);

			specialViewComboBox.SelectedIndex = 0;

			mainPanel.Resize += (sender, args) => CalculateScrollbars();
			CalculateScrollbars();
			vScrollBar.ValueChanged += UpdateEventHandler;
			hScrollBar.ValueChanged += UpdateEventHandler;

			mru.Changed += (sender, args) => RecreateMRUList();
			RecreateMRUList();

			cursor = new TileCursor();

			this.ProjectLoaded += (sender, args) => {
				EnableControls();
				mainPanel.Invalidate();
			};
			this.Disposed += this.OnDisposed;
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
			exportToolStripMenuItem.Enabled = true;
			foreach (ToolStripItem item in menuStrip.Items) {
				item.Enabled = true;
			}
			currentProject.Undo.Command += (sender, args) => this.UpdateUndoRedoState();
			//layers.Show();
			//audio.Show();
			//images.Show();
			//animations.Show();
			//palette.Show();
		}

		#region Load/Save

		private bool CheckForUnsavedChanges() {
			//If there is no open project, there is nothing to save...
			if (currentProject == null) {
				return true;
			}

			DialogResult res = MessageBox.Show(this, "Your project might have unsaved changes. Do you wish to save them now?",
			                                   "Exit", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

			switch (res) {
				//If user clicked cancel, we should not continue
			case DialogResult.Cancel:
				return false;

				//If user clicked no, we can go on without saving
			case DialogResult.No:
				return true;

				//If user clicked yes, we should try to save
			case DialogResult.Yes:
				if (this.savePath == null) {

					//If user cancelled the save dialog, we should not continue without saving
					if (!this.SetSaveLocation()) {
						return false;
					}
				}
				Save();

				return true;
			default:
				throw new InvalidOperationException("Don't know what happened... user clicked a button that is not Yes, No or Cancel");

			}
		}

		private void RecreateMRUList() {
			mruToolStripMenuItem.DropDownItems.Clear();
			foreach (string item in mru) {
				ToolStripMenuItem menuItem = new ToolStripMenuItem();
				menuItem.Text = item;
				menuItem.Click += (sender, args) => {
					if (CheckForUnsavedChanges()) {
						string path = menuItem.Text;
						if (!Directory.Exists(path)) {
							if (MessageBox.Show(this, "The specified directory does not exist!\nDo you want to remove it from the list?",
							                    "Invalid Directory", MessageBoxButtons.YesNo) == DialogResult.Yes) {
								mru.Remove(path);
							}
						}
						else {
							savePath = path;
							if (!Load()) {
								if (MessageBox.Show(this, "Do you want to remove it from the list?",
								                    "Invalid Directory", MessageBoxButtons.YesNo) == DialogResult.Yes) {
									mru.Remove(path);
									savePath = null;
								}
							}
						}
					}
				};
				mruToolStripMenuItem.DropDownItems.Add(menuItem);
			}
		}

		private event EventHandler ProjectLoaded;
		private void OnProjectLoaded(EventArgs e) {
			if (ProjectLoaded != null) ProjectLoaded(this, e);
		}

		private new bool Load() {
			if (locker.IsLocked(savePath)) {
				if (MessageBox.Show("The project is already open in another application, or the application closed " +
				                    "without properly closing the project. Do you really want to open it?",
				                    "Project is locked",
				                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) {
					locker.Unlock();
					locker.Lock(savePath, true);
				}
				else {
					return true;
				}
			}
			else {
				locker.Lock(savePath);
			}

			try {
				currentProject = Project.FromFiles(savePath);
			}
			catch (Project.LoadException) {
				MessageBox.Show(this, "The selected folder is not a project, or it is corrupted.", "Invalid folder");
				savePath = null;
				return false;
			}

			currentMap = currentProject.maps[currentProject.startupMapName];

			projectLoadables.ForEach(l => l.Load(currentProject));
			layers.Load(currentMap);

			foreach (string mapName in currentProject.maps.Keys) {
				mapComboBox.Items.Add(mapName);
			}
			mapComboBox.SelectedIndex = mapComboBox.Items.IndexOf(currentMap.name);
			mapComboBox.SelectedIndexChanged += (sender, args) => {
				ChangeCurrentMap((string)mapComboBox.SelectedItem);
			};

			OnProjectLoaded(EventArgs.Empty);
			return true;
		}

		private void Save() {
			if (this.savePath == null && !SetSaveLocation()) {
				return;
			}
			using(Saver saver = new FileSaver(this.savePath)) {
				this.currentProject.Save(saver);
			}
		}

		private bool SetSaveLocation() {
            folderDialog.SelectedPath = mru.FirstOrDefault() ?? Directory.GetCurrentDirectory();
			if (folderDialog.ShowDialog() == DialogResult.OK) {
				this.savePath = folderDialog.SelectedPath;
				return true;
			}
			else return false;
		}

		private void saveProjectToolStripMenuItem_Click(object sender, EventArgs e) {
			Save();
		}

		private void saveProjectAsToolStripMenuItem_Click(object sender, EventArgs e) {
			if (SetSaveLocation()) {
				Save();
				mru.Add(savePath);
			}
		}

		private void exportToolStripMenuItem_Click(object sender, EventArgs e) {
			string oldSavePath = this.savePath;
			if (SetSaveLocation()) {
				string saveBase = this.savePath;
				this.savePath = Path.Combine(this.savePath, "data");
				if (!Directory.Exists(savePath)) Directory.CreateDirectory(savePath);
				Save();
				File.WriteAllLines(Path.Combine(this.savePath, "game.xml"),
				                   new string[] {
				                       "<?xml version=\"1.0\"?>",
				                       "<settings>",
				                       "  <loader>file</loader>",
				                       "  <loadpath>data</loadpath>",
				                       "</settings>",
				                   });
				Array.ForEach(new string[] {
				    "corelib.dll", "SdlDotNet.dll", "Tao.Lua.dll", "Tao.Sdl.dll", "runner.exe"
				}, s => File.Copy(s, Path.Combine(saveBase, s), true));
			}
			this.savePath = oldSavePath;
		}

		private void newProjectToolStripMenuItem_Click(object sender, EventArgs e) {
			if (CheckForUnsavedChanges() && SetSaveLocation()) {
				string savePathNew = savePath;
				savePath = "NewGame";
				Load();
				savePath = savePathNew;
				Save();
				mru.Add(savePath);
			}
		}

		private void loadProjectToolStripMenuItem_Click(object sender, EventArgs e) {
			if (SetSaveLocation()) {
				Load();
				mru.Add(savePath);
			}
		}

		#endregion

		private void ChangeCurrentMap(string mapName) {
			currentMap = currentProject.maps[mapName];
			mapComboBox.SelectedIndex = mapComboBox.Items.IndexOf(mapName);
			layers.Load(currentMap);
		}

		#region Rendering

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
			for (int l = 0; l < currentMap.layerGroups.Count; ++l)
			{
				if (!layers.checkedListBox.CheckedIndices.Contains(currentMap.layerGroups.Count - l - 1)) continue;
				bool selectedLayer = layers.checkedListBox.SelectedIndex == currentMap.layerGroups.Count - l - 1;
				LayerGroup layerGroup = currentMap.layerGroups[l];
				foreach (var layer in layerGroup)
				{
					for (int i = 0; i < currentMap.width; ++i)
					{
						for (int j = 0; j < currentMap.height; ++j)
						{
							// TODO: layer should paint itself...
							layer.tiles[i, j].gfx.Draw(x + i * size, y + j * size, e.Graphics);
							if (layer.objects[i, j] != null)
							{
								layer.objects[i, j].Gfx.Draw(x + i * size, y + j * size, e.Graphics);
							}

							if (specialViewComboBox.SelectedIndex == 1 || selectedLayer)
							{

								if (!layer.tiles[i, j].passable && passabilityButton.Checked)
								{
									DrawBox(x + i * size, y + j * size, size - 1, size - 1, passBrush, passPen, e.Graphics);
								}

								if (!string.IsNullOrEmpty(layer.tiles[i, j].onStep))
								{
									DrawBox(x + i * size, y + j * size, size - 1, size - 1, stepEventBrush, stepEventPen, e.Graphics);
								}
								if (!string.IsNullOrEmpty(layer.tiles[i, j].locationName))
								{
									DrawBox(x + i * size, y + j * size, size - 1, size - 1, locationBrush, locationPen, e.Graphics);
								}
								if (layer.objects[i, j] != null && !string.IsNullOrEmpty(layer.objects[i, j].actionScript))
								{
									DrawBox(x + i * size, y + j * size, size - 1, size - 1, actionBrush, actionPen, e.Graphics);
								}
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

		#endregion

		private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
			this.Close();
		}

		private void EditorForm_FormClosing(object sender, FormClosingEventArgs e) {
			if (!CheckForUnsavedChanges()) {
				e.Cancel = true;
			}
		}

		private void resizeToolStripMenuItem_Click(object sender, EventArgs e) {
			Size thisSize = new Size(currentMap.width, currentMap.height);
			using(ComposedForm form = new ComposedForm("Resize map", ComposedForm.Parts.Size)) {
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
			using(ComposedForm form = new ComposedForm("New map", ComposedForm.Parts.Name | ComposedForm.Parts.Size)) {
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

		#region Mouse events

		bool dragging = false;

		Point? tileLocation = null;
		private Point GetTileCoords(int x, int y) {
			return new Point(
			           (hScrollBar.Value * currentProject.tileSize + x) / currentProject.tileSize,
			           (vScrollBar.Value * currentProject.tileSize + y) / currentProject.tileSize
			       );
		}

		private void mainPanel_MouseClick(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.Right && currentProject != null && !IsOutOfBounds(e.Location)) {
				tileLocation = GetTileCoords(e.X, e.Y);
				//tileLocation = new Point(-hScrollBar.Value * currentProject.tileSize + e.X / currentProject.tileSize,
				//	-vScrollBar.Value * currentProject.tileSize + e.Y / currentProject.tileSize);
				contextMenu.Show(mainPanel, e.Location);
			}
		}

		private void mainPanel_MouseMove(object sender, MouseEventArgs e) {
			if (currentProject == null) return;

			Point p = GetTileCoords(e.X, e.Y);
			cursor.UpdateCoords(e.X, e.Y, p.X, p.Y);

			if (dragging && e.Button == MouseButtons.Left) {
				if (layers.checkedListBox.CheckedIndices.Contains(layers.checkedListBox.SelectedIndex)) {
					//cursor.Click(currentMap.layers[currentMap.layers.Count - layers.checkedListBox.SelectedIndex - 1]);
					cursor.DoEdit(CurrentLayer);
				}
			}

			mainPanel.Invalidate();
		}

		private void EditorForm_Deactivate(object sender, EventArgs e) {
			dragging = false;
			cursor.EndEdit();
		}

		private void mainPanel_MouseDown(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.Left) {
				dragging = true;
				if (layers.checkedListBox.CheckedIndices.Contains(layers.checkedListBox.SelectedIndex)) {
					//cursor.Click(currentMap.layers[currentMap.layers.Count - layers.checkedListBox.SelectedIndex - 1]);
					cursor.DoEdit(CurrentLayer);
				}
			}
		}

		private void mainPanel_MouseUp(object sender, MouseEventArgs e) {
			dragging = false;
			if (e.Button == MouseButtons.Left) {
				cursor.EndEdit();
			}
		}

		private bool IsOutOfBounds(Point point) {
			int x = hScrollBar.Value * currentProject.tileSize;
			int y = vScrollBar.Value * currentProject.tileSize;
			return (point.X + x >= currentMap.width * currentProject.tileSize) || (point.Y + y >= currentMap.height * currentProject.tileSize);
		}

		#endregion

		#region Context menu

		private void locationMenuItem_Click(object sender, EventArgs e) {
			using(ComposedForm form = new ComposedForm("Location name", ComposedForm.Parts.Name, ComposedForm.Parts.None)) {
				string originalName = CurrentLayer[0].tiles[tileLocation.Value.X, tileLocation.Value.Y].locationName;
				form.SetName(originalName);
				if (form.ShowDialog(this) == DialogResult.OK) {
					var undoName = string.Format("Changed location: {0} -> {1}", originalName, form.GetName());
					currentProject.Undo.DoCommand(new UndoCommandList(undoName, new UndoCommand(
					                                  delegate() {
														  CurrentLayer[0].tiles[tileLocation.Value.X, tileLocation.Value.Y].locationName = form.GetName();
					                                  },
					                                  delegate() {
														  CurrentLayer[0].tiles[tileLocation.Value.X, tileLocation.Value.Y].locationName = originalName;
					                                  }
					                              )));
				}
			}
		}

		private void scriptsToolStripMenuItem_Click(object sender, EventArgs e) {
			using(ScriptEditor form = new ScriptEditor(currentProject)) {
				form.ShowDialog(this);
			}
		}

		private void onstepMenuItem_Click(object sender, EventArgs e) {
			string currentScript = CurrentLayer[0].tiles[tileLocation.Value.X, tileLocation.Value.Y].onStep;
			using(ScriptSelector selector = new ScriptSelector(currentScript, currentProject)) {
				if (selector.ShowDialog(this) == DialogResult.OK) {
					currentProject.Undo.DoCommand(new UndoCommandList("Changed OnStep event", new UndoCommand(
					                                  delegate() {
														  CurrentLayer[0].tiles[tileLocation.Value.X, tileLocation.Value.Y].onStep = selector.Script;
					                                  },
					                                  delegate() {
														  CurrentLayer[0].tiles[tileLocation.Value.X, tileLocation.Value.Y].onStep = currentScript;
					                                  }
					                              )));
				}
			}
		}

		private void onActionToolStripMenuItem_Click(object sender, EventArgs e) {
			Map.Obj obj = CurrentLayer[0].objects[tileLocation.Value.X, tileLocation.Value.Y];
			string currentScript = obj.actionScript;
			using(ScriptSelector selector = new ScriptSelector(currentScript, currentProject)) {
				if (selector.ShowDialog(this) == DialogResult.OK) {
					currentProject.Undo.DoCommand(new UndoCommandList("Changed OnAction event", new UndoCommand(
					                                  delegate() {
					                                      obj.actionScript = selector.Script;
					                                  },
					                                  delegate() {
					                                      obj.actionScript = currentScript;
					                                  }
					                              )));
				}
			}
		}

		private void onCollideToolStripMenuItem_Click(object sender, EventArgs e) {
			Map.Obj obj = CurrentLayer[0].objects[tileLocation.Value.X, tileLocation.Value.Y];
			string currentScript = obj.collideScript;
			using(ScriptSelector selector = new ScriptSelector(currentScript, currentProject)) {
				if (selector.ShowDialog(this) == DialogResult.OK) {
					currentProject.Undo.DoCommand(new UndoCommandList("Changed OnCollide event", new UndoCommand(
					                                  delegate() {
					                                      obj.collideScript = selector.Script;
					                                  },
					                                  delegate() {
					                                      obj.collideScript = currentScript;
					                                  }
					                              )));
				}
			}
		}

		private void movementScriptToolStripMenuItem_Click(object sender, EventArgs e) {
			Map.Obj obj = CurrentLayer[0].objects[tileLocation.Value.X, tileLocation.Value.Y];
			string currentScript = obj.movementAIScript;
			using(ScriptSelector selector = new ScriptSelector(currentScript, currentProject)) {
				if (selector.ShowDialog(this) == DialogResult.OK) {
					currentProject.Undo.DoCommand(new UndoCommandList("Changed Movement script", new UndoCommand(
					                                  delegate() {
					                                      obj.movementAIScript = selector.Script;
					                                  },
					                                  delegate() {
					                                      obj.movementAIScript = currentScript;
					                                  }
					                              )));
				}
			}
		}

		private void contextMenu_Opened(object sender, EventArgs e) {
			bool isObject = CurrentLayer[0].objects[tileLocation.Value.X, tileLocation.Value.Y] != null;
			onActionToolStripMenuItem.Enabled = isObject;
			movementScriptToolStripMenuItem.Enabled = isObject;
			onCollideToolStripMenuItem.Enabled = isObject;
		}

		#endregion

		private void OnDisposed(object sender, EventArgs e) {
			locker.Unlock();
		}

		private void UpdateUndoRedoState() {
			undoToolStripMenuItem.Enabled = currentProject.Undo.CanUndo;
			redoToolStripMenuItem.Enabled = currentProject.Undo.CanRedo;
			if (undoToolStripMenuItem.Enabled) {
				undoToolStripMenuItem.Text = string.Format("Undo \"{0}\"", currentProject.Undo.UndoName);
			}
			else {
				undoToolStripMenuItem.Text = "Undo";
			}
			if (redoToolStripMenuItem.Enabled) {
				redoToolStripMenuItem.Text = string.Format("Redo \"{0}\"", currentProject.Undo.RedoName);
			}
			else {
				redoToolStripMenuItem.Text = "Redo";
			}
		}

		private void undoToolStripMenuItem_Click(object sender, EventArgs e) {
			currentProject.Undo.Undo();
			mainPanel.Invalidate();
		}

		private void redoToolStripMenuItem_Click(object sender, EventArgs e) {
			currentProject.Undo.Redo();
			mainPanel.Invalidate();
		}

		private void UnhandledExceptionHandler(object sender, System.Threading.ThreadExceptionEventArgs args) {
			if (args.Exception is Project.CannotRemoveException) {
				var ex = (Project.CannotRemoveException)args.Exception;
				MessageBox.Show(ex.Message, "Cannot remove object", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else {
				MessageBox.Show("An unexpected error occured. The program will now close.", "Unexpected error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				System.Diagnostics.Debug.WriteLine(args.Exception.ToString());
				this.Close();
			}
		}
	}
}
