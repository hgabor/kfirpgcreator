using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace KFIRPG.editor {
	partial class AudioLibrary: DockableForm, Project.Loadable {
		public AudioLibrary() {
			InitializeComponent();
		}

		BindingList<KeyValuePair<string, byte[]>> musicList;
		public new void Load(Project project) {
			musicList = new BindingList<KeyValuePair<string, byte[]>>(new ListDictionaryAdapter<string, byte[]>(project.musics));
			this.list.DisplayMember = "Key";
			this.list.DataSource = musicList;
		}

		private void removebutton_Click(object sender, EventArgs e) {
			if (list.SelectedItem != null) {
				musicList.Remove((KeyValuePair<string, byte[]>)list.SelectedItem);
			}
		}

		private void addbutton_Click(object sender, EventArgs e) {
			if (openFileDialog.ShowDialog() == DialogResult.OK) {
				string fileName = Path.GetFileName(openFileDialog.FileName);
				//If a file already exists with the same name, do not allow to add
				foreach (var kvp in musicList) {
					if (kvp.Key.ToLower() == fileName.ToLower()) {
						MessageBox.Show("A file with the same name already exists!", "Cannot add sound/music");
						return;
					}
				}
				byte[] contents = System.IO.File.ReadAllBytes(openFileDialog.FileName);
				musicList.Add(new KeyValuePair<string, byte[]>(fileName, contents));
			}
		}
	}
}
