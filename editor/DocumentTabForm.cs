﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using System.IO;

namespace KFIRPG.editor {
	partial class DocumentTabForm: Form, IDockContent {
		static DocumentTabForm() {
			string dir = "./"; // Insert the path to your xshd-files.
			FileSyntaxModeProvider fsmProvider; // Provider
			if (Directory.Exists(dir)) {
				fsmProvider = new FileSyntaxModeProvider(dir); // Create new provider with the highlighting directory.
				HighlightingManager.Manager.AddSyntaxModeFileProvider(fsmProvider); // Attach to the text editor.
			}
		}

		TextEditorControl textEditor = new TextEditorControl();

		public Script Script { get; private set; }

		public DocumentTabForm(Script script) {
			if (script == null) throw new ArgumentNullException("script");

			Script = script;
			InitializeComponent();

			DockHandler = new DockContentHandler(this);
			DockHandler.DockAreas = DockAreas.Document;
			DockHandler.AllowEndUserDocking = false;

			Controls.Add(textEditor);

			textEditor.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
			textEditor.Name = "textEditor";
			textEditor.BorderStyle = BorderStyle.Fixed3D;
			textEditor.ShowMatchingBracket = true;
			textEditor.SetHighlighting("lua");
			textEditor.ShowSpaces = true;
			textEditor.ShowTabs = true;
			textEditor.Dock = DockStyle.Fill;
			textEditor.Text = Script.Text;

			script.NameChanged += ScriptNameChanged;
			FormClosed += (sender, args) => {
				script.NameChanged -= ScriptNameChanged;
			};
			Text = Script.ShortName;
		}

		private void ScriptNameChanged(object sender, EventArgs e) {
			Text = Script.ShortName;
		}

		public event EventHandler Saved;
		public void Save() {
			Script.Text = textEditor.Text;
			if (Saved != null) Saved(this, new EventArgs());
		}

		public TextEditorControl TextEditor {
			get { return textEditor; }
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
