using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.editor {
	class Script: IComparable<Script> {
		public event EventHandler NameChanged;
		public bool IsFolder { get; set; }

		private string name;
		public string Name {
			get {
				return name;
			}
			set {
				name = value;
				string[] nameParts = name.Split('/');
				shortName = nameParts[nameParts.Length - 1];

				if (NameChanged != null) NameChanged(this, new EventArgs());
			}
		}
		string shortName;
		public string ShortName {
			get {
				return shortName;
			}
			set {
				if (value == null) throw new ArgumentNullException();
				if (value.Trim() == "") throw new ArgumentException("Short name cannot be empty!");
				if (value.Contains("/")) throw new ArgumentException("Short name cannot contain / characters!");
				string[] nameParts = name.Split('/');
				nameParts[nameParts.Length - 1] = value;
				Name = string.Join("/", nameParts);
			}
		}
		public string Text {
			get;
			set;
		}
		

		public Script(string name, string text) {
			this.Name = name;
			this.Text = text;
			this.IsFolder = false;
		}

		public Script(string name) {
			this.Name = name.TrimEnd('/');
			this.Text = null;
			this.IsFolder = true;
		}

		public Script(Script script) {
			this.Name = script.Name;
			this.Text = script.Text;
			this.IsFolder = script.IsFolder;
		}

		#region IComparable<Script> Members

		public int CompareTo(Script other) {
			if (this.IsFolder && !other.IsFolder) return -1;
			if (!this.IsFolder && other.IsFolder) return 1;
			return this.ShortName.CompareTo(other.ShortName);
		}

		#endregion
	}
}
