using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KFIRPG.editor {
	partial class AnimationLibrary: DockableForm, Project.Loadable {
		private Project project;

		public AnimationLibrary() {
			InitializeComponent();
		}

		public new void Load(Project project) {
			this.project = project;
			listBox.DisplayMember = "Key";
			listBox.DataSource = project.animations;
		}

		private void addButton_Click(object sender, EventArgs e) {
			using(AnimationDialog dialog = new AnimationDialog(project)) {
				if (dialog.ShowDialog(this) == DialogResult.OK) {
					string name = dialog.AnimationName;
					var anim = dialog.Animation;
					var list = new Undo.UndoCommandList("Add animation " + name, new Undo.UndoCommand(
						delegate() {
							project.animations.Add(name, anim);
						},
						delegate() {
							project.animations.Remove(name);
						}
					));
					project.Undo.DoCommand(list);
				}
			}
		}

		private void listBox_DoubleClick(object sender, EventArgs e) {
			if (listBox.SelectedIndex == -1) return;
			var animKvp = (KeyValuePair<string, Animation>)listBox.SelectedItem;
			Animation anim = animKvp.Value;
			string name = animKvp.Key;
			using(AnimationDialog dialog = new AnimationDialog(anim, project)) {
				if (dialog.ShowDialog(this) == DialogResult.OK) {
					Animation newAnim = dialog.Animation;
					string newName = dialog.AnimationName;
					var oldGroups = anim.groups;
					var oldSheet = anim.sheet;
					
					var list = new Undo.UndoCommandList("Modify sprite " + anim.Name, new Undo.UndoCommand(
						delegate() {
							anim.groups = newAnim.groups;
							anim.sheet = newAnim.sheet;
							if (name != newName) {
								project.animations.Remove(name);
								project.animations.Add(newName, anim);
							}
						},
						delegate() {
							anim.groups = oldGroups;
							anim.sheet = oldSheet;
							if (name != newName) {
								project.animations.Remove(newName);
								project.animations.Add(name, anim);
							}
						}
					));
					project.Undo.DoCommand(list);
				}
			}
		}

		void DelButtonClick(object sender, EventArgs e) {
			if (listBox.SelectedIndex == -1) return;
			var animKvp = (KeyValuePair<string, Animation>)listBox.SelectedItem;
			project.RemoveAnimation(animKvp.Value);
		}
	}
}
