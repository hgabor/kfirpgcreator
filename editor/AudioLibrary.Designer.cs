namespace KFIRPG.editor {
	partial class AudioLibrary {
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
			this.list = new System.Windows.Forms.ListBox();
			this.addbutton = new System.Windows.Forms.Button();
			this.removebutton = new System.Windows.Forms.Button();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.SuspendLayout();
			// 
			// list
			// 
			this.list.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.list.FormattingEnabled = true;
			this.list.IntegralHeight = false;
			this.list.Location = new System.Drawing.Point(12, 12);
			this.list.Name = "list";
			this.list.Size = new System.Drawing.Size(128, 56);
			this.list.TabIndex = 0;
			// 
			// addbutton
			// 
			this.addbutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.addbutton.Image = global::KFIRPG.editor.Properties.Resources.add;
			this.addbutton.Location = new System.Drawing.Point(12, 74);
			this.addbutton.Name = "addbutton";
			this.addbutton.Size = new System.Drawing.Size(30, 30);
			this.addbutton.TabIndex = 1;
			this.addbutton.UseVisualStyleBackColor = true;
			this.addbutton.Click += new System.EventHandler(this.addbutton_Click);
			// 
			// removebutton
			// 
			this.removebutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.removebutton.Image = global::KFIRPG.editor.Properties.Resources.cross;
			this.removebutton.Location = new System.Drawing.Point(48, 74);
			this.removebutton.Name = "removebutton";
			this.removebutton.Size = new System.Drawing.Size(30, 30);
			this.removebutton.TabIndex = 2;
			this.removebutton.UseVisualStyleBackColor = true;
			this.removebutton.Click += new System.EventHandler(this.removebutton_Click);
			// 
			// openFileDialog
			// 
			this.openFileDialog.Filter = "Sound and music|*.ogg;*.mp3;*.wav|All files|*.*";
			this.openFileDialog.RestoreDirectory = true;
			this.openFileDialog.Title = "Select sound/music";
			// 
			// AudioLibrary
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(152, 116);
			this.Controls.Add(this.removebutton);
			this.Controls.Add(this.addbutton);
			this.Controls.Add(this.list);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "AudioLibrary";
			this.ShowInTaskbar = false;
			this.Text = "AudioLibrary";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox list;
		private System.Windows.Forms.Button addbutton;
		private System.Windows.Forms.Button removebutton;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
	}
}