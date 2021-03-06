﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KFIRPG.editor {
	internal partial class LayersToolbar: DockableForm {
		public LayersToolbar() {
			InitializeComponent();
		}

		internal new void Load(Map currentMap) {
			checkedListBox.Items.Clear();
			for (int l = currentMap.layerGroups.Count - 1; l >= 0; --l) {
				checkedListBox.Items.Add(currentMap.layerGroups[l].Name, true);
			}
			checkedListBox.SelectedIndex = 0;
		}
	}
}
