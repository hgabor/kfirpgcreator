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
			this.timeoutNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.canceButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.currentFrameNumUpDown = new System.Windows.Forms.NumericUpDown();
			this.maxFramesNumUpDown = new System.Windows.Forms.NumericUpDown();
			this.spriteSelector = new KFIRPG.editor.SpriteSelector();
			((System.ComponentModel.ISupportInitialize)(this.timeoutNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.currentFrameNumUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.maxFramesNumUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spriteSelector)).BeginInit();
			this.SuspendLayout();
			// 
			// timeoutNumericUpDown
			// 
			this.timeoutNumericUpDown.Location = new System.Drawing.Point(122, 12);
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
			this.timeoutNumericUpDown.Size = new System.Drawing.Size(49, 20);
			this.timeoutNumericUpDown.TabIndex = 4;
			this.timeoutNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.timeoutNumericUpDown.ValueChanged += new System.EventHandler(this.timeoutNumericUpDown_ValueChanged);
			// 
			// canceButton
			// 
			this.canceButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.canceButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.canceButton.Location = new System.Drawing.Point(245, 192);
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
			this.okButton.Location = new System.Drawing.Point(164, 192);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 6;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			// 
			// currentFrameNumUpDown
			// 
			this.currentFrameNumUpDown.Location = new System.Drawing.Point(12, 12);
			this.currentFrameNumUpDown.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.currentFrameNumUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.currentFrameNumUpDown.Name = "currentFrameNumUpDown";
			this.currentFrameNumUpDown.Size = new System.Drawing.Size(49, 20);
			this.currentFrameNumUpDown.TabIndex = 7;
			this.currentFrameNumUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.currentFrameNumUpDown.ValueChanged += new System.EventHandler(this.currentFrameNumUpDown_ValueChanged);
			// 
			// maxFramesNumUpDown
			// 
			this.maxFramesNumUpDown.Location = new System.Drawing.Point(67, 12);
			this.maxFramesNumUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.maxFramesNumUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.maxFramesNumUpDown.Name = "maxFramesNumUpDown";
			this.maxFramesNumUpDown.Size = new System.Drawing.Size(49, 20);
			this.maxFramesNumUpDown.TabIndex = 8;
			this.maxFramesNumUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.maxFramesNumUpDown.ValueChanged += new System.EventHandler(this.maxFramesNumUpDown_ValueChanged);
			// 
			// spriteSelector
			// 
			this.spriteSelector.AllowNoSelection = false;
			this.spriteSelector.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.spriteSelector.Location = new System.Drawing.Point(12, 38);
			this.spriteSelector.Name = "spriteSelector";
			this.spriteSelector.SelectedIndex = 0;
			this.spriteSelector.Size = new System.Drawing.Size(308, 148);
			this.spriteSelector.SpriteHeight = 32;
			this.spriteSelector.SpriteWidth = 32;
			this.spriteSelector.TabIndex = 13;
			this.spriteSelector.TabStop = false;
			this.spriteSelector.SelectedIndexChanged += new System.EventHandler(this.spriteSelector_SelectedIndexChanged);
			// 
			// AnimationSelector
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(332, 227);
			this.Controls.Add(this.spriteSelector);
			this.Controls.Add(this.maxFramesNumUpDown);
			this.Controls.Add(this.currentFrameNumUpDown);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.canceButton);
			this.Controls.Add(this.timeoutNumericUpDown);
			this.Name = "AnimationSelector";
			this.Text = "AnimationSelector";
			((System.ComponentModel.ISupportInitialize)(this.timeoutNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.currentFrameNumUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.maxFramesNumUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spriteSelector)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.NumericUpDown timeoutNumericUpDown;
		private System.Windows.Forms.Button canceButton;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.NumericUpDown currentFrameNumUpDown;
		private System.Windows.Forms.NumericUpDown maxFramesNumUpDown;
		private SpriteSelector spriteSelector;
	}
}