using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KFIRPG.editor
{
	class SimpleLayerGroup : LayerGroup
	{
		public SimpleLayerGroup(string name, Map.Layer layer)
			: base(name)
		{
			base.layers[0] = layer;
		}
	}
}
