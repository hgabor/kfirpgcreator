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
			this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
			this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
			this.tilesPanel = new System.Windows.Forms.Panel();
			this.tabControl1.SuspendLayout();
			this.tilePage.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tilePage);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(297, 282);
			this.tabControl1.TabIndex = 0;
			// 
			// tilePage
			// 
			this.tilePage.Controls.Add(this.hScrollBar1);
			this.tilePage.Controls.Add(this.vScrollBar1);
			this.tilePage.Controls.Add(this.tilesPanel);
			this.tilePage.Location = new System.Drawing.Point(4, 22);
			this.tilePage.Name = "tilePage";
			this.tilePage.Padding = new System.Windows.Forms.Padding(3);
			this.tilePage.Size = new System.Drawing.Size(289, 256);
			this.tilePage.TabIndex = 0;
			this.tilePage.Text = "Tiles";
			this.tilePage.UseVisualStyleBackColor = true;
			// 
			// hScrollBar1
			// 
			this.hScrollBar1.Location = new System.Drawing.Point(0, 240);
			this.hScrollBar1.Name = "hScrollBar1";
			this.hScrollBar1.Size = new System.Drawing.Size(273, 16);
			this.hScrollBar1.TabIndex = 2;
			// 
			// vScrollBar1
			// 
			this.vScrollBar1.Location = new System.Drawing.Point(273, 0);
			this.vScrollBar1.Name = "vScrollBar1";
			this.vScrollBar1.Size = new System.Drawing.Size(16, 240);
			this.vScrollBar1.TabIndex = 1;
			// 
			// tilesPanel
			// 
			this.tilesPanel.Location = new System.Drawing.Point(0, 0);
			this.tilesPanel.Name = "tilesPanel";
			this.tilesPanel.Size = new System.Drawing.Size(273, 240);
			this.tilesPanel.TabIndex = 0;
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
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tilePage;
		private System.Windows.Forms.HScrollBar hScrollBar1;
		private System.Windows.Forms.VScrollBar vScrollBar1;
		private System.Windows.Forms.Panel tilesPanel;
	}
}