using System.Data;
using System.Reflection;
using System.Text;
using Fred68.InputForms;
using InputForms;

namespace DbWin
{

	public partial class Form1:Form
	{
		ConnectionString cs;
		MySQLconn conn;
		Color statStripBkCol;
		RotatingChar rotchar;
		Busy busy;

		/*******************************************/
		// Ctors e funzioni principali
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
			if(this.Icon != null)
				InputForm.SetIcon(this.Icon);
		}

		private void ReplaceGUIText()
		{
			SuspendLayout();
			Text = Application.ProductName;
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
		// Versione e help
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
		// Funzioni con chiamata diretta
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
				busy.busy = true;
				Task<string> task = Task<string>.Factory.StartNew(() => conn.Connect());
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

		void ShowDataTable(DataTableInfo dti)
		{
			GridBox gb = new GridBox(dti.datatable,VediCodiceSingolo);     // Evento per doppio click su un codice
			gb.Show();
		}


		void EditDataTable(DataTableInfo dti)
		{
			int iTipo = -1;
			string? tipo = string.Empty;
			Type? tTipo = null;

			if(dti.Righe != 1)									// Edit possibile solo il codice � unico (una riga soltanto)
			{
				MsgBox.Show("Ammessa tabella con una sola riga");
				return;
			}

			iTipo = dti.IndiceColonna("TIPO");					// Cerca la colonna con il TIPO
			
			if(iTipo != -1)										// Legge il tipo di dati
			{
				tTipo = dti.TipoColonna(iTipo);					// tTipo non � null, perch� la colona esiste
				if(tTipo != typeof(string))
				{
					MsgBox.Show("Colonna 'TIPO' con dati non string");
					return;
				}
			}
			else
			{
				MsgBox.Show("Colonna 'TIPO' mancante.");
				return;
			}

			tipo = (string?)dti[0,iTipo];

			List<string> lShow = CFG.GetList(CFG.ListType.Show,tipo);               // Legge i campi dalla configurazione
			List<string> lReadOnly = CFG.GetList(CFG.ListType.Readonly,tipo);
			List<string> lDropdown = CFG.GetList(CFG.ListType.Dropdown,tipo);

			FormData fd = new FormData();                       // Prepara i dati per l'Input Form
			foreach(string s in lShow)                          // Nome del campo
			{
				if(dti.datatable.Columns.Contains(s))
				{
					int indx = dti.datatable.Columns.IndexOf(s);           // Indice
					Type tp = dti.datatable.Columns[indx].DataType;        // Tipo di dato
					bool bRo = lReadOnly.Contains(s);           // Readonly
					bool bDr = lDropdown.Contains(s);           // Dropdown
					fd.Add(s,dti[0,indx],bRo,bDr);
				}

			}

			if(fd.Count > 0)
			{
				InputForm inf = new InputForm(fd,true,60);
				if(inf.ShowDialog() == DialogResult.OK)
				{
					string cod = string.Empty;
					string mod = string.Empty;
					if(fd.isValid)								// Estrae il codice
					{
						
						try
						{
							cod = fd["CODICE"];
							mod = fd["MODIFICA"];
						}
						catch(KeyNotFoundException ex)
						{
							MsgBox.Show("Errore: " + ex.Message);
							fd.isValid = false;
						}
					}

					if(fd.isValid)								// Controlla se esiste gi�
					{
						MsgBox.Show($"Update / insert:{Environment.NewLine}{fd.Dump()}");
						DataTableInfo dtnfo = ContaCodici(cod,mod);
						//ShowDataTable(dtnfo.datatable);
						ShowDataTable(dtnfo);
						

					}
#warning COMPLETARE: Mostrare modifica o aggiunta, chiedere, inserire, mostrare il risultato dell'inserimento
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
		// Funzioni dopo completamento task
		/*******************************************/

		void AfterTask(Task<DataTableInfo> t)
		{
			DataTableInfo dti = t.Result;

			busy.busy = false;
			UpdateForm();
			BeginInvoke(new Action(() => t.Result.dtFunc(dti)));
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


		/*******************************************/
		// Funzione per l'avvio di un task
		/*******************************************/

		/// <summary>
		/// Avvia un Task per eseguire una funzione
		/// </summary>
		/// <param name="func">delegate o funzioen anonima che restituisce un oggetto DataTableInfo</param>
		void Chiama(Func<DataTableInfo> func, TaskOptions topt = TaskOptions.Default)
		{
			CancellationTokenSource cts = new CancellationTokenSource();
			CancellationToken token = cts.Token;
			Task<DataTableInfo> task = Task<DataTableInfo>.Factory.StartNew(func,token);

			if( (topt & TaskOptions.ExecAfterTask) !=0)			// Se deve eseguire l'operazione dopo il completamento del task...
			{
				if( (topt & TaskOptions.DoNotWait) !=0)			// Attende o no il completamento del task ?
				{
					task.ContinueWith(AfterTask);				// Se no, prenota esecuzione alla fine del task
				}
				else
				{
					task.Wait();								// Se s�, aspetta la fine del task...
					AfterTask(task);							// ...ed esegue l'operazione
				}
			}
		}


		/*******************************************/
		// Funzioni con task
		/*******************************************/

		public void ShowStatus(Info info)
		{
			if(!busy.busy)
			{
				CancellationTokenSource cts = new CancellationTokenSource();
				CancellationToken token = cts.Token;
				busy.busy = true;
				Task<string> task = Task<string>.Factory.StartNew(() => conn.GetStatus(info),token);
				task.ContinueWith(ShowTaskMsg);
			}
		}

		void VediCodici()
		{
			FormData fd = new FormData();
			fd.Add("Codice","*");
			fd.Add("Modifica","*");
			fd.Add("Limite",100);
			if((new InputForm(fd)).ShowDialog() == DialogResult.OK)
			{
				DataTableInfo dti = new DataTableInfo(ShowDataTable);
				Chiama(()=>conn.VediCodici(dti,fd["Codice"],fd["Modifica"],fd["Limite"]));
			}

		}

		void VediDescrizioni()
		{
			FormData fd = new FormData();
			fd.Add("Codice","*");
			fd.Add("Modifica","*");
			fd.Add("Limite",100);
			if((new InputForm(fd)).ShowDialog() == DialogResult.OK)
			{
				DataTableInfo dti = new DataTableInfo(ShowDataTable);
				Chiama(() => conn.VediDescrizioni(dti,fd["Codice"],fd["Modifica"],fd["Limite"]));
			}
		}

		void VediCodice()
		{
			FormData fd = new FormData();
			fd.Add("Codice","100*");
			fd.Add("Modifica","a");
			if((new InputForm(fd)).ShowDialog() == DialogResult.OK)
			{
				DataTableInfo dti = new DataTableInfo(ShowDataTable);
				Chiama(() => conn.VediCodiceSingolo(dti,fd["Codice"],fd["Modifica"]));
			}
		}

		void VediCodiceSingolo(string cod,string mod)
		{
			DataTableInfo dti = new DataTableInfo(EditDataTable);
			Chiama(() => conn.VediCodiceSingolo(dti,cod,mod));
		}

		void EsplodiCodice()
		{
			FormData fd = new FormData();
			fd.Add("Codice","100.12.002");
			fd.Add("Modifica","d");
			fd.Add("Profondit�",100);
			if((new InputForm(fd)).ShowDialog() == DialogResult.OK)
			{
				DataTableInfo dti = new DataTableInfo(ShowDataTable);
				Chiama(() => conn.EsplodiCodice(dti,fd["Codice"],fd["Modifica"],fd["Profondit�"]));
			}
		}

		DataTableInfo ContaCodici(string cod,string mod)
		{
			DataTableInfo dti = new DataTableInfo(ShowDataTable);
			Chiama(() => conn.ContaCodici(dti,cod,mod),TaskOptions.ExecAfterTask);
			return dti;
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
			VediCodici();
		}

		private void descrizioniToolStripMenuItem_Click(object sender,EventArgs e)
		{
			VediDescrizioni();
		}

		private void esplodiToolStripMenuItem_Click(object sender,EventArgs e)
		{
			EsplodiCodice();
		}

		private void contaCodiciToolStripMenuItem_Click(object sender,EventArgs e)
		{
			ContaCodici("100.11.123","a");
		}
	}
}
