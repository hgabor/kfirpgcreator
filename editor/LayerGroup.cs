using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KFIRPG.editor
{
	/// <summary>
	/// A group of layers. It is represented to the a user as a layer, but in reality it can contain
	/// multiple game layers.
	/// </summary>
	abstract class LayerGroup
	{
		protected readonly Map.Layer[] layers;

		protected LayerGroup(int layerCount = 1)
		{
			layers = new Map.Layer[layerCount];
		}
	}
}
