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
	}
}
