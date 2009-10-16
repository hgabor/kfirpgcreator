using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace KFIRPG.editor {
	public class Program {
		[STAThread]
		public static void Main() {
			Application.EnableVisualStyles();
			using (Form form = new EditorForm()) {
				Application.Run(form);
			}
		}
	}
}
