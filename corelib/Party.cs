using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.corelib {
	class Party {
		Game game;
		List<Sprite> members = new List<Sprite>();
		Sprite leader;
		public Sprite Leader {
			get { return leader; }
			private set {
				leader = value;
				game.vm["player"] = leader;
			}
		}

		public Party(Game game) {
			this.game = game;
		}

		public void Add(Sprite member) {
			if (Leader == null) Leader = member;
			this.members.Add(member);
		}

		public void Render(int x, int y, SdlDotNet.Graphics.Surface surface) {
			leader.Render(x, y, surface);
		}
	}
}
