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
