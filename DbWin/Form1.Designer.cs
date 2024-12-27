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
			components = new System.ComponentModel.Container();
			menuStrip1 = new MenuStrip();
			fileToolStripMenuItem = new ToolStripMenuItem();
			parametriDiConnessioneToolStripMenuItem = new ToolStripMenuItem();
			connettiToolStripMenuItem = new ToolStripMenuItem();
			disconnettiToolStripMenuItem = new ToolStripMenuItem();
			statoToolStripMenuItem = new ToolStripMenuItem();
			statoToolStripMenuItem1 = new ToolStripMenuItem();
			connectionStringToolStripMenuItem = new ToolStripMenuItem();
			schemaToolStripMenuItem = new ToolStripMenuItem();
			proceduresToolStripMenuItem = new ToolStripMenuItem();
			functionsToolStripMenuItem = new ToolStripMenuItem();
			utenteToolStripMenuItem = new ToolStripMenuItem();
			esciToolStripMenuItem = new ToolStripMenuItem();
			interrogaToolStripMenuItem = new ToolStripMenuItem();
			vediCodiciToolStripMenuItem1 = new ToolStripMenuItem();
			vediCodiciToolStripMenuItem2 = new ToolStripMenuItem();
			helpToolStripMenuItem = new ToolStripMenuItem();
			informazioniToolStripMenuItem = new ToolStripMenuItem();
			dettagliToolStripMenuItem = new ToolStripMenuItem();
			TESTtsmi = new ToolStripMenuItem();
			mSGBOXToolStripMenuItem = new ToolStripMenuItem();
			asyncTestToolStripMenuItem = new ToolStripMenuItem();
			vediCodiciToolStripMenuItem = new ToolStripMenuItem();
			dataTableToolStripMenuItem = new ToolStripMenuItem();
			activeTsMenuItem = new ToolStripMenuItem();
			busyTsMenuItem = new ToolStripMenuItem();
			statusStrip1 = new StatusStrip();
			tsStatus = new ToolStripStatusLabel();
			toolStripStatusLabel2 = new ToolStripStatusLabel();
			toolStripStatusLabel3 = new ToolStripStatusLabel();
			refreshTimer = new System.Windows.Forms.Timer(components);
			menuStrip1.SuspendLayout();
			statusStrip1.SuspendLayout();
			SuspendLayout();
			// 
			// menuStrip1
			// 
			menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem,interrogaToolStripMenuItem,helpToolStripMenuItem,TESTtsmi,activeTsMenuItem,busyTsMenuItem });
			menuStrip1.Location = new Point(0,0);
			menuStrip1.Name = "menuStrip1";
			menuStrip1.Size = new Size(800,24);
			menuStrip1.TabIndex = 0;
			menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { parametriDiConnessioneToolStripMenuItem,connettiToolStripMenuItem,disconnettiToolStripMenuItem,statoToolStripMenuItem,esciToolStripMenuItem });
			fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			fileToolStripMenuItem.Size = new Size(87,20);
			fileToolStripMenuItem.Text = "Connessione";
			// 
			// parametriDiConnessioneToolStripMenuItem
			// 
			parametriDiConnessioneToolStripMenuItem.Name = "parametriDiConnessioneToolStripMenuItem";
			parametriDiConnessioneToolStripMenuItem.Size = new Size(134,22);
			parametriDiConnessioneToolStripMenuItem.Text = "Parametri";
			parametriDiConnessioneToolStripMenuItem.Click += parametriDiConnessioneToolStripMenuItem_Click;
			// 
			// connettiToolStripMenuItem
			// 
			connettiToolStripMenuItem.Name = "connettiToolStripMenuItem";
			connettiToolStripMenuItem.Size = new Size(134,22);
			connettiToolStripMenuItem.Text = "Connetti";
			connettiToolStripMenuItem.Click += connettiToolStripMenuItem_Click;
			// 
			// disconnettiToolStripMenuItem
			// 
			disconnettiToolStripMenuItem.Name = "disconnettiToolStripMenuItem";
			disconnettiToolStripMenuItem.Size = new Size(134,22);
			disconnettiToolStripMenuItem.Text = "Disconnetti";
			disconnettiToolStripMenuItem.Click += disconnettiToolStripMenuItem_Click;
			// 
			// statoToolStripMenuItem
			// 
			statoToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { statoToolStripMenuItem1,connectionStringToolStripMenuItem,schemaToolStripMenuItem,proceduresToolStripMenuItem,functionsToolStripMenuItem,utenteToolStripMenuItem });
			statoToolStripMenuItem.Name = "statoToolStripMenuItem";
			statoToolStripMenuItem.Size = new Size(134,22);
			statoToolStripMenuItem.Text = "Stato";
			// 
			// statoToolStripMenuItem1
			// 
			statoToolStripMenuItem1.Name = "statoToolStripMenuItem1";
			statoToolStripMenuItem1.Size = new Size(193,22);
			statoToolStripMenuItem1.Text = "Stato";
			statoToolStripMenuItem1.Click += statoToolStripMenuItem1_Click;
			// 
			// connectionStringToolStripMenuItem
			// 
			connectionStringToolStripMenuItem.Name = "connectionStringToolStripMenuItem";
			connectionStringToolStripMenuItem.Size = new Size(193,22);
			connectionStringToolStripMenuItem.Text = "Stringa di connessione";
			connectionStringToolStripMenuItem.Click += connectionStringToolStripMenuItem_Click;
			// 
			// schemaToolStripMenuItem
			// 
			schemaToolStripMenuItem.Name = "schemaToolStripMenuItem";
			schemaToolStripMenuItem.Size = new Size(193,22);
			schemaToolStripMenuItem.Text = "Schema";
			schemaToolStripMenuItem.Click += schemaToolStripMenuItem_Click;
			// 
			// proceduresToolStripMenuItem
			// 
			proceduresToolStripMenuItem.Name = "proceduresToolStripMenuItem";
			proceduresToolStripMenuItem.Size = new Size(193,22);
			proceduresToolStripMenuItem.Text = "Procedure";
			proceduresToolStripMenuItem.Click += proceduresToolStripMenuItem_Click;
			// 
			// functionsToolStripMenuItem
			// 
			functionsToolStripMenuItem.Name = "functionsToolStripMenuItem";
			functionsToolStripMenuItem.Size = new Size(193,22);
			functionsToolStripMenuItem.Text = "Funzioni";
			functionsToolStripMenuItem.Click += functionsToolStripMenuItem_Click;
			// 
			// utenteToolStripMenuItem
			// 
			utenteToolStripMenuItem.Name = "utenteToolStripMenuItem";
			utenteToolStripMenuItem.Size = new Size(193,22);
			utenteToolStripMenuItem.Text = "Usr";
			utenteToolStripMenuItem.Click += utenteToolStripMenuItem_Click;
			// 
			// esciToolStripMenuItem
			// 
			esciToolStripMenuItem.Name = "esciToolStripMenuItem";
			esciToolStripMenuItem.Size = new Size(134,22);
			esciToolStripMenuItem.Text = "Esci";
			esciToolStripMenuItem.Click += esciToolStripMenuItem_Click;
			// 
			// interrogaToolStripMenuItem
			// 
			interrogaToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { vediCodiciToolStripMenuItem1,vediCodiciToolStripMenuItem2 });
			interrogaToolStripMenuItem.Name = "interrogaToolStripMenuItem";
			interrogaToolStripMenuItem.Size = new Size(67,20);
			interrogaToolStripMenuItem.Text = "Interroga";
			// 
			// vediCodiciToolStripMenuItem1
			// 
			vediCodiciToolStripMenuItem1.Name = "vediCodiciToolStripMenuItem1";
			vediCodiciToolStripMenuItem1.Size = new Size(180,22);
			vediCodiciToolStripMenuItem1.Text = "Vedi codici";
			vediCodiciToolStripMenuItem1.Click += vediCodiciToolStripMenuItem1_Click;
			// 
			// vediCodiciToolStripMenuItem2
			// 
			vediCodiciToolStripMenuItem2.Name = "vediCodiciToolStripMenuItem2";
			vediCodiciToolStripMenuItem2.Size = new Size(180,22);
			vediCodiciToolStripMenuItem2.Text = "Vedi codici";
			vediCodiciToolStripMenuItem2.Click += vediCodiciToolStripMenuItem2_Click;
			// 
			// helpToolStripMenuItem
			// 
			helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { informazioniToolStripMenuItem,dettagliToolStripMenuItem });
			helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			helpToolStripMenuItem.Size = new Size(24,20);
			helpToolStripMenuItem.Text = "?";
			// 
			// informazioniToolStripMenuItem
			// 
			informazioniToolStripMenuItem.Name = "informazioniToolStripMenuItem";
			informazioniToolStripMenuItem.Size = new Size(115,22);
			informazioniToolStripMenuItem.Text = "Info";
			informazioniToolStripMenuItem.Click += informazioniToolStripMenuItem_Click;
			// 
			// dettagliToolStripMenuItem
			// 
			dettagliToolStripMenuItem.Name = "dettagliToolStripMenuItem";
			dettagliToolStripMenuItem.Size = new Size(115,22);
			dettagliToolStripMenuItem.Text = "Dettagli";
			dettagliToolStripMenuItem.Click += dettagliToolStripMenuItem_Click;
			// 
			// TESTtsmi
			// 
			TESTtsmi.DropDownItems.AddRange(new ToolStripItem[] { mSGBOXToolStripMenuItem,asyncTestToolStripMenuItem,vediCodiciToolStripMenuItem,dataTableToolStripMenuItem });
			TESTtsmi.Name = "TESTtsmi";
			TESTtsmi.Size = new Size(45,20);
			TESTtsmi.Text = "TEST";
			// 
			// mSGBOXToolStripMenuItem
			// 
			mSGBOXToolStripMenuItem.Name = "mSGBOXToolStripMenuItem";
			mSGBOXToolStripMenuItem.Size = new Size(131,22);
			mSGBOXToolStripMenuItem.Text = "MsgBox";
			mSGBOXToolStripMenuItem.Visible = false;
			mSGBOXToolStripMenuItem.Click += MsgBoxToolStripMenuItem_Click;
			// 
			// asyncTestToolStripMenuItem
			// 
			asyncTestToolStripMenuItem.Name = "asyncTestToolStripMenuItem";
			asyncTestToolStripMenuItem.Size = new Size(131,22);
			asyncTestToolStripMenuItem.Text = "async test";
			asyncTestToolStripMenuItem.Visible = false;
			asyncTestToolStripMenuItem.Click += asyncTestToolStripMenuItem_Click;
			// 
			// vediCodiciToolStripMenuItem
			// 
			vediCodiciToolStripMenuItem.Name = "vediCodiciToolStripMenuItem";
			vediCodiciToolStripMenuItem.Size = new Size(131,22);
			vediCodiciToolStripMenuItem.Text = "Vedi codici";
			vediCodiciToolStripMenuItem.Click += vediCodiciToolStripMenuItem_Click;
			// 
			// dataTableToolStripMenuItem
			// 
			dataTableToolStripMenuItem.Name = "dataTableToolStripMenuItem";
			dataTableToolStripMenuItem.Size = new Size(131,22);
			dataTableToolStripMenuItem.Text = "DataTable";
			dataTableToolStripMenuItem.Click += dataTableToolStripMenuItem_Click;
			// 
			// activeTsMenuItem
			// 
			activeTsMenuItem.Alignment = ToolStripItemAlignment.Right;
			activeTsMenuItem.Font = new Font("Courier New",9F,FontStyle.Regular,GraphicsUnit.Point,0);
			activeTsMenuItem.Name = "activeTsMenuItem";
			activeTsMenuItem.Size = new Size(26,20);
			activeTsMenuItem.Text = "A";
			// 
			// busyTsMenuItem
			// 
			busyTsMenuItem.Alignment = ToolStripItemAlignment.Right;
			busyTsMenuItem.Font = new Font("Courier New",9F,FontStyle.Regular,GraphicsUnit.Point,0);
			busyTsMenuItem.Name = "busyTsMenuItem";
			busyTsMenuItem.Size = new Size(26,20);
			busyTsMenuItem.Text = "B";
			// 
			// statusStrip1
			// 
			statusStrip1.Items.AddRange(new ToolStripItem[] { tsStatus,toolStripStatusLabel2,toolStripStatusLabel3 });
			statusStrip1.Location = new Point(0,428);
			statusStrip1.Name = "statusStrip1";
			statusStrip1.Size = new Size(800,22);
			statusStrip1.TabIndex = 1;
			statusStrip1.Text = "statusStrip1";
			// 
			// tsStatus
			// 
			tsStatus.Name = "tsStatus";
			tsStatus.Size = new Size(36,17);
			tsStatus.Text = "tsStat";
			tsStatus.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// toolStripStatusLabel2
			// 
			toolStripStatusLabel2.Name = "toolStripStatusLabel2";
			toolStripStatusLabel2.Size = new Size(118,17);
			toolStripStatusLabel2.Text = "toolStripStatusLabel2";
			toolStripStatusLabel2.Visible = false;
			// 
			// toolStripStatusLabel3
			// 
			toolStripStatusLabel3.Name = "toolStripStatusLabel3";
			toolStripStatusLabel3.RightToLeft = RightToLeft.No;
			toolStripStatusLabel3.Size = new Size(118,17);
			toolStripStatusLabel3.Text = "toolStripStatusLabel3";
			toolStripStatusLabel3.Visible = false;
			// 
			// refreshTimer
			// 
			refreshTimer.Tick += refreshTimer_Tick;
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
			Text = "DesignToolsServer";
			FormClosing += Form1_FormClosing;
			Load += Form1_Load;
			menuStrip1.ResumeLayout(false);
			menuStrip1.PerformLayout();
			statusStrip1.ResumeLayout(false);
			statusStrip1.PerformLayout();
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
		private ToolStripStatusLabel tsStatus;
		private ToolStripStatusLabel toolStripStatusLabel2;
		private ToolStripStatusLabel toolStripStatusLabel3;
		private ToolStripMenuItem statoToolStripMenuItem;
		private ToolStripMenuItem statoToolStripMenuItem1;
		private ToolStripMenuItem connectionStringToolStripMenuItem;
		private ToolStripMenuItem schemaToolStripMenuItem;
		private ToolStripMenuItem proceduresToolStripMenuItem;
		private ToolStripMenuItem functionsToolStripMenuItem;
		private ToolStripMenuItem TESTtsmi;
		private ToolStripMenuItem mSGBOXToolStripMenuItem;
		private ToolStripMenuItem dettagliToolStripMenuItem;
		private ToolStripMenuItem utenteToolStripMenuItem;
		private ToolStripMenuItem asyncTestToolStripMenuItem;
		private ToolStripMenuItem activeTsMenuItem;
		private System.Windows.Forms.Timer refreshTimer;
		private ToolStripMenuItem busyTsMenuItem;
		private ToolStripMenuItem vediCodiciToolStripMenuItem;
		private ToolStripMenuItem dataTableToolStripMenuItem;
		private ToolStripMenuItem interrogaToolStripMenuItem;
		private ToolStripMenuItem vediCodiciToolStripMenuItem1;
		private ToolStripMenuItem vediCodiciToolStripMenuItem2;
	}
}
