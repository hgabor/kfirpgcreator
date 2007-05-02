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
using Core.GameObjects;
using SdlDotNet.Core;
using SdlDotNet.Graphics;

namespace SDLPlugin {
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
		
		internal Graphics(Game game) {
			this.game = game;
			//screen = Video.SetVideoMode(1024, 768, false, false, true);
			screen = Video.SetVideoMode(640, 480);
		}

		System.DateTime lastUpdate = DateTime.Now;
		int tickCounter = 0;
		double fps = 0;

		private void Paint() {
			if (++tickCounter == 10) {
				System.DateTime now = System.DateTime.Now;
				System.TimeSpan timeSpan = now - lastUpdate;
				fps = 10d / timeSpan.TotalMilliseconds * 1000;
				tickCounter = 0;
				lastUpdate = now;
			}
			int height = game.MapHeight;
			foreach (GameObject o in game.VisibleObjects) {
				System.IO.Stream bitmapStream = game.Loader.GetFile(o.Id, "Still1.png");
				System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(bitmapStream);
				int centerX = (height + o.X - o.Y) * 32 / 100;
				int centerY = (o.X + o.Y) * 16 / 100 + 16;
				int leftX = centerX - (bitmap.Width / 2);
				int topY = centerY - bitmap.Height + (bitmap.Width / 4);
				Surface s = new Surface(bitmap);
				screen.Blit(s, new System.Drawing.Point(leftX, topY));
			}
			screen.Update();
		}
		
		public void Render() {
			//throw new NotImplementedException();
			Events.Poll();
			this.Paint();
		}
	}
}
