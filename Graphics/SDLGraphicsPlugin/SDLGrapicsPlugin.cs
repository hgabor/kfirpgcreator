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
			
			for (int j = 0; j < height; ++j) {
				for (int i = 0; i < width; ++i) {
					int coordX = (width * 32) + (i * 32) - (j * 32);
					int coordY = i * 16 + j * 16;
					System.IO.Stream bitmapStream = game.Loader.GetFile(game.CurrentMap.GetTile(i, j).Id, "Still1.png");
					Surface s = new Surface(bitmapStream as System.IO.MemoryStream);
					screen.Blit(s, new System.Drawing.Point(coordX, coordY));
				}
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
