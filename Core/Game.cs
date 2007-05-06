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

namespace KFI_RPG_Creator.Core {
	/// <summary>
	/// The main class that ties the plugins together, and contains the game loop.
	/// </summary>
	public class Game {
		ObjectLoader loader;
		public ObjectLoader Loader {
			get {
				return loader;
			}
		}
		GraphicsPlugin graphicsPlugin;
		public GraphicsPlugin Graphics {
			get {
				return graphicsPlugin;
			}
		}
		Controller controller;
		public Controller Controller {
			get {
				return Controller;
			}
		}
		Logic gameLogic;
		public Logic Logic {
			get {
				return gameLogic;
			}
		}
		
		public Game(GraphicsPluginFactory graphicsPluginFactory, ControllerFactory controllerFactory, LogicFactory logicFactory) {
			loader = new ObjectLoader_File();
			this.graphicsPlugin = graphicsPluginFactory.Create(this);
			this.controller = controllerFactory.Create();
			this.gameLogic = logicFactory.CreateLogic(loader);
		}
		
		
		public void Run() {
			while(!controller.Poll(Button.BACK)) {
				graphicsPlugin.Render();
			}
		}

		#if EXECUTABLE

		public static void Main(string[] args) {
			GraphicsPluginFactory graphicsPluginFactory = null;
			ControllerFactory controllerFactory = null;
			LogicFactory logicFactory = null;
			
			string fileName = System.IO.Path.GetFullPath("SDLPlugin.dll");
			System.Reflection.Assembly pluginAssembly = System.Reflection.Assembly.LoadFile(fileName);
			Type[] types = pluginAssembly.GetTypes();
			
			foreach(Type type in types) {
				System.Type pluginInterface = type.GetInterface("KFI_RPG_Creator.Core.GraphicsPluginFactory");
				System.Reflection.ConstructorInfo pluginCtor = type.GetConstructor(Type.EmptyTypes);
				if(
					pluginInterface != null &&
					pluginCtor != null
				)  {
					graphicsPluginFactory = (type.GetConstructor(Type.EmptyTypes).Invoke(null) as GraphicsPluginFactory);
				}
				
				pluginInterface = type.GetInterface("KFI_RPG_Creator.Core.ControllerFactory");
				if(
					pluginInterface != null &&
					pluginCtor != null
				) {
					controllerFactory = (type.GetConstructor(Type.EmptyTypes).Invoke(null) as ControllerFactory);
				}
			}
			
			fileName = System.IO.Path.GetFullPath("Logic.dll");
			pluginAssembly = System.Reflection.Assembly.LoadFile(fileName);
			types = pluginAssembly.GetTypes();
			foreach(Type type in types) {
				System.Type pluginInterface = type.GetInterface("KFI_RPG_Creator.Core.LogicFactory");
				System.Reflection.ConstructorInfo pluginCtor = type.GetConstructor(Type.EmptyTypes);
				if(
					pluginInterface != null &&
					pluginCtor != null
				) {
					logicFactory = (type.GetConstructor(Type.EmptyTypes).Invoke(null) as LogicFactory);
				}
			}
			if (graphicsPluginFactory == null) {
				Console.Error.WriteLine("A megadott grafikai plugin nem megfelelõ");
			}
			else if (controllerFactory == null) {
				Console.Error.WriteLine("Amegadott controller-plugin nem megfelelõ");
			}
			else if (logicFactory == null) {
				Console.Error.WriteLine("A megadott logikai plugin nem megfelelõ");
			}
			else {
				Game g = new Game(graphicsPluginFactory, controllerFactory, logicFactory);
				g.Run();
			}
		}

		#endif
	}
}
