using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.editor {
	class Script {
		private string name;
		public string Name {
			get {
				return name;
			}
			set {
				name = value;
				string[] nameParts = name.Split('/');
				ShortName = nameParts[nameParts.Length - 1];
			}
		}
		public string ShortName {
			get; private set;
		}
		public string Text {
			get;
			set;
		}
		

		public Script(string name, string text) {
			this.Name = name;
			this.Text = text;
		}
	}
}
