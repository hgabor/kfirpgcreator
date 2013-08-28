﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KFIRPG.corelib;

namespace KFIRPG.editor
{
	/// <summary>
	/// A group of layers. It is represented to the a user as a layer, but in reality it can contain
	/// multiple game layers.
	/// </summary>
	abstract class LayerGroup : IEnumerable<Map.Layer>
	{
		protected readonly Map.Layer[] layers;
		public string Name { get; private set; }

		public int LayerCount { get { return layers.Length; } }

		public Map.Layer this[int i]
		{
			get { return layers[i]; }
		}

		protected LayerGroup(string name, int layerCount = 1)
		{
			this.Name = name;
			layers = new Map.Layer[layerCount];
		}

		internal void Resize(int newX, int newY)
		{
			Array.ForEach(layers, l => l.Resize(newX, newY));
		}

		public static LayerGroup Create(string name, string type, int width, int height, string path, Map map, Project project)
		{
			switch (type)
			{
				case "simple":
					Map.Layer layer = new Map.Layer(width, height, path, map, project);
					return new SimpleLayerGroup(name, layer);
				default:
					throw new ArgumentException(string.Format("LayerGroup type '{0}' is not supported"));
			}
		}

		IEnumerator<Map.Layer> IEnumerable<Map.Layer>.GetEnumerator()
		{
			return ((IEnumerable<Map.Layer>)layers).GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return layers.GetEnumerator();
		}
	}
}
