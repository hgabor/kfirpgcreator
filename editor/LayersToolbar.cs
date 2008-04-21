using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KFIRPG.editor {
	internal partial class LayersToolbar: Form {
		public LayersToolbar() {
			InitializeComponent();
		}

		public new void Load(Map currentMap) {
			checkedListBox.Items.Clear();
			for (int l = currentMap.layers.Count - 1; l >= 0; --l) {
				checkedListBox.Items.Add(currentMap.layers[l].name, true);
			}
			checkedListBox.SelectedIndex = 0;
		}
	}
}
