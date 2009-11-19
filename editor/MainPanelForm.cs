using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace KFIRPG.editor {
	public partial class MainPanelForm: Form, IDockContent {
		public MainPanelForm() {
			InitializeComponent();
			DockHandler = new DockContentHandler(this);
			DockHandler.DockAreas = DockAreas.Document;
			DockHandler.AllowEndUserDocking = false;
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
