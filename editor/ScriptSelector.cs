using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KFIRPG.editor {
	partial class ScriptSelector: Form {
		Project project;
		List<Script> scripts;
		public string Script {
			get { return (string)comboBox.SelectedItem; }
		}

		public ScriptSelector(string current, Project project) {
			InitializeComponent();
			this.project = project;
			scripts = project.scripts;
			Populate();
			comboBox.SelectedItem = current;
		}

		public void Populate() {
			string current = (string)comboBox.SelectedItem;
			comboBox.Items.Clear();
			comboBox.Items.Add("");
			foreach (Script script in scripts) {
				comboBox.Items.Add(script.Name);
			}
			if (current != null && comboBox.Items.Contains(current)) {
				comboBox.SelectedItem = current;
			}
			else {
				comboBox.SelectedIndex = 0;
			}
		}

		private void editButton_Click(object sender, EventArgs e) {
			if (comboBox.SelectedIndex == 0) {
				using (ScriptEditor editor = new ScriptEditor(project)) {
					editor.ShowDialog(this);
					Populate();
				}
			}
			else {
				using (ScriptEditor editor = new ScriptEditor(scripts.Find(sc => sc.Name == (string)comboBox.SelectedItem), project)) {
					editor.ShowDialog(this);
					Populate();
				}
			}
		}
	}
}
