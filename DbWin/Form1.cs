using MySqlX.XDevAPI.Relational;
using Org.BouncyCastle.Pqc.Crypto.Lms;
using System.Data;
using System.Reflection;
using System.Text;
using static Fred68.CfgReader.CfgReader;



namespace DbWin
{
	public partial class Form1:Form
	{

		ConnectionString cs;
		MySQLconn conn;
		Color statStripBkCol;
		RotatingChar rotchar;
		Busy busy;
		static CancellationTokenSource? cts = null;         // new CancellationTokenSource();
		CancellationToken token = CancellationToken.None;   // cts.Token;

		/*******************************************/
		// Ctors and main functions
		/*******************************************/

		/// <summary>
		/// Ctor
		/// </summary>
		public Form1()
		{
			InitializeComponent();
			ReplaceGUIText();
			cs = new ConnectionString(Cfg.Config.CONN_server,Cfg.Config.CONN_port,Cfg.Config.CONN_user,Cfg.Config.CONN_password,Cfg.Config.CONN_database);
			conn = new MySQLconn();
			rotchar = new RotatingChar(activeTsMenuItem);
			busy = new Busy(busyTsMenuItem,"B"," ");
		}

		private void ReplaceGUIText()
		{
			SuspendLayout();
			fileToolStripMenuItem.Text = Cfg.Msg.MnuConnecting;
			parametriDiConnessioneToolStripMenuItem.Text = Cfg.Msg.MnuParameters;
			connettiToolStripMenuItem.Text = Cfg.Msg.MnuConnect;
			disconnettiToolStripMenuItem.Text = Cfg.Msg.MnuDisconnect;
			statoToolStripMenuItem.Text = Cfg.Msg.MnuStatus;
			statoToolStripMenuItem1.Text = Cfg.Msg.MnuStatus;
			connectionStringToolStripMenuItem.Text = Cfg.Msg.MnuConnString;
			schemaToolStripMenuItem.Text = Cfg.Msg.MnuSchema;
			proceduresToolStripMenuItem.Text = Cfg.Msg.MnuProcedures;
			functionsToolStripMenuItem.Text = Cfg.Msg.MnuFunctions;
			utenteToolStripMenuItem.Text = "Utente";
			esciToolStripMenuItem.Text = Cfg.Msg.MnuExit;
			helpToolStripMenuItem.Text = "?";
			informazioniToolStripMenuItem.Text = Cfg.Msg.MnuNfo;
			dettagliToolStripMenuItem.Text = Cfg.Msg.MnuDetails;
			utenteToolStripMenuItem.Text = Cfg.Msg.MnuUser;
			Text = "DesignToolsServer";
			ResumeLayout(true);
		}

		/// <summary>
		/// On Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Form1_Load(object sender,EventArgs e)
		{
			statStripBkCol = statusStrip1.BackColor;

#if DEBUG
			TESTtsmi.Visible = true;
#else
			TESTtsmi.Visible = false;
#endif

			refreshTimer.Interval = 1000;
			refreshTimer.Start();
			busy.busy = false;
			UpdateForm();
		}

		/// <summary>
		/// On Closing
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Form1_FormClosing(object sender,FormClosingEventArgs e)
		{
			Disconnetti();                              // Chiede conferma di disconnessione
			ConnectionState st = conn.Status;           // Se non è connesso, chiede conferma di chiusura
			if((st == ConnectionState.Closed) || (st == ConnectionState.Broken))
			{
				if(MessageBox.Show(Cfg.Msg.MsgClosing,Cfg.Msg.MnuClosing,MessageBoxButtons.OKCancel,MessageBoxIcon.Warning) != DialogResult.OK)
				{
					e.Cancel = true;                    // Se la chiusura non è confermata, annulla il comando
					UpdateForm();
				}
			}
			else
			{
				e.Cancel = true;                        // In tutti gli altri casi, annulla il comando
				UpdateForm();
			}
		}

		/*******************************************/
		// Version and help
		/*******************************************/

		/// <summary>
		/// Version string
		/// </summary>
		/// <param name="execAssy">from Assembly.GetExecutingAssembly()</param>
		/// <returns></returns>
		public string Version(Assembly asm,bool details = false)
		{
			StringBuilder strb = new StringBuilder();
			try
			{
				strb.AppendLine(Application.ProductName);
				if(asm != null)
				{
					System.Version? v = asm.GetName().Version;
					if(v != null)
						strb.AppendLine($"Version: {v.ToString()} ({BuildTime(asm)})");
					if(details)
					{
						string? n = asm.GetName().Name;
						if(n != null)
							strb.AppendLine("Assembly name: " + n);
						strb.AppendLine("BuildTime time: " + File.GetCreationTime(asm.Location).ToString());
						strb.AppendLine("BuildTime number: " + BuildTime(asm,true));
						strb.AppendLine("Executable path: " + Application.ExecutablePath);
					}
				}
				strb.AppendLine("Copyright: " + Application.CompanyName);
			}
			catch { }
			return strb.ToString();
		}

