using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KFIRPG.editor {
	public partial class SpriteSheetDialog: Form {
		public SpriteSheetDialog() {
			InitializeComponent();
		}

		private void imageButton_Click(object sender, EventArgs e) {
			if (openFileDialog.ShowDialog(this) == DialogResult.OK) {
				using (Bitmap bm = new Bitmap(openFileDialog.FileName)) {
					pictureBox.Image = new Bitmap(bm);
				}
			}
		}

		private void addAnimationButton_Click(object sender, EventArgs e) {
			using (AnimationSelector selector = new AnimationSelector((Bitmap)pictureBox.Image, (int)widthNumericUpDown.Value, (int)HeightNumericUpDown.Value)) {
				using (ComposedForm composed = new ComposedForm("Select name and direction", ComposedForm.Parts.Name | ComposedForm.Parts.Direction, ComposedForm.Parts.Name)) {
					if (composed.ShowDialog(this) == DialogResult.OK && selector.ShowDialog(this) == DialogResult.OK) {
						SpriteSheet.Animation anim = new SpriteSheet.Animation(selector.Start, selector.Count, selector.Timeout);
						if (composed.GetDirection() == "") {
							foreach (string dir in new string[] { "up", "down", "left", "right" }) {
								SpriteSheet.AnimationType type = new SpriteSheet.AnimationType(composed.GetName(), dir);
								listBox.Items.Add(new KeyValuePair<SpriteSheet.AnimationType, SpriteSheet.Animation>(type, anim));
							}
						}
						else {
							SpriteSheet.AnimationType type = new SpriteSheet.AnimationType(composed.GetName(), composed.GetDirection());
							listBox.Items.Add(new KeyValuePair<SpriteSheet.AnimationType, SpriteSheet.Animation>(type, anim));
						}
					}
				}
			}
		}

		private void delAnimationButton_Click(object sender, EventArgs e) {
			if (listBox.SelectedIndex != -1) {
				listBox.Items.RemoveAt(listBox.SelectedIndex);
			}
		}

		private void listBox_DoubleClick(object sender, EventArgs e) {
			if (listBox.SelectedIndex == -1) return;
			int selected = listBox.SelectedIndex;
			KeyValuePair<SpriteSheet.AnimationType, SpriteSheet.Animation> kvp = (KeyValuePair<SpriteSheet.AnimationType, SpriteSheet.Animation>)listBox.SelectedItem;
			using (AnimationSelector selector = new AnimationSelector((Bitmap)pictureBox.Image, (int)widthNumericUpDown.Value, (int)HeightNumericUpDown.Value)) {
				if (selector.ShowDialog(this) == DialogResult.OK) {
					listBox.Items.RemoveAt(selected);
					SpriteSheet.Animation anim = new SpriteSheet.Animation(selector.Start, selector.Count, selector.Timeout);
					listBox.Items.Insert(selected, new KeyValuePair<SpriteSheet.AnimationType, SpriteSheet.Animation>(kvp.Key, anim));
				}
			}
		}
	}
}
