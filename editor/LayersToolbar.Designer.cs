namespace KFIRPG.editor {
	partial class LayersToolbar {
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
			this.checkedListBox = new System.Windows.Forms.CheckedListBox();
			this.deletebutton = new System.Windows.Forms.Button();
			this.downbutton = new System.Windows.Forms.Button();
			this.upbutton = new System.Windows.Forms.Button();
			this.addbutton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// checkedListBox
			// 
			this.checkedListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.checkedListBox.FormattingEnabled = true;
			this.checkedListBox.IntegralHeight = false;
			this.checkedListBox.Location = new System.Drawing.Point(12, 12);
			this.checkedListBox.Name = "checkedListBox";
			this.checkedListBox.Size = new System.Drawing.Size(228, 116);
			this.checkedListBox.TabIndex = 0;
			// 
			// deletebutton
			// 
			this.deletebutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.deletebutton.Image = global::KFIRPG.editor.Properties.Resources.cross;
			this.deletebutton.Location = new System.Drawing.Point(84, 134);
			this.deletebutton.Name = "deletebutton";
			this.deletebutton.Size = new System.Drawing.Size(30, 30);
			this.deletebutton.TabIndex = 3;
			this.deletebutton.UseVisualStyleBackColor = true;
			// 
			// downbutton
			// 
			this.downbutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.downbutton.Image = global::KFIRPG.editor.Properties.Resources.arrow_down;
			this.downbutton.Location = new System.Drawing.Point(48, 134);
			this.downbutton.Name = "downbutton";
			this.downbutton.Size = new System.Drawing.Size(30, 30);
			this.downbutton.TabIndex = 2;
			this.downbutton.UseVisualStyleBackColor = true;
			// 
			// upbutton
			// 
			this.upbutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.upbutton.Image = global::KFIRPG.editor.Properties.Resources.arrow_up;
			this.upbutton.Location = new System.Drawing.Point(12, 134);
			this.upbutton.Name = "upbutton";
			this.upbutton.Size = new System.Drawing.Size(30, 30);
			this.upbutton.TabIndex = 1;
			this.upbutton.UseVisualStyleBackColor = true;
			// 
			// addbutton
			// 
			this.addbutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.addbutton.Image = global::KFIRPG.editor.Properties.Resources.add;
			this.addbutton.Location = new System.Drawing.Point(120, 134);
			this.addbutton.Name = "addbutton";
			this.addbutton.Size = new System.Drawing.Size(30, 30);
			this.addbutton.TabIndex = 4;
			this.addbutton.UseVisualStyleBackColor = true;
			// 
			// LayersToolbar
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(252, 176);
			this.Controls.Add(this.addbutton);
			this.Controls.Add(this.deletebutton);
			this.Controls.Add(this.downbutton);
			this.Controls.Add(this.upbutton);
			this.Controls.Add(this.checkedListBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "LayersToolbar";
			this.ShowInTaskbar = false;
			this.Text = "Layers";
			this.ResumeLayout(false);

		}

		#endregion

		public System.Windows.Forms.CheckedListBox checkedListBox;
		public System.Windows.Forms.Button upbutton;
		public System.Windows.Forms.Button downbutton;
		public System.Windows.Forms.Button deletebutton;
		public System.Windows.Forms.Button addbutton;

	}
}