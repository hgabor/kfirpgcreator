namespace KFIRPG.editor {
	partial class Palette {
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
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tilePage = new System.Windows.Forms.TabPage();
			this.hScrollBar = new System.Windows.Forms.HScrollBar();
			this.vScrollBar = new System.Windows.Forms.VScrollBar();
			this.passabilityPage = new System.Windows.Forms.TabPage();
			this.impassableButton = new System.Windows.Forms.Button();
			this.passableButton = new System.Windows.Forms.Button();
			this.tilesPanel = new KFIRPG.editor.DoubleBufferedPanel();
			this.tabControl1.SuspendLayout();
			this.tilePage.SuspendLayout();
			this.passabilityPage.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tilePage);
			this.tabControl1.Controls.Add(this.passabilityPage);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(297, 282);
			this.tabControl1.TabIndex = 0;
			// 
			// tilePage
			// 
			this.tilePage.Controls.Add(this.tilesPanel);
			this.tilePage.Controls.Add(this.hScrollBar);
			this.tilePage.Controls.Add(this.vScrollBar);
			this.tilePage.Location = new System.Drawing.Point(4, 22);
			this.tilePage.Name = "tilePage";
			this.tilePage.Padding = new System.Windows.Forms.Padding(3);
			this.tilePage.Size = new System.Drawing.Size(289, 256);
			this.tilePage.TabIndex = 0;
			this.tilePage.Text = "Tiles";
			this.tilePage.UseVisualStyleBackColor = true;
			// 
			// hScrollBar
			// 
			this.hScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.hScrollBar.Enabled = false;
			this.hScrollBar.Location = new System.Drawing.Point(0, 240);
			this.hScrollBar.Name = "hScrollBar";
			this.hScrollBar.Size = new System.Drawing.Size(273, 16);
			this.hScrollBar.TabIndex = 2;
			// 
			// vScrollBar
			// 
			this.vScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.vScrollBar.Enabled = false;
			this.vScrollBar.Location = new System.Drawing.Point(273, 0);
			this.vScrollBar.Name = "vScrollBar";
			this.vScrollBar.Size = new System.Drawing.Size(16, 240);
			this.vScrollBar.TabIndex = 1;
			// 
			// passabilityPage
			// 
			this.passabilityPage.Controls.Add(this.impassableButton);
			this.passabilityPage.Controls.Add(this.passableButton);
			this.passabilityPage.Location = new System.Drawing.Point(4, 22);
			this.passabilityPage.Name = "passabilityPage";
			this.passabilityPage.Padding = new System.Windows.Forms.Padding(3);
			this.passabilityPage.Size = new System.Drawing.Size(289, 256);
			this.passabilityPage.TabIndex = 1;
			this.passabilityPage.Text = "Passability";
			this.passabilityPage.UseVisualStyleBackColor = true;
			// 
			// impassableButton
			// 
			this.impassableButton.Image = global::KFIRPG.editor.Properties.Resources.cross;
			this.impassableButton.Location = new System.Drawing.Point(46, 6);
			this.impassableButton.Name = "impassableButton";
			this.impassableButton.Size = new System.Drawing.Size(32, 32);
			this.impassableButton.TabIndex = 1;
			this.impassableButton.UseVisualStyleBackColor = true;
			this.impassableButton.Click += new System.EventHandler(this.impassableButton_Click);
			// 
			// passableButton
			// 
			this.passableButton.Image = global::KFIRPG.editor.Properties.Resources.tick;
			this.passableButton.Location = new System.Drawing.Point(8, 6);
			this.passableButton.Name = "passableButton";
			this.passableButton.Size = new System.Drawing.Size(32, 32);
			this.passableButton.TabIndex = 0;
			this.passableButton.UseVisualStyleBackColor = true;
			this.passableButton.Click += new System.EventHandler(this.passableButton_Click);
			// 
			// tilesPanel
			// 
			this.tilesPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tilesPanel.Location = new System.Drawing.Point(0, 0);
			this.tilesPanel.Name = "tilesPanel";
			this.tilesPanel.Size = new System.Drawing.Size(273, 240);
			this.tilesPanel.TabIndex = 3;
			this.tilesPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.tilesPanel_Paint);
			this.tilesPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tilesPanel_MouseClick);
			// 
			// Palette
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(297, 282);
			this.Controls.Add(this.tabControl1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "Palette";
			this.ShowInTaskbar = false;
			this.Text = "Palette";
			this.tabControl1.ResumeLayout(false);
			this.tilePage.ResumeLayout(false);
			this.passabilityPage.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tilePage;
		private System.Windows.Forms.HScrollBar hScrollBar;
		private System.Windows.Forms.VScrollBar vScrollBar;
		private System.Windows.Forms.TabPage passabilityPage;
		private System.Windows.Forms.Button passableButton;
		private System.Windows.Forms.Button impassableButton;
		private DoubleBufferedPanel tilesPanel;
	}
}