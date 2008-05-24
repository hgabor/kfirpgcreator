namespace KFIRPG.editor {
	partial class SpriteSheetDialog {
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
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.imageButton = new System.Windows.Forms.Button();
			this.pictureBox = new System.Windows.Forms.PictureBox();
			this.widthNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.HeightNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.yNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.label4 = new System.Windows.Forms.Label();
			this.xNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.label5 = new System.Windows.Forms.Label();
			this.nameTextBox = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.widthNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.HeightNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.yNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.xNumericUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.Location = new System.Drawing.Point(168, 222);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 0;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(249, 222);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 1;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// imageButton
			// 
			this.imageButton.Location = new System.Drawing.Point(12, 38);
			this.imageButton.Name = "imageButton";
			this.imageButton.Size = new System.Drawing.Size(75, 23);
			this.imageButton.TabIndex = 2;
			this.imageButton.Text = "Image...";
			this.imageButton.UseVisualStyleBackColor = true;
			this.imageButton.Click += new System.EventHandler(this.imageButton_Click);
			// 
			// pictureBox
			// 
			this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.pictureBox.Location = new System.Drawing.Point(93, 38);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(231, 117);
			this.pictureBox.TabIndex = 3;
			this.pictureBox.TabStop = false;
			// 
			// widthNumericUpDown
			// 
			this.widthNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.widthNumericUpDown.Location = new System.Drawing.Point(56, 161);
			this.widthNumericUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.widthNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.widthNumericUpDown.Name = "widthNumericUpDown";
			this.widthNumericUpDown.Size = new System.Drawing.Size(75, 20);
			this.widthNumericUpDown.TabIndex = 4;
			this.widthNumericUpDown.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 163);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(38, 13);
			this.label1.TabIndex = 5;
			this.label1.Text = "Width:";
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(137, 163);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(41, 13);
			this.label2.TabIndex = 7;
			this.label2.Text = "Height:";
			// 
			// HeightNumericUpDown
			// 
			this.HeightNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.HeightNumericUpDown.Location = new System.Drawing.Point(181, 161);
			this.HeightNumericUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.HeightNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.HeightNumericUpDown.Name = "HeightNumericUpDown";
			this.HeightNumericUpDown.Size = new System.Drawing.Size(75, 20);
			this.HeightNumericUpDown.TabIndex = 6;
			this.HeightNumericUpDown.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(137, 189);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(17, 13);
			this.label3.TabIndex = 11;
			this.label3.Text = "Y:";
			// 
			// yNumericUpDown
			// 
			this.yNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.yNumericUpDown.Location = new System.Drawing.Point(181, 187);
			this.yNumericUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.yNumericUpDown.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
			this.yNumericUpDown.Name = "yNumericUpDown";
			this.yNumericUpDown.Size = new System.Drawing.Size(75, 20);
			this.yNumericUpDown.TabIndex = 10;
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 189);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(17, 13);
			this.label4.TabIndex = 9;
			this.label4.Text = "X:";
			// 
			// xNumericUpDown
			// 
			this.xNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.xNumericUpDown.Location = new System.Drawing.Point(56, 187);
			this.xNumericUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.xNumericUpDown.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
			this.xNumericUpDown.Name = "xNumericUpDown";
			this.xNumericUpDown.Size = new System.Drawing.Size(75, 20);
			this.xNumericUpDown.TabIndex = 8;
			// 
			// openFileDialog
			// 
			this.openFileDialog.Filter = "PNG|*.png|All files|*.*";
			this.openFileDialog.Title = "Select Image";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(12, 15);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(38, 13);
			this.label5.TabIndex = 15;
			this.label5.Text = "Name:";
			// 
			// nameTextBox
			// 
			this.nameTextBox.Location = new System.Drawing.Point(56, 12);
			this.nameTextBox.Name = "nameTextBox";
			this.nameTextBox.Size = new System.Drawing.Size(268, 20);
			this.nameTextBox.TabIndex = 16;
			// 
			// SpriteSheetDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(336, 257);
			this.Controls.Add(this.nameTextBox);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.yNumericUpDown);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.xNumericUpDown);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.HeightNumericUpDown);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.widthNumericUpDown);
			this.Controls.Add(this.pictureBox);
			this.Controls.Add(this.imageButton);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Name = "SpriteSheetDialog";
			this.Text = "Add/edit sprite sheet";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.widthNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.HeightNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.yNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.xNumericUpDown)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button imageButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		public System.Windows.Forms.PictureBox pictureBox;
		public System.Windows.Forms.NumericUpDown widthNumericUpDown;
		public System.Windows.Forms.NumericUpDown HeightNumericUpDown;
		public System.Windows.Forms.NumericUpDown yNumericUpDown;
		public System.Windows.Forms.NumericUpDown xNumericUpDown;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.Label label5;
		public System.Windows.Forms.Button okButton;
		public System.Windows.Forms.TextBox nameTextBox;
	}
}