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
			this.scriptNamesListBox = new System.Windows.Forms.ComboBox();
			this.scriptTextBox = new System.Windows.Forms.RichTextBox();
			this.nameTextBox = new System.Windows.Forms.TextBox();
			this.nameLabel = new System.Windows.Forms.Label();
			this.scriptLabel = new System.Windows.Forms.Label();
			this.saveButton = new System.Windows.Forms.Button();
			this.removeButton = new System.Windows.Forms.Button();
			this.newButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// scriptNamesListBox
			// 
			this.scriptNamesListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.scriptNamesListBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.scriptNamesListBox.FormattingEnabled = true;
			this.scriptNamesListBox.Location = new System.Drawing.Point(12, 12);
			this.scriptNamesListBox.Name = "scriptNamesListBox";
			this.scriptNamesListBox.Size = new System.Drawing.Size(160, 21);
			this.scriptNamesListBox.TabIndex = 0;
			this.scriptNamesListBox.SelectedIndexChanged += new System.EventHandler(this.scriptNamesListBox_SelectedIndexChanged);
			// 
			// scriptTextBox
			// 
			this.scriptTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.scriptTextBox.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.scriptTextBox.Location = new System.Drawing.Point(12, 91);
			this.scriptTextBox.Name = "scriptTextBox";
			this.scriptTextBox.Size = new System.Drawing.Size(241, 147);
			this.scriptTextBox.TabIndex = 1;
			this.scriptTextBox.Text = "";
			// 
			// nameTextBox
			// 
			this.nameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.nameTextBox.Location = new System.Drawing.Point(12, 52);
			this.nameTextBox.Name = "nameTextBox";
			this.nameTextBox.Size = new System.Drawing.Size(241, 20);
			this.nameTextBox.TabIndex = 2;
			// 
			// nameLabel
			// 
			this.nameLabel.AutoSize = true;
			this.nameLabel.Location = new System.Drawing.Point(9, 36);
			this.nameLabel.Name = "nameLabel";
			this.nameLabel.Size = new System.Drawing.Size(147, 13);
			this.nameLabel.TabIndex = 3;
			this.nameLabel.Text = "Script name (must be unique):";
			// 
			// scriptLabel
			// 
			this.scriptLabel.AutoSize = true;
			this.scriptLabel.Location = new System.Drawing.Point(9, 75);
			this.scriptLabel.Name = "scriptLabel";
			this.scriptLabel.Size = new System.Drawing.Size(37, 13);
			this.scriptLabel.TabIndex = 4;
			this.scriptLabel.Text = "Script:";
			// 
			// saveButton
			// 
			this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.saveButton.Location = new System.Drawing.Point(178, 244);
			this.saveButton.Name = "saveButton";
			this.saveButton.Size = new System.Drawing.Size(75, 23);
			this.saveButton.TabIndex = 5;
			this.saveButton.Text = "Save";
			this.saveButton.UseVisualStyleBackColor = true;
			this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
			// 
			// removeButton
			// 
			this.removeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.removeButton.Location = new System.Drawing.Point(97, 244);
			this.removeButton.Name = "removeButton";
			this.removeButton.Size = new System.Drawing.Size(75, 23);
			this.removeButton.TabIndex = 6;
			this.removeButton.Text = "Remove";
			this.removeButton.UseVisualStyleBackColor = true;
			this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
			// 
			// newButton
			// 
			this.newButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.newButton.Location = new System.Drawing.Point(178, 12);
			this.newButton.Name = "newButton";
			this.newButton.Size = new System.Drawing.Size(75, 23);
			this.newButton.TabIndex = 7;
			this.newButton.Text = "New";
			this.newButton.UseVisualStyleBackColor = true;
			this.newButton.Click += new System.EventHandler(this.newButton_Click);
			// 
			// ScriptEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(265, 279);
			this.Controls.Add(this.newButton);
			this.Controls.Add(this.removeButton);
			this.Controls.Add(this.saveButton);
			this.Controls.Add(this.scriptLabel);
			this.Controls.Add(this.nameLabel);
			this.Controls.Add(this.nameTextBox);
			this.Controls.Add(this.scriptTextBox);
			this.Controls.Add(this.scriptNamesListBox);
			this.Name = "ScriptEditor";
			this.Text = "ScriptEditor";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ScriptEditor_FormClosing);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox scriptNamesListBox;
		private System.Windows.Forms.RichTextBox scriptTextBox;
		private System.Windows.Forms.TextBox nameTextBox;
		private System.Windows.Forms.Label nameLabel;
		private System.Windows.Forms.Label scriptLabel;
		private System.Windows.Forms.Button saveButton;
		private System.Windows.Forms.Button removeButton;
		private System.Windows.Forms.Button newButton;
	}
}