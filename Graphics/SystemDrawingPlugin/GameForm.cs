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
using Core.GameObjects;

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
		int tickCounter = 0;
		double fps = 0;
		
		protected override void OnPaint(PaintEventArgs e) {
			e.Graphics.Clear(Color.Black);
			if (++tickCounter == 10) {
				System.DateTime now = System.DateTime.Now;
				System.TimeSpan timeSpan = now - lastUpdate;
				fps = 10d / timeSpan.TotalMilliseconds * 1000;
				tickCounter = 0;
				lastUpdate = now;
			}
			
			int height = game.MapHeight;
			int width = game.MapWidth;
			
			foreach (GameObject o in game.VisibleObjects) {
				System.IO.Stream bitmapStream = game.Loader.GetFile(o.Id, "Still1.png");
				System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(bitmapStream);
				int centerX = (height + o.X - o.Y) * 32 / 100;
				int centerY = (o.X + o.Y) * 16 / 100 + 16;
				int leftX = centerX - (bitmap.Width / 2);
				int topY = centerY - bitmap.Height + (bitmap.Width / 4);
				
				e.Graphics.DrawImage(bitmap, leftX, topY);
			}
			
			e.Graphics.DrawString(
				String.Format("FPS: {0}", fps),
				new System.Drawing.Font("Arial", 10),
				System.Drawing.Brushes.White,
				3,
				3
			);
		}
		
		const int FramesPerSec = 50;
		const int MillisecPerFrame = 1000 / FramesPerSec;

		public void StartRendering() {
			try {
				#if DEBUG
				Console.WriteLine("Showing dialog");
				this.Shown += delegate { Console.WriteLine("Shown"); };
				#endif
				
				using (System.Threading.Timer fpsTimer = new System.Threading.Timer(
					delegate { Invoke( new MethodInvoker(Invalidate) ); },
					null,
					System.Threading.Timeout.Infinite,
					System.Threading.Timeout.Infinite)
				) {
					this.Shown += delegate { fpsTimer.Change(0, MillisecPerFrame); };
					this.ShowDialog();
				}
				#if DEBUG
				Console.WriteLine("Finished dialog");
				#endif
			}
			catch(Exception e) {
				Console.Error.WriteLine(e.Message);
			}
		}
	}
}
