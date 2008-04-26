using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.corelib {
	[AttributeUsage(AttributeTargets.Method)]
	class ScriptAttribute: Attribute { }
	[AttributeUsage(AttributeTargets.Method)]
	class BlockingScriptAttribute: Attribute { }

	class ScriptLib {
		Dialogs dialogs;
		Game game;
		Random random = new Random();

		[BlockingScript]
		public void Message(string message) {
			//dialogs.Message(message);
			FadeAnimation anim = new FadeAnimation(game);
			anim.FromImage = game.TakeScreenshot();
			Form form = new Form(dialogs, game);
			Form.Menu menu = new Form.Menu(10, 10, 500, 300, form);
			TextGraphics text = new TextGraphics(message, 10, 10, TextGraphics.Align.Center, dialogs, game);
			menu.Add(text);
			form.AddPanel("message", menu);
			game.PushScreen(form);
			game.PushScreen(anim);
		}

		[BlockingScript]
		public void Ask(string question, string param1, string param2) {
			Ask(question, new string[] { param1, param2 });
		}

		public void Ask(string question, params string[] answers) {
			FadeAnimation anim = new FadeAnimation(game);
			anim.FromImage = game.TakeScreenshot();
			Form form = new Form(dialogs, game);
			Form.Menu qmenu = new Form.Menu(10, 10, 500, 300, form);
			TextGraphics qtext = new TextGraphics(question, 10, 10, TextGraphics.Align.Center, dialogs, game);
			qmenu.Add(qtext);
			form.AddPanel("message", qmenu);

			int i = 0;
			foreach (string answer in answers) {
				Form.Item aitem = new Form.Item(10, 220 + 20 * i, 500, 20, form);
				TextGraphics stext = new TextGraphics(answer, 0, 0, TextGraphics.Align.Left, dialogs, game);
				aitem.Add(stext);
				++i;
				int j = i;
				aitem.Selected += (sender, args) => {
					form.ReturnValue = j;
				};
				qmenu.AddMenuItem(aitem);
			}
			game.PushScreen(form);
			game.PushScreen(anim);
		}

		[Script]
		public void StartMusic(string fileName) {
			game.audio.StartMusic(fileName);
		}

		[Script]
		public void Turn(Sprite her, Sprite to) {
			int x = to.X - her.X;
			int y = to.Y - her.Y;
			if (x + y > 0) {
				if (y - x > 0) her.Turn(Sprite.Dir.Down);
				else her.Turn(Sprite.Dir.Right);
			}
			else {
				if (y - x > 0) her.Turn(Sprite.Dir.Left);
				else her.Turn(Sprite.Dir.Up);
			}
		}

		[Script]
		public void WriteLine(object text) {
			Console.WriteLine(text.ToString());
		}

		public void ShortJump(int x, int y, int layer) {
			FadeAnimation anim = new FadeAnimation(game);
			anim.FromImage = game.TakeScreenshot();
			Sprite leader = game.Party.Leader;
			game.currentMap.Move(leader, leader.X, leader.Y, leader.Layer, x, y, layer);
			game.currentMap.OnStep(x, y, layer);
			game.PushScreen(anim);
		}

		public void LongJump(Location loc) {
			FadeAnimation anim = new FadeAnimation(game);
			anim.FromImage = game.TakeScreenshot();
			Sprite leader = game.Party.Leader;
			game.currentMap.Remove(leader, leader.X, leader.Y, leader.Layer);
			game.currentMap = new Map(loc.MapName, game);
			game.currentMap.Place(leader, loc.X, loc.Y, loc.Layer);
			game.PushScreen(anim);
		}

		[Script]
		public void MoveTo(string locationStr) {
			Location location = game.GetLocation(locationStr);
			if (location.MapName == game.currentMap.Name) {
				ShortJump(location.X, location.Y, location.Layer);
			}
			else {
				LongJump(location);
			}
		}

		public ScriptLib(Game game) {
			this.game = game;
			dialogs = new Dialogs(game);
		}
	}
}
