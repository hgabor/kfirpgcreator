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

		LayersToolbar layers;
		AudioLibrary audio;
		ImageLibrary images;
		Palette palette;

		Cursor cursor;

		public void BindFormWithMenuItem(Form form, ToolStripMenuItem menuitem) {
			form.FormClosing += (sender, args) => {
				args.Cancel = true;
				form.Hide();
				menuitem.Checked = false;
			};
			menuitem.Click += (sender, args) => {
				if (menuitem.Checked) {
					form.Hide();
					menuitem.Checked = false;
				}
				else {
					form.Show();
					menuitem.Checked = true;
				}
			};
		}

		public EditorForm() {
			InitializeComponent();
			//currentProject = Project.FromFiles("TestGame");
			//currentMap = currentProject.maps[currentProject.startupMapName];

			//Layers toolbar
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

			specialViewComboBox.SelectedIndex = 0;

			this.Resize += (sender, args) => CalculateScrollbars();
			CalculateScrollbars();
			vScrollBar.ValueChanged += UpdateEventHandler;
			hScrollBar.ValueChanged += UpdateEventHandler;

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
			vScrollBar.Maximum = currentMap.height;
			vScrollBar.LargeChange = mainPanel.Height / currentProject.tileSize;
			if (vScrollBar.Maximum >= vScrollBar.LargeChange && vScrollBar.Value >= vScrollBar.Maximum - vScrollBar.LargeChange) {
				vScrollBar.Value = vScrollBar.Maximum - vScrollBar.LargeChange;
			}
			hScrollBar.Enabled = true;
			hScrollBar.Minimum = 0;
			hScrollBar.Maximum = currentMap.width;
			hScrollBar.LargeChange = mainPanel.Width / currentProject.tileSize;
			if (hScrollBar.Maximum >= hScrollBar.LargeChange && hScrollBar.Value >= hScrollBar.Maximum - hScrollBar.LargeChange) {
				hScrollBar.Value = hScrollBar.Maximum - hScrollBar.LargeChange;
			}
		}

		private void EnableControls() {
			saveProjectToolStripMenuItem.Enabled = true;
			foreach (ToolStripItem item in menuStrip.Items) {
				item.Enabled = true;
			}
			layers.Show();
			//audio.Show();
			//images.Show();
			palette.Show();
		}

		private new void Load() {
			currentProject = Project.FromFiles(savePath);
			currentMap = currentProject.maps[currentProject.startupMapName];

			audio.Load(currentProject);
			images.Load(currentProject);
			palette.Load(currentProject);
			layers.Load(currentMap);

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
				else if (MessageBox.Show(layers,
					string.Format("Do you want to remove layer \"{0}\"?", layers.checkedListBox.SelectedItem),
					"Remove layer", MessageBoxButtons.YesNo) == DialogResult.Yes) {
					currentMap.layers.RemoveAt(currentMap.layers.Count - layers.checkedListBox.SelectedIndex - 1);
					layers.Load(currentMap);
					mainPanel.Invalidate();
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

			EnableControls();
			mainPanel.Invalidate();
		}

		private void Save() {
			if (this.savePath == null) throw new Exception("Save path is not set!");
			Directory.SetCurrentDirectory(this.savePath);

			//Global.xml
			XmlDocument global = new XmlDocument();
			global.AppendChild(global.CreateXmlDeclaration("1.0", null, null));
			XmlElement globalRoot = global.CreateElement("settings");
			global.AppendChild(globalRoot);
			globalRoot.AppendChild(global.CreateElement("scriptvm")).InnerText = currentProject.scriptvm;
			globalRoot.AppendChild(global.CreateElement("startscript")).InnerText = currentProject.startupScriptName;
			globalRoot.AppendChild(global.CreateElement("defaultmap")).InnerText = currentProject.startupMapName;
			globalRoot.AppendChild(global.CreateElement("tilesize")).InnerText = currentProject.tileSize.ToString();
			globalRoot.AppendChild(global.CreateElement("startx")).InnerText = currentProject.startX.ToString();
			globalRoot.AppendChild(global.CreateElement("starty")).InnerText = currentProject.startY.ToString();
			globalRoot.AppendChild(global.CreateElement("startl")).InnerText = currentProject.startLayer.ToString();
			globalRoot.AppendChild(global.CreateElement("screenwidth")).InnerText = currentProject.screenWidth.ToString();
			globalRoot.AppendChild(global.CreateElement("screenheight")).InnerText = currentProject.screenHeight.ToString();
			XmlElement party = global.CreateElement("party");
			globalRoot.AppendChild(party);
			foreach (Sprite sp in currentProject.party) {
				party.AppendChild(global.CreateElement("character")).InnerText = sp.name;
			}

			//Locations.xml
			XmlElement locations = global.CreateElement("locations");
			globalRoot.AppendChild(locations);

			//Images
			Directory.CreateDirectory("img");
			using (StreamWriter sw = new StreamWriter(File.Create("img.list"))) {
				foreach (KeyValuePair<string, SpriteSheet> sheet in currentProject.sheets) {
					sw.WriteLine(sheet.Key);
					if (File.Exists("img/" + sheet.Key + ".png")) {
						File.Delete("img/" + sheet.Key + ".png");
					}
					sheet.Value.sheet.Save("img/" + sheet.Key + ".png");
					XmlDocument doc = new XmlDocument();
					doc.AppendChild(doc.CreateXmlDeclaration("1.0", null, null));
					XmlElement rootNode = doc.CreateElement("spritesheet");
					doc.AppendChild(rootNode);
					XmlElement cols = doc.CreateElement("cols");
					rootNode.AppendChild(doc.CreateElement("cols")).InnerText = sheet.Value.cols.ToString();
					foreach (KeyValuePair<string, SpriteSheet.Animation> anim in sheet.Value.states) {
						XmlElement image = doc.CreateElement("image");
						rootNode.AppendChild(image);
						image.SetAttribute("type", anim.Key);
						image.AppendChild(doc.CreateElement("start")).InnerText = anim.Value.startFrame.ToString();
						image.AppendChild(doc.CreateElement("count")).InnerText = anim.Value.frameCount.ToString();
						image.AppendChild(doc.CreateElement("interval")).InnerText = anim.Value.interval.ToString();
						image.AppendChild(doc.CreateElement("timeout")).InnerText = anim.Value.timeOut.ToString();
					}
					doc.Save("img/" + sheet.Key + ".xml");
				}
			}

			//Maps
			Directory.CreateDirectory("maps");
			using (StreamWriter sw = new StreamWriter(File.Create("maps.list"))) {
				foreach (KeyValuePair<string, Map> map in currentProject.maps) {
					Directory.CreateDirectory("maps/" + map.Key + "/layers");
					sw.WriteLine(map.Key);
					XmlDocument info = new XmlDocument();
					info.AppendChild(info.CreateXmlDeclaration("1.0", null, null));
					XmlElement infoRoot = info.CreateElement("map");
					info.AppendChild(infoRoot);
					infoRoot.AppendChild(info.CreateElement("layers")).InnerText = map.Value.layers.Count.ToString();
					infoRoot.AppendChild(info.CreateElement("width")).InnerText = map.Value.width.ToString();
					infoRoot.AppendChild(info.CreateElement("height")).InnerText = map.Value.height.ToString();
					info.Save("maps/" + map.Key + "/info.xml");

					XmlDocument objects = new XmlDocument();
					objects.AppendChild(objects.CreateXmlDeclaration("1.0", null, null));
					XmlElement oRoot = objects.CreateElement("objects");
					objects.AppendChild(oRoot);
					XmlDocument onstep = new XmlDocument();
					onstep.AppendChild(onstep.CreateXmlDeclaration("1.0", null, null));
					XmlElement sRoot = onstep.CreateElement("events");
					onstep.AppendChild(sRoot);

					for (int l = 0; l < map.Value.layers.Count; ++l) {
						Map.Layer layer = map.Value.layers[l];
						using (StreamWriter tileSw = new StreamWriter(File.Create(string.Format("maps/{0}/layers/tiles.{1}", map.Key, l))),
							passSw = new StreamWriter(File.Create(string.Format("maps/{0}/layers/passability.{1}", map.Key, l)))) {
							for (int j = 0; j < map.Value.height; ++j) {
								string[] tileLine = new string[map.Value.width];
								string[] passLine = new string[map.Value.width];
								for (int i = 0; i < map.Value.width; ++i) {
									tileLine[i] = layer.tiles[i, j].gfx.Id.ToString();
									passLine[i] = layer.tiles[i, j].passable ? "1" : "0";
									if (!string.IsNullOrEmpty(layer.tiles[i, j].onStep)) {
										XmlElement ev = onstep.CreateElement("event");
										sRoot.AppendChild(ev);
										ev.AppendChild(onstep.CreateElement("x")).InnerText = i.ToString();
										ev.AppendChild(onstep.CreateElement("y")).InnerText = j.ToString();
										ev.AppendChild(onstep.CreateElement("layer")).InnerText = l.ToString();
										ev.AppendChild(onstep.CreateElement("script")).InnerText = layer.tiles[i, j].onStep;
									}
									if (!string.IsNullOrEmpty(layer.tiles[i, j].locationName)) {
										string locName = layer.tiles[i, j].locationName;
										XmlElement location = global.CreateElement("location");
										locations.AppendChild(location);
										location.SetAttribute("name", locName);
										location.AppendChild(global.CreateElement("x")).InnerText = i.ToString();
										location.AppendChild(global.CreateElement("y")).InnerText = j.ToString();
										location.AppendChild(global.CreateElement("layer")).InnerText = l.ToString();
										location.AppendChild(global.CreateElement("map")).InnerText = map.Key;
									}
									if (layer.objects[i, j] != null) {
										Map.Obj obj = layer.objects[i, j];
										XmlElement o = objects.CreateElement("object");
										oRoot.AppendChild(o);
										o.AppendChild(objects.CreateElement("x")).InnerText = i.ToString();
										o.AppendChild(objects.CreateElement("y")).InnerText = j.ToString();
										o.AppendChild(objects.CreateElement("layer")).InnerText = l.ToString();
										o.AppendChild(objects.CreateElement("sprite")).InnerText = obj.Sprite.name;
										o.AppendChild(objects.CreateElement("action")).InnerText = obj.actionScript;
										o.AppendChild(objects.CreateElement("movement")).InnerText = obj.movementAIScript;
									}
								}
								tileSw.WriteLine(string.Join(" ", tileLine));
								passSw.WriteLine(string.Join(" ", passLine));
							}
						}
						File.WriteAllText(string.Format("maps/{0}/layers/name.{1}", map.Key, l), layer.name);
					}
					objects.Save("maps/" + map.Key + "/objects.xml");
					onstep.Save("maps/" + map.Key + "/onstep.xml");
				}
			}

			//Scripts
			Directory.CreateDirectory("scripts/map");
			Directory.CreateDirectory("scripts/movement");
			using (StreamWriter sw = new StreamWriter(File.Create("scripts.list"))) {
				foreach (Script script in currentProject.scripts) {
					sw.WriteLine(script.name);
					File.WriteAllText("scripts/" + script.name, script.text);
				}
			}

			global.Save("global.xml");
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

							if (layer.tiles[i, j].onStep != "") {
								DrawBox(x + i * size, y + j * size, size - 1, size - 1, stepEventBrush, stepEventPen, e.Graphics);
							}
							if (layer.tiles[i, j].locationName != "") {
								DrawBox(x + i * size, y + j * size, size - 1, size - 1, locationBrush, locationPen, e.Graphics);
							}
							if (layer.objects[i, j] != null && layer.objects[i, j].actionScript != "") {
								DrawBox(x + i * size, y + j * size, size - 1, size - 1, actionBrush, actionPen, e.Graphics);
							}
						}
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

		private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
			this.Close();
		}

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
			dragging = true;
			if (layers.checkedListBox.CheckedIndices.Contains(layers.checkedListBox.SelectedIndex)) {
				cursor.Click(currentMap.layers[currentMap.layers.Count - layers.checkedListBox.SelectedIndex - 1]);
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
	}
}
