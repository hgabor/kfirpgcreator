using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KFIRPG.editor {
	public partial class AudioLibrary: Form {
		class Audio {
			string outerLocation = null;
			public string OuterLocation {
				get { return outerLocation; }
				set {
					outerLocation = value;
					if (outerLocation != null) {
						innerName = System.IO.Path.GetFileName(outerLocation);
					}
				}
			}
			string innerName = null;
			public string InnerName {
				get { return innerName; }
				set { innerName = value; }
			}

			public override string ToString() {
				if (outerLocation == null && innerName == null) return "::Invalid::";
				else if (outerLocation == null) return innerName;
				else return string.Format("{0} ({1})", innerName, outerLocation);
			}
		}

		public AudioLibrary() {
			InitializeComponent();
		}

		internal new void Load(Project project) {
			this.list.Items.Clear();
			string[] list = project.loader.LoadText("music.list").Split('\n');
			foreach (string itemName in list) {
				if (itemName.Trim() == "") continue;
				Audio item = new Audio();
				item.InnerName = itemName.Trim();
				this.list.Items.Add(item);
			}
		}
	}
}
