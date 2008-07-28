using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KFIRPG.editor {
	partial class AnimationLibrary: DockableForm {
		private Project project;

		public AnimationLibrary() {
			InitializeComponent();
		}

		BindingList<KeyValuePair<string, Animation>> animations;
		internal new void Load(Project project) {
			this.project = project;
			animations = new BindingList<KeyValuePair<string,Animation>>(new ListDictionaryAdapter<string, Animation>(project.animations));
			listBox.DisplayMember = "Key";
			listBox.DataSource = animations;
		}

		private void addButton_Click(object sender, EventArgs e) {
			using (AnimationDialog dialog = new AnimationDialog(project)) {
				if (dialog.ShowDialog(this) == DialogResult.OK) {
					animations.Add(new KeyValuePair<string, Animation>(dialog.AnimationName, dialog.Animation));
				}
			}
		}

		private void listBox_DoubleClick(object sender, EventArgs e) {
			if (listBox.SelectedIndex == -1) return;
			var animKvp = (KeyValuePair<string, Animation>)listBox.SelectedItem;
			Animation anim = animKvp.Value;
			string name = animKvp.Key;
			using (AnimationDialog dialog = new AnimationDialog(anim, project)) {
				if (dialog.ShowDialog(this) == DialogResult.OK) {
					Animation newAnim = dialog.Animation;
					string newName = dialog.AnimationName;
					anim.groups = newAnim.groups;
					anim.sheet = newAnim.sheet;
					if (name != newName) {
						animations[animations.IndexOf(animKvp)] = new KeyValuePair<string, Animation>(newName, anim);
					}
				}
			}
		}
	}
}
