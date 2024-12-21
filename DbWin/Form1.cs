using Org.BouncyCastle.Pqc.Crypto.Lms;
using System.Data;
using System.Reflection;
using System.Text;

namespace DbWin
{
	public partial class Form1:Form
	{
		ConnectionString cs;
		MySQLconn conn;

		/// <summary>
		/// Ctor
		/// </summary>
		public Form1()
		{
			InitializeComponent();
			cs = new ConnectionString("127.0.0.1","3306","pippo","pippo01","dbc01");
			conn = new MySQLconn();
		}

		/// <summary>
		/// On Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Form1_Load(object sender,EventArgs e)
		{
			UpdateForm();
		}

		/// <summary>
		/// On Closing
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Form1_FormClosing(object sender,FormClosingEventArgs e)
		{
			Disconnetti();								// Chiede conferma di disconnessione
			ConnectionState st = conn.Status;			// Se non è connesso, chiede conferma di chiusura
			if ((st == ConnectionState.Closed) || (st == ConnectionState.Broken))	
			{
				if(MessageBox.Show(Messages.Msg.Closing,Messages.Titles.Closing,MessageBoxButtons.OKCancel,MessageBoxIcon.Warning) != DialogResult.OK)
				{
					e.Cancel = true;					// Se la chiusura non è confermata, annulla il comando
					UpdateForm();
				}
			}
			else
			{
				e.Cancel = true;						// In tutti gli altri casi, annulla il comando
				UpdateForm();
			}
		}

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

		public void UpdateForm()
		{
			tsStatus.Text = conn.GetStatus();
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
			conn.ConnectionString = cs;
			conn.Connect();
			UpdateForm();
		}

		public void Disconnetti()
		{
			if(conn != null)
			{
				if(conn.Status == System.Data.ConnectionState.Open)
				{
					if(MessageBox.Show(Messages.Msg.Disconnecting,Messages.Titles.Disconnecting,MessageBoxButtons.OKCancel,MessageBoxIcon.Warning) == DialogResult.OK)
					{
						conn.Disconnect();
					}
				}
			}
			UpdateForm();
		}

		#region - Command handlers -
		private void informazioniToolStripMenuItem_Click(object sender,EventArgs e)
		{
			MessageBox.Show(Version(Assembly.GetExecutingAssembly()));
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

		#endregion ---
	}
}
