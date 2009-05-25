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
			System.Windows.Forms.SplitContainer splitContainer;
			this.filesTreeView = new System.Windows.Forms.TreeView();
			this.dockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
			splitContainer = new System.Windows.Forms.SplitContainer();
			splitContainer.Panel1.SuspendLayout();
			splitContainer.Panel2.SuspendLayout();
			splitContainer.SuspendLayout();
			this.toolStrip.SuspendLayout();
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
			splitContainer.Panel1.Controls.Add(this.filesTreeView);
			// 
			// splitContainer.Panel2
			// 
			splitContainer.Panel2.Controls.Add(this.dockPanel);
			splitContainer.Size = new System.Drawing.Size(535, 372);
			splitContainer.SplitterDistance = 178;
			splitContainer.TabIndex = 11;
			// 
			// filesTreeView
			// 
			this.filesTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.filesTreeView.HideSelection = false;
			this.filesTreeView.Location = new System.Drawing.Point(0, 0);
			this.filesTreeView.Name = "filesTreeView";
			this.filesTreeView.PathSeparator = "/";
			this.filesTreeView.Size = new System.Drawing.Size(178, 372);
			this.filesTreeView.TabIndex = 9;
			this.filesTreeView.DoubleClick += new System.EventHandler(this.filesTreeView_DoubleClick);
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
			// toolStrip
			// 
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton,
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
			this.newToolStripButton.Image = global::KFIRPG.editor.Properties.Resources.page_white;
			this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.newToolStripButton.Name = "newToolStripButton";
			this.newToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.newToolStripButton.Text = "newToolStripButton";
			this.newToolStripButton.Click += new System.EventHandler(this.newToolStripButton_Click);
			// 
			// saveToolStripButton
			// 
			this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.saveToolStripButton.Image = global::KFIRPG.editor.Properties.Resources.disk;
			this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.saveToolStripButton.Name = "saveToolStripButton";
			this.saveToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.saveToolStripButton.Text = "saveToolStripButton";
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
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel;
		private System.Windows.Forms.TreeView filesTreeView;
		private System.Windows.Forms.ToolStrip toolStrip;
		private System.Windows.Forms.ToolStripButton newToolStripButton;
		private System.Windows.Forms.ToolStripButton saveToolStripButton;
	}
}