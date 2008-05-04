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

		internal new void Load(Project project) {
			listbox.Items.Clear();
			foreach (string itemStr in project.loader.LoadText("img.list").Split('\n')) {
				if (itemStr.Trim() == "") continue;
				listbox.Items.Add(itemStr.Trim());
			}
			listbox.SelectedIndexChanged += (sender, args) => {
				if (!string.IsNullOrEmpty((string)listbox.SelectedItem)) {
					pictureBox.Image = project.sheets[(string)listbox.SelectedItem].sheet;
				}
			};
			listbox.DoubleClick += (sender, args) => {
				SpriteSheet sheet = project.sheets[(string)listbox.SelectedItem];
				using (SpriteSheetDialog dialog = new SpriteSheetDialog()) {
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
						sheet.sheet = (Bitmap)dialog.pictureBox.Image;
						sheet.spriteWidth = (int)dialog.widthNumericUpDown.Value;
						sheet.spriteHeight = (int)dialog.HeightNumericUpDown.Value;
						sheet.x = (int)dialog.xNumericUpDown.Value;
						sheet.y = (int)dialog.yNumericUpDown.Value;
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
	}
}
