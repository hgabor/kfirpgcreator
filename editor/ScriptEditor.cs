using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Xml;
using System.IO;
using System.Windows.Forms;

using ICSharpCode.TextEditor.Document;

namespace KFIRPG.editor {

	partial class ScriptEditor: Form {

		static ScriptEditor() {
			string dir = "./"; // Insert the path to your xshd-files.
			FileSyntaxModeProvider fsmProvider; // Provider
			if (Directory.Exists(dir)) {
				fsmProvider = new FileSyntaxModeProvider(dir); // Create new provider with the highlighting directory.
				HighlightingManager.Manager.AddSyntaxModeFileProvider(fsmProvider); // Attach to the text editor.
			}
		}

		List<Script> scripts;
		Script currentScript;
		ICSharpCode.TextEditor.TextEditorControl textEditor = new ICSharpCode.TextEditor.TextEditorControl();

		public ScriptEditor(Project project) {
			InitializeComponent();
			Controls.Add(textEditor);

			textEditor.Location = new Point(12, 91);
			textEditor.Size = new Size(241, 147);
			textEditor.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
			textEditor.Name = "textEditor";
			textEditor.BorderStyle = BorderStyle.Fixed3D;
			textEditor.TextChanged += (sender, args) => textEditor.Refresh();
			textEditor.ShowMatchingBracket = true;
			textEditor.SetHighlighting("lua");
			textEditor.ShowSpaces = true;
			textEditor.ShowTabs = true;


			scripts = project.scripts;
			if (scripts.Count != 0) {
				currentScript = scripts[0];
				nameTextBox.Text = currentScript.name;
				textEditor.Text = currentScript.text;
			}
			else {
				currentScript = new Script("unnamed", "");
				nameTextBox.Text = "unnamed";
			}
			PopulateList();
		}

		public ScriptEditor(Script currentScript, Project project) {
			InitializeComponent();
			scripts = project.scripts;
			this.currentScript = currentScript != null ? currentScript : new Script("unnamed", "");
			nameTextBox.Text = this.currentScript.name;
			textEditor.Text = this.currentScript.text;
			PopulateList();
		}

		private void PopulateList() {
			scriptNamesListBox.Items.Clear();
			scriptNamesListBox.SelectedIndexChanged -= scriptNamesListBox_SelectedIndexChanged;
			foreach (Script s in scripts) {
				scriptNamesListBox.Items.Add(s.name);
			}
			if (scripts.Contains(currentScript)) {
				scriptNamesListBox.SelectedIndex = scriptNamesListBox.Items.IndexOf(currentScript.name);
			}
			scriptNamesListBox.SelectedIndexChanged += scriptNamesListBox_SelectedIndexChanged;
		}

		private void Save() {
			if (nameTextBox.Text.Trim() == "") {
				MessageBox.Show("Name must not be empty!");
				return;
			}
			if (scripts.Contains(currentScript)) {
				if (scripts.TrueForAll(sc => sc.name == currentScript.name || sc.name != nameTextBox.Text.Trim())) {
					currentScript.name = nameTextBox.Text.Trim();
					currentScript.text = textEditor.Text.Replace("\r", "");
				}
				else {
					MessageBox.Show("Name must be unique");
				}
			}
			else {
				if (scripts.TrueForAll(sc => sc.name != nameTextBox.Text.Trim())) {
					currentScript.name = nameTextBox.Text.Trim();
					currentScript.text = textEditor.Text.Replace("\r", "");
					scripts.Add(currentScript);
				}
				else {
					MessageBox.Show("Name must be unique");
				}
			}
			PopulateList();
		}

		private void saveButton_Click(object sender, EventArgs e) {
			Save();
		}

		private void removeButton_Click(object sender, EventArgs e) {
			scripts.Remove(currentScript);
			currentScript = new Script("unnamed", "");
			nameTextBox.Text = "unnamed";
			textEditor.Text = "";
		}

		private bool CanContinue() {
			if (textEditor.Text.Replace("\r\n", "\n") != currentScript.text.Replace("\r\n", "\n") || nameTextBox.Text.Trim() != currentScript.name) {
				switch (MessageBox.Show(this, "The script contains unsaved changes. So you wish to save?", "Unsaved changes", MessageBoxButtons.YesNoCancel)) {
					case DialogResult.Yes:
						Save();
						return true;
					case DialogResult.No:
						return true;
					case DialogResult.Cancel:
						return false;
					default:
						throw new Exception("If you get this message, the MessageBox programmers really messed up something");
				}
			}
			else return true;
		}

		private void newButton_Click(object sender, EventArgs e) {
			if (CanContinue()) {
				currentScript = new Script("unnamed", "");
				nameTextBox.Text = "unnamed";
				textEditor.Text = "";
			}
		}

		private void scriptNamesListBox_SelectedIndexChanged(object sender, EventArgs e) {
			if (CanContinue()) {
				currentScript = scripts.Find(sc => sc.name == scriptNamesListBox.SelectedItem.ToString());
				nameTextBox.Text = currentScript.name;
				textEditor.Text = currentScript.text;
			}
			PopulateList();
		}

		private void ScriptEditor_FormClosing(object sender, FormClosingEventArgs e) {
			e.Cancel = !CanContinue();
		}
	}
}

