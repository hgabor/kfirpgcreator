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

		class ScriptNode: Node, IComparable<ScriptNode> {
			public Script Script { get; private set; }
			public bool IsFolder {
				get { return Script.IsFolder; }
			}

			public ScriptNode(Script script) {
				this.Script = script;
				if (this.IsFolder) {
					this.Image = KFIRPG.editor.Properties.Resources.folder;
				}
				else {
					this.Image = KFIRPG.editor.Properties.Resources.page;
				}
			}

			public ScriptNode(ScriptNode node) {
				this.Script = new Script(node.Script);
				if (this.IsFolder) {
					this.Image = KFIRPG.editor.Properties.Resources.folder;
				}
				else {
					this.Image = KFIRPG.editor.Properties.Resources.page;
				}
			}

			public override string Text {
				get {
					return Script.ShortName;
				}
				set {
					throw new NotImplementedException();
				}
			}

			#region IComparable<ScriptNode> Members

			public int CompareTo(ScriptNode other) {
				return this.Script.CompareTo(other.Script);
			}

			#endregion
		}

		/// <summary>
		/// Opens the script editor window with no scripts open.
		/// </summary>
		/// <param name="project"></param>
		public ScriptEditor(Project project)
			: this(null, project) {
		}

		/// <summary>
		/// Opens the script editor window with the selected script open.
		/// </summary>
		/// <param name="currentScript">The script to open</param>
		/// <param name="project"></param>
		public ScriptEditor(Script currentScript, Project project) {
			InitializeComponent();
			scripts = project.scripts;

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

		private void InsertNodeSorted(ScriptNode node, Collection<Node> parent) {
			int i = 0;
			//Find where to insert
			while (i < parent.Count && node.CompareTo((ScriptNode)parent[i]) > 0) {
				++i;
			}
			parent.Insert(i, node);
		}

		/// <summary>
		/// Populates the tree view with the project's scripts.
		/// </summary>
		private void PopulateList() {
			scriptsTreeView.BeginUpdate();

			scripts.Sort();

			foreach (Script s in scripts) {
				string[] nameParts = s.Name.Split('/');

				Collection<Node> sNodes = scriptsModel.Nodes;

				for (int i = 0; i < nameParts.Length - 1; ++i) {
					//Check if folder exists
					Node folderNode = FindNodeByName(nameParts[i], sNodes);

					if (folderNode == null) {
						throw new Exception("Invalid folder name");
					}
					sNodes = folderNode.Nodes;
				}
				InsertNodeSorted(new ScriptNode(s), sNodes);
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

		/// <summary>
		/// Opens a script in the editor. If the script is already open, it activates the open tab.
		/// </summary>
		/// <param name="script"></param>
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

		/// <summary>
		/// Creates a new script. Asks the user for the name.
		/// </summary>
		/// <param name="isFolder"></param>
		private void CreateNewScript(bool isFolder) {
			Node n;
			if (scriptsTreeView.SelectedNode != null) {
				n = (Node)scriptsTreeView.SelectedNode.Tag;
			}
			else {
				n = scriptsModel.Root;
			}

			Collection<Node> nInsertHere;
			if (n is ScriptNode && !((ScriptNode)n).IsFolder) {
				nInsertHere = n.Parent.Nodes;
				n = n.Parent;
			}
			else if (((ScriptNode)n).IsFolder || n == scriptsModel.Root) {
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
			string caption = isFolder ? "New Folder" : "New Script";

			using (ComposedForm f = new ComposedForm(caption, ComposedForm.Parts.Name)) {
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

			if (isFolder) {
				Script newScript = new Script(scriptPath + scriptName);
				InsertNodeSorted(new ScriptNode(newScript), nInsertHere);
				scripts.Add(newScript);
			}
			else {
				Script newScript = new Script(scriptPath + scriptName, "");
				InsertNodeSorted(new ScriptNode(newScript), nInsertHere);
				scripts.Add(newScript);
				LoadScript(newScript);
			}
		}

		private void newScript_Handler(object sender, EventArgs e) {
			CreateNewScript(false);
		}

		private void newFolder_Handler(object sender, EventArgs e) {
			CreateNewScript(true);
		}

		private void saveToolStripButton_Click(object sender, EventArgs e) {
			if (dockPanel.ActiveDocument == null) {
				throw new InvalidOperationException("There is no document open to save.");
			}
			((DocumentTabForm)dockPanel.ActiveDocument).Save();
		}

		private void scriptsTreeView_DoubleClick(object sender, EventArgs e) {
			if (scriptsTreeView.SelectedNode == null) return;
			if (scriptsTreeView.SelectedNode.Tag is ScriptNode &&
				!((ScriptNode)scriptsTreeView.SelectedNode.Tag).IsFolder) {
				LoadScript(((ScriptNode)scriptsTreeView.SelectedNode.Tag).Script);
			}
		}

		private void scriptsTreeView_MouseClick(object sender, MouseEventArgs e) {
			//Check for right clicks to open context menu
			if (e.Button == MouseButtons.Right &&
				scriptsTreeView.SelectedNode != null &&
				!((ScriptNode)scriptsTreeView.SelectedNode.Tag).IsFolder) {
				scriptContextMenu.Show(scriptsTreeView, e.Location);
			}
			else if (e.Button == MouseButtons.Right &&
				scriptsTreeView.SelectedNode != null &&
				((ScriptNode)scriptsTreeView.SelectedNode.Tag).IsFolder) {
				folderContextMenu.Show(scriptsTreeView, e.Location);
			}
		}

		/// <summary>
		/// Deletes a node and all child nodes recursively. Removes to associated scripts too.
		/// </summary>
		/// <param name="basenode"></param>
		private void DeleteNodeRecursive(Node basenode) {
			ScriptNode node = (ScriptNode)basenode;

			Script script = ((ScriptNode)node).Script;
			//Close associated editor window
			DocumentTabForm opendoc = FindOpenDocument(script);
			if (opendoc != null) opendoc.Close();
			scripts.Remove(script);

			if (node.IsFolder) {
				while (node.Nodes.Count != 0) {
					DeleteNodeRecursive(node.Nodes[0]);
				}
			}
			node.Parent.Nodes.Remove(node);
		}

		/// <summary>
		/// After a rename/move/copy, the full script names will be invalid: this function repairs them.
		/// </summary>
		/// <param name="node"></param>
		private void CorrectScriptNameRecursive(Node node) {
			if (node is ScriptNode) {
				string[] sa = Array.ConvertAll<object, string>(scriptsModel.GetPath(node).FullPath, o => o.ToString());
				string s = String.Join("/", sa);
				Script scr = ((ScriptNode)node).Script;
				scr.Name = s;
			}
			foreach (Node child in node.Nodes) {
				CorrectScriptNameRecursive(child);
			}
		}

		/// <summary>
		/// Moves a node and all child nodes to another node. Full names will be correct after the move.
		/// </summary>
		/// <param name="thisNode"></param>
		/// <param name="dropNode"></param>
		private void MoveNode(Node thisNode, Node dropNode) {
			if (dropNode.Nodes.Contains(thisNode)) return;
			thisNode.Parent.Nodes.Remove(thisNode);
			InsertNodeSorted((ScriptNode)thisNode, dropNode.Nodes);
			CorrectScriptNameRecursive(thisNode);
		}

		/// <summary>
		/// Moves a node and all child nodes to another node. Full names will be correct after the copy.
		/// Do not call this function directly, use the safer CopyNode instead.
		/// </summary>
		/// <param name="thisNode"></param>
		/// <param name="dropNode"></param>
		private void CopyNodeRecursive(Node thisNode, Node dropNode) {
			ScriptNode newNode = new ScriptNode((ScriptNode)thisNode);
			scripts.Add(newNode.Script);
			InsertNodeSorted(newNode, dropNode.Nodes);
			CorrectScriptNameRecursive(newNode);
			if (newNode.IsFolder) {
				foreach (Node childNode in thisNode.Nodes) {
					CopyNodeRecursive(childNode, newNode);
				}
			}
		}

		/// <summary>
		/// Moves a node and all child nodes to another node. Full names will be correct after the copy.
		/// Will not do anything, if trying to copy into one of the node's child nodes.
		/// </summary>
		/// <param name="thisNode"></param>
		/// <param name="dropNode"></param>
		private void CopyNode(Node thisNode, Node dropNode) {
			if (dropNode.Nodes.Contains(thisNode)) return;
			CopyNodeRecursive(thisNode, dropNode);
		}

		static string qScript = "Are you sure you want to delete script \"{0}\"?";
		static string cScript = "Delete script";
		static string qFolder = "Are you sure you want to delete folder \"{0}\" and all of its contents?";
		static string cFolder = "Delete folder";
		private void deleteScriptToolStripMenuItem_Click(object sender, EventArgs e) {
			ScriptNode n = (ScriptNode)scriptsTreeView.SelectedNode.Tag;
			string question = n.IsFolder ? qFolder : qScript;
			string caption = n.IsFolder ? cFolder : cScript;
			
			if (MessageBox.Show(
				string.Format(question, n.Script.ShortName),
				caption,
				MessageBoxButtons.YesNo) == DialogResult.Yes) {
				DeleteNodeRecursive(n);
			}
		}

		private void renameScriptToolStripMenuItem_Click(object sender, EventArgs e) {
			ScriptNode n = (ScriptNode)scriptsTreeView.SelectedNode.Tag;
			string caption = n.IsFolder ? "Rename folder" : "Rename script";
			using (ComposedForm form = new ComposedForm("Rename script", ComposedForm.Parts.Name)) {
				form.AddNameChecker(name => !HasChild(n.Parent, s => name == s.Text));
				form.SetName(n.Text);
				if (form.ShowDialog() == DialogResult.OK) {
					n.Script.ShortName = form.GetName();
					CorrectScriptNameRecursive(n);
					//Re-add node to place alphabetically
					Collection<Node> parent = n.Parent.Nodes;
					parent.Remove(n);
					InsertNodeSorted(n, parent);
				}
			}
		}

		private void scriptsTreeView_ItemDrag(object sender, ItemDragEventArgs e) {
			TreeNodeAdv node = ((TreeNodeAdv[])e.Item)[0];
			if (node.Tag is ScriptNode) {
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
				if (((ScriptNode)node.Tag).IsFolder) {
					return (Node)node.Tag;
				}
				else {
					return ((ScriptNode)node.Tag).Parent;
				}
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

