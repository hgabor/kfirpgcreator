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
using System.Collections.Generic;
using KFI_RPG_Creator.Core;
using SdlDotNet.Core;
using SdlDotNet.Graphics;

namespace KFI_RPG_Creator.SDLPlugin {
	public class Factory: GraphicsPluginFactory {
		public GraphicsPlugin Create(Game game) {
			return new Graphics(game);
		}
	}

	/// <summary>
	/// Description of Graphics.
	/// </summary>
	public class Graphics: GraphicsPlugin {
		Game game;
		
		Surface screen;
		
		const int ScreenWidth = 640;
		const int ScreenHeight = 480;
		
		internal Graphics(Game game) {
			this.game = game;
			//screen = Video.SetVideoMode(1024, 768, false, false, true);
			screen = Video.SetVideoMode(ScreenWidth, ScreenHeight);
		}

		// Needed because surfaces leak memory, even when Dispose()-t
		Dictionary<string, Surface> surfaces = new Dictionary<string, Surface>();
		Surface GetSurface(string typeID, string filename, Bitmap bitmap) {
			string key = typeID + "//" + filename;
			if (!surfaces.ContainsKey(key)) {
				surfaces.Add(key, new Surface(bitmap));
			}
			return surfaces[key];
		}

		SdlDotNet.Graphics.Sprites.TextSprite textsprite = new SdlDotNet.Graphics.Sprites.TextSprite(new SdlDotNet.Graphics.Font("arial.ttf", 12));
		int lastTick = 0;
		void Paint() {
			screen.Fill(Color.Black);
			double fps = 1000d / (Timer.TicksElapsed - lastTick);
			lastTick = Timer.TicksElapsed;
			//textsprite.Text = game.Fps.ToString();
			textsprite.Text = fps.ToString();
			int height = game.Logic.Height;
			//Magick constants:
			// 64 - Width of a tile bitmap
			// 32 - Half width of a tile
			// 16 - Half height of a tile (it's actuall 15.5, but tiles overlap)
			// 36 - Length (in pixels) of the 100*100*100 cube's edges - SQRT(32^2 + 16^2)
			int pcenterX = (height + game.Logic.CenterX - game.Logic.CenterY) * 32 / 100;
			int pcenterY = (game.Logic.CenterX + game.Logic.CenterY) * 16 / 100 + 16 - game.Logic.CenterZ * 36 / 100;
			int pX = ScreenWidth/2 - pcenterX;
			int pY = ScreenHeight/2 - pcenterY;
			foreach (Sprite o in game.Logic.VisibleScreen.VisibleSprites) {
				System.IO.Stream bitmapStream;
				string bitmapFileName;
				if (game.Loader.FileExists(o.TypeID, "Still1"+o.Facing+".png")) {
					bitmapFileName = "Still1"+o.Facing+".png";
				}
				else {
					bitmapFileName = "Still1.png";
				}
				bitmapStream = game.Loader.GetFile(o.TypeID, bitmapFileName);
				using (System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(bitmapStream)) {
					int centerX = (height + o.X - o.Y) * 32 / 100 + pX;
					int centerY = (o.X + o.Y) * 16 / 100 + 16+ pY - o.Z;
					int leftX = centerX - (bitmap.Width / 2);
					int topY = centerY - bitmap.Height + (bitmap.Width / 4);
					screen.Blit(GetSurface(o.TypeID, bitmapFileName, bitmap), new System.Drawing.Point(leftX, topY));
					screen.Blit(textsprite, new System.Drawing.Point(0, 0));
				}
			}
			screen.Update();
		}
		
		public void Render() {
			Events.Poll();
			this.Paint();
		}
	}
}
