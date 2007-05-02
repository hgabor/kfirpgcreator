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
		/*public GameMap CurrentMap {
			get {
				return currentMap;
			}
		}*/
		public int MapWidth {
			get { return currentMap.Width; }
		}
		public int MapHeight {
			get { return currentMap.Height; }
		}
		
		public GameObject[] VisibleObjects {
			get { return currentMap.GetAllObjects(); }
		}
		
		ObjectLoader loader;
		public ObjectLoader Loader {
			get {
				return loader;
			}
		}
		
		GraphicsPlugin graphicsPlugin;
		Controller.Controller controller;
		
		public Game(GraphicsPluginFactory graphicsPluginFactory, Controller.ControllerFactory controllerFactory) {
			loader = new ObjectLoader_File();
			GameObject[,] tiles = new GameObjectImpl[3,3];
			currentMap = new GameMapImpl(300, 400);
			currentMap.AddObject(new GameObjectImpl("testtile", 0, 0, loader));
			currentMap.AddObject(new GameObjectImpl("testtile", 100, 0, loader));
			currentMap.AddObject(new GameObjectImpl("testtile", 200, 0, loader));
			currentMap.AddObject(new GameObjectImpl("testtile", 0, 100, loader));
			currentMap.AddObject(new GameObjectImpl("testtile", 100, 100, loader));
			currentMap.AddObject(new GameObjectImpl("testtile2", 200, 100, loader));
			currentMap.AddObject(new GameObjectImpl("testtile", 0, 200, loader));
			currentMap.AddObject(new GameObjectImpl("testtile2", 100, 200, loader));
			currentMap.AddObject(new GameObjectImpl("testtile2", 200, 200, loader));
			currentMap.AddObject(new GameObjectImpl("testtile2", 0, 300, loader));
			currentMap.AddObject(new GameObjectImpl("testtile2", 100, 300, loader));
			currentMap.AddObject(new GameObjectImpl("testtile2", 200, 300, loader));
			currentMap.AddObject(new GameObjectImpl("Gray ball", 20, 20, loader));

			this.graphicsPlugin = graphicsPluginFactory.Create(this);
			this.controller = controllerFactory.Create();
		}
		
		
		public void Run() {
			while(!controller.Poll(Controller.Button.BACK)) {
				graphicsPlugin.Render();
			}
		}

		#if EXECUTABLE

		public static void Main(string[] args) {
			string fileName = System.IO.Path.GetFullPath("SDLPlugin.dll");
			System.Reflection.Assembly pluginAssembly = System.Reflection.Assembly.LoadFile(fileName);
			Type[] types = pluginAssembly.GetTypes();
			GraphicsPluginFactory graphicsPluginFactory = null;
			Controller.ControllerFactory controllerFactory = null;
			foreach(Type type in types) {
				System.Type pluginInterface = type.GetInterface("Core.GraphicsPluginFactory");
				System.Reflection.ConstructorInfo pluginCtor = type.GetConstructor(Type.EmptyTypes);
				if(
					pluginInterface != null &&
					pluginCtor != null
				)  {
					graphicsPluginFactory = (type.GetConstructor(Type.EmptyTypes).Invoke(null) as GraphicsPluginFactory);
				}
				
				pluginInterface = type.GetInterface("Core.Controller.ControllerFactory");
				if(
					pluginInterface != null &&
					pluginCtor != null
				) {
					controllerFactory = (type.GetConstructor(Type.EmptyTypes).Invoke(null) as Controller.ControllerFactory);
				}
			}
			if (graphicsPluginFactory == null) {
				Console.Error.WriteLine("A megadott grafikai plugin nem megfelelõ");
			}
			else {
				Game g = new Game(graphicsPluginFactory, controllerFactory);
				g.Run();
			}
		}

		#endif
	}
}
