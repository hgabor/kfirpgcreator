using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;


namespace KFIRPG.editor {

	partial class ScriptEditor: Form {

		List<Script> scripts;
		Script currentScript;

		ICSharpCode.TextEditor.TextEditorControl textEditor;

		public ScriptEditor(Project project)
			: this(null, project) {
		}

		public ScriptEditor(Script currentScript, Project project) {
			InitializeComponent();
			scripts = project.scripts;

			dockPanel.ActiveDocumentChanged += (sender, args) => {
				if (dockPanel.ActiveDocument != null) {
					textEditor = ((DocumentTabForm)dockPanel.ActiveDocument).TextEditor;
				}
				else {
					textEditor = null;
				}
			};

			if (currentScript != null) {
				LoadScript(currentScript);
			}
			PopulateList();
		}

		private class ScriptTreeNode: TreeNode {
			public ScriptTreeNode(Script script, string displayName)
				: base(displayName) {
				this.Script = script;
			}

			public Script Script {
				get;
				private set;
			}
		}

		private void PopulateList() {
			foreach (Script s in scripts) {
				string[] nameParts = s.Name.Split('/');


				TreeNodeCollection nodes = filesTreeView.Nodes;
				for (int i = 0; i < nameParts.Length - 1; ++i) {
					TreeNode[] currentNodes = nodes.Find(nameParts[i], false);
					TreeNode currentNode = null;
					if (currentNodes.Length == 0) {
						currentNode = nodes.Add(nameParts[i], nameParts[i]);
					}
					else {
						currentNode = currentNodes[i];
					}
					nodes = currentNode.Nodes;
				}
				nodes.Add(new ScriptTreeNode(s, nameParts[nameParts.Length - 1]));
			}
			if (scripts.Contains(currentScript)) {
				TreeNodeCollection nodes = filesTreeView.Nodes;
				string[] nameParts = currentScript.Name.Split('/');
				for (int i = 0; i < nameParts.Length - 1; ++i) {
					nodes = nodes[nameParts[i]].Nodes;
				}
				TreeNode n = null;
				for (int i = 0; i < nodes.Count; ++i) {
					if (nodes[i].Text == nameParts[nameParts.Length - 1]) {
						n = nodes[i];
						break;
					}
				}
				n.EnsureVisible();
				filesTreeView.SelectedNode = n;
			}
		}

		private void LoadScript(Script script) {
			//If the document is already open, simply switch to it
			foreach (var docBase in dockPanel.Documents) {
				DocumentTabForm doc = (DocumentTabForm)docBase;
				if (doc.Script == script) {
					doc.DockHandler.Activate();
					return;
				}
			}
			//If it wasn't, open it
			new DocumentTabForm(currentScript).DockHandler.Show(this.dockPanel, DockState.Document);
		}

		private void filesTreeView_DoubleClick(object sender, EventArgs e) {
			string name = filesTreeView.SelectedNode.FullPath;
			currentScript = scripts.Find(sc => sc.Name == name);
			LoadScript(currentScript);
		}
	}
}

