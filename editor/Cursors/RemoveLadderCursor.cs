using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KFIRPG.editor.Cursors
{
	class RemoveLadderCursor : Cursor
	{
		Project project;

		public override string Name
		{
			get
			{
				return "Delete ladder(s)";
			}
		}

		public RemoveLadderCursor(Project project)
		{
			this.project = project;
		}

		protected override void Edit(LayerGroup layerGroup)
		{
			if (!(layerGroup is SimpleLayerGroup)) throw new NotSupportedException();
			Layer layer = layerGroup[0];
			Map map = layer.Map;
			int tileX = this.tileX;
			int tileY = this.tileY;
			if (tileX >= map.ladders.GetLength(0) || tileY >= map.ladders.GetLength(1)) return;

			Map.Ladder oldLadder = map.ladders[tileX, tileY];
			if (oldLadder != null)
			{
				AddCommand(
					delegate()
					{
						map.ladders[tileX, tileY] = null;
					},
					delegate()
					{
						map.ladders[tileX, tileY] = oldLadder;
					}
				);
			}
			if (tileY < map.height - 1)
			{
				Map.Ladder oldLadder2 = map.ladders[tileX, tileY + 1];
				if (oldLadder2 != null)
				{
					AddCommand(
						delegate()
						{
							map.ladders[tileX, tileY + 1] = null;
						},
						delegate()
						{
							map.ladders[tileX, tileY + 1] = oldLadder2;
						}
					);
				}
			}
		}

		protected override void PreDraw(int x, int y, System.Drawing.Graphics g) { }
		protected override void DrawCursor(System.Drawing.Graphics g) { }
	}
}
