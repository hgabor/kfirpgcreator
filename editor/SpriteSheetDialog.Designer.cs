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
			this.listBox = new System.Windows.Forms.ListBox();
			this.addAnimationButton = new System.Windows.Forms.Button();
			this.delAnimationButton = new System.Windows.Forms.Button();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
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
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.Location = new System.Drawing.Point(205, 280);
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
			this.cancelButton.Location = new System.Drawing.Point(124, 280);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 1;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// imageButton
			// 
			this.imageButton.Location = new System.Drawing.Point(12, 12);
			this.imageButton.Name = "imageButton";
			this.imageButton.Size = new System.Drawing.Size(75, 23);
			this.imageButton.TabIndex = 2;
			this.imageButton.Text = "Image...";
			this.imageButton.UseVisualStyleBackColor = true;
			this.imageButton.Click += new System.EventHandler(this.imageButton_Click);
			// 
			// pictureBox
			// 
			this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.pictureBox.Location = new System.Drawing.Point(93, 12);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(187, 66);
			this.pictureBox.TabIndex = 3;
			this.pictureBox.TabStop = false;
			// 
			// widthNumericUpDown
			// 
			this.widthNumericUpDown.Location = new System.Drawing.Point(56, 84);
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
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 86);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(38, 13);
			this.label1.TabIndex = 5;
			this.label1.Text = "Width:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(137, 86);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(41, 13);
			this.label2.TabIndex = 7;
			this.label2.Text = "Height:";
			// 
			// HeightNumericUpDown
			// 
			this.HeightNumericUpDown.Location = new System.Drawing.Point(181, 84);
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
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(137, 112);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(17, 13);
			this.label3.TabIndex = 11;
			this.label3.Text = "Y:";
			// 
			// yNumericUpDown
			// 
			this.yNumericUpDown.Location = new System.Drawing.Point(181, 110);
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
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 112);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(17, 13);
			this.label4.TabIndex = 9;
			this.label4.Text = "X:";
			// 
			// xNumericUpDown
			// 
			this.xNumericUpDown.Location = new System.Drawing.Point(56, 110);
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
			// listBox
			// 
			this.listBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.listBox.FormattingEnabled = true;
			this.listBox.IntegralHeight = false;
			this.listBox.Location = new System.Drawing.Point(11, 136);
			this.listBox.Name = "listBox";
			this.listBox.Size = new System.Drawing.Size(233, 138);
			this.listBox.TabIndex = 12;
			// 
			// addAnimationButton
			// 
			this.addAnimationButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.addAnimationButton.Image = global::KFIRPG.editor.Properties.Resources.add;
			this.addAnimationButton.Location = new System.Drawing.Point(250, 136);
			this.addAnimationButton.Name = "addAnimationButton";
			this.addAnimationButton.Size = new System.Drawing.Size(30, 30);
			this.addAnimationButton.TabIndex = 13;
			this.addAnimationButton.UseVisualStyleBackColor = true;
			// 
			// delAnimationButton
			// 
			this.delAnimationButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.delAnimationButton.Image = global::KFIRPG.editor.Properties.Resources.cross;
			this.delAnimationButton.Location = new System.Drawing.Point(250, 172);
			this.delAnimationButton.Name = "delAnimationButton";
			this.delAnimationButton.Size = new System.Drawing.Size(30, 30);
			this.delAnimationButton.TabIndex = 14;
			this.delAnimationButton.UseVisualStyleBackColor = true;
			// 
			// openFileDialog
			// 
			this.openFileDialog.Filter = "PNG|*.png|All files|*.*";
			this.openFileDialog.Title = "Select Image";
			// 
			// SpriteSheetDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(292, 315);
			this.Controls.Add(this.delAnimationButton);
			this.Controls.Add(this.addAnimationButton);
			this.Controls.Add(this.listBox);
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

		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button imageButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button addAnimationButton;
		private System.Windows.Forms.Button delAnimationButton;
		public System.Windows.Forms.PictureBox pictureBox;
		public System.Windows.Forms.ListBox listBox;
		public System.Windows.Forms.NumericUpDown widthNumericUpDown;
		public System.Windows.Forms.NumericUpDown HeightNumericUpDown;
		public System.Windows.Forms.NumericUpDown yNumericUpDown;
		public System.Windows.Forms.NumericUpDown xNumericUpDown;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
	}
}