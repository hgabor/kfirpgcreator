namespace KFIRPG.editor {
	partial class AnimationSelector {
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
			this.beginEndComboBox = new System.Windows.Forms.ComboBox();
			this.pictureBox = new System.Windows.Forms.PictureBox();
			this.timeoutNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.canceButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.timeoutNumericUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// beginEndComboBox
			// 
			this.beginEndComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.beginEndComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.beginEndComboBox.FormattingEnabled = true;
			this.beginEndComboBox.Items.AddRange(new object[] {
            "Begin",
            "End"});
			this.beginEndComboBox.Location = new System.Drawing.Point(12, 12);
			this.beginEndComboBox.Name = "beginEndComboBox";
			this.beginEndComboBox.Size = new System.Drawing.Size(166, 21);
			this.beginEndComboBox.TabIndex = 2;
			// 
			// pictureBox
			// 
			this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.pictureBox.Location = new System.Drawing.Point(12, 39);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(325, 193);
			this.pictureBox.TabIndex = 3;
			this.pictureBox.TabStop = false;
			this.pictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseClick);
			this.pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_Paint);
			// 
			// timeoutNumericUpDown
			// 
			this.timeoutNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.timeoutNumericUpDown.Location = new System.Drawing.Point(184, 12);
			this.timeoutNumericUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.timeoutNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.timeoutNumericUpDown.Name = "timeoutNumericUpDown";
			this.timeoutNumericUpDown.Size = new System.Drawing.Size(153, 20);
			this.timeoutNumericUpDown.TabIndex = 4;
			this.timeoutNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// canceButton
			// 
			this.canceButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.canceButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.canceButton.Location = new System.Drawing.Point(262, 238);
			this.canceButton.Name = "canceButton";
			this.canceButton.Size = new System.Drawing.Size(75, 23);
			this.canceButton.TabIndex = 5;
			this.canceButton.Text = "Cancel";
			this.canceButton.UseVisualStyleBackColor = true;
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.Location = new System.Drawing.Point(181, 238);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 6;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			// 
			// AnimationSelector
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(349, 273);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.canceButton);
			this.Controls.Add(this.timeoutNumericUpDown);
			this.Controls.Add(this.pictureBox);
			this.Controls.Add(this.beginEndComboBox);
			this.Name = "AnimationSelector";
			this.Text = "AnimationSelector";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.timeoutNumericUpDown)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ComboBox beginEndComboBox;
		private System.Windows.Forms.PictureBox pictureBox;
		private System.Windows.Forms.NumericUpDown timeoutNumericUpDown;
		private System.Windows.Forms.Button canceButton;
		private System.Windows.Forms.Button okButton;
	}
}