using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KFIRPG.editor {
	partial class SpriteDialog: Form {
		Project project;
		bool newSprite;
		Sprite sprite;
		public SpriteDialog(Project project, Sprite sprite) {
			InitializeComponent();
			this.project = project;
			foreach (var anim in project.animations.Values) {
				animationComboBox.Items.Add(anim);
			}
			animationComboBox.DisplayMember = "Name";
			if (sprite == null) {
				this.sprite = new Sprite(project);
				this.newSprite = true;
				animationComboBox.SelectedIndex = 0;
			}
			else {
				this.sprite = sprite;
				this.newSprite = false;
				nameTextBox.Text = sprite.Name;
				animationComboBox.SelectedItem = sprite.animation;
				speedNumericUpDown.Value = sprite.speed;
				noclipCheckBox.Checked = sprite.noclip;
				foreach (KeyValuePair<string, string> kvp in sprite.ext) {
					listBox.Items.Add(kvp);
				}
			}
		}

		private void addButton_Click(object sender, EventArgs e) {
			using (KeyValueDialog dialog = new KeyValueDialog(sprite.ext.Keys)) {
				if (dialog.ShowDialog(this) == DialogResult.OK) {
					listBox.Items.Add(dialog.KeyValuePair);
				}
			}
		}

		private void delButton_Click(object sender, EventArgs e) {
			if (listBox.SelectedIndex != -1) listBox.Items.RemoveAt(listBox.SelectedIndex);
		}

		private void listBox_DoubleClick(object sender, EventArgs e) {
			if (listBox.SelectedIndex == -1) return;
			List<string> forbiddenKeys = new List<string>(sprite.ext.Keys);
			forbiddenKeys.Remove(((KeyValuePair<string, string>)listBox.SelectedItem).Key);
			using (KeyValueDialog dialog = new KeyValueDialog((KeyValuePair<string, string>)listBox.SelectedItem, forbiddenKeys)) {
				if (dialog.ShowDialog(this) == DialogResult.OK) {
					int id = listBox.SelectedIndex;
					listBox.Items.RemoveAt(id);
					listBox.Items.Insert(id, dialog.KeyValuePair);
				}
			}
		}

		private void okButton_Click(object sender, EventArgs e) {
			foreach (string name in project.sprites.Keys) {
				if (nameTextBox.Text.ToLower().Trim() == name) {
					if (!newSprite && sprite.Name == name) continue;
					else {
						MessageBox.Show(this, "A sprite named \"{0}\" already exists!", name);
						return;
					}
				}
			}
			sprite.animation = (Animation)animationComboBox.SelectedItem;
			sprite.speed = (int)speedNumericUpDown.Value;
			sprite.noclip = noclipCheckBox.Checked;
			sprite.ext.Clear();
			foreach (KeyValuePair<string, string> kvp in listBox.Items) {
				sprite.ext.Add(kvp.Key, kvp.Value);
			}
			if (newSprite) {
				project.sprites.Add(nameTextBox.Text.ToLower().Trim(), sprite);
			}
			else {
				sprite.Name = nameTextBox.Text.ToLower().Trim();
			}
			DialogResult = DialogResult.OK;
		}
	}
}
