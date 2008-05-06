namespace KFIRPG.editor {
	partial class SelectTwoLayersDialog {
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
			this.baseListBox = new System.Windows.Forms.ListBox();
			this.topListBox = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.cancelButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// baseListBox
			// 
			this.baseListBox.FormattingEnabled = true;
			this.baseListBox.IntegralHeight = false;
			this.baseListBox.Location = new System.Drawing.Point(77, 9);
			this.baseListBox.Name = "baseListBox";
			this.baseListBox.Size = new System.Drawing.Size(120, 96);
			this.baseListBox.TabIndex = 0;
			// 
			// topListBox
			// 
			this.topListBox.FormattingEnabled = true;
			this.topListBox.IntegralHeight = false;
			this.topListBox.Location = new System.Drawing.Point(265, 9);
			this.topListBox.Name = "topListBox";
			this.topListBox.Size = new System.Drawing.Size(120, 96);
			this.topListBox.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(59, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Base layer:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(205, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(54, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Top layer:";
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(310, 120);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 4;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// okButton
			// 
			this.okButton.Location = new System.Drawing.Point(229, 120);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 5;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			// 
			// SelectTwoLayersDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(397, 155);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.topListBox);
			this.Controls.Add(this.baseListBox);
			this.Name = "SelectTwoLayersDialog";
			this.Text = "Select Two Layers";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListBox baseListBox;
		private System.Windows.Forms.ListBox topListBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button okButton;
	}
}