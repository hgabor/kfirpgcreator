using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace KFIRPG.editor {
	partial class AudioLibrary: DockableForm {
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

		BindingList<KeyValuePair<string, byte[]>> musicList;
		internal new void Load(Project project) {
			musicList = new BindingList<KeyValuePair<string, byte[]>>(new ListDictionaryAdapter<string, byte[]>(project.musics));
			this.list.DisplayMember = "Key";
			this.list.DataSource = musicList;
		}
	}
}
