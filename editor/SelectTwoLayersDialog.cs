using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KFIRPG.editor {
	partial class SelectTwoLayersDialog: Form {
		private SelectTwoLayersDialog(bool different, Map map) {
			InitializeComponent();
			foreach (var layer in map.SimpleLayerGroups) {
				baseListBox.Items.Add(layer);
				topListBox.Items.Add(layer);
			}
			if (different) {
				okButton.Click += (sender, args) => {
					if (baseListBox.SelectedItem == topListBox.SelectedItem) {
						MessageBox.Show(this, "You must select different layers!");
					}
					else {
						DialogResult = DialogResult.OK;
					}
				};
			}
			else {
				okButton.DialogResult = DialogResult.OK;
			}
		}

		private static bool SelectLayers(SelectTwoLayersDialog form, ref Map.Layer layer1, ref Map.Layer layer2) {
			form.baseListBox.SelectedItem = layer1;
			form.topListBox.SelectedItem = layer2;
			if (form.ShowDialog() == DialogResult.OK) {
				layer1 = (Map.Layer)form.baseListBox.SelectedItem;
				layer2 = (Map.Layer)form.topListBox.SelectedItem;
				return true;
			}
			else return false;
		}

		public static bool SelectTwoLayers(Map map, ref Map.Layer layer1, ref Map.Layer layer2) {
			using (SelectTwoLayersDialog form = new SelectTwoLayersDialog(false, map)) {
				return SelectLayers(form, ref layer1, ref layer2);
			}
		}

		public static bool SelectTwoDifferentLayers(Map map, ref Map.Layer layer1, ref Map.Layer layer2) {
			using (SelectTwoLayersDialog form = new SelectTwoLayersDialog(true, map)) {
				return SelectLayers(form, ref layer1, ref layer2);
			}
		}
	}
}
