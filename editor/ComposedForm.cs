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
			Size = 2,
			Direction = 4
		}

		#region Name

		Predicate<string> nameChecker;
		TextBox nameTextBox;
		public string GetName() {
			if (nameTextBox == null) throw new InvalidOperationException();
			else return nameTextBox.Text;
		}
		public void SetName(string name) {
			if (nameTextBox == null) throw new InvalidOperationException();
			else nameTextBox.Text = name;
		}
		public void AddNameChecker(Predicate<string> pred) {
			nameChecker += pred;
		}

		#endregion

		#region Size

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

		#endregion

		#region Direction

		ComboBox dirList;
		public string GetDirection() {
			if (dirList == null) throw new InvalidOperationException();
			else return (string)dirList.SelectedItem;
		}
		public void SetDirection(string direction) {
			if (dirList == null) throw new InvalidOperationException();
			else dirList.SelectedItem = direction;
		}

		#endregion

		public ComposedForm(string caption, Parts parts)
			: this(caption, parts, parts) { }

		const int spacing = 10;
		public ComposedForm(string caption, Parts parts, Parts required) {
			InitializeComponent();
			this.Text = caption;
			int startHere = 0;

			Button okButton = new Button();
			int maxWidth = 2 * okButton.Width + 3 * spacing;

			//Add Name textbox
			if ((parts & Parts.Name) == Parts.Name) {
				Label label = new Label();
				label.Text = "Name:";
				label.Top = startHere + spacing;
				label.Left = spacing;
				label.Width = 70;
				label.TextAlign = ContentAlignment.MiddleRight;
				Controls.Add(label);
				nameTextBox = new TextBox();
				nameTextBox.Top = startHere + spacing;
				nameTextBox.Left = label.Right + spacing;

				if ((required & Parts.Name) == Parts.Name) {
					AddNameChecker(s => !string.IsNullOrEmpty(s));
				}

				EventHandler nameCheckerHandler = (sender, args) => {
					if (nameChecker == null) return;
					foreach (Predicate<string> pred in nameChecker.GetInvocationList()) {
						if (!pred(nameTextBox.Text)) {
							okButton.Enabled = false;
							return;
						}
						okButton.Enabled = true;
					}
				};

				nameTextBox.TextChanged += nameCheckerHandler;

				nameCheckerHandler(this, new EventArgs());

				Controls.Add(nameTextBox);
				if ((nameTextBox.Right + spacing) > maxWidth) maxWidth = nameTextBox.Right + spacing;
				startHere = nameTextBox.Bottom + spacing;
			}

			//Add width and height controls
			if ((parts & Parts.Size) == Parts.Size) {
				Label label = new Label();
				label.Text = "Width:";
				label.Top = startHere + spacing;
				label.Left = spacing;
				label.Width = 70;
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
				label.Width = 70;
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

			//Add a direction selector (up, down, left, right)
			if ((parts & Parts.Direction) == Parts.Direction) {
				Label label = new Label();
				label.Text = "Direction:";
				label.Top = startHere + spacing;
				label.Left = spacing;
				label.Width = 70;
				label.TextAlign = ContentAlignment.MiddleRight;
				Controls.Add(label);
				dirList = new ComboBox();
				dirList.Top = startHere + spacing;
				dirList.Left = label.Right + spacing;
				dirList.DropDownStyle = ComboBoxStyle.DropDownList;
				if ((required & Parts.Direction) != Parts.Direction) {
					dirList.Items.Add("");
				}
				dirList.Items.AddRange(new string[] { "down", "up", "left", "right" });
				dirList.SelectedIndex = 0;
				Controls.Add(dirList);
				if ((dirList.Right + spacing) > maxWidth) maxWidth = dirList.Right + spacing;
				startHere = dirList.Bottom + spacing;
			}


			Button cancelButton = new Button();
			cancelButton.Text = "Cancel";
			cancelButton.DialogResult = DialogResult.Cancel;
			cancelButton.Top = startHere + spacing;
			cancelButton.Left = maxWidth - spacing - cancelButton.Width;
			Controls.Add(cancelButton);
			this.CancelButton = cancelButton;

			okButton.Text = "OK";
			okButton.DialogResult = DialogResult.OK;
			okButton.Top = startHere + spacing;
			okButton.Left = cancelButton.Left - spacing - okButton.Width;
			Controls.Add(okButton);
			this.AcceptButton = okButton;

			this.ClientSize = new Size(maxWidth, cancelButton.Bottom + spacing);
		}
	}
}
