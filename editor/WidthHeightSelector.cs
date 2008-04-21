using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KFIRPG.editor {
	public partial class WidthHeightSelector: Form {
		public WidthHeightSelector() {
			InitializeComponent();
		}

		private void okbutton_Click(object sender, EventArgs e) {
			this.DialogResult = DialogResult.OK;
		}

		private void cancelbutton_Click(object sender, EventArgs e) {
			this.DialogResult = DialogResult.Cancel;
		}

		public static Size Select(Size defValue, IWin32Window owner) {
			using (WidthHeightSelector form = new WidthHeightSelector()) {
				form.widthNumericUpDown.Minimum = 1;
				form.widthNumericUpDown.Value = defValue.Width;
				form.heightNumericUpDown.Minimum = 1;
				form.heightNumericUpDown.Value = defValue.Height;
				if (form.ShowDialog(owner) == DialogResult.OK) {
					return new Size((int)form.widthNumericUpDown.Value, (int)form.heightNumericUpDown.Value);
				}
				else {
					return defValue;
				}
			}
		}
	}
}
