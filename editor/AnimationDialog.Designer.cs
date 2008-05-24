namespace KFIRPG.editor {
	partial class AnimationDialog {
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
			this.nameTextBox = new System.Windows.Forms.TextBox();
			this.sheetComboBox = new System.Windows.Forms.ComboBox();
			this.pictureBox = new System.Windows.Forms.PictureBox();
			this.groupsListBox = new System.Windows.Forms.ListBox();
			this.cancelButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.addButton = new System.Windows.Forms.Button();
			this.delButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// nameTextBox
			// 
			this.nameTextBox.Location = new System.Drawing.Point(12, 12);
			this.nameTextBox.Name = "nameTextBox";
			this.nameTextBox.Size = new System.Drawing.Size(162, 20);
			this.nameTextBox.TabIndex = 0;
			this.nameTextBox.TextChanged += new System.EventHandler(this.nameTextBox_TextChanged);
			// 
			// sheetComboBox
			// 
			this.sheetComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.sheetComboBox.FormattingEnabled = true;
			this.sheetComboBox.Location = new System.Drawing.Point(12, 38);
			this.sheetComboBox.Name = "sheetComboBox";
			this.sheetComboBox.Size = new System.Drawing.Size(162, 21);
			this.sheetComboBox.TabIndex = 1;
			// 
			// pictureBox
			// 
			this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.pictureBox.Location = new System.Drawing.Point(180, 12);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(100, 213);
			this.pictureBox.TabIndex = 2;
			this.pictureBox.TabStop = false;
			// 
			// groupsListBox
			// 
			this.groupsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)));
			this.groupsListBox.FormattingEnabled = true;
			this.groupsListBox.IntegralHeight = false;
			this.groupsListBox.Location = new System.Drawing.Point(12, 65);
			this.groupsListBox.Name = "groupsListBox";
			this.groupsListBox.Size = new System.Drawing.Size(162, 160);
			this.groupsListBox.TabIndex = 3;
			this.groupsListBox.DoubleClick += new System.EventHandler(this.groupsListBox_DoubleClick);
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(205, 238);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 4;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// okButton
			// 
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.Enabled = false;
			this.okButton.Location = new System.Drawing.Point(124, 238);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 5;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			// 
			// addButton
			// 
			this.addButton.Image = global::KFIRPG.editor.Properties.Resources.add;
			this.addButton.Location = new System.Drawing.Point(12, 231);
			this.addButton.Name = "addButton";
			this.addButton.Size = new System.Drawing.Size(30, 30);
			this.addButton.TabIndex = 6;
			this.addButton.UseVisualStyleBackColor = true;
			this.addButton.Click += new System.EventHandler(this.addButton_Click);
			// 
			// delButton
			// 
			this.delButton.Image = global::KFIRPG.editor.Properties.Resources.cross;
			this.delButton.Location = new System.Drawing.Point(48, 231);
			this.delButton.Name = "delButton";
			this.delButton.Size = new System.Drawing.Size(30, 30);
			this.delButton.TabIndex = 7;
			this.delButton.UseVisualStyleBackColor = true;
			// 
			// AnimationDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.Add(this.delButton);
			this.Controls.Add(this.addButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.groupsListBox);
			this.Controls.Add(this.pictureBox);
			this.Controls.Add(this.sheetComboBox);
			this.Controls.Add(this.nameTextBox);
			this.Name = "AnimationDialog";
			this.Text = "AnimationDialog";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox nameTextBox;
		private System.Windows.Forms.ComboBox sheetComboBox;
		private System.Windows.Forms.PictureBox pictureBox;
		private System.Windows.Forms.ListBox groupsListBox;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button addButton;
		private System.Windows.Forms.Button delButton;
	}
}