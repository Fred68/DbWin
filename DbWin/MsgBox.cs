//using System;	
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using System.Windows.Forms.VisualStyles;

#pragma warning disable CS8602  // Dereferenziamento di un riferimento eventualmente Null.                                                            

namespace DbWin
{
	public sealed class MsgBox:Form
	{

		public static readonly float maxScreenSizeRatio = 0.8f;

		private FlowLayoutPanel? flowLayoutPanel1;
		private Button? button1;
		private Button? button2;
		private Button? button3;
		private TextBox? textBox1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer? components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if(disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MsgBox));
			flowLayoutPanel1 = new FlowLayoutPanel();
			button1 = new Button();
			button2 = new Button();
			button3 = new Button();
			textBox1 = new TextBox();
			flowLayoutPanel1.SuspendLayout();
			SuspendLayout();
			// 
			// flowLayoutPanel1
			// 
			flowLayoutPanel1.AutoSize = true;
			flowLayoutPanel1.Controls.Add(button1);
			flowLayoutPanel1.Controls.Add(button2);
			flowLayoutPanel1.Controls.Add(button3);
			flowLayoutPanel1.Dock = DockStyle.Bottom;
			flowLayoutPanel1.FlowDirection = FlowDirection.RightToLeft;
			flowLayoutPanel1.Location = new Point(0,28);
			flowLayoutPanel1.Name = "flowLayoutPanel1";
			flowLayoutPanel1.Size = new Size(280,29);
			flowLayoutPanel1.TabIndex = 0;
			// 
			// button1
			// 
			button1.Location = new Point(202,3);
			button1.Name = "button1";
			button1.Size = new Size(75,23);
			button1.TabIndex = 0;
			button1.Text = "button1";
			button1.UseVisualStyleBackColor = true;
			// 
			// button2
			// 
			button2.Location = new Point(121,3);
			button2.Name = "button2";
			button2.Size = new Size(75,23);
			button2.TabIndex = 1;
			button2.Text = "button2";
			button2.UseVisualStyleBackColor = true;
			// 
			// button3
			// 
			button3.Location = new Point(40,3);
			button3.Name = "button3";
			button3.Size = new Size(75,23);
			button3.TabIndex = 2;
			button3.Text = "button3";
			button3.UseVisualStyleBackColor = true;
			// 
			// textBox1
			// 
			textBox1.BorderStyle = BorderStyle.None;
			textBox1.Dock = DockStyle.Top;
			textBox1.Location = new Point(0,0);
			textBox1.Multiline = true;
			textBox1.Name = "textBox1";
			textBox1.Size = new Size(280,18);
			textBox1.TabIndex = 1;
			textBox1.WordWrap = false;
			// 
			// MsgBox
			// 
			AutoScaleDimensions = new SizeF(7F,15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(280,57);
			Controls.Add(textBox1);
			Controls.Add(flowLayoutPanel1);
			FormBorderStyle = FormBorderStyle.Fixed3D;
			Icon = (Icon)resources.GetObject("$this.Icon");
			MaximizeBox = false;
			MinimizeBox = false;
			MinimumSize = new Size(300,100);
			Name = "MsgBox";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "MsgBox";
			flowLayoutPanel1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private void AdjustSize()
		{
			textBox1.Height = ClientSize.Height - flowLayoutPanel1.Height;
		}

		private MsgBox(string message,string caption,MessageBoxButtons buttons,ScrollBars scrollbars)
		{
			InitializeComponent();

			SuspendLayout();
			switch(buttons)
			{
				case MessageBoxButtons.OK:
					button1.Visible = true;
					button1.DialogResult = DialogResult.OK;
					button2.Visible = false;
					button3.Visible = false;
					break;
				case MessageBoxButtons.OKCancel:
					button1.Visible = true;
					button1.DialogResult = DialogResult.OK;
					button2.Visible = true;
					button2.DialogResult = DialogResult.Cancel;
					button3.Visible = false;
					break;
				case MessageBoxButtons.AbortRetryIgnore:
					button1.Visible = true;
					button1.DialogResult = DialogResult.Abort;
					button2.Visible = true;
					button2.DialogResult = DialogResult.Retry;
					button3.Visible = true;
					button3.DialogResult = DialogResult.Ignore;
					break;
				case MessageBoxButtons.YesNoCancel:
					button1.Visible = true;
					button1.DialogResult = DialogResult.Yes;
					button2.Visible = true;
					button2.DialogResult = DialogResult.No;
					button3.Visible = true;
					button3.DialogResult = DialogResult.Cancel;

					break;
				case MessageBoxButtons.YesNo:
					button1.Visible = true;
					button1.DialogResult = DialogResult.Yes;
					button2.Visible = true;
					button2.DialogResult = DialogResult.No;
					button3.Visible = false;
					break;
				case MessageBoxButtons.RetryCancel:
					button1.Visible = true;
					button1.DialogResult = DialogResult.Retry;
					button2.Visible = true;
					button2.DialogResult = DialogResult.Cancel;
					button3.Visible = false;
					break;
				case MessageBoxButtons.CancelTryContinue:
					button1.Visible = true;
					button1.DialogResult = DialogResult.Cancel;
					button2.Visible = true;
					button2.DialogResult = DialogResult.TryAgain;
					button3.Visible = true;
					button3.DialogResult = DialogResult.Continue;
					break;
			}
#warning Usare lingua IT, EN o altra.
			button1.Text = button1.DialogResult.ToString();
			button2.Text = button2.DialogResult.ToString();
			button3.Text = button3.DialogResult.ToString();


			textBox1.Text = message;
			Text = caption;

			AdjustSize();

			Screen scr = Screen.FromControl(this);
			int xMax = (int)(scr.Bounds.Width * maxScreenSizeRatio);
			int yMax = (int)(scr.Bounds.Height * maxScreenSizeRatio);

			bool scrBx, scrBy;
			scrBx = scrollbars == ScrollBars.Horizontal || scrollbars == ScrollBars.Both;
			scrBy = scrollbars == ScrollBars.Vertical || scrollbars == ScrollBars.Both;

			Size increase = TextRenderer.MeasureText(message,textBox1.Font) - textBox1.Size;
			if(Width + increase.Width > xMax)
			{
				Width = xMax;
				scrBx = true;
			}
			else
			{
				Width += increase.Width;
			}
			if(Height + increase.Height > yMax)
			{
				Height = yMax;
				scrBy = true;
			}
			else
			{
				Height += increase.Height;
			}

			if(scrBx && scrBy)
			{
				textBox1.ScrollBars = ScrollBars.Both;
			}
			else if(scrBx)
			{
				textBox1.ScrollBars = ScrollBars.Horizontal;
			}
			else if(scrBy)
			{
				textBox1.ScrollBars = ScrollBars.Vertical;
			}
			else
			{
				textBox1.ScrollBars = ScrollBars.None;
			}

			AdjustSize();
			ResumeLayout(true);
		}

		public static DialogResult Show(string message,string caption = "",MessageBoxButtons buttons = MessageBoxButtons.OK,ScrollBars scrollbars = ScrollBars.None)
		{
			return new MsgBox(message,caption,buttons,scrollbars).ShowDialog();
		}
	}
}

#pragma warning restore CS8602
