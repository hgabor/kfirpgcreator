namespace KFIRPG.editor {
	partial class AnimationLibrary {
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
			this.listBox = new System.Windows.Forms.ListBox();
			this.addButton = new System.Windows.Forms.Button();
			this.delButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// listBox
			// 
			this.listBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.listBox.IntegralHeight = false;
			this.listBox.Location = new System.Drawing.Point(12, 12);
			this.listBox.Name = "listBox";
			this.listBox.Size = new System.Drawing.Size(268, 182);
			this.listBox.TabIndex = 0;
			this.listBox.DoubleClick += new System.EventHandler(this.listBox_DoubleClick);
			// 
			// addButton
			// 
			this.addButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.addButton.Image = global::KFIRPG.editor.Properties.Resources.add;
			this.addButton.Location = new System.Drawing.Point(12, 200);
			this.addButton.Name = "addButton";
			this.addButton.Size = new System.Drawing.Size(30, 30);
			this.addButton.TabIndex = 1;
			this.addButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.addButton.UseVisualStyleBackColor = true;
			this.addButton.Click += new System.EventHandler(this.addButton_Click);
			// 
			// delButton
			// 
			this.delButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.delButton.Image = global::KFIRPG.editor.Properties.Resources.cross;
			this.delButton.Location = new System.Drawing.Point(48, 200);
			this.delButton.Name = "delButton";
			this.delButton.Size = new System.Drawing.Size(30, 30);
			this.delButton.TabIndex = 2;
			this.delButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.delButton.UseVisualStyleBackColor = true;
			this.delButton.Click += new System.EventHandler(this.DelButtonClick);
			// 
			// AnimationLibrary
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(292, 242);
			this.Controls.Add(this.delButton);
			this.Controls.Add(this.addButton);
			this.Controls.Add(this.listBox);
			this.Name = "AnimationLibrary";
			this.Text = "AnimationLibrary";
			this.ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.ListBox listBox;
		private System.Windows.Forms.Button addButton;
		private System.Windows.Forms.Button delButton;
	}
}