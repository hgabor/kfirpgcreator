using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;

namespace KFIRPG.editor {
	class SpriteSelector: PictureBox {

		#region Designer Vars

		private bool allowNone = true;
		[System.ComponentModel.Category("Behavior")]
		[System.ComponentModel.DefaultValue(true)]
		public bool AllowNoSelection {
			get { return allowNone; }
			set {
				if (value && selectedIndex == -1) SelectedIndex = 0;
				allowNone = value;
			}
		}
		
		[System.ComponentModel.Category("Appearance")]
		[System.ComponentModel.DefaultValue(typeof(System.Drawing.Color), "Red")]
		public Color SelectionPenColor {
			get { return selectionPen.Color; }
			set {
				selectionPen.Color = value;
				Invalidate();
			}
		}
		[System.ComponentModel.Category("Appearance")]
		[System.ComponentModel.DefaultValue(1F)]
		public float SelectionPenWidth {
			get { return selectionPen.Width; }
			set {
				selectionPen.Width = value; ;
				Invalidate();
			}
		}

		#endregion

		#region Public Fields

		private int spriteWidth = 32;
		[System.ComponentModel.Browsable(false)]
		public int SpriteWidth {
			get { return spriteWidth; }
			set {
				spriteWidth = value;
				if (selectedIndex != -1) Invalidate();
			}
		}

		private int spriteHeight = 32;
		[System.ComponentModel.Browsable(false)]
		public int SpriteHeight {
			get { return spriteHeight; }
			set {
				spriteHeight = value;
				if (selectedIndex != -1) Invalidate();
			}
		}


		private int selectedIndex = -1;
		[System.ComponentModel.Browsable(false)]
		public int SelectedIndex {
			get {
				return selectedIndex;
			}
			set {
				if (value < -1 || value > MaxIndex || (value == -1 && allowNone)) {
					throw new ArgumentOutOfRangeException();
				}
				else {
					selectedIndex = value;
					if (SelectedIndexChanged != null) SelectedIndexChanged(this, new EventArgs());
					this.Invalidate();
				}
			}
		}
		

		private Pen selectionPen = new Pen(Color.Red);
		[System.ComponentModel.Browsable(false)]
		public Pen SelectionPen {
			get { return selectionPen; }
			set {
				if (value == null) throw new ArgumentNullException();
				selectionPen = value;
				if (selectedIndex != -1) Invalidate();
			}
		}

		#endregion

		#region Events

		public event EventHandler SelectedIndexChanged;

		#endregion



		private int Cols {
			get {
				if (this.Image == null) return 0;
				else return this.Image.Width / SpriteWidth;
			}
		}
		private int Rows {
			get {
				if (this.Image == null) return 0;
				else return this.Image.Height / SpriteHeight;
			}
		}
		private int SelectedX {
			get { return SelectedIndex % Cols; }
		}
		private int SelectedY {
			get { return SelectedIndex / Cols; }
		}
		private int MaxIndex {
			get {
				int val = Cols * Rows - 1;
				return val == -1 ? 0 : val;
			}
		}

		protected override void OnPaint(PaintEventArgs pe) {
			base.OnPaint(pe);
			if (selectedIndex != -1 && Image != null) {
				pe.Graphics.DrawRectangle(selectionPen, SelectedX * spriteWidth, SelectedY * spriteHeight, spriteWidth - 1, spriteHeight - 1);
			}
		}

		protected override void OnMouseClick(MouseEventArgs e) {
			if (e.X >= Image.Width || e.Y >= Image.Height) return;
			int x = e.X / spriteWidth;
			int y = e.Y / spriteHeight;
			SelectedIndex = y * Cols + x;
			base.OnMouseClick(e);
		}
	}
}
