//using MySqlX.XDevAPI.Relational;
//using Org.BouncyCastle.Pqc.Crypto.Lms;
using System.Data;
using System.Reflection;
using System.Text;
//using static Fred68.CfgReader.CfgReader;
//using static Org.BouncyCastle.Math.EC.ECCurve;
using Fred68.InputForms;
using InputForms;
using Org.BouncyCastle.Bcpg.OpenPgp;

namespace DbWin
{

	public delegate void ShowCodeFunc(string cod, string mod);		// Delegate per mostrare un singolo codice
	public delegate void ShowDataTableFunc(DataTable dt);			// Delegate per mostrare un DataTable

	public partial class Form1:Form
	{

		ConnectionString cs;
		MySQLconn conn;
		Color statStripBkCol;
		RotatingChar rotchar;
		Busy busy;
		static CancellationTokenSource? cts = null;         // new CancellationTokenSource();
		CancellationToken token = CancellationToken.None;   // cts.Token;

		//InputForm inf;

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
			cs = new ConnectionString(CFG.Config.CONN_server,CFG.Config.CONN_port,CFG.Config.CONN_user,CFG.Config.CONN_password,CFG.Config.CONN_database);
			conn = new MySQLconn();
			rotchar = new RotatingChar(activeTsMenuItem);
			busy = new Busy(busyTsMenuItem,"B"," ");
			if(this.Icon != null)	InputForm.SetIcon(this.Icon);
		}

		private void ReplaceGUIText()
		{
			SuspendLayout();
			Text = "DesignToolsServer";
			fileToolStripMenuItem.Text = CFG.Msg.MnuConnecting;
			parametriDiConnessioneToolStripMenuItem.Text = CFG.Msg.MnuParameters;
			connettiToolStripMenuItem.Text = CFG.Msg.MnuConnect;
			disconnettiToolStripMenuItem.Text = CFG.Msg.MnuDisconnect;
			statoToolStripMenuItem.Text = CFG.Msg.MnuStatus;
			statoToolStripMenuItem1.Text = CFG.Msg.MnuStatus;
			connectionStringToolStripMenuItem.Text = CFG.Msg.MnuConnString;
			schemaToolStripMenuItem.Text = CFG.Msg.MnuSchema;
			proceduresToolStripMenuItem.Text = CFG.Msg.MnuProcedures;
			functionsToolStripMenuItem.Text = CFG.Msg.MnuFunctions;
			utenteToolStripMenuItem.Text = CFG.Msg.MnuUser;
			esciToolStripMenuItem.Text = CFG.Msg.MnuExit;
			helpToolStripMenuItem.Text = "?";
			informazioniToolStripMenuItem.Text = CFG.Msg.MnuNfo;
			dettagliToolStripMenuItem.Text = CFG.Msg.MnuDetails;
			utenteToolStripMenuItem.Text = CFG.Msg.MnuUser;
			interrogaToolStripMenuItem.Text = CFG.Msg.MnuQuery;
			vediCodiciToolStripMenuItem2.Text = CFG.Msg.MnuViewCodes;
			configurazioneToolStripMenuItem.Text = CFG.Msg.MnuConfig;

			toolStripMenuItem1.Text = CFG.Msg.MnuViewCodes;
			vediCodiceToolStripMenuItem.Text = CFG.Msg.MnuViewCode;
			descrizioniToolStripMenuItem.Text = CFG.Msg.MnuViewDescr;
			esplodiToolStripMenuItem.Text = CFG.Msg.MnuExplode;

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

			if(CFG.Config.INI_quick)
			{
				Connetti();
			}
		}

