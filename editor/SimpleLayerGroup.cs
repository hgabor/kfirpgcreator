using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KFIRPG.editor
{
	class SimpleLayerGroup : LayerGroup
	{
		public SimpleLayerGroup(Map.Layer layer)
		{
			base.layers[0] = layer;
		}
	}
}
