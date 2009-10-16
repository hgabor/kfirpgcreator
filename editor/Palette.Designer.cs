namespace KFIRPG.editor {
	partial class Palette {
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
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tilePage = new System.Windows.Forms.TabPage();
			this.hScrollBar = new System.Windows.Forms.HScrollBar();
			this.vScrollBar = new System.Windows.Forms.VScrollBar();
			this.objectPage = new System.Windows.Forms.TabPage();
			this.addObjectButton = new System.Windows.Forms.Button();
			this.removeObjectButton = new System.Windows.Forms.Button();
			this.clearScriptCheckBox = new System.Windows.Forms.CheckBox();
			this.objectsListBox = new System.Windows.Forms.ListBox();
			this.specialPage = new System.Windows.Forms.TabPage();
			this.removeLadderButton = new System.Windows.Forms.Button();
			this.ladderButton = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.impassableButton = new System.Windows.Forms.Button();
			this.passableButton = new System.Windows.Forms.Button();
			this.tilesPanel = new KFIRPG.editor.DoubleBufferedPanel();
			this.tabControl1.SuspendLayout();
			this.tilePage.SuspendLayout();
			this.objectPage.SuspendLayout();
			this.specialPage.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tilePage);
			this.tabControl1.Controls.Add(this.objectPage);
			this.tabControl1.Controls.Add(this.specialPage);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(297, 282);
			this.tabControl1.TabIndex = 0;
			// 
			// tilePage
			// 
			this.tilePage.Controls.Add(this.tilesPanel);
			this.tilePage.Controls.Add(this.hScrollBar);
			this.tilePage.Controls.Add(this.vScrollBar);
			this.tilePage.Location = new System.Drawing.Point(4, 22);
			this.tilePage.Name = "tilePage";
			this.tilePage.Padding = new System.Windows.Forms.Padding(3);
			this.tilePage.Size = new System.Drawing.Size(289, 256);
			this.tilePage.TabIndex = 0;
			this.tilePage.Text = "Tiles";
			this.tilePage.UseVisualStyleBackColor = true;
			// 
			// hScrollBar
			// 
			this.hScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.hScrollBar.Enabled = false;
			this.hScrollBar.Location = new System.Drawing.Point(0, 240);
			this.hScrollBar.Name = "hScrollBar";
			this.hScrollBar.Size = new System.Drawing.Size(273, 16);
			this.hScrollBar.TabIndex = 2;
			// 
			// vScrollBar
			// 
			this.vScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.vScrollBar.Enabled = false;
			this.vScrollBar.Location = new System.Drawing.Point(273, 0);
			this.vScrollBar.Name = "vScrollBar";
			this.vScrollBar.Size = new System.Drawing.Size(16, 240);
			this.vScrollBar.TabIndex = 1;
			// 
			// objectPage
			// 
			this.objectPage.Controls.Add(this.addObjectButton);
			this.objectPage.Controls.Add(this.removeObjectButton);
			this.objectPage.Controls.Add(this.clearScriptCheckBox);
			this.objectPage.Controls.Add(this.objectsListBox);
			this.objectPage.Location = new System.Drawing.Point(4, 22);
			this.objectPage.Name = "objectPage";
			this.objectPage.Padding = new System.Windows.Forms.Padding(3);
			this.objectPage.Size = new System.Drawing.Size(289, 256);
			this.objectPage.TabIndex = 2;
			this.objectPage.Text = "Objects";
			this.objectPage.UseVisualStyleBackColor = true;
			// 
			// addObjectButton
			// 
			this.addObjectButton.Image = global::KFIRPG.editor.Properties.Resources.add;
			this.addObjectButton.Location = new System.Drawing.Point(229, 0);
			this.addObjectButton.Name = "addObjectButton";
			this.addObjectButton.Size = new System.Drawing.Size(30, 30);
			this.addObjectButton.TabIndex = 3;
			this.addObjectButton.UseVisualStyleBackColor = true;
			this.addObjectButton.Click += new System.EventHandler(this.addObjectButton_Click);
			// 
			// removeObjectButton
			// 
			this.removeObjectButton.Image = global::KFIRPG.editor.Properties.Resources.cross;
			this.removeObjectButton.Location = new System.Drawing.Point(259, 0);
			this.removeObjectButton.Name = "removeObjectButton";
			this.removeObjectButton.Size = new System.Drawing.Size(30, 30);
			this.removeObjectButton.TabIndex = 2;
			this.removeObjectButton.UseVisualStyleBackColor = true;
			// 
			// clearScriptCheckBox
			// 
			this.clearScriptCheckBox.AutoSize = true;
			this.clearScriptCheckBox.Location = new System.Drawing.Point(8, 6);
			this.clearScriptCheckBox.Name = "clearScriptCheckBox";
			this.clearScriptCheckBox.Size = new System.Drawing.Size(191, 17);
			this.clearScriptCheckBox.TabIndex = 1;
			this.clearScriptCheckBox.Text = "Erase action and movement scripts";
			this.clearScriptCheckBox.UseVisualStyleBackColor = true;
			this.clearScriptCheckBox.CheckedChanged += new System.EventHandler(this.SelectObjectCursor_Handler);
			// 
			// objectsListBox
			// 
			this.objectsListBox.DisplayMember = "Name";
			this.objectsListBox.FormattingEnabled = true;
			this.objectsListBox.IntegralHeight = false;
			this.objectsListBox.Location = new System.Drawing.Point(0, 29);
			this.objectsListBox.Name = "objectsListBox";
			this.objectsListBox.Size = new System.Drawing.Size(289, 227);
			this.objectsListBox.TabIndex = 0;
			this.objectsListBox.SelectedIndexChanged += new System.EventHandler(this.SelectObjectCursor_Handler);
			this.objectsListBox.DoubleClick += new System.EventHandler(this.objectsListBox_DoubleClick);
			// 
			// specialPage
			// 
			this.specialPage.Controls.Add(this.removeLadderButton);
			this.specialPage.Controls.Add(this.ladderButton);
			this.specialPage.Controls.Add(this.label2);
			this.specialPage.Controls.Add(this.label1);
			this.specialPage.Controls.Add(this.impassableButton);
			this.specialPage.Controls.Add(this.passableButton);
			this.specialPage.Location = new System.Drawing.Point(4, 22);
			this.specialPage.Name = "specialPage";
			this.specialPage.Padding = new System.Windows.Forms.Padding(3);
			this.specialPage.Size = new System.Drawing.Size(289, 256);
			this.specialPage.TabIndex = 3;
			this.specialPage.Text = "Special";
			this.specialPage.UseVisualStyleBackColor = true;
			// 
			// removeLadderButton
			// 
			this.removeLadderButton.Image = global::KFIRPG.editor.Properties.Resources.cross;
			this.removeLadderButton.Location = new System.Drawing.Point(44, 70);
			this.removeLadderButton.Name = "removeLadderButton";
			this.removeLadderButton.Size = new System.Drawing.Size(32, 32);
			this.removeLadderButton.TabIndex = 5;
			this.removeLadderButton.UseVisualStyleBackColor = true;
			this.removeLadderButton.Click += new System.EventHandler(this.removeLadderButton_Click);
			// 
			// ladderButton
			// 
			this.ladderButton.Image = global::KFIRPG.editor.Properties.Resources.tick;
			this.ladderButton.Location = new System.Drawing.Point(6, 70);
			this.ladderButton.Name = "ladderButton";
			this.ladderButton.Size = new System.Drawing.Size(32, 32);
			this.ladderButton.TabIndex = 4;
			this.ladderButton.UseVisualStyleBackColor = true;
			this.ladderButton.Click += new System.EventHandler(this.ladderButton_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 54);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(40, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Ladder";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Passability";
			// 
			// impassableButton
			// 
			this.impassableButton.Image = global::KFIRPG.editor.Properties.Resources.cross;
			this.impassableButton.Location = new System.Drawing.Point(44, 19);
			this.impassableButton.Name = "impassableButton";
			this.impassableButton.Size = new System.Drawing.Size(32, 32);
			this.impassableButton.TabIndex = 1;
			this.impassableButton.UseVisualStyleBackColor = true;
			this.impassableButton.Click += new System.EventHandler(this.impassableButton_Click);
			// 
			// passableButton
			// 
			this.passableButton.Image = global::KFIRPG.editor.Properties.Resources.tick;
			this.passableButton.Location = new System.Drawing.Point(6, 19);
			this.passableButton.Name = "passableButton";
			this.passableButton.Size = new System.Drawing.Size(32, 32);
			this.passableButton.TabIndex = 0;
			this.passableButton.UseVisualStyleBackColor = true;
			this.passableButton.Click += new System.EventHandler(this.passableButton_Click);
			// 
			// tilesPanel
			// 
			this.tilesPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tilesPanel.Location = new System.Drawing.Point(0, 0);
			this.tilesPanel.Name = "tilesPanel";
			this.tilesPanel.Size = new System.Drawing.Size(273, 240);
			this.tilesPanel.TabIndex = 3;
			this.tilesPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.tilesPanel_Paint);
			this.tilesPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tilesPanel_MouseClick);
			// 
			// Palette
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(297, 282);
			this.Controls.Add(this.tabControl1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "Palette";
			this.ShowInTaskbar = false;
			this.Text = "Palette";
			this.tabControl1.ResumeLayout(false);
			this.tilePage.ResumeLayout(false);
			this.objectPage.ResumeLayout(false);
			this.objectPage.PerformLayout();
			this.specialPage.ResumeLayout(false);
			this.specialPage.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tilePage;
		private System.Windows.Forms.HScrollBar hScrollBar;
		private System.Windows.Forms.VScrollBar vScrollBar;
		private DoubleBufferedPanel tilesPanel;
		private System.Windows.Forms.TabPage objectPage;
		private System.Windows.Forms.ListBox objectsListBox;
		private System.Windows.Forms.CheckBox clearScriptCheckBox;
		private System.Windows.Forms.TabPage specialPage;
		private System.Windows.Forms.Button impassableButton;
		private System.Windows.Forms.Button passableButton;
		private System.Windows.Forms.Button removeLadderButton;
		private System.Windows.Forms.Button ladderButton;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button addObjectButton;
		private System.Windows.Forms.Button removeObjectButton;
	}
}