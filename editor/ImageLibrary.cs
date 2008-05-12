using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KFIRPG.editor {
	public partial class ImageLibrary: Form {
		public ImageLibrary() {
			InitializeComponent();
		}

		Project project;
		private void LoadItems() {
			listbox.Items.Clear();
			foreach (string itemStr in project.sheets.Keys) {
				if (itemStr.Trim() == "") continue;
				listbox.Items.Add(itemStr.Trim());
			}
		}

		internal new void Load(Project project) {
			this.project = project;
			LoadItems();
			listbox.SelectedIndexChanged += (sender, args) => {
				if (!string.IsNullOrEmpty((string)listbox.SelectedItem)) {
					pictureBox.Image = project.sheets[(string)listbox.SelectedItem].sheet;
				}
			};
			listbox.DoubleClick += (sender, args) => {
				SpriteSheet sheet = project.sheets[(string)listbox.SelectedItem];
				using (SpriteSheetDialog dialog = new SpriteSheetDialog()) {
					string oldName = (string)listbox.SelectedItem;
					dialog.nameTextBox.Text = oldName;
					dialog.okButton.Click += (in_sender, in_args) => {
						if (dialog.nameTextBox.Text.Trim() == "") {
							MessageBox.Show("Name cannot be empty");
						}
						else if (dialog.pictureBox.Image == null) {
							MessageBox.Show("Image cannot be empty");
						}
						else if (dialog.nameTextBox.Text.ToLower() == oldName) dialog.DialogResult = DialogResult.OK;
						else {
							if (!project.sheets.ContainsKey(dialog.nameTextBox.Text.ToLower())) {
								dialog.DialogResult = DialogResult.OK;
							}
							else {
								MessageBox.Show(this, string.Format("A sprite named \"{0}\" already exists!", dialog.nameTextBox.Text));
							}
						}
					};
					dialog.pictureBox.Image = sheet.sheet;
					dialog.widthNumericUpDown.Value = sheet.spriteWidth;
					dialog.HeightNumericUpDown.Value = sheet.spriteHeight;
					dialog.xNumericUpDown.Value = sheet.x;
					dialog.yNumericUpDown.Value = sheet.y;
					dialog.listBox.DisplayMember = "Key";
					foreach (KeyValuePair<SpriteSheet.AnimationType, SpriteSheet.Animation> anim in sheet.animations) {
						dialog.listBox.Items.Add(anim);
					}
					if (dialog.ShowDialog(this) == DialogResult.OK) {
						if (dialog.nameTextBox.Text != oldName) {
							project.sheets.Remove(oldName);
							project.sheets.Add(dialog.nameTextBox.Text.Trim().ToLower(), sheet);
						}
						sheet.sheet = (Bitmap)dialog.pictureBox.Image;
						sheet.spriteWidth = (int)dialog.widthNumericUpDown.Value;
						sheet.spriteHeight = (int)dialog.HeightNumericUpDown.Value;
						sheet.x = (int)dialog.xNumericUpDown.Value;
						sheet.y = (int)dialog.yNumericUpDown.Value;
						sheet.animations.Clear();
						foreach (KeyValuePair<SpriteSheet.AnimationType, SpriteSheet.Animation> kvp in dialog.listBox.Items) {
							sheet.animations.Add(kvp.Key, kvp.Value);
						}
						int index = listbox.SelectedIndex;
						LoadItems();
						listbox.SelectedIndex = index;
					}
				}
			};
		}

		private void colorbutton_Click(object sender, EventArgs e) {
			colorDialog.Color = pictureBox.BackColor;
			if (colorDialog.ShowDialog() == DialogResult.OK) {
				pictureBox.BackColor = colorDialog.Color;
				colorbutton.BackColor = colorDialog.Color;
			}
		}

		private void addbutton_Click(object sender, EventArgs e) {
			using (SpriteSheetDialog dialog = new SpriteSheetDialog()) {
				dialog.okButton.Click += (in_sender, in_args) => {
					if (dialog.nameTextBox.Text.Trim() == "") {
						MessageBox.Show("Name cannot be empty");
					}
					else if (dialog.pictureBox.Image == null) {
						MessageBox.Show("Image cannot be empty");
					}
					else if (!project.sheets.ContainsKey(dialog.nameTextBox.Text)) {
						dialog.DialogResult = DialogResult.OK;
					}
					else {
						MessageBox.Show(this, string.Format("A sprite named \"{0}\" already exists!", dialog.nameTextBox.Text));
					}
				};
				if (dialog.ShowDialog(this) == DialogResult.OK) {
					SpriteSheet sheet = new SpriteSheet(project);
					project.sheets.Add(dialog.nameTextBox.Text.Trim().ToLower(), sheet);
					sheet.sheet = (Bitmap)dialog.pictureBox.Image;
					sheet.spriteWidth = (int)dialog.widthNumericUpDown.Value;
					sheet.spriteHeight = (int)dialog.HeightNumericUpDown.Value;
					sheet.x = (int)dialog.xNumericUpDown.Value;
					sheet.y = (int)dialog.yNumericUpDown.Value;
					foreach (KeyValuePair<SpriteSheet.AnimationType, SpriteSheet.Animation> kvp in dialog.listBox.Items) {
						sheet.animations.Add(kvp.Key, kvp.Value);
					}
					int index = listbox.SelectedIndex;
					LoadItems();
					listbox.SelectedIndex = index;
				}
			}
		}
	}
}
