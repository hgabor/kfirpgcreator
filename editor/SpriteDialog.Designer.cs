namespace KFIRPG.editor {
	partial class SpriteDialog {
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
			this.speedNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.noclipCheckBox = new System.Windows.Forms.CheckBox();
			this.animationComboBox = new System.Windows.Forms.ComboBox();
			this.listBox = new System.Windows.Forms.ListBox();
			this.cancelButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.delButton = new System.Windows.Forms.Button();
			this.addButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.speedNumericUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// nameTextBox
			// 
			this.nameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.nameTextBox.Location = new System.Drawing.Point(12, 12);
			this.nameTextBox.Name = "nameTextBox";
			this.nameTextBox.Size = new System.Drawing.Size(268, 20);
			this.nameTextBox.TabIndex = 1;
			// 
			// speedNumericUpDown
			// 
			this.speedNumericUpDown.Location = new System.Drawing.Point(167, 39);
			this.speedNumericUpDown.Name = "speedNumericUpDown";
			this.speedNumericUpDown.Size = new System.Drawing.Size(48, 20);
			this.speedNumericUpDown.TabIndex = 2;
			// 
			// noclipCheckBox
			// 
			this.noclipCheckBox.AutoSize = true;
			this.noclipCheckBox.Location = new System.Drawing.Point(221, 40);
			this.noclipCheckBox.Name = "noclipCheckBox";
			this.noclipCheckBox.Size = new System.Drawing.Size(59, 17);
			this.noclipCheckBox.TabIndex = 3;
			this.noclipCheckBox.Text = "No clip";
			this.noclipCheckBox.UseVisualStyleBackColor = true;
			// 
			// animationComboBox
			// 
			this.animationComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.animationComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.animationComboBox.FormattingEnabled = true;
			this.animationComboBox.Location = new System.Drawing.Point(12, 38);
			this.animationComboBox.Name = "animationComboBox";
			this.animationComboBox.Size = new System.Drawing.Size(149, 21);
			this.animationComboBox.TabIndex = 0;
			// 
			// listBox
			// 
			this.listBox.FormattingEnabled = true;
			this.listBox.IntegralHeight = false;
			this.listBox.Location = new System.Drawing.Point(12, 65);
			this.listBox.Name = "listBox";
			this.listBox.Size = new System.Drawing.Size(232, 167);
			this.listBox.TabIndex = 4;
			this.listBox.DoubleClick += new System.EventHandler(this.listBox_DoubleClick);
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(205, 238);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 5;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// okButton
			// 
			this.okButton.Location = new System.Drawing.Point(124, 238);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 6;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// delButton
			// 
			this.delButton.Image = global::KFIRPG.editor.Properties.Resources.cross;
			this.delButton.Location = new System.Drawing.Point(250, 101);
			this.delButton.Name = "delButton";
			this.delButton.Size = new System.Drawing.Size(30, 30);
			this.delButton.TabIndex = 8;
			this.delButton.UseVisualStyleBackColor = true;
			this.delButton.Click += new System.EventHandler(this.delButton_Click);
			// 
			// addButton
			// 
			this.addButton.Image = global::KFIRPG.editor.Properties.Resources.add;
			this.addButton.Location = new System.Drawing.Point(250, 65);
			this.addButton.Name = "addButton";
			this.addButton.Size = new System.Drawing.Size(30, 30);
			this.addButton.TabIndex = 7;
			this.addButton.UseVisualStyleBackColor = true;
			this.addButton.Click += new System.EventHandler(this.addButton_Click);
			// 
			// SpriteDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.Add(this.delButton);
			this.Controls.Add(this.addButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.listBox);
			this.Controls.Add(this.noclipCheckBox);
			this.Controls.Add(this.speedNumericUpDown);
			this.Controls.Add(this.nameTextBox);
			this.Controls.Add(this.animationComboBox);
			this.Name = "SpriteDialog";
			this.Text = "SpriteDialog";
			((System.ComponentModel.ISupportInitialize)(this.speedNumericUpDown)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button addButton;
		private System.Windows.Forms.Button delButton;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.TextBox nameTextBox;
		private System.Windows.Forms.NumericUpDown speedNumericUpDown;
		private System.Windows.Forms.CheckBox noclipCheckBox;
		private System.Windows.Forms.ComboBox animationComboBox;
		private System.Windows.Forms.ListBox listBox;


	}
}