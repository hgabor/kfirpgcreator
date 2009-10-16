using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KFIRPG.editor {
	partial class AnimationDialog: Form {
		Project project;
		Dictionary<string, Animation.Group> groups;
		BindingList<KeyValuePair<string, Animation.Group>> groupList;

		public Animation Animation {
			get {
				Animation ret = new Animation(CurrentSheet, project);
				ret.groups = groups;
				return ret;
			}
		}
		public string AnimationName {
			get {
				return nameTextBox.Text.Trim();
			}
		}

		Animation baseAnimation;
		public AnimationDialog(Animation baseAnimation, Project project)
			: this(project) {
			this.baseAnimation = baseAnimation;
			nameTextBox.Text = baseAnimation.Name;

			sheetComboBox.SelectedItem = new KeyValuePair<string, SpriteSheet>(baseAnimation.sheet.Name, baseAnimation.sheet);

			foreach (var kvp in baseAnimation.groups) {
				groupList.Add(kvp);
			}
		}

		private SpriteSheet CurrentSheet {
			get {
				return ((KeyValuePair<string, SpriteSheet>)sheetComboBox.SelectedItem).Value;
			}
		}

		public AnimationDialog(Project project) {
			InitializeComponent();
			this.project = project;
			groups = new Dictionary<string, Animation.Group>();
			groupList = new BindingList<KeyValuePair<string, Animation.Group>>(new ListDictionaryAdapter<string, Animation.Group>(groups));

			groupsListBox.DisplayMember = "Key";
			groupsListBox.DataSource = groupList;

			sheetComboBox.SelectedIndexChanged += (sender, args) => {
				pictureBox.Image = CurrentSheet.sheet;
			};
			sheetComboBox.DisplayMember = "Key";
			sheetComboBox.ValueMember = "Value";
			sheetComboBox.DataSource = new ListDictionaryAdapter<string, SpriteSheet>(project.sheets);
			if (sheetComboBox.Items.Count != 0) sheetComboBox.SelectedIndex = 0;
		}

		private void nameTextBox_TextChanged(object sender, EventArgs e) {
			string text = nameTextBox.Text.Trim();
			if (text == "")
				okButton.Enabled = false;
			else if (project.animations.ContainsKey(text)) {
				if (baseAnimation != null && text == baseAnimation.Name) {
					okButton.Enabled = true;
				}
				else {
					okButton.Enabled = false;
				}
			}
			else {
				okButton.Enabled = true;
			}
		}

		private void AddAnimationGroup(string name, Animation.Frame[] frames) {
			Animation.Group group = new Animation.Group();
			group.frames = new List<Animation.Frame>(frames);
			groupList.Add(new KeyValuePair<string,Animation.Group>(name, group));
		}

		private void addButton_Click(object sender, EventArgs e) {
			using (ComposedForm composed = new ComposedForm("Select name and direction",
				ComposedForm.Parts.Name | ComposedForm.Parts.Direction,
				ComposedForm.Parts.Name)) {
				foreach (string groupName in groups.Keys) {
					composed.AddNameChecker(s => s.Trim() != groupName.Split('.')[0]);
				}
				composed.AddNameChecker(s => !s.Contains("."));
				if (composed.ShowDialog(this) == DialogResult.OK) {

					using (AnimationSelector selector = new AnimationSelector(CurrentSheet, project)) {
						if (selector.ShowDialog(this) == DialogResult.OK) {
							string name = composed.GetName();
							if (composed.GetDirection() == "") {
								AddAnimationGroup(name + ".down", selector.SelectedFrames);
								AddAnimationGroup(name + ".left", selector.SelectedFrames);
								AddAnimationGroup(name + ".right", selector.SelectedFrames);
								AddAnimationGroup(name + ".up", selector.SelectedFrames);
							}
							else {
								AddAnimationGroup(name + "." + composed.GetDirection(), selector.SelectedFrames);
							}
						}
					}
				}
			}
		}

		//TODO: Ability to change name/direction
		private void groupsListBox_DoubleClick(object sender, EventArgs e) {
			if (groupsListBox.SelectedIndex == -1) return;
			Animation.Group anim = ((KeyValuePair<string, Animation.Group>)groupsListBox.SelectedItem).Value;
			using (AnimationSelector selector = new AnimationSelector(anim, CurrentSheet, project)) {
				if (selector.ShowDialog(this) == DialogResult.OK) {
					anim.frames = new List<Animation.Frame>(selector.SelectedFrames);
				}
			}
		}
	}
}
