using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KFIRPG.editor {
	public partial class KeyValueDialog: Form {
		public KeyValueDialog() : this(new KeyValuePair<string, string>("", ""), new string[0]) { }

		public KeyValueDialog(KeyValuePair<string, string> data) : this(data, new string[0]) { }

		public KeyValueDialog(ICollection<string> forbiddenKeys) : this(new KeyValuePair<string, string>("", ""), forbiddenKeys) { }

		public KeyValueDialog(KeyValuePair<string, string> data, ICollection<string> forbiddenKeys) {
			InitializeComponent();
			keyTextBox.Text = data.Key;
			valueTextBox.Text = data.Value;
			keyTextBox.TextChanged += (sender, args) => {
				if (keyTextBox.Text.ToLower().Trim() == "" || forbiddenKeys.Contains(keyTextBox.Text.ToLower().Trim())) {
					okButton.Enabled = false;
				}
				else {
					okButton.Enabled = true;
				}
			};
		}

		public KeyValuePair<string, string> KeyValuePair {
			get {
				return new KeyValuePair<string, string>(keyTextBox.Text, valueTextBox.Text);
			}
		}
	}
}
