using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KFIRPG.editor {
	partial class AnimationSelector: Form {
		public int Start { get { return begin; } }
		public int Count { get { return end - begin + 1; } }
		public int Timeout { get { return (int)timeoutNumericUpDown.Value; } }

		Bitmap bm;
		int spriteWidth, spriteHeight;
		int cols;
		public AnimationSelector(Bitmap bm, int spriteWidth, int spriteHeight) : this(bm, spriteWidth, spriteHeight, 0, 0, 1) { }
		public AnimationSelector(Bitmap bm, int spriteWidth, int spriteHeight, int begin, int end, int timeout) {
			InitializeComponent();
			this.bm = bm;
			this.spriteWidth = spriteWidth;
			this.spriteHeight = spriteHeight;
			this.begin = begin;
			this.end = end;
			this.timeoutNumericUpDown.Value = timeout;
			cols = bm.Width / spriteWidth;
			pictureBox.Image = bm;
			beginEndComboBox.SelectedValueChanged += (sender, args) => pictureBox.Invalidate();
			beginEndComboBox.SelectedIndex = 0;
		}
		int begin = 0;
		int end = 0;
		Pen pen = Pens.Red;
		Brush brush = new SolidBrush(Color.FromArgb(128 + 64, Color.Red));

		bool BeginSelected {
			get {
				return (string)beginEndComboBox.SelectedItem == "Begin";
			}
		}

		private void pictureBox_Paint(object sender, PaintEventArgs e) {
			int x, y;
			if (BeginSelected) {
				x = begin % cols * spriteWidth;
				y = begin / cols * spriteHeight;
			}
			else {
				x = end % cols * spriteWidth;
				y = end / cols * spriteHeight;
			}
			e.Graphics.FillRectangle(brush, x, y, spriteWidth - 1, spriteHeight - 1);
			e.Graphics.DrawRectangle(pen, x, y, spriteWidth - 1, spriteHeight - 1);
		}

		private void pictureBox_MouseClick(object sender, MouseEventArgs e) {
			if (e.X >= bm.Width || e.Y >= bm.Height) return;
			int id = e.X / spriteWidth + e.Y / spriteHeight * cols;
			if (BeginSelected) begin = id; else end = id;
			pictureBox.Invalidate();
		}
	}
}
