﻿using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.corelib {
	class Party {
		List<Sprite> members = new List<Sprite>();
		Sprite leader;
		public Sprite Leader { get { return leader; } }

		public Party() {
		}

		public void Add(Sprite member) {
			if (leader == null) leader = member;
			this.members.Add(member);
		}

		public void Draw(int x, int y, SdlDotNet.Graphics.Surface surface) {
			leader.Draw(x, y, surface);
		}
	}
}