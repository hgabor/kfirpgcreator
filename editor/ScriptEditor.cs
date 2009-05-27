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

		const string PageKey = "page";
		const string EditedPageKey = "editedpage";
		const string FolderKey = "folder";

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

			ImageList images = new ImageList();
			images.ColorDepth = ColorDepth.Depth32Bit;
			images.Images.Add(PageKey, KFIRPG.editor.Properties.Resources.page);
			images.Images.Add(EditedPageKey, KFIRPG.editor.Properties.Resources.page_red);
			images.Images.Add(FolderKey, KFIRPG.editor.Properties.Resources.folder);
			filesTreeView.ImageList = images;

			PopulateList();

			if (currentScript != null) {
				LoadScript(currentScript);
			}
		}

		private class ScriptTreeNode: TreeNode {
			public ScriptTreeNode(Script script, string displayName)
				: base(displayName) {
				this.Script = script;
				this.ImageKey = PageKey;
				this.SelectedImageKey = PageKey;
			}

			public Script Script {
				get;
				private set;
			}
		}

		private TreeNode FindNode(Script script) {
			TreeNodeCollection nodes = filesTreeView.Nodes;
			string[] nameParts = script.Name.Split('/');
			for (int i = 0; i < nameParts.Length - 1; ++i) {
				nodes = nodes[nameParts[i]].Nodes;
			}
			for (int i = 0; i < nodes.Count; ++i) {
				if (nodes[i].Text == nameParts[nameParts.Length - 1]) {
					return nodes[i];
				}
			}
			return null;
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
						currentNode.ImageKey = FolderKey;
						currentNode.SelectedImageKey = FolderKey;
					}
					else {
						currentNode = currentNodes[i];
					}
					nodes = currentNode.Nodes;
				}
				nodes.Add(new ScriptTreeNode(s, nameParts[nameParts.Length - 1]));
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
			var newTabForm = new DocumentTabForm(script);
			newTabForm.DockHandler.Show(this.dockPanel, DockState.Document);

			TreeNode n = FindNode(script);

			newTabForm.TextEditor.TextChanged += (sender, args) => {
				n.ImageKey = EditedPageKey;
				n.SelectedImageKey = EditedPageKey;
			};
			newTabForm.Saved += (sender, args) => {
				n.ImageKey = PageKey;
				n.SelectedImageKey = PageKey;
			};
			newTabForm.FormClosed += (sender, args) => {
				n.ImageKey = PageKey;
				n.SelectedImageKey = PageKey;

				//If this was the last document closed
				if (dockPanel.DocumentsCount == 1) {
					saveToolStripButton.Enabled = false;
				}
			};

			n.EnsureVisible();
			filesTreeView.SelectedNode = n;
			saveToolStripButton.Enabled = true;
		}

		private void filesTreeView_DoubleClick(object sender, EventArgs e) {
			if (!(filesTreeView.SelectedNode is ScriptTreeNode)) return;
			string name = filesTreeView.SelectedNode.FullPath;
			currentScript = scripts.Find(sc => sc.Name == name);
			LoadScript(currentScript);
		}

		private void newToolStripButton_Click(object sender, EventArgs e) {
			TreeNode selectedNode = filesTreeView.SelectedNode;
			TreeNodeCollection insertHere;
			string scriptPath;
			if (selectedNode == null) {
				insertHere = filesTreeView.Nodes;
				scriptPath = "";
			}
			else {
				if (selectedNode is ScriptTreeNode) {
					if (selectedNode.Parent == null) {
						insertHere = filesTreeView.Nodes;
						scriptPath = "";
					}
					else {
						insertHere = selectedNode.Parent.Nodes;
						scriptPath = selectedNode.Parent.FullPath + "/";
					}
				}
				else {
					insertHere = selectedNode.Nodes;
					scriptPath = selectedNode.FullPath + "/";
				}
			}
			string scriptName;
			using (ComposedForm f = new ComposedForm("New Script", ComposedForm.Parts.Name)) {
				f.AddNameChecker(s => {
					return !scripts.Exists(script => script.Name == scriptPath + s);
				});
				f.AddNameChecker(s => {
					foreach (char c in Path.GetInvalidFileNameChars()) {
						if (s.IndexOf(c) != -1) return false;
					}
					return true;
				});
				if (f.ShowDialog() == DialogResult.OK) {
					scriptName = f.GetName();
				}
				else {
					return;
				}
			}
			Script newScript = new Script(scriptPath + scriptName, "");
			insertHere.Add(new ScriptTreeNode(newScript, newScript.ShortName));
			scripts.Add(newScript);
			LoadScript(newScript);
		}

		private void saveToolStripButton_Click(object sender, EventArgs e) {
			if (dockPanel.ActiveDocument == null) {
				throw new InvalidOperationException("There is no document open to save.");
			}
			((DocumentTabForm)dockPanel.ActiveDocument).Save();
		}
	}
}