		/// <summary>
		/// On Closing
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Form1_FormClosing(object sender,FormClosingEventArgs e)
		{
			Disconnetti();                              // Chiede conferma di disconnessione
			ConnectionState st = conn.Status;           // Se non � connesso, chiede conferma di chiusura
			if((st == ConnectionState.Closed) || (st == ConnectionState.Broken))
			{
				if(!CFG.Config.INI_quick)
				{
					if(MessageBox.Show(CFG.Msg.MsgClosing,CFG.Msg.MnuClosing,MessageBoxButtons.OKCancel,MessageBoxIcon.Warning) != DialogResult.OK)
					{
						e.Cancel = true;                    // Se la chiusura non � confermata, annulla il comando
						UpdateForm();
					}
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
				strb.AppendLine("Copyright: " + Application.CompanyName);
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
						string fn = Path.GetFullPath(Application.ExecutablePath);
						string pth = fn.Substring(0,fn.LastIndexOf(Path.GetFileName(fn)));
						strb.AppendLine("Path: " + pth);
						strb.AppendLine("Executable: " + Path.GetFileName(fn));
						strb.AppendLine($"Config file: {CFG._cfgFile}");
						strb.AppendLine($"Text msg file: {CFG._msgFile}");
					}
				}
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

		public void Disconnetti()
		{
			if(conn != null)
			{
				if(conn.Status == System.Data.ConnectionState.Open)
				{
					bool dsc = false;
					if(CFG.Config.INI_quick)
					{
						dsc = true;
					}
					else
					{
						dsc = (MessageBox.Show(CFG.Msg.MsgDisconnecting,CFG.Msg.MnuDisconnecting,MessageBoxButtons.OKCancel,MessageBoxIcon.Warning) == DialogResult.OK);
					}

					if(dsc)
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

		void ShowMsgConnection(Task<string> t)
		{
			string msg = t.Result;
			busy.busy = false;
			UpdateForm();
#if DEBUG
			MsgBox.Show(msg);
#endif
		}

		void ShowTaskMsg(Task<string> t)
		{
			string st = t.Result;
			busy.busy = false;
			UpdateForm();
			BeginInvoke(new Action(() => MsgBox.Show(st)));
		}

		void ShowTaskDataTable(Task<DataTable> t)
		{
			DataTable dt = t.Result;
			busy.busy = false;
			UpdateForm();
			BeginInvoke(new Action(() => ShowDataTable(dt)));
		}
		void EditTaskDataTable(Task<DataTable> t)
		{
			DataTable dt = t.Result;
			busy.busy = false;
			UpdateForm();
			BeginInvoke(new Action(() => EditDataTable(dt)));
		}

		void VediCodiciString()
		{
			if(!busy.busy)
			{
				cts = new CancellationTokenSource();
				token = cts.Token;
				busy.busy = true;
				Task<string> task = Task<string>.Factory.StartNew(() => conn.VediCodiciString("100*","*",100),token);
				task.ContinueWith(ShowTaskMsg);
			}
		}

		void VediCodici()
		{
			if(!busy.busy)
			{
				FormData fd = new FormData();
				fd.Add("Codice","*");
				fd.Add("Modifica","*");
				fd.Add("Limite",100);
				if((new InputForm(fd)).ShowDialog() == DialogResult.OK)
				{
					//MsgBox.Show(fd.Dump());
					cts = new CancellationTokenSource();
					token = cts.Token;
					busy.busy = true;
					Task<DataTable> task = Task<DataTable>.Factory.StartNew(() => conn.VediCodici(fd["Codice"],fd["Modifica"],fd["Limite"]),token);
					task.ContinueWith(ShowTaskDataTable);
				}
			}
		}

		void VediDescrizioni()
		{
			if(!busy.busy)
			{
				FormData fd = new FormData();
				fd.Add("Codice","*");
				fd.Add("Modifica","*");
				fd.Add("Limite",100);
				if((new InputForm(fd)).ShowDialog() == DialogResult.OK)
				{
					cts = new CancellationTokenSource();
					token = cts.Token;
					busy.busy = true;
					Task<DataTable> task = Task<DataTable>.Factory.StartNew(() => conn.VediDescrizioni(fd["Codice"],fd["Modifica"],fd["Limite"]),token);
					task.ContinueWith(ShowTaskDataTable);
				}
			}
		}

		void VediCodice()
		{
			if(!busy.busy)
			{
				FormData fd = new FormData();
				fd.Add("Codice","100*");
				fd.Add("Modifica","a");
				if((new InputForm(fd)).ShowDialog() == DialogResult.OK)
				{
					cts = new CancellationTokenSource();
					token = cts.Token;
					busy.busy = true;
					Task<DataTable> task = Task<DataTable>.Factory.StartNew(() => conn.VediCodiceSingolo(fd["Codice"],fd["Modifica"]),token);
					task.ContinueWith(ShowTaskDataTable);
				}
			}
		}

		void VediCodiceSingolo(string cod,string mod)
		{
			if(!busy.busy)
			{
				cts = new CancellationTokenSource();
				token = cts.Token;
				busy.busy = true;
				Task<DataTable> task = Task<DataTable>.Factory.StartNew(() => conn.VediCodiceSingolo(cod,mod),token);
				task.ContinueWith(EditTaskDataTable);
			}
		}

		void EsplodiCodiceString()
		{
			if(!busy.busy)
			{
				FormData fd = new FormData();
				fd.Add("Codice","100*");
				fd.Add("Modifica","a");
				fd.Add("Profondit�",100);
				if((new InputForm(fd)).ShowDialog() == DialogResult.OK)

					cts = new CancellationTokenSource();
					token = cts.Token;
					busy.busy = true;
					Task<string> task = Task<string>.Factory.StartNew(() => conn.EsplodiCodiceString(fd["Codice"],fd["Modifica"],fd["Profondit�"]),token);
					task.ContinueWith(ShowTaskMsg);
			}	
		}

		void EsplodiCodice()
		{
			if(!busy.busy)
			{
				FormData fd = new FormData();
				fd.Add("Codice","100.12.002");
				fd.Add("Modifica","d");
				fd.Add("Profondit�",100);
				if((new InputForm(fd)).ShowDialog() == DialogResult.OK)
				{
					cts = new CancellationTokenSource();
					token = cts.Token;
					busy.busy = true;
					Task<DataTable> task = Task<DataTable>.Factory.StartNew(() => conn.EsplodiCodice(fd["Codice"],fd["Modifica"],fd["Profondit�"]),token);
					task.ContinueWith(ShowTaskDataTable);
				}
			}
		}
		
		void ShowDataTable(DataTable dt)
		{
			GridBox gb = new GridBox(dt,VediCodiceSingolo);		// Evento per doppio click su un codice
			gb.Show();
		}

		void EditDataTable(DataTable dt)
		{
			int iTipo = -1;
			string tipo = string.Empty;
			Type? tTipo = null;

			int nRows = dt.Rows.Count;							// Numeri di riche e di colonne
			int nCols = dt.Columns.Count;

			if(nRows != 1)										// Edit possibile solo il codice � unico (una riga soltanto)
			{
				MsgBox.Show("Numero errato di righe.");
				return;
			}
			
			for(int i=0; i<nCols; i++)							// Cerca la colonna con il TIPO
			{
				if(dt.Columns[i].ColumnName == "TIPO")
				{
					iTipo = i;
					tTipo = dt.Columns[i].DataType;
					break;
				}
			}

			if( (iTipo == -1) || (tTipo == null))				// Se la colonna TIPO non esiste, interrompe con errore
			{
				MsgBox.Show("Tipo di codice (colonna 'TIPO') mancante.");
				return;
			}
			if(!(tTipo == typeof(string)))	// Non � null
			{
				MsgBox.Show("Colonna 'TIPO' con dati non string");
				return;
			}
			
			DataRow drc = dt.Rows[0];							// Dati della riga
			tipo = (string)drc[iTipo];							// Tipo di record (P, A, C, S)

			List<string> lShow = CFG.GetList(CFG.ListType.Show,tipo);				// Legge i campi dalla configurazione
			List<string> lReadOnly = CFG.GetList(CFG.ListType.Readonly,tipo);
			List<string> lDropdown = CFG.GetList(CFG.ListType.Dropdown,tipo);

			FormData fd = new FormData();						// Prepara i dati per l'Input Form
			foreach(string s in lShow)							// Nome del campo
			{
				if(dt.Columns.Contains(s))
				{
					int indx = dt.Columns.IndexOf(s);			// Indice
					Type tp = dt.Columns[indx].DataType;		// Tipo di dato
					bool bRo = lReadOnly.Contains(s);			// Readonly
					bool bDr = lDropdown.Contains(s);			// Dropdown
					fd.Add(s, drc[indx], bRo,bDr);
				}

			}

			
			if(fd.Count > 0)
			{
				InputForm inf = new InputForm(fd);
				if(inf.ShowDialog() == DialogResult.OK)
				{
					if(fd.isModified)
					{
						MsgBox.Show($"Update / insert:{Environment.NewLine}{fd.Dump()}");
					}
					#warning COMPLETARE: modifica o aggiunta... Domanda, poi chiamata al database
				}
			}
			else
			{
				MsgBox.Show("Nessun dato trovato");
			}

			


			//GridBox gb = new GridBox(dt,VediCodiceSingolo);		// Evento per doppio click su un codice
			//gb.Show();
			
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

		private void dataTableToolStripMenuItem_Click(object sender,EventArgs e)
		{
			VediCodici();
		}

		private void vediCodiciToolStripMenuItem2_Click(object sender,EventArgs e)
		{
			VediCodici();
		}

		private void configurazioneToolStripMenuItem_Click(object sender,EventArgs e)
		{
			MsgBox.Show(CFG.DumpEntries());
		}

		private void vediCodiceToolStripMenuItem_Click(object sender,EventArgs e)
		{
			VediCodice();
		}

		private void toolStripMenuItem1_Click(object sender,EventArgs e)
		{
			VediCodiciString();
		}

		private void descrizioniToolStripMenuItem_Click(object sender,EventArgs e)
		{
			VediDescrizioni();
		}

		private void esplodiToolStripMenuItem_Click(object sender,EventArgs e)
		{
			EsplodiCodice();
		}
	}
}
