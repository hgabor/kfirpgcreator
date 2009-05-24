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
			System.Windows.Forms.SplitContainer splitContainer1;
			this.filesTreeView = new System.Windows.Forms.TreeView();
			this.dockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			splitContainer1 = new System.Windows.Forms.SplitContainer();
			splitContainer1.Panel1.SuspendLayout();
			splitContainer1.Panel2.SuspendLayout();
			splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			splitContainer1.Location = new System.Drawing.Point(0, 25);
			splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			splitContainer1.Panel1.Controls.Add(this.filesTreeView);
			// 
			// splitContainer1.Panel2
			// 
			splitContainer1.Panel2.Controls.Add(this.dockPanel);
			splitContainer1.Size = new System.Drawing.Size(535, 372);
			splitContainer1.SplitterDistance = 178;
			splitContainer1.TabIndex = 11;
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
			// toolStrip1
			// 
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(535, 25);
			this.toolStrip1.TabIndex = 10;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// ScriptEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(535, 397);
			this.Controls.Add(splitContainer1);
			this.Controls.Add(this.toolStrip1);
			this.Name = "ScriptEditor";
			this.Text = "ScriptEditor";
			splitContainer1.Panel1.ResumeLayout(false);
			splitContainer1.Panel2.ResumeLayout(false);
			splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel;
		private System.Windows.Forms.TreeView filesTreeView;
		private System.Windows.Forms.ToolStrip toolStrip1;
	}
}