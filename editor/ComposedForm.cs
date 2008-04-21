using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KFIRPG.editor {
	public partial class ComposedForm: Form {

		[Flags]
		public enum Parts: uint {
			None = 0,
			Name = 1,
			Size = 2
		}

		TextBox nameTextBox;
		public string GetName() {
			if (nameTextBox == null) throw new InvalidOperationException();
			else return nameTextBox.Text;
		}
		public void SetName(string name) {
			if (nameTextBox == null) throw new InvalidOperationException();
			else nameTextBox.Text = name;
		}

		NumericUpDown widthNum;
		NumericUpDown heightNum;
		public Size GetSize() {
			if (widthNum == null || heightNum == null) throw new InvalidOperationException();
			else return new Size((int)widthNum.Value, (int)heightNum.Value);
		}
		public void SetSize(Size size) {
			if (widthNum == null || heightNum == null) throw new InvalidOperationException();
			else {
				widthNum.Value = size.Width;
				heightNum.Value = size.Height;
			}
		}

		const int spacing = 10;
		public ComposedForm(string caption, Parts parts) {
			InitializeComponent();
			this.Text = caption;
			int startHere = 0;

			Button okButton = new Button();
			int maxWidth = 2 * okButton.Width + 3 * spacing;

			if ((parts & Parts.Name) == Parts.Name) {
				Label label = new Label();
				label.Text = "Name:";
				label.Top = startHere + spacing;
				label.Left = spacing;
				label.Width = 50;
				label.TextAlign = ContentAlignment.MiddleRight;
				Controls.Add(label);
				nameTextBox = new TextBox();
				nameTextBox.Top = startHere + spacing;
				nameTextBox.Left = label.Right + spacing;
				nameTextBox.Text = "unnamed";
				nameTextBox.TextChanged += (sender, args) => {
					if (string.IsNullOrEmpty(nameTextBox.Text)) okButton.Enabled = false;
					else okButton.Enabled = true;
				};
				Controls.Add(nameTextBox);
				if ((nameTextBox.Right + spacing) > maxWidth) maxWidth = nameTextBox.Right + spacing;
				startHere = nameTextBox.Bottom + spacing;
			}
			if ((parts & Parts.Size) == Parts.Size) {
				Label label = new Label();
				label.Text = "Width:";
				label.Top = startHere + spacing;
				label.Left = spacing;
				label.Width = 50;
				label.TextAlign = ContentAlignment.MiddleRight;
				Controls.Add(label);
				widthNum = new NumericUpDown();
				widthNum.Minimum = 1;
				widthNum.Maximum = 99999;
				widthNum.Value = 1;
				widthNum.Top = startHere + spacing;
				widthNum.Left = label.Right + spacing;
				widthNum.Width = 70;
				Controls.Add(widthNum);
				label = new Label();
				label.Text = "Height:";
				label.Top = startHere + spacing;
				label.Left = widthNum.Right + spacing;
				label.Width = 50;
				label.TextAlign = ContentAlignment.MiddleRight;
				Controls.Add(label);
				heightNum = new NumericUpDown();
				heightNum.Minimum = 1;
				heightNum.Maximum = 99999;
				heightNum.Value = 1;
				heightNum.Top = startHere + spacing;
				heightNum.Left = label.Right + spacing;
				heightNum.Width = 70;
				Controls.Add(heightNum);
				if ((heightNum.Right + spacing) > maxWidth) maxWidth = heightNum.Right + spacing;
				startHere = heightNum.Bottom + spacing;
			}


			okButton.Text = "OK";
			okButton.Click += (sender, args) => {
				this.DialogResult = DialogResult.OK;
			};
			okButton.Top = startHere + spacing;
			okButton.Left = maxWidth - spacing - okButton.Width;
			Controls.Add(okButton);
			this.AcceptButton = okButton;
			Button cancelButton = new Button();
			cancelButton.Text = "Cancel";
			cancelButton.Click += (sender, args) => {
				this.DialogResult = DialogResult.Cancel;
			};
			cancelButton.Top = startHere + spacing;
			cancelButton.Left = okButton.Left - spacing - cancelButton.Width;
			Controls.Add(cancelButton);
			this.CancelButton = cancelButton;

			this.ClientSize = new Size(maxWidth, cancelButton.Bottom + spacing);
		}
	}
}
