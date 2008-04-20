namespace KFIRPG.editor {
	partial class ImageLibrary {
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
			this.listbox = new System.Windows.Forms.ListBox();
			this.addbutton = new System.Windows.Forms.Button();
			this.removebutton = new System.Windows.Forms.Button();
			this.pictureBox = new System.Windows.Forms.PictureBox();
			this.colorbutton = new System.Windows.Forms.Button();
			this.colorDialog = new System.Windows.Forms.ColorDialog();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// listbox
			// 
			this.listbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)));
			this.listbox.FormattingEnabled = true;
			this.listbox.IntegralHeight = false;
			this.listbox.Location = new System.Drawing.Point(12, 12);
			this.listbox.Name = "listbox";
			this.listbox.Size = new System.Drawing.Size(139, 104);
			this.listbox.TabIndex = 0;
			// 
			// addbutton
			// 
			this.addbutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.addbutton.Image = global::KFIRPG.editor.Properties.Resources.add;
			this.addbutton.Location = new System.Drawing.Point(12, 122);
			this.addbutton.Name = "addbutton";
			this.addbutton.Size = new System.Drawing.Size(30, 30);
			this.addbutton.TabIndex = 1;
			this.addbutton.UseVisualStyleBackColor = true;
			// 
			// removebutton
			// 
			this.removebutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.removebutton.Image = global::KFIRPG.editor.Properties.Resources.cross;
			this.removebutton.Location = new System.Drawing.Point(48, 122);
			this.removebutton.Name = "removebutton";
			this.removebutton.Size = new System.Drawing.Size(30, 30);
			this.removebutton.TabIndex = 2;
			this.removebutton.UseVisualStyleBackColor = true;
			// 
			// pictureBox
			// 
			this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.pictureBox.Location = new System.Drawing.Point(157, 12);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(145, 140);
			this.pictureBox.TabIndex = 3;
			this.pictureBox.TabStop = false;
			// 
			// colorbutton
			// 
			this.colorbutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.colorbutton.Location = new System.Drawing.Point(84, 122);
			this.colorbutton.Name = "colorbutton";
			this.colorbutton.Size = new System.Drawing.Size(30, 30);
			this.colorbutton.TabIndex = 4;
			this.colorbutton.UseVisualStyleBackColor = true;
			this.colorbutton.Click += new System.EventHandler(this.colorbutton_Click);
			// 
			// colorDialog
			// 
			this.colorDialog.AnyColor = true;
			// 
			// ImageLibrary
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(314, 164);
			this.Controls.Add(this.colorbutton);
			this.Controls.Add(this.pictureBox);
			this.Controls.Add(this.removebutton);
			this.Controls.Add(this.addbutton);
			this.Controls.Add(this.listbox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "ImageLibrary";
			this.ShowInTaskbar = false;
			this.Text = "ImageLibrary";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox listbox;
		private System.Windows.Forms.Button addbutton;
		private System.Windows.Forms.Button removebutton;
		private System.Windows.Forms.PictureBox pictureBox;
		private System.Windows.Forms.Button colorbutton;
		private System.Windows.Forms.ColorDialog colorDialog;
	}
}