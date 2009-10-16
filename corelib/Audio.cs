using System;
using System.Collections.Generic;
using System.Text;
using SdlDotNet.Audio;

namespace KFIRPG.corelib {
	/// <summary>
	/// The audio subsystem.
	/// </summary>
	class Audio {
		MusicCollection mc = new MusicCollection();
		Music nowPlaying;
		Game game;
		bool ok = true;

		public Audio(Game game) {
			this.game = game;
		}

		/// <summary>
		/// Plays the selected music. The music must be in the "music" folder.
		/// If a music is currently playing, it is stopped.
		/// </summary>
		/// <param name="filename">The name of the music (with extension).</param>
		public void StartMusic(string filename) {
			if (!ok) return;
			if (nowPlaying != null) {
				nowPlaying.Dispose();
			}
			nowPlaying = new Music(game.loader.LoadRaw("music/" + filename));
			// When trying to play music/sound without a sound card, a DivideByZeroException
			// is thrown. I couldn't find an easier way to test for the sound card.
			try {
				nowPlaying.Play(true);
			}
			catch (DivideByZeroException) {
				ok = false;
			}
		}

		internal void StopMusic() {
			if (nowPlaying != null) nowPlaying.Dispose();
			nowPlaying = null;
		}
	}
}
