namespace KFIRPG.editor {
	partial class MainPanelForm {
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
			this.vScrollBar = new System.Windows.Forms.VScrollBar();
			this.hScrollBar = new System.Windows.Forms.HScrollBar();
			this.mainPanel = new KFIRPG.editor.DoubleBufferedPanel();
			this.SuspendLayout();
			// 
			// vScrollBar
			// 
			this.vScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.vScrollBar.Location = new System.Drawing.Point(276, 0);
			this.vScrollBar.Name = "vScrollBar";
			this.vScrollBar.Size = new System.Drawing.Size(16, 257);
			this.vScrollBar.TabIndex = 5;
			// 
			// hScrollBar
			// 
			this.hScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.hScrollBar.Location = new System.Drawing.Point(0, 257);
			this.hScrollBar.Name = "hScrollBar";
			this.hScrollBar.Size = new System.Drawing.Size(276, 16);
			this.hScrollBar.TabIndex = 6;
			// 
			// mainPanel
			// 
			this.mainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.mainPanel.BackColor = System.Drawing.Color.Black;
			this.mainPanel.Location = new System.Drawing.Point(0, 0);
			this.mainPanel.Name = "mainPanel";
			this.mainPanel.Size = new System.Drawing.Size(276, 257);
			this.mainPanel.TabIndex = 7;
			// 
			// MainPanelForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.Add(this.vScrollBar);
			this.Controls.Add(this.hScrollBar);
			this.Controls.Add(this.mainPanel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "MainPanelForm";
			this.Text = "MainPanelForm";
			this.ResumeLayout(false);

		}

		#endregion

		internal System.Windows.Forms.VScrollBar vScrollBar;
		internal System.Windows.Forms.HScrollBar hScrollBar;
		internal DoubleBufferedPanel mainPanel;

	}
}