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

		class ScriptNode: Node {
			public Script Script { get; private set; }

			public ScriptNode(Script script) {
				this.Script = script;
				this.Image = KFIRPG.editor.Properties.Resources.page;
			}

			public ScriptNode(ScriptNode node) {
				this.Script = new Script(node.Script);
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

		private DocumentTabForm FindOpenDocument(Script script) {
			foreach (var docBase in dockPanel.Documents) {
				DocumentTabForm doc = (DocumentTabForm)docBase;
				if (doc.Script == script) {
					return doc;
				}
			}
			return null;
		}

		private void LoadScript(Script script) {
			//If the document is already open, simply switch to it
			DocumentTabForm doc = FindOpenDocument(script);
			if (doc != null) {
				doc.DockHandler.Activate();
				return;
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

		private void newScript_Handler(object sender, EventArgs e) {
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

		private void scriptsTreeView_DoubleClick(object sender, EventArgs e) {
			if (scriptsTreeView.SelectedNode == null) return;
			if (scriptsTreeView.SelectedNode.Tag is ScriptNode) {
				LoadScript(((ScriptNode)scriptsTreeView.SelectedNode.Tag).Script);
			}
		}

		private void scriptsTreeView_MouseClick(object sender, MouseEventArgs e) {
			//Check for right clicks to open context menu
			if (e.Button == MouseButtons.Right &&
				scriptsTreeView.SelectedNode != null &&
				scriptsTreeView.SelectedNode.Tag is ScriptNode) {
				scriptContextMenu.Show(scriptsTreeView, e.Location);
			}
			else if (e.Button == MouseButtons.Right &&
				scriptsTreeView.SelectedNode != null &&
				scriptsTreeView.SelectedNode.Tag is FolderNode) {
				folderContextMenu.Show(scriptsTreeView, e.Location);
			}
		}

		private void DeleteNodeRecursive(Node node) {
			if (node is ScriptNode) {
				Script script = ((ScriptNode)node).Script;
				//Close associated editor window
				FindOpenDocument(script).Close();
				scripts.Remove(script);
			}
			else {
				while (node.Nodes.Count != 0) {
					DeleteNodeRecursive(node.Nodes[0]);
				}
			}
			node.Parent.Nodes.Remove(node);
		}

		private void CorrectScriptNameRecursive(Node node) {
			if (node is ScriptNode) {
				string[] sa = Array.ConvertAll<object, string>(scriptsModel.GetPath(node).FullPath, o => o.ToString());
				string s = String.Join("/", sa);
				Script scr = ((ScriptNode)node).Script;
				scr.Name = s;
			}
			else {
				foreach (Node child in node.Nodes) {
					CorrectScriptNameRecursive(child);
				}
			}
		}

		private void MoveNode(Node thisNode, Node dropNode) {
			if (dropNode.Nodes.Contains(thisNode)) return;
			thisNode.Parent.Nodes.Remove(thisNode);
			dropNode.Nodes.Add(thisNode);
			CorrectScriptNameRecursive(thisNode);
		}

		private void CopyNodeRecursive(Node thisNode, Node dropNode) {
			if (thisNode is ScriptNode) {
				ScriptNode newNode = new ScriptNode((ScriptNode)thisNode);
				scripts.Add(newNode.Script);
				dropNode.Nodes.Add(newNode);
				CorrectScriptNameRecursive(newNode);
			}
			else {
				FolderNode newNode = new FolderNode(thisNode.Text);
				dropNode.Nodes.Add(newNode);
				foreach (Node childNode in thisNode.Nodes) {
					CopyNodeRecursive(childNode, newNode);
				}
			}
		}

		private void CopyNode(Node thisNode, Node dropNode) {
			if (dropNode.Nodes.Contains(thisNode)) return;
			CopyNodeRecursive(thisNode, dropNode);
		}

		//TODO: Merge the four functions into two
		private void deleteScriptToolStripMenuItem_Click(object sender, EventArgs e) {
			ScriptNode n = (ScriptNode)scriptsTreeView.SelectedNode.Tag;
			if (MessageBox.Show(
				string.Format("Are you sure you want to delete script \"{0}\"?", n.Script.ShortName),
				"Delete script",
				MessageBoxButtons.YesNo) == DialogResult.Yes) {
				DeleteNodeRecursive(n);
			}
		}

		private void renameScriptToolStripMenuItem_Click(object sender, EventArgs e) {
			ScriptNode n = (ScriptNode)scriptsTreeView.SelectedNode.Tag;
			using (ComposedForm form = new ComposedForm("Rename script", ComposedForm.Parts.Name)) {
				form.AddNameChecker(name => !HasChild(n.Parent, s => name == s.Text));
				form.SetName(n.Text);
				if (form.ShowDialog() == DialogResult.OK) {
					n.Script.ShortName = form.GetName();
				}
			}
		}

		private void deleteFolderToolStripMenuItem_Click(object sender, EventArgs e) {
			FolderNode n = (FolderNode)scriptsTreeView.SelectedNode.Tag;
			if (MessageBox.Show(
				string.Format("Are you sure you want to delete folder \"{0}\" and all of its contents?", n.Text),
				"Delete folder",
				MessageBoxButtons.YesNo) == DialogResult.Yes) {
				DeleteNodeRecursive(n);
			}

		}

		private void renameFolderToolStripMenuItem_Click(object sender, EventArgs e) {
			FolderNode n = (FolderNode)scriptsTreeView.SelectedNode.Tag;
			using (ComposedForm form = new ComposedForm("Rename folder", ComposedForm.Parts.Name)) {
				form.AddNameChecker(name => !HasChild(n.Parent, s => name == s.Text));
				form.SetName(n.Text);
				if (form.ShowDialog() == DialogResult.OK) {
					n.Text = form.GetName();
					CorrectScriptNameRecursive(n);
				}
			}
		}

		private void scriptsTreeView_ItemDrag(object sender, ItemDragEventArgs e) {
			TreeNodeAdv node = ((TreeNodeAdv[])e.Item)[0];
			if (node.Tag is ScriptNode || node.Tag is FolderNode) {
				Node snode = (Node)node.Tag;
				DoDragDrop(snode, DragDropEffects.Copy | DragDropEffects.Move);
			}
			else {
				throw new InvalidOperationException("You tried to drag a node that is not a folder nor a script.");
			}
		}

		private Node GetRealInsertNode(TreeNodeAdv node) {
			if (node == null) {
				return scriptsModel.Root;
			}
			else if (node.Tag is ScriptNode) {
				return ((ScriptNode)node.Tag).Parent;
			}
			else if (node.Tag is FolderNode) {
				return (Node)node.Tag;
			}
			throw new ArgumentException("Invalid TreeNode", "node");
		}

		private object GetData(IDataObject data) {
			return data.GetData(data.GetFormats()[0]);
		}

		private bool HasAncestor(Node node, Predicate<Node> pred) {
			if (node.Parent == null) {
				return false;
			}
			else if (pred(node.Parent)) {
				return true;
			}
			else {
				return HasAncestor(node.Parent, pred);
			}
		}

		private bool HasChild(Node node, Predicate<Node> pred) {
			foreach (Node child in node.Nodes) {
				if (pred(child)) return true;
			}
			return false;
		}

		const int LEFT_BUTTON = 1;
		const int RIGHT_BUTTON = 2;
		const int SHIFT_KEY = 4;
		const int CTRL_KEY = 8;
		const int MIDDLE_BUTTON = 16;
		const int ALT_KEY = 32;

		private void scriptsTreeView_DragOver(object sender, DragEventArgs e) {
			object o = GetData(e.Data);
			if (!(o is Node)) {
				e.Effect = DragDropEffects.None;
				return;
			}
			Node draggedNode = (Node)o;
			TreeNodeAdv node = scriptsTreeView.DropPosition.Node;
			Node insertHere = GetRealInsertNode(node);

			if (HasChild(insertHere, n => n.Text == draggedNode.Text)) {
				e.Effect = DragDropEffects.None;
				return;
			}
			if (insertHere == draggedNode || HasAncestor(insertHere, n => n == draggedNode)) {
				e.Effect = DragDropEffects.None;
				return;
			}

			if ((e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy &&
				(e.KeyState & CTRL_KEY) == CTRL_KEY) {
				e.Effect = DragDropEffects.Copy;
			}
			else if ((e.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move) {
				e.Effect = DragDropEffects.Move;
			}
			else {
				throw new InvalidOperationException("You shouldn't be able to drag an undroppable object");
			}
		}

		private void scriptsTreeView_DragDrop(object sender, DragEventArgs e) {
			object o = GetData(e.Data);
			if (!(o is Node)) {
				e.Effect = DragDropEffects.None;
				return;
			}
			//TreeNodeAdv node = scriptsTreeView.GetNodeAt(scriptsTreeView.PointToClient(new Point(e.X, e.Y)));
			TreeNodeAdv node = scriptsTreeView.DropPosition.Node;
			Node insertHere = GetRealInsertNode(node);
			if (e.Effect == DragDropEffects.Move) {
				MoveNode((Node)o, insertHere);
				return;
			}
			if (e.Effect == DragDropEffects.Copy) {
				CopyNode((Node)o, insertHere);
				return;
			}
			throw new InvalidOperationException();
		}
	}
}

