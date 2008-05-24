namespace KFIRPG.editor {
	partial class EditorForm {
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
			this.components = new System.ComponentModel.Container();
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mruToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.gameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.specialViewComboBox = new System.Windows.Forms.ToolStripComboBox();
			this.mapComboBox = new System.Windows.Forms.ToolStripComboBox();
			this.hScrollBar = new System.Windows.Forms.HScrollBar();
			this.vScrollBar = new System.Windows.Forms.VScrollBar();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.folderDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.locationMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mainPanel = new KFIRPG.editor.DoubleBufferedPanel();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.animationLibraryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.passabilityButton = new System.Windows.Forms.ToolStripButton();
			this.newProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.layersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.paletteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.audioLibraryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.imageLibraryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.resizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.scriptsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.onstepMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.onActionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.movementScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip.SuspendLayout();
			this.toolStrip.SuspendLayout();
			this.contextMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip
			// 
			this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.mapToolStripMenuItem,
            this.gameToolStripMenuItem});
			this.menuStrip.Location = new System.Drawing.Point(0, 0);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.Size = new System.Drawing.Size(499, 24);
			this.menuStrip.TabIndex = 0;
			this.menuStrip.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newProjectToolStripMenuItem,
            this.loadProjectToolStripMenuItem,
            this.saveProjectToolStripMenuItem,
            this.mruToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// mruToolStripMenuItem
			// 
			this.mruToolStripMenuItem.Name = "mruToolStripMenuItem";
			this.mruToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
			this.mruToolStripMenuItem.Text = "Recent projects";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(146, 6);
			// 
			// viewToolStripMenuItem
			// 
			this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.layersToolStripMenuItem,
            this.paletteToolStripMenuItem,
            this.toolStripSeparator2,
            this.audioLibraryToolStripMenuItem,
            this.animationLibraryToolStripMenuItem,
            this.imageLibraryToolStripMenuItem});
			this.viewToolStripMenuItem.Enabled = false;
			this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
			this.viewToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
			this.viewToolStripMenuItem.Text = "View";
			// 
			// mapToolStripMenuItem
			// 
			this.mapToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newMapToolStripMenuItem,
            this.resizeToolStripMenuItem});
			this.mapToolStripMenuItem.Enabled = false;
			this.mapToolStripMenuItem.Name = "mapToolStripMenuItem";
			this.mapToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
			this.mapToolStripMenuItem.Text = "Map";
			// 
			// gameToolStripMenuItem
			// 
			this.gameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.scriptsToolStripMenuItem});
			this.gameToolStripMenuItem.Enabled = false;
			this.gameToolStripMenuItem.Name = "gameToolStripMenuItem";
			this.gameToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
			this.gameToolStripMenuItem.Text = "Game";
			// 
			// toolStrip
			// 
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.passabilityButton,
            this.specialViewComboBox,
            this.mapComboBox});
			this.toolStrip.Location = new System.Drawing.Point(0, 24);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Size = new System.Drawing.Size(499, 25);
			this.toolStrip.TabIndex = 1;
			this.toolStrip.Text = "ToolBar";
			// 
			// specialViewComboBox
			// 
			this.specialViewComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.specialViewComboBox.Items.AddRange(new object[] {
            "Current layer",
            "All layers"});
			this.specialViewComboBox.MaxDropDownItems = 2;
			this.specialViewComboBox.Name = "specialViewComboBox";
			this.specialViewComboBox.Size = new System.Drawing.Size(121, 25);
			this.specialViewComboBox.ToolTipText = "Set visibility for special tiles";
			this.specialViewComboBox.SelectedIndexChanged += new System.EventHandler(this.UpdateEventHandler);
			// 
			// mapComboBox
			// 
			this.mapComboBox.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.mapComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.mapComboBox.Name = "mapComboBox";
			this.mapComboBox.Size = new System.Drawing.Size(121, 25);
			this.mapComboBox.Sorted = true;
			this.mapComboBox.ToolTipText = "Selects the map to work with";
			// 
			// hScrollBar
			// 
			this.hScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.hScrollBar.Location = new System.Drawing.Point(0, 382);
			this.hScrollBar.Name = "hScrollBar";
			this.hScrollBar.Size = new System.Drawing.Size(483, 16);
			this.hScrollBar.TabIndex = 1;
			// 
			// vScrollBar
			// 
			this.vScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.vScrollBar.Location = new System.Drawing.Point(483, 49);
			this.vScrollBar.Name = "vScrollBar";
			this.vScrollBar.Size = new System.Drawing.Size(16, 333);
			this.vScrollBar.TabIndex = 0;
			// 
			// statusStrip
			// 
			this.statusStrip.Location = new System.Drawing.Point(0, 398);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(499, 22);
			this.statusStrip.TabIndex = 3;
			this.statusStrip.Text = "statusStrip1";
			// 
			// contextMenu
			// 
			this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.locationMenuItem,
            this.onstepMenuItem,
            this.onActionToolStripMenuItem,
            this.movementScriptToolStripMenuItem});
			this.contextMenu.Name = "contextMenu";
			this.contextMenu.Size = new System.Drawing.Size(162, 92);
			this.contextMenu.Opened += new System.EventHandler(this.contextMenu_Opened);
			// 
			// locationMenuItem
			// 
			this.locationMenuItem.Name = "locationMenuItem";
			this.locationMenuItem.Size = new System.Drawing.Size(161, 22);
			this.locationMenuItem.Text = "Location...";
			this.locationMenuItem.Click += new System.EventHandler(this.locationMenuItem_Click);
			// 
			// mainPanel
			// 
			this.mainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.mainPanel.BackColor = System.Drawing.Color.Black;
			this.mainPanel.Location = new System.Drawing.Point(0, 49);
			this.mainPanel.Name = "mainPanel";
			this.mainPanel.Size = new System.Drawing.Size(483, 333);
			this.mainPanel.TabIndex = 4;
			this.mainPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.mainPanel_Paint);
			this.mainPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mainPanel_MouseMove);
			this.mainPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mainPanel_MouseClick);
			this.mainPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mainPanel_MouseDown);
			this.mainPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mainPanel_MouseUp);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(151, 6);
			// 
			// animationLibraryToolStripMenuItem
			// 
			this.animationLibraryToolStripMenuItem.Image = global::KFIRPG.editor.Properties.Resources.film;
			this.animationLibraryToolStripMenuItem.Name = "animationLibraryToolStripMenuItem";
			this.animationLibraryToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
			this.animationLibraryToolStripMenuItem.Text = "Animation Library";
			// 
			// passabilityButton
			// 
			this.passabilityButton.CheckOnClick = true;
			this.passabilityButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.passabilityButton.Image = global::KFIRPG.editor.Properties.Resources.stop;
			this.passabilityButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.passabilityButton.Name = "passabilityButton";
			this.passabilityButton.Size = new System.Drawing.Size(23, 22);
			this.passabilityButton.Text = "Passability";
			this.passabilityButton.ToolTipText = "Toggle passability view";
			this.passabilityButton.Click += new System.EventHandler(this.UpdateEventHandler);
			// 
			// newProjectToolStripMenuItem
			// 
			this.newProjectToolStripMenuItem.Image = global::KFIRPG.editor.Properties.Resources.page_white;
			this.newProjectToolStripMenuItem.Name = "newProjectToolStripMenuItem";
			this.newProjectToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
			this.newProjectToolStripMenuItem.Text = "New project...";
			// 
			// loadProjectToolStripMenuItem
			// 
			this.loadProjectToolStripMenuItem.Image = global::KFIRPG.editor.Properties.Resources.folder;
			this.loadProjectToolStripMenuItem.Name = "loadProjectToolStripMenuItem";
			this.loadProjectToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
			this.loadProjectToolStripMenuItem.Text = "Load project...";
			this.loadProjectToolStripMenuItem.Click += new System.EventHandler(this.loadProjectToolStripMenuItem_Click);
			// 
			// saveProjectToolStripMenuItem
			// 
			this.saveProjectToolStripMenuItem.Enabled = false;
			this.saveProjectToolStripMenuItem.Image = global::KFIRPG.editor.Properties.Resources.disk;
			this.saveProjectToolStripMenuItem.Name = "saveProjectToolStripMenuItem";
			this.saveProjectToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
			this.saveProjectToolStripMenuItem.Text = "Save project";
			this.saveProjectToolStripMenuItem.Click += new System.EventHandler(this.saveProjectToolStripMenuItem_Click);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Image = global::KFIRPG.editor.Properties.Resources.door;
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// layersToolStripMenuItem
			// 
			this.layersToolStripMenuItem.Checked = true;
			this.layersToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.layersToolStripMenuItem.Image = global::KFIRPG.editor.Properties.Resources.layers;
			this.layersToolStripMenuItem.Name = "layersToolStripMenuItem";
			this.layersToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
			this.layersToolStripMenuItem.Text = "Layers";
			// 
			// paletteToolStripMenuItem
			// 
			this.paletteToolStripMenuItem.Checked = true;
			this.paletteToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.paletteToolStripMenuItem.Image = global::KFIRPG.editor.Properties.Resources.palette;
			this.paletteToolStripMenuItem.Name = "paletteToolStripMenuItem";
			this.paletteToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
			this.paletteToolStripMenuItem.Text = "Palette";
			// 
			// audioLibraryToolStripMenuItem
			// 
			this.audioLibraryToolStripMenuItem.Image = global::KFIRPG.editor.Properties.Resources.music;
			this.audioLibraryToolStripMenuItem.Name = "audioLibraryToolStripMenuItem";
			this.audioLibraryToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
			this.audioLibraryToolStripMenuItem.Text = "Audio Library";
			// 
			// imageLibraryToolStripMenuItem
			// 
			this.imageLibraryToolStripMenuItem.Image = global::KFIRPG.editor.Properties.Resources.picture;
			this.imageLibraryToolStripMenuItem.Name = "imageLibraryToolStripMenuItem";
			this.imageLibraryToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
			this.imageLibraryToolStripMenuItem.Text = "Image Library";
			// 
			// newMapToolStripMenuItem
			// 
			this.newMapToolStripMenuItem.Image = global::KFIRPG.editor.Properties.Resources.map_add;
			this.newMapToolStripMenuItem.Name = "newMapToolStripMenuItem";
			this.newMapToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
			this.newMapToolStripMenuItem.Text = "New...";
			// 
			// resizeToolStripMenuItem
			// 
			this.resizeToolStripMenuItem.Image = global::KFIRPG.editor.Properties.Resources.arrow_out;
			this.resizeToolStripMenuItem.Name = "resizeToolStripMenuItem";
			this.resizeToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
			this.resizeToolStripMenuItem.Text = "Resize...";
			this.resizeToolStripMenuItem.Click += new System.EventHandler(this.resizeToolStripMenuItem_Click);
			// 
			// scriptsToolStripMenuItem
			// 
			this.scriptsToolStripMenuItem.Image = global::KFIRPG.editor.Properties.Resources.pencil;
			this.scriptsToolStripMenuItem.Name = "scriptsToolStripMenuItem";
			this.scriptsToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
			this.scriptsToolStripMenuItem.Text = "Scripts...";
			this.scriptsToolStripMenuItem.Click += new System.EventHandler(this.scriptsToolStripMenuItem_Click);
			// 
			// onstepMenuItem
			// 
			this.onstepMenuItem.Image = global::KFIRPG.editor.Properties.Resources.lightning;
			this.onstepMenuItem.Name = "onstepMenuItem";
			this.onstepMenuItem.Size = new System.Drawing.Size(161, 22);
			this.onstepMenuItem.Text = "OnStep...";
			this.onstepMenuItem.Click += new System.EventHandler(this.onstepMenuItem_Click);
			// 
			// onActionToolStripMenuItem
			// 
			this.onActionToolStripMenuItem.Image = global::KFIRPG.editor.Properties.Resources.lightning;
			this.onActionToolStripMenuItem.Name = "onActionToolStripMenuItem";
			this.onActionToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
			this.onActionToolStripMenuItem.Text = "OnAction...";
			this.onActionToolStripMenuItem.Click += new System.EventHandler(this.onActionToolStripMenuItem_Click);
			// 
			// movementScriptToolStripMenuItem
			// 
			this.movementScriptToolStripMenuItem.Image = global::KFIRPG.editor.Properties.Resources.lightning;
			this.movementScriptToolStripMenuItem.Name = "movementScriptToolStripMenuItem";
			this.movementScriptToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
			this.movementScriptToolStripMenuItem.Text = "Movement script...";
			this.movementScriptToolStripMenuItem.Click += new System.EventHandler(this.movementScriptToolStripMenuItem_Click);
			// 
			// EditorForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(499, 420);
			this.Controls.Add(this.mainPanel);
			this.Controls.Add(this.hScrollBar);
			this.Controls.Add(this.statusStrip);
			this.Controls.Add(this.vScrollBar);
			this.Controls.Add(this.toolStrip);
			this.Controls.Add(this.menuStrip);
			this.MainMenuStrip = this.menuStrip;
			this.Name = "EditorForm";
			this.Text = "KFI RPG Creator";
			this.Deactivate += new System.EventHandler(this.EditorForm_Deactivate);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditorForm_FormClosing);
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.contextMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip;
		private System.Windows.Forms.ToolStrip toolStrip;
		private System.Windows.Forms.VScrollBar vScrollBar;
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.HScrollBar hScrollBar;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem layersToolStripMenuItem;
		private DoubleBufferedPanel mainPanel;
		private System.Windows.Forms.ToolStripMenuItem newProjectToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem loadProjectToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveProjectToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.FolderBrowserDialog folderDialog;
		private System.Windows.Forms.ToolStripMenuItem audioLibraryToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem imageLibraryToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem paletteToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem mapToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem resizeToolStripMenuItem;
		private System.Windows.Forms.ToolStripButton passabilityButton;
		private System.Windows.Forms.ToolStripMenuItem gameToolStripMenuItem;
		private System.Windows.Forms.ToolStripComboBox specialViewComboBox;
		private System.Windows.Forms.ToolStripComboBox mapComboBox;
		private System.Windows.Forms.ContextMenuStrip contextMenu;
		private System.Windows.Forms.ToolStripMenuItem locationMenuItem;
		private System.Windows.Forms.ToolStripMenuItem onstepMenuItem;
		private System.Windows.Forms.ToolStripMenuItem scriptsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem newMapToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem onActionToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem movementScriptToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem mruToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem animationLibraryToolStripMenuItem;
	}
}