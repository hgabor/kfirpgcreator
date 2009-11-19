using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace KFIRPG.editor {
	class DockableForm: Form, IDockContent {

		protected DockableForm() {
			DockHandler = new WeifenLuo.WinFormsUI.Docking.DockContentHandler(this);
			DockHandler.DockAreas =
				DockAreas.DockBottom |
				DockAreas.DockLeft |
				DockAreas.DockRight |
				DockAreas.DockTop |
				DockAreas.Float;
		}

		#region IDockContent Members

		public DockContentHandler DockHandler {
			get;
			private set;
		}

		public new void OnActivated(EventArgs e) {
		}

		public new void OnDeactivate(EventArgs e) {
		}

		#endregion
	}
}
