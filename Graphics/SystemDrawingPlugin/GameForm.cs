// Copyright (C) 2007 Gábor Halász
// 
// This program is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either version 2
// of the License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using Core;

namespace SystemDrawingPlugin {
	/// <summary>
	/// Description of GameForm.
	/// </summary>
	public partial class GameForm : Form, GraphicsPlugin {
		Game game;
		
		public Core.Game Game {
			set {
				game = value;
			}
		}
		
		public GameForm() {
			InitializeComponent();
			SetStyle(
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.DoubleBuffer |
				ControlStyles.Opaque |
				ControlStyles.UserPaint,
				true
			);
		}
		
		System.DateTime lastUpdate = DateTime.Now;
		
		protected override void OnPaint(PaintEventArgs e) {
			//base.OnPaint(e);
			System.DateTime now = System.DateTime.Now;
			System.TimeSpan timeSpan = now - lastUpdate;
			double fps;
			if (timeSpan.Milliseconds == 0) {
				fps = 0;
			}
			else {
				fps = 1d / timeSpan.Milliseconds * 1000;
			}
			
			int height = game.CurrentMap.Height;
			int width = game.CurrentMap.Width;
			
			for (int j = 0; j < height; ++j) {
				for (int i = 0; i < width; ++i) {
					//TODO: lookup correct algorithm for isometric calculations
					int coordX = (width * 32) + (i * 32) - (j * 32);
					int coordY = i * 15 + j * 15;
					Bitmap bmp = new Bitmap(game.Loader.GetFile(game.CurrentMap.GetTile(i, j).Id, "Still1.png"));
					e.Graphics.DrawImage(bmp, coordX, coordY);
					e.Graphics.DrawString(
						String.Format("({0}; {1})", i, j),
						new System.Drawing.Font("Arial", 10),
						System.Drawing.Brushes.Black,
						coordX+10,
						coordY+10
					);
				}
			}
			
			e.Graphics.DrawString(
				String.Format("FPS: {0}", fps),
				new System.Drawing.Font("Arial", 10),
				System.Drawing.Brushes.White,
				3,
				3
			);
			lastUpdate = now;
			this.Invalidate();
		}

		public void StartRendering() {
			this.ShowDialog();
		}

		
		#if DEBUG
		/*
		public static void Main(string[] args) {
			Core.GameLevel level = new Core.GameLevel("SystemDrawingPlugin.dll");
			level.Run();
		}
		*/
		#endif
	}
}
