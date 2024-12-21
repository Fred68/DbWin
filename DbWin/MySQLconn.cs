using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;		// Per MySqlConnection


namespace DbWin
{

	/// <summary>
	/// MySQLconn class
	/// </summary>
	internal class MySQLconn
	{
		MySqlConnection? conn;
		ConnectionString cstr;
		DataTable? dtConn;

		#region --- Properties ---

		/// <summary>
		/// MySqlConnection (read only)
		/// </summary>
		public MySqlConnection? Connection { get {return conn;} }

		/// <summary>
		/// Connection status (readonly)
		/// </summary>
		public ConnectionState Status
		{
			get
			{
			ConnectionState cst;
			if(conn != null)
				cst = conn.State;
			else
				cst = ConnectionState.Broken;
			return cst;
			}
		}
		
		/// <summary>
		/// Connection string
		/// </summary>
		public ConnectionString ConnectionString
		{
			get { return cstr;}
			set { cstr = value;}
		}
		#endregion  --- 

		/// <summary>
		/// Ctor
		/// </summary>
		public MySQLconn()
		{
			cstr = new ConnectionString();
			conn = null;
			dtConn = null;
		}

		/// <summary>
		/// Ctor
		/// </summary>
		/// <param name="cs"></param>
		public MySQLconn(ConnectionString cs)
		{
			cstr = cs;
			conn = null;
			dtConn = null;
		}

		/// <summary>
		/// Connect
		/// </summary>
		public void Connect()
		{
			try
			{
				if(conn == null)
				{
					conn = new MySqlConnection(cstr.ToString());
					conn.Open();
					MessageBox.Show(GetStatus(true,false));

					dtConn = conn.GetSchema();
				}
				else
				{
					MessageBox.Show($"Already connected: {conn.State.ToString()}");
				}
			}
			catch(MySql.Data.MySqlClient.MySqlException ex)
			{
				MessageBox.Show($"{ex.Number}:{ex.Message}" + $"\nConnection string: {cstr.ToString()}");
				Disconnect();

			}
		}

		/// <summary>
		/// Disconnect
		/// </summary>
		public void Disconnect()
		{
			if(conn != null)
			{
				conn.Close();
				conn = null;
				MessageBox.Show(Messages.Msg.Disconnected);
			}
			else
			{
				MessageBox.Show(Messages.Msg.NotConnected);
			}
		}

		/// <summary>
		/// Get connection status
		/// </summary>
		/// <param name="bShowConnString"></param>
		/// <param name="bShowExtraInfo"></param>
		/// <returns></returns>
		public string GetStatus(bool bShowConnString = false, bool bShowExtraInfo = false)
		{
			StringBuilder sb = new StringBuilder();
			if(bShowConnString)
			{
				sb.AppendLine($"Connection string: {cstr.ToString()}");
			}
			if( conn != null )
			{
				sb.AppendLine($"Connection: {conn.State.ToString()}");
				if(bShowExtraInfo)
				{
					if(dtConn != null)
					{
						sb.Append(DisplayDataTable(dtConn));
					}
				}
			}
			else
			{
				sb.AppendLine($"Non connected");
			}
			
			return sb.ToString();
		}

		/// <summary>
		/// Display data table content
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		public string DisplayDataTable(DataTable dt)
		{
			StringBuilder sb = new StringBuilder();
			foreach(DataRow dr in dt.Rows)
			{
				foreach(DataColumn dc in dt.Columns)
				{
					sb.Append($"{dc.ColumnName}={dr[dc]}\t");
				}
				sb.AppendLine("-----");
			}
			return sb.ToString();
		}
	}
}
