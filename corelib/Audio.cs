using System;
using System.Collections.Generic;
using System.Text;
using SdlDotNet.Audio;

namespace KFIRPG.corelib {
	class Audio {
		MusicCollection mc = new MusicCollection();
		Music nowPlaying;
		Game game;

		public Audio(Game game) {
			this.game = game;
		}

		public void StartMusic(string filename) {
			if (nowPlaying != null) {
				nowPlaying.Dispose();
			}
			nowPlaying = new Music(game.loader.LoadRaw("music/" + filename));
			nowPlaying.Play(true);
		}
	}
}
