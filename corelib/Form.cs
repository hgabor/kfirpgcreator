using System;
using System.Collections.Generic;
using System.Text;
using SdlDotNet.Graphics;

namespace KFIRPG.corelib {
	class Form: Screen {
		public abstract class Panel {
			int x;
			int y;
			protected int width;
			protected int height;
			protected Form form;
			public SdlDotNet.Graphics.Primitives.Box box;
			protected Panel(int x, int y, int width, int height, Form form) {
				this.x = x;
				this.y = y;
				this.width = width;
				this.height = height;
				this.form = form;
				box = new SdlDotNet.Graphics.Primitives.Box(new System.Drawing.Point(x, y), new System.Drawing.Size(width - 1, height - 1));
			}

			internal abstract void Think();

			protected abstract void DrawInternal(int x, int y, Surface surface);
			protected virtual void DrawBackground(int x, int y, Surface surface) {
				form.dialogs.DrawWindow(box, surface);
			}

			List<Graphics> contents = new List<Graphics>();
			public void Add(Graphics graphics) {
				contents.Add(graphics);
			}

			//TODO: Adjust panel accoring to the x/y params
			public void Draw(int x, int y, Surface surface) {
				//surface.Draw(box, form.dialogs.bgColor, false, true);
				DrawBackground(x, y, surface);
				foreach (Graphics gfx in contents) {
					gfx.Blit(x + this.x, y + this.y, surface);
				}
				DrawInternal(x + this.x, y + this.y, surface);
			}
		}
		public class Menu: Panel {
			List<Item> menuItems = new List<Item>();
			Item activeMenuItem = null;

			public Menu(int x, int y, int width, int height, Form form)
				: base(x, y, width, height, form) {
			}

			public void AddMenuItem(Item menuItem) {
				menuItems.Add(menuItem);
				if (activeMenuItem == null) activeMenuItem = menuItem;
			}

			private void ButtonPress_Handler(object sender, UserInput.ButtonEventArgs args) {
				if (activeMenuItem == null) return;
				if (form.game.Input.IsPressed(UserInput.Buttons.Up)) {
					int menuId = menuItems.IndexOf(activeMenuItem);
					if (menuId == 0) activeMenuItem = menuItems[menuItems.Count - 1];
					else activeMenuItem = menuItems[menuId - 1];
				}
				else if (form.game.Input.IsPressed(UserInput.Buttons.Down)) {
					int menuId = menuItems.IndexOf(activeMenuItem);
					if (menuId == menuItems.Count - 1) activeMenuItem = menuItems[0];
					else activeMenuItem = menuItems[menuId + 1];
				}
			}

			public void UnRegister() {
				form.game.Input.ButtonStateChanged -= ButtonPress_Handler;
			}
			public void Register() {
				form.game.Input.ButtonStateChanged += ButtonPress_Handler;
			}

			internal override void Think() {
				if (activeMenuItem == null) {
					if (form.game.Input.IsPressed(UserInput.Buttons.Action) || form.game.Input.IsPressed(UserInput.Buttons.Back)) {
						form.DeactivatePanel();
					}
				}
				else {
					//TODO: different menu layouts
					if (form.game.Input.IsPressed(UserInput.Buttons.Action)) {
						activeMenuItem.OnSelected();
					}
					else if (form.game.Input.IsPressed(UserInput.Buttons.Back)) {
						form.DeactivatePanel();
					}
				}
			}

			protected override void DrawInternal(int x, int y, Surface surface) {
				foreach (Item item in menuItems) {
					if (activeMenuItem == item) {
						surface.Draw(activeMenuItem.box, form.dialogs.selectedBg, false, true);
						surface.Draw(activeMenuItem.box, form.dialogs.selectedBorder);
					}
					item.Draw(x, y, surface);
				}
			}
		}
		public class Item: Panel {
			public event EventHandler Selected;

			public Item(int x, int y, int width, int height, Form form)
				: base(x, y, width, height, form) { }

			internal void OnSelected() {
				Selected(this, new EventArgs());
			}

			internal override void Think() { }

			protected override void DrawInternal(int x, int y, Surface surface) { }

			protected override void DrawBackground(int x, int y, Surface surface) { }
		}

		Dialogs dialogs;
		Game game;
		public Form(Dialogs dialogs, Game game) {
			this.dialogs = dialogs;
			this.game = game;
		}

		Dictionary<string, Menu> panels = new Dictionary<string, Menu>();
		List<Menu> activePanels = new List<Menu>();
		Menu activePanel = null;
		public void AddPanel(string name, Menu panel) {
			panels.Add(name, panel);
			MakePanelActive(name);
		}

		public void MakePanelActive(string name) {
			Menu panel = panels[name];
			if (activePanel != null) activePanel.UnRegister();
			if (activePanels.Contains(panel)) {
				int id = activePanels.IndexOf(panel);
				activePanels.RemoveRange(id + 1, activePanels.Count - id);
			}
			else {
				activePanels.Add(panel);
			}
			activePanel = panel;
			activePanel.Register();
		}

		public int ReturnValue {
			set {
				FadeAnimation animation = new FadeAnimation(game);
				animation.FromImage = game.TakeScreenshot();
				game.PopScreen();
				game.vm.ContinueWithValue(value);
				game.PushScreen(animation);
			}
		}

		void DeactivatePanel() {
			activePanel.UnRegister();
			activePanels.Remove(activePanel);
			if (activePanels.Count == 0) {
				//Self destruct
				FadeAnimation animation = new FadeAnimation(game);
				animation.FromImage = game.TakeScreenshot();
				game.PopScreen();
				game.vm.ContinueWithValue(null);
				game.PushScreen(animation);
			}
			else {
				activePanel = activePanels[activePanels.Count - 1];
				activePanel.Register();
			}
		}


		public override void Draw(SdlDotNet.Graphics.Surface surface) {
			foreach (Panel panel in panels.Values) {
				panel.Draw(0, 0, surface);
			}
		}

		public override void Think() {
			activePanel.Think();
		}
	}
}
