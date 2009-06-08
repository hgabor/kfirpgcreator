using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using Aga.Controls.Tree;


namespace KFIRPG.editor {

	partial class ScriptEditor: Form {

		List<Script> scripts;

		TreeModel scriptsModel = new TreeModel();

		ICSharpCode.TextEditor.TextEditorControl textEditor;

		const string PageKey = "page";
		const string EditedPageKey = "editedpage";
		const string FolderKey = "folder";

		class ScriptNode: Node {
			public Script Script { get; private set; }


			public ScriptNode(Script script) {
				this.Script = script;
				this.Image = KFIRPG.editor.Properties.Resources.page;
			}

			public override string Text {
				get {
					return Script.ShortName;
				}
				set {
					throw new NotImplementedException();
				}
			}
		}

		class FolderNode: Node {
			public FolderNode(string name)
				: base(name) {
				this.Image = KFIRPG.editor.Properties.Resources.folder;
			}
		}


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

			scriptsTreeView.Model = scriptsModel;

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

		private ScriptNode FindScriptNode(Script script) {
			Collection<Node> nodes = scriptsModel.Nodes;
			string[] nameParts = script.Name.Split('/');
			for (int i = 0; i < nameParts.Length - 1; ++i) {
				nodes = FindNodeByName(nameParts[i], nodes).Nodes;
			}
			foreach (Node n in nodes) {
				if (n.Text == script.ShortName) {
					if (!(n is ScriptNode)) {
						throw new InvalidOperationException("The node is not a script!");
					}
					return (ScriptNode)n;
				}
			}
			return null;
		}

		private Node FindNodeByName(string name, Collection<Node> root) {
			foreach (Node node in root) {
				if (node.Text == name) return node;
			}
			return null;
		}

		private void PopulateList() {
			scriptsTreeView.BeginUpdate();

			foreach (Script s in scripts) {
				string[] nameParts = s.Name.Split('/');

				Collection<Node> sNodes = scriptsModel.Nodes;

				for (int i = 0; i < nameParts.Length - 1; ++i) {
					//Check if folder already exists
					Node folderNode = FindNodeByName(nameParts[i], sNodes);
					
					if (folderNode == null) {
						folderNode = new FolderNode(nameParts[i]);
						sNodes.Add(folderNode);
					}
					sNodes = folderNode.Nodes;
				}
				sNodes.Add(new ScriptNode(s));
			}

			scriptsTreeView.EndUpdate();
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

			ScriptNode sn = FindScriptNode(script);

			newTabForm.TextEditor.TextChanged += (sender, args) => {
				sn.Image = KFIRPG.editor.Properties.Resources.page_red;
			};
			newTabForm.Saved += (sender, args) => {
				sn.Image = KFIRPG.editor.Properties.Resources.page;
			};
			newTabForm.FormClosed += (sender, args) => {
				sn.Image = KFIRPG.editor.Properties.Resources.page;

				//If this was the last document closed
				if (dockPanel.DocumentsCount == 1) {
					saveToolStripButton.Enabled = false;
				}
			};

			TreeNodeAdv tn = scriptsTreeView.FindNodeByTag(sn);
			scriptsTreeView.EnsureVisible(tn);
			scriptsTreeView.SelectedNode = tn;

			saveToolStripButton.Enabled = true;
		}

		private void newToolStripButton_Click(object sender, EventArgs e) {
			Node n;
			if (scriptsTreeView.SelectedNode != null) {
				n = (Node)scriptsTreeView.SelectedNode.Tag;
			}
			else {
				n = scriptsModel.Root;
			}

			Collection<Node> nInsertHere;
			if (n is ScriptNode) {
				nInsertHere = n.Parent.Nodes;
				n = n.Parent;
			}
			else if (n is FolderNode || n == scriptsModel.Root) {
				nInsertHere = n.Nodes;
			}
			else {
				throw new InvalidOperationException("Tree should only contain folders and scripts");
			}

			string scriptPath = string.Join("/",
				Array.ConvertAll<object, string>(
					scriptsModel.GetPath(n).FullPath,
					o => ((Node)o).Text));
			if (scriptPath != "") {
				scriptPath += "/";
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
			nInsertHere.Add(new ScriptNode(newScript));
			scripts.Add(newScript);
			LoadScript(newScript);
		}

		private void saveToolStripButton_Click(object sender, EventArgs e) {
			if (dockPanel.ActiveDocument == null) {
				throw new InvalidOperationException("There is no document open to save.");
			}
			((DocumentTabForm)dockPanel.ActiveDocument).Save();
		}


		private void filesTreeView_ItemDrag(object sender, ItemDragEventArgs e) {
			if (e.Item is ScriptTreeNode) {
				ScriptTreeNode node = (ScriptTreeNode)e.Item;
				DoDragDrop(node.Script, DragDropEffects.Copy | DragDropEffects.Move);
			}
		}

		private void filesTreeView_DragOver(object sender, DragEventArgs e) {
			//Only allow drop if we are dragging a script
			if (e.Data.GetDataPresent(typeof(Script))) {
				e.Effect = DragDropEffects.Move;
			}
		}

		private void FindTreeNode(int x, int y) {
			
		}

		private void filesTreeView_DragDrop(object sender, DragEventArgs e) {
			//TreeNode node = filesTreeView.PointToClient
			Console.WriteLine();
		}

		private void scriptsTreeView_DoubleClick(object sender, EventArgs e) {
			if (scriptsTreeView.SelectedNode.Tag is ScriptNode) {
				LoadScript(((ScriptNode)scriptsTreeView.SelectedNode.Tag).Script);
			}
		}
	}
}

