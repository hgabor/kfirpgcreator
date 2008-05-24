using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KFIRPG.editor {
	partial class AnimationSelector: Form {
		Animation.Frame[] selectedFrames;
		public Animation.Frame[] SelectedFrames {
			get { return selectedFrames; }
		}

		public AnimationSelector(Animation.Group group, SpriteSheet sheet, Project project){
			InitializeComponent();
			spriteSelector.Image = sheet.sheet;
			spriteSelector.SpriteWidth = sheet.spriteWidth;
			spriteSelector.SpriteHeight = sheet.spriteHeight;
			selectedFrames = group.frames.ToArray();

			//Must be set last because their events are already hooked up
			maxFramesNumUpDown.Value = group.frames.Count;
			spriteSelector.SelectedIndex = selectedFrames[0].sheetId;
			timeoutNumericUpDown.Value = selectedFrames[0].time;
		}

		public AnimationSelector(SpriteSheet sheet, Project project) {
			InitializeComponent();
			spriteSelector.Image = sheet.sheet;
			spriteSelector.SpriteWidth = sheet.spriteWidth;
			spriteSelector.SpriteHeight = sheet.spriteHeight;
			selectedFrames = new Animation.Frame[1];
			selectedFrames[0] = new Animation.Frame(0, 1);
		}

		private void maxFramesNumUpDown_ValueChanged(object sender, EventArgs e) {
			currentFrameNumUpDown.Maximum = maxFramesNumUpDown.Value;
			Animation.Frame[] newFrames = new Animation.Frame[(int)maxFramesNumUpDown.Value];
			Array.Copy(selectedFrames, newFrames, (int)Math.Min(selectedFrames.Length, newFrames.Length));
			Animation.Frame frame = selectedFrames[selectedFrames.Length - 1];
			for (int i = selectedFrames.Length; i < newFrames.Length; ++i) {
				newFrames[i] = new Animation.Frame(frame.sheetId, frame.time);
			}
			selectedFrames = newFrames;
		}

		bool updateSelection = true;

		private void currentFrameNumUpDown_ValueChanged(object sender, EventArgs e) {
			updateSelection = false;
			spriteSelector.SelectedIndex = selectedFrames[(int)currentFrameNumUpDown.Value - 1].sheetId;
			timeoutNumericUpDown.Value = selectedFrames[(int)currentFrameNumUpDown.Value - 1].time;
			updateSelection = true;
		}

		private void spriteSelector_SelectedIndexChanged(object sender, EventArgs e) {
			if (updateSelection) {
				selectedFrames[(int)currentFrameNumUpDown.Value - 1].sheetId = spriteSelector.SelectedIndex;
			}
		}

		private void timeoutNumericUpDown_ValueChanged(object sender, EventArgs e) {
			selectedFrames[(int)currentFrameNumUpDown.Value - 1].time = (int)timeoutNumericUpDown.Value;
		}

	}
}