		/// <summary>
		/// Build string
		/// </summary>
		/// <param name="asm"></param>
		/// <returns></returns>
		public string BuildTime(Assembly asm,bool number = false)
		{
			StringBuilder strb = new StringBuilder();
			if(asm != null)
			{
				DateTime dt = File.GetCreationTime(asm.Location);
				if(number)
					strb.Append(String.Format("{0:yyMMddhh}.{0:mmss}",dt));
				else
					strb.Append(dt.ToString("d"));
			}
			return strb.ToString();
		}

		/*******************************************/
		// Functions
		/*******************************************/

		public void UpdateRotchar()
		{
			rotchar.Update();
		}

		public void UpdateForm()
		{
			tsStatus.Text = conn.GetStatus(Info.Status).Trim();
			if(conn.IsConnected)
			{
				statusStrip1.BackColor = Color.LightGreen;
			}
			else
			{
				statusStrip1.BackColor = statStripBkCol;
			}
		}

		public void ParametriConnessione()
		{
			ConnectionForm form = new ConnectionForm(cs);
			if(form.ShowDialog() == DialogResult.OK)
			{
				cs = new ConnectionString(form.cs);
			}
			UpdateForm();
		}

		public void Connetti()
		{
			if(!busy.busy)
			{
				conn.ConnectionString = cs;
				cts = new CancellationTokenSource();
				token = cts.Token;
				busy.busy = true;
				Task<string> task = Task<string>.Factory.StartNew(() => conn.Connect(),token);
				task.ContinueWith(ShowMsgConnection);
			}
		}

		void ShowMsgConnection(Task<string> t)
		{
			string msg = t.Result;
			busy.busy = false;
			UpdateForm();
			#if DEBUG
			MsgBox.Show(msg);
			#endif
		}

		public void Disconnetti()
		{
			if(conn != null)
			{
				if(conn.Status == System.Data.ConnectionState.Open)
				{
					if(MessageBox.Show(Cfg.Msg.MsgDisconnecting,Cfg.Msg.MnuDisconnecting,MessageBoxButtons.OKCancel,MessageBoxIcon.Warning) == DialogResult.OK)
					{
						string msg = conn.Disconnect();
#if DEBUG
						MsgBox.Show(msg);
#endif
						conn.ConnectionString = new ConnectionString();
					}
				}
			}
			UpdateForm();
		}

		public void ShowStatus(Info info)
		{
			if(!busy.busy)
			{
				cts = new CancellationTokenSource();
				token = cts.Token;
				busy.busy = true;
				Task<string> task = Task<string>.Factory.StartNew(() => conn.GetStatus(info),token);
				task.ContinueWith(ShowTaskMsg);
			}
		}

		void ShowTaskMsg(Task<string> t)
		{
			string st = t.Result;
			busy.busy = false;
			UpdateForm();
			BeginInvoke(new Action(() => MsgBox.Show(st)));
		}

		void VediCodici()
		{
			if(!busy.busy)
			{
				cts = new CancellationTokenSource();
				token = cts.Token;
				busy.busy = true;
				Task<string> task = Task<string>.Factory.StartNew(() => conn.VediCodici("100%","%",100),token);
				task.ContinueWith(ShowTaskMsg);
			}
		}


		/*******************************************/
		// Handlers
		/*******************************************/

		private void informazioniToolStripMenuItem_Click(object sender,EventArgs e)
		{
			MsgBox.Show(Version(Assembly.GetExecutingAssembly()));
		}

		private void connettiToolStripMenuItem_Click(object sender,EventArgs e)
		{
			Connetti();
		}

		private void disconnettiToolStripMenuItem_Click(object sender,EventArgs e)
		{
			Disconnetti();
		}

		private void esciToolStripMenuItem_Click(object sender,EventArgs e)
		{
			Close();
		}

		private void parametriDiConnessioneToolStripMenuItem_Click(object sender,EventArgs e)
		{
			ParametriConnessione();
		}

		private void statoToolStripMenuItem1_Click(object sender,EventArgs e)
		{
			ShowStatus(Info.Status);
		}

		private void connectionStringToolStripMenuItem_Click(object sender,EventArgs e)
		{
			ShowStatus(Info.ConnectionString);
		}

		private void schemaToolStripMenuItem_Click(object sender,EventArgs e)
		{
			ShowStatus(Info.Schema);
		}

		private void proceduresToolStripMenuItem_Click(object sender,EventArgs e)
		{
			ShowStatus(Info.Procedures);
		}

		private void functionsToolStripMenuItem_Click(object sender,EventArgs e)
		{
			ShowStatus(Info.Functions);
		}

		private void MsgBoxToolStripMenuItem_Click(object sender,EventArgs e)
		{
			MsgBox.Show("test");
		}

		private void dettagliToolStripMenuItem_Click(object sender,EventArgs e)
		{
			MsgBox.Show(Version(Assembly.GetExecutingAssembly(),true));
		}

		private void utenteToolStripMenuItem_Click(object sender,EventArgs e)
		{
			ShowStatus(Info.User);
		}

		private void asyncTestToolStripMenuItem_Click(object sender,EventArgs e)
		{
			ShowStatus(Info.User);
		}

		private void refreshTimer_Tick(object sender,EventArgs e)
		{
			rotchar.Update(true);
			busy.Invalidate();
		}

		private void vediCodiciToolStripMenuItem_Click(object sender,EventArgs e)
		{
			VediCodici();	
		}
	}
}
