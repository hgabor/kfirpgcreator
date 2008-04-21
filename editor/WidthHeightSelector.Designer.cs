namespace KFIRPG.editor {
	partial class WidthHeightSelector {
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
			this.widthNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.heightNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.okbutton = new System.Windows.Forms.Button();
			this.cancelbutton = new System.Windows.Forms.Button();
			this.widthlabel = new System.Windows.Forms.Label();
			this.heightlabel = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.widthNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.heightNumericUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// widthNumericUpDown
			// 
			this.widthNumericUpDown.Location = new System.Drawing.Point(59, 9);
			this.widthNumericUpDown.Name = "widthNumericUpDown";
			this.widthNumericUpDown.Size = new System.Drawing.Size(120, 20);
			this.widthNumericUpDown.TabIndex = 0;
			// 
			// heightNumericUpDown
			// 
			this.heightNumericUpDown.Location = new System.Drawing.Point(59, 35);
			this.heightNumericUpDown.Name = "heightNumericUpDown";
			this.heightNumericUpDown.Size = new System.Drawing.Size(120, 20);
			this.heightNumericUpDown.TabIndex = 1;
			// 
			// okbutton
			// 
			this.okbutton.Location = new System.Drawing.Point(205, 9);
			this.okbutton.Name = "okbutton";
			this.okbutton.Size = new System.Drawing.Size(75, 23);
			this.okbutton.TabIndex = 2;
			this.okbutton.Text = "OK";
			this.okbutton.UseVisualStyleBackColor = true;
			this.okbutton.Click += new System.EventHandler(this.okbutton_Click);
			// 
			// cancelbutton
			// 
			this.cancelbutton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelbutton.Location = new System.Drawing.Point(205, 38);
			this.cancelbutton.Name = "cancelbutton";
			this.cancelbutton.Size = new System.Drawing.Size(75, 23);
			this.cancelbutton.TabIndex = 3;
			this.cancelbutton.Text = "Cancel";
			this.cancelbutton.UseVisualStyleBackColor = true;
			this.cancelbutton.Click += new System.EventHandler(this.cancelbutton_Click);
			// 
			// widthlabel
			// 
			this.widthlabel.AutoSize = true;
			this.widthlabel.Location = new System.Drawing.Point(15, 11);
			this.widthlabel.Name = "widthlabel";
			this.widthlabel.Size = new System.Drawing.Size(38, 13);
			this.widthlabel.TabIndex = 4;
			this.widthlabel.Text = "Width:";
			this.widthlabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// heightlabel
			// 
			this.heightlabel.AutoSize = true;
			this.heightlabel.Location = new System.Drawing.Point(12, 37);
			this.heightlabel.Name = "heightlabel";
			this.heightlabel.Size = new System.Drawing.Size(41, 13);
			this.heightlabel.TabIndex = 5;
			this.heightlabel.Text = "Height:";
			this.heightlabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// WidthHeightSelector
			// 
			this.AcceptButton = this.okbutton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelbutton;
			this.ClientSize = new System.Drawing.Size(292, 73);
			this.Controls.Add(this.heightlabel);
			this.Controls.Add(this.widthlabel);
			this.Controls.Add(this.cancelbutton);
			this.Controls.Add(this.okbutton);
			this.Controls.Add(this.heightNumericUpDown);
			this.Controls.Add(this.widthNumericUpDown);
			this.Name = "WidthHeightSelector";
			this.Text = "WidthHeightSelector";
			((System.ComponentModel.ISupportInitialize)(this.widthNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.heightNumericUpDown)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.NumericUpDown widthNumericUpDown;
		private System.Windows.Forms.NumericUpDown heightNumericUpDown;
		private System.Windows.Forms.Button okbutton;
		private System.Windows.Forms.Button cancelbutton;
		private System.Windows.Forms.Label widthlabel;
		private System.Windows.Forms.Label heightlabel;

	}
}