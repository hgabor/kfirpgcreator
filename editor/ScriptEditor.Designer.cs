namespace KFIRPG.editor {
	partial class ScriptEditor {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.SplitContainer splitContainer;
			this.scriptsTreeView = new Aga.Controls.Tree.TreeViewAdv();
			this.nodeIcon1 = new Aga.Controls.Tree.NodeControls.NodeIcon();
			this.nodeTextBox1 = new Aga.Controls.Tree.NodeControls.NodeTextBox();
			this.dockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
			this.scriptContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.renameScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.newFolderToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.folderContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.newScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.renameFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			splitContainer = new System.Windows.Forms.SplitContainer();
			splitContainer.Panel1.SuspendLayout();
			splitContainer.Panel2.SuspendLayout();
			splitContainer.SuspendLayout();
			this.scriptContextMenu.SuspendLayout();
			this.toolStrip.SuspendLayout();
			this.folderContextMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer
			// 
			splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			splitContainer.Location = new System.Drawing.Point(0, 25);
			splitContainer.Name = "splitContainer";
			// 
			// splitContainer.Panel1
			// 
			splitContainer.Panel1.Controls.Add(this.scriptsTreeView);
			// 
			// splitContainer.Panel2
			// 
			splitContainer.Panel2.Controls.Add(this.dockPanel);
			splitContainer.Size = new System.Drawing.Size(535, 372);
			splitContainer.SplitterDistance = 178;
			splitContainer.TabIndex = 11;
			// 
			// scriptsTreeView
			// 
			this.scriptsTreeView.AllowDrop = true;
			this.scriptsTreeView.BackColor = System.Drawing.SystemColors.Window;
			this.scriptsTreeView.DefaultToolTipProvider = null;
			this.scriptsTreeView.DisplayDraggingNodes = true;
			this.scriptsTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scriptsTreeView.DragDropMarkColor = System.Drawing.Color.Black;
			this.scriptsTreeView.LineColor = System.Drawing.SystemColors.ControlDark;
			this.scriptsTreeView.Location = new System.Drawing.Point(0, 0);
			this.scriptsTreeView.Model = null;
			this.scriptsTreeView.Name = "scriptsTreeView";
			this.scriptsTreeView.NodeControls.Add(this.nodeIcon1);
			this.scriptsTreeView.NodeControls.Add(this.nodeTextBox1);
			this.scriptsTreeView.SelectedNode = null;
			this.scriptsTreeView.Size = new System.Drawing.Size(178, 372);
			this.scriptsTreeView.TabIndex = 10;
			this.scriptsTreeView.Text = "Scripts";
			this.scriptsTreeView.DragOver += new System.Windows.Forms.DragEventHandler(this.scriptsTreeView_DragOver);
			this.scriptsTreeView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.scriptsTreeView_MouseClick);
			this.scriptsTreeView.DoubleClick += new System.EventHandler(this.scriptsTreeView_DoubleClick);
			this.scriptsTreeView.DragDrop += new System.Windows.Forms.DragEventHandler(this.scriptsTreeView_DragDrop);
			this.scriptsTreeView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.scriptsTreeView_ItemDrag);
			// 
			// nodeIcon1
			// 
			this.nodeIcon1.DataPropertyName = "Image";
			this.nodeIcon1.LeftMargin = 1;
			this.nodeIcon1.ParentColumn = null;
			// 
			// nodeTextBox1
			// 
			this.nodeTextBox1.DataPropertyName = "Text";
			this.nodeTextBox1.EditEnabled = false;
			this.nodeTextBox1.IncrementalSearchEnabled = true;
			this.nodeTextBox1.LeftMargin = 3;
			this.nodeTextBox1.ParentColumn = null;
			// 
			// dockPanel
			// 
			this.dockPanel.ActiveAutoHideContent = null;
			this.dockPanel.AllowEndUserDocking = false;
			this.dockPanel.AllowEndUserNestedDocking = false;
			this.dockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dockPanel.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingWindow;
			this.dockPanel.Location = new System.Drawing.Point(0, 0);
			this.dockPanel.Name = "dockPanel";
			this.dockPanel.Size = new System.Drawing.Size(353, 372);
			this.dockPanel.TabIndex = 8;
			// 
			// scriptContextMenu
			// 
			this.scriptContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.renameScriptToolStripMenuItem,
            this.deleteScriptToolStripMenuItem});
			this.scriptContextMenu.Name = "scriptContextMenu";
			this.scriptContextMenu.Size = new System.Drawing.Size(114, 48);
			// 
			// renameScriptToolStripMenuItem
			// 
			this.renameScriptToolStripMenuItem.Image = global::KFIRPG.editor.Properties.Resources.pencil;
			this.renameScriptToolStripMenuItem.Name = "renameScriptToolStripMenuItem";
			this.renameScriptToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
			this.renameScriptToolStripMenuItem.Text = "Rename";
			this.renameScriptToolStripMenuItem.Click += new System.EventHandler(this.renameScriptToolStripMenuItem_Click);
			// 
			// deleteScriptToolStripMenuItem
			// 
			this.deleteScriptToolStripMenuItem.Image = global::KFIRPG.editor.Properties.Resources.cross;
			this.deleteScriptToolStripMenuItem.Name = "deleteScriptToolStripMenuItem";
			this.deleteScriptToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
			this.deleteScriptToolStripMenuItem.Text = "Delete";
			this.deleteScriptToolStripMenuItem.Click += new System.EventHandler(this.deleteScriptToolStripMenuItem_Click);
			// 
			// toolStrip
			// 
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton,
            this.newFolderToolStripButton,
            this.saveToolStripButton});
			this.toolStrip.Location = new System.Drawing.Point(0, 0);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Size = new System.Drawing.Size(535, 25);
			this.toolStrip.TabIndex = 10;
			this.toolStrip.Text = "toolStrip";
			// 
			// newToolStripButton
			// 
			this.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.newToolStripButton.Image = global::KFIRPG.editor.Properties.Resources.page_white_add;
			this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.newToolStripButton.Name = "newToolStripButton";
			this.newToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.newToolStripButton.Text = "newToolStripButton";
			this.newToolStripButton.Click += new System.EventHandler(this.newScript_Handler);
			// 
			// newFolderToolStripButton
			// 
			this.newFolderToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.newFolderToolStripButton.Image = global::KFIRPG.editor.Properties.Resources.folder_add;
			this.newFolderToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.newFolderToolStripButton.Name = "newFolderToolStripButton";
			this.newFolderToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.newFolderToolStripButton.Text = "toolStripButton1";
			this.newFolderToolStripButton.Click += new System.EventHandler(this.newFolder_Handler);
			// 
			// saveToolStripButton
			// 
			this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.saveToolStripButton.Enabled = false;
			this.saveToolStripButton.Image = global::KFIRPG.editor.Properties.Resources.disk;
			this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.saveToolStripButton.Name = "saveToolStripButton";
			this.saveToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.saveToolStripButton.Text = "saveToolStripButton";
			this.saveToolStripButton.Click += new System.EventHandler(this.saveToolStripButton_Click);
			// 
			// folderContextMenu
			// 
			this.folderContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newScriptToolStripMenuItem,
            this.newFolderToolStripMenuItem,
            this.toolStripSeparator1,
            this.renameFolderToolStripMenuItem,
            this.deleteFolderToolStripMenuItem});
			this.folderContextMenu.Name = "folderContextMenu";
			this.folderContextMenu.Size = new System.Drawing.Size(153, 120);
			// 
			// newScriptToolStripMenuItem
			// 
			this.newScriptToolStripMenuItem.Image = global::KFIRPG.editor.Properties.Resources.page_white_add;
			this.newScriptToolStripMenuItem.Name = "newScriptToolStripMenuItem";
			this.newScriptToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.newScriptToolStripMenuItem.Text = "New Script...";
			this.newScriptToolStripMenuItem.Click += new System.EventHandler(this.newScript_Handler);
			// 
			// newFolderToolStripMenuItem
			// 
			this.newFolderToolStripMenuItem.Image = global::KFIRPG.editor.Properties.Resources.folder_add;
			this.newFolderToolStripMenuItem.Name = "newFolderToolStripMenuItem";
			this.newFolderToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.newFolderToolStripMenuItem.Text = "New Folder...";
			this.newFolderToolStripMenuItem.Click += new System.EventHandler(this.newFolder_Handler);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
			// 
			// renameFolderToolStripMenuItem
			// 
			this.renameFolderToolStripMenuItem.Image = global::KFIRPG.editor.Properties.Resources.pencil;
			this.renameFolderToolStripMenuItem.Name = "renameFolderToolStripMenuItem";
			this.renameFolderToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.renameFolderToolStripMenuItem.Text = "Rename...";
			this.renameFolderToolStripMenuItem.Click += new System.EventHandler(this.renameScriptToolStripMenuItem_Click);
			// 
			// deleteFolderToolStripMenuItem
			// 
			this.deleteFolderToolStripMenuItem.Image = global::KFIRPG.editor.Properties.Resources.cross;
			this.deleteFolderToolStripMenuItem.Name = "deleteFolderToolStripMenuItem";
			this.deleteFolderToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.deleteFolderToolStripMenuItem.Text = "Delete";
			this.deleteFolderToolStripMenuItem.Click += new System.EventHandler(this.deleteScriptToolStripMenuItem_Click);
			// 
			// ScriptEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(535, 397);
			this.Controls.Add(splitContainer);
			this.Controls.Add(this.toolStrip);
			this.Name = "ScriptEditor";
			this.Text = "ScriptEditor";
			splitContainer.Panel1.ResumeLayout(false);
			splitContainer.Panel2.ResumeLayout(false);
			splitContainer.ResumeLayout(false);
			this.scriptContextMenu.ResumeLayout(false);
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.folderContextMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel;
		private System.Windows.Forms.ToolStrip toolStrip;
		private System.Windows.Forms.ToolStripButton newToolStripButton;
		private System.Windows.Forms.ToolStripButton saveToolStripButton;
		private Aga.Controls.Tree.TreeViewAdv scriptsTreeView;
		private Aga.Controls.Tree.NodeControls.NodeIcon nodeIcon1;
		private Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBox1;
		private System.Windows.Forms.ContextMenuStrip scriptContextMenu;
		private System.Windows.Forms.ToolStripMenuItem deleteScriptToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip folderContextMenu;
		private System.Windows.Forms.ToolStripMenuItem deleteFolderToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem renameFolderToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem renameScriptToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem newScriptToolStripMenuItem;
		private System.Windows.Forms.ToolStripButton newFolderToolStripButton;
		private System.Windows.Forms.ToolStripMenuItem newFolderToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
	}
}