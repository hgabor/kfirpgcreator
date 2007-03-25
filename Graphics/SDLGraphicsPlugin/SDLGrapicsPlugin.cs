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
using Core;
using SdlDotNet.Graphics;
using SdlDotNet.Core;

namespace SDLGraphicsPlugin {
	/// <summary>
	/// Description of SDLGrapicsPlugin.
	/// </summary>
	public class SDLGrapicsPlugin : Core.GraphicsPlugin {
		Game game;
		
		Surface screen;

		public SDLGrapicsPlugin() {
		}
		
		public Core.Game Game {
			set {
				game = value;
			}
		}
		
		void RenderScene(object sender, TickEventArgs e) {
			int height = game.CurrentMap.Height;
			int width = game.CurrentMap.Width;
			
			//TODO: Replace MemoryStream with Stream
			for (int j = 0; j < height; ++j) {
				for (int i = 0; i < width; ++i) {
					int coordX = (width * 32) + (i * 32) - (j * 32);
					int coordY = i * 16 + j * 16;
					System.IO.Stream bitmapStream = game.Loader.GetFile(game.CurrentMap.GetTile(i, j).Id, "Still1.png");
//					Surface s = new Surface(bitmapStream as System.IO.MemoryStream);
					Surface s = new Surface(new System.Drawing.Bitmap(bitmapStream));
					screen.Blit(s, new System.Drawing.Point(coordX, coordY));
				}
			}
			foreach (Core.GameObjects.GameObject o in game.CurrentMap.GetAllObjects()) {
				System.IO.Stream bitmapStream = game.Loader.GetFile(o.Id, "Still1.png");
				System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(bitmapStream);
				int centerX = (width * 32) + (o.X * 32 - o.Y * 32) / 100 + 32;
				int centerY = (o.X * 32 + o.Y * 32) / 100;
				int leftX = centerX - (bitmap.Width / 2);
				int topY = centerY - bitmap.Height + (bitmap.Width / 4);
				Surface s = new Surface(bitmap);
				screen.Blit(s, new System.Drawing.Point(leftX, topY));
				screen.Draw(
					new SdlDotNet.Graphics.Primitives.Line(
						(short)centerX,
						(short)centerY,
						(short)centerX,
						(short)centerY
					),
					System.Drawing.Color.Red
				);
			}
			screen.Update();
		}
		
		public void StartRendering() {
			try {
				screen = Video.SetVideoMode(300, 200);
				Video.WindowCaption = "Title";
				Events.Tick += RenderScene;
				Events.Run();
			}
			catch(Exception e) {
				Console.Error.WriteLine("A program futása közben kivétel történt:");
				Console.Error.WriteLine(e.Message);
			}
		}
	}
}
