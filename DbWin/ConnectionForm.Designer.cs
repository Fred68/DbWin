namespace DbWin
{
	partial class ConnectionForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if(disposing && (components != null))
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectionForm));
			tbSvr = new TextBox();
			tbPrt = new TextBox();
			tbUsr = new TextBox();
			tbPwd = new TextBox();
			tbDb = new TextBox();
			label1 = new Label();
			label2 = new Label();
			label3 = new Label();
			label4 = new Label();
			label5 = new Label();
			button1 = new Button();
			button2 = new Button();
			SuspendLayout();
			// 
			// tbSvr
			// 
			tbSvr.Location = new Point(112,70);
			tbSvr.Name = "tbSvr";
			tbSvr.Size = new Size(144,23);
			tbSvr.TabIndex = 2;
			tbSvr.Visible = false;
			// 
			// tbPrt
			// 
			tbPrt.Location = new Point(112,99);
			tbPrt.Name = "tbPrt";
			tbPrt.Size = new Size(144,23);
			tbPrt.TabIndex = 3;
			tbPrt.Visible = false;
			// 
			// tbUsr
			// 
			tbUsr.Location = new Point(112,12);
			tbUsr.Name = "tbUsr";
			tbUsr.Size = new Size(144,23);
			tbUsr.TabIndex = 0;
			// 
			// tbPwd
			// 
			tbPwd.Location = new Point(112,41);
			tbPwd.Name = "tbPwd";
			tbPwd.Size = new Size(144,23);
			tbPwd.TabIndex = 1;
			// 
			// tbDb
			// 
			tbDb.Location = new Point(112,128);
			tbDb.Name = "tbDb";
			tbDb.Size = new Size(144,23);
			tbDb.TabIndex = 4;
			tbDb.Visible = false;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(17,73);
			label1.Name = "label1";
			label1.Size = new Size(39,15);
			label1.TabIndex = 5;
			label1.Text = "Server";
			label1.Visible = false;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(17,102);
			label2.Name = "label2";
			label2.Size = new Size(29,15);
			label2.TabIndex = 6;
			label2.Text = "Port";
			label2.Visible = false;
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new Point(17,15);
			label3.Name = "label3";
			label3.Size = new Size(30,15);
			label3.TabIndex = 7;
			label3.Text = "User";
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Location = new Point(17,44);
			label4.Name = "label4";
			label4.Size = new Size(30,15);
			label4.TabIndex = 8;
			label4.Text = "Pwd";
			// 
			// label5
			// 
			label5.AutoSize = true;
			label5.Location = new Point(17,131);
			label5.Name = "label5";
			label5.Size = new Size(22,15);
			label5.TabIndex = 9;
			label5.Text = "Db";
			label5.Visible = false;
			// 
			// button1
			// 
			button1.DialogResult = DialogResult.OK;
			button1.Location = new Point(280,12);
			button1.Name = "button1";
			button1.Size = new Size(100,23);
			button1.TabIndex = 10;
			button1.Text = "ok";
			button1.UseVisualStyleBackColor = true;
			button1.Click += button1_Click;
			// 
			// button2
			// 
			button2.DialogResult = DialogResult.Cancel;
			button2.Location = new Point(280,40);
			button2.Name = "button2";
			button2.Size = new Size(100,23);
			button2.TabIndex = 11;
			button2.Text = "cancel";
			button2.UseVisualStyleBackColor = true;
			// 
			// ConnectionForm
			// 
			AutoScaleDimensions = new SizeF(7F,15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(400,163);
			Controls.Add(button2);
			Controls.Add(button1);
			Controls.Add(label5);
			Controls.Add(label4);
			Controls.Add(label3);
			Controls.Add(label2);
			Controls.Add(label1);
			Controls.Add(tbDb);
			Controls.Add(tbPwd);
			Controls.Add(tbUsr);
			Controls.Add(tbPrt);
			Controls.Add(tbSvr);
			Icon = (Icon)resources.GetObject("$this.Icon");
			Name = "ConnectionForm";
			StartPosition = FormStartPosition.CenterScreen;
			Load += ConnectionForm_Load;
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private TextBox tbSvr;
		private TextBox tbPrt;
		private TextBox tbUsr;
		private TextBox tbPwd;
		private TextBox tbDb;
		private Label label1;
		private Label label2;
		private Label label3;
		private Label label4;
		private Label label5;
		private Button button1;
		private Button button2;
	}
}