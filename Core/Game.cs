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
using Core.GameObjects;

namespace Core {
	/// <summary>
	/// Szint, ami átjárást biztosít a térképet közt.
	/// </summary>
	public class Game {
		GameMap currentMap;
		public GameMap CurrentMap {
			get {
				return currentMap;
			}
		}
		
		ObjectLoader loader;
		public ObjectLoader Loader {
			get {
				return loader;
			}
		}
		
		GraphicsPlugin graphicsPlugin;
		
		public Game(GraphicsPlugin graphicsPlugin) {
			loader = new ObjectLoader_File();
			GameObject[,] tiles = new GameObjectImpl[3,3];
			tiles[0,0] = new GameObjectImpl("testtile", loader);
			tiles[1,0] = new GameObjectImpl("testtile", loader);
			tiles[2,0] = new GameObjectImpl("testtile2", loader);
			tiles[0,1] = new GameObjectImpl("testtile", loader);
			tiles[1,1] = new GameObjectImpl("testtile", loader);
			tiles[2,1] = new GameObjectImpl("testtile2", loader);
			tiles[0,2] = new GameObjectImpl("testtile2", loader);
			tiles[1,2] = new GameObjectImpl("testtile2", loader);
			tiles[2,2] = new GameObjectImpl("testtile2", loader);
			currentMap = new GameMapImpl(tiles);

			this.graphicsPlugin = graphicsPlugin;
			this.graphicsPlugin.Game = this;
		}
		
		System.Threading.Thread displayThread;
		
		public void Run() {
			displayThread = new System.Threading.Thread(graphicsPlugin.StartRendering);
			//displayThread.Name = "Display Thread";
			displayThread.Start();
			while(true) {
			}
		}

		#if EXECUTABLE

		public static void Main(string[] args) {
			//System.Threading.Thread.CurrentThread.Name = "Main Thread";

			string fileName = System.IO.Path.GetFullPath("SDLGraphicsPlugin.dll");
			System.Reflection.Assembly pluginAssembly = System.Reflection.Assembly.LoadFile(fileName);
			Type[] types = pluginAssembly.GetTypes();
			GraphicsPlugin found = null;
			foreach(Type type in types) {
				System.Type pluginInterface = type.GetInterface("Core.GraphicsPlugin");
				System.Reflection.ConstructorInfo pluginCtor = type.GetConstructor(Type.EmptyTypes);
				if(
					pluginInterface != null &&
					pluginCtor != null
				)  {
					found = (type.GetConstructor(Type.EmptyTypes).Invoke(null) as GraphicsPlugin);
				}
			}
			if (found == null) {
				Console.Error.WriteLine("A megadott grafikai plugin nem megfelelõ");
			}
			else {
				Game g = new Game(found);
				g.Run();
			}
		}

		#endif
	}
}
