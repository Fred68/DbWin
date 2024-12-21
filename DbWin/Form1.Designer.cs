namespace DbWin
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			menuStrip1 = new MenuStrip();
			fileToolStripMenuItem = new ToolStripMenuItem();
			connettiToolStripMenuItem = new ToolStripMenuItem();
			disconnettiToolStripMenuItem = new ToolStripMenuItem();
			esciToolStripMenuItem = new ToolStripMenuItem();
			parametriDiConnessioneToolStripMenuItem = new ToolStripMenuItem();
			helpToolStripMenuItem = new ToolStripMenuItem();
			informazioniToolStripMenuItem = new ToolStripMenuItem();
			statusStrip1 = new StatusStrip();
			menuStrip1.SuspendLayout();
			SuspendLayout();
			// 
			// menuStrip1
			// 
			menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem,helpToolStripMenuItem });
			menuStrip1.Location = new Point(0,0);
			menuStrip1.Name = "menuStrip1";
			menuStrip1.Size = new Size(800,24);
			menuStrip1.TabIndex = 0;
			menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { parametriDiConnessioneToolStripMenuItem,connettiToolStripMenuItem,disconnettiToolStripMenuItem,esciToolStripMenuItem });
			fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			fileToolStripMenuItem.Size = new Size(87,20);
			fileToolStripMenuItem.Text = "Connessione";
			// 
			// connettiToolStripMenuItem
			// 
			connettiToolStripMenuItem.Name = "connettiToolStripMenuItem";
			connettiToolStripMenuItem.Size = new Size(180,22);
			connettiToolStripMenuItem.Text = "Connetti";
			connettiToolStripMenuItem.Click += connettiToolStripMenuItem_Click;
			// 
			// disconnettiToolStripMenuItem
			// 
			disconnettiToolStripMenuItem.Name = "disconnettiToolStripMenuItem";
			disconnettiToolStripMenuItem.Size = new Size(180,22);
			disconnettiToolStripMenuItem.Text = "Disconnetti";
			disconnettiToolStripMenuItem.Click += disconnettiToolStripMenuItem_Click;
			// 
			// esciToolStripMenuItem
			// 
			esciToolStripMenuItem.Name = "esciToolStripMenuItem";
			esciToolStripMenuItem.Size = new Size(180,22);
			esciToolStripMenuItem.Text = "Esci";
			esciToolStripMenuItem.Click += esciToolStripMenuItem_Click;
			// 
			// parametriDiConnessioneToolStripMenuItem
			// 
			parametriDiConnessioneToolStripMenuItem.Name = "parametriDiConnessioneToolStripMenuItem";
			parametriDiConnessioneToolStripMenuItem.Size = new Size(180,22);
			parametriDiConnessioneToolStripMenuItem.Text = "Parametri";
			parametriDiConnessioneToolStripMenuItem.Click += parametriDiConnessioneToolStripMenuItem_Click;
			// 
			// helpToolStripMenuItem
			// 
			helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { informazioniToolStripMenuItem });
			helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			helpToolStripMenuItem.Size = new Size(24,20);
			helpToolStripMenuItem.Text = "?";
			// 
			// informazioniToolStripMenuItem
			// 
			informazioniToolStripMenuItem.Name = "informazioniToolStripMenuItem";
			informazioniToolStripMenuItem.Size = new Size(141,22);
			informazioniToolStripMenuItem.Text = "Informazioni";
			informazioniToolStripMenuItem.Click += informazioniToolStripMenuItem_Click;
			// 
			// statusStrip1
			// 
			statusStrip1.Location = new Point(0,428);
			statusStrip1.Name = "statusStrip1";
			statusStrip1.Size = new Size(800,22);
			statusStrip1.TabIndex = 1;
			statusStrip1.Text = "statusStrip1";
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(7F,15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800,450);
			Controls.Add(statusStrip1);
			Controls.Add(menuStrip1);
			MainMenuStrip = menuStrip1;
			Name = "Form1";
			Text = "Form1";
			FormClosing += Form1_FormClosing;
			Load += Form1_Load;
			menuStrip1.ResumeLayout(false);
			menuStrip1.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private MenuStrip menuStrip1;
		private ToolStripMenuItem fileToolStripMenuItem;
		private ToolStripMenuItem helpToolStripMenuItem;
		private StatusStrip statusStrip1;
		private ToolStripMenuItem connettiToolStripMenuItem;
		private ToolStripMenuItem disconnettiToolStripMenuItem;
		private ToolStripMenuItem esciToolStripMenuItem;
		private ToolStripMenuItem informazioniToolStripMenuItem;
		private ToolStripMenuItem parametriDiConnessioneToolStripMenuItem;
	}
}
