using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;
using Org.BouncyCastle.Tls.Crypto;      // Per MySqlConnection


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


		/*******************************************/
		// Properties
		/*******************************************/

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
		/// Is connected ? (readonly)
		/// </summary>
		public bool IsConnected
		{
			get
			{
				return ((Status==ConnectionState.Open)||(Status==ConnectionState.Executing)||(Status==ConnectionState.Fetching));
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

		/*******************************************/
		// Ctors
		/*******************************************/

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

		/*******************************************/
		// Main fuctions
		/*******************************************/

		/// <summary>
		/// Connect
		/// </summary>
		public string Connect()
		{
			StringBuilder sb = new StringBuilder();
			try
			{
				if(conn == null)
				{
					conn = new MySqlConnection(cstr.ToString());
					conn.Open();
					dtConn = conn.GetSchema();
					sb.AppendLine(GetStatus(Info.Status | Info.ConnectionString));
				}
				else
				{
					sb.AppendLine($"Already connected: {conn.State.ToString()}");
				}
			}
			catch(MySql.Data.MySqlClient.MySqlException ex)
			{
				sb.AppendLine($"{ex.Number}:{ex.Message}" + $"\nConnection string: {cstr.ToString()}");
				Disconnect();

			}
			return sb.ToString().Trim();
		}

		/// <summary>
		/// Disconnect
		/// </summary>
		public string Disconnect()
		{
			StringBuilder sb = new StringBuilder();
			if(conn != null)
			{
				conn.Close();
				conn = null;
				sb.AppendLine(Messages.Msg.Disconnected);
			}
			else
			{
				sb.AppendLine(Messages.Msg.NotConnected);
			}
			return sb.ToString().Trim();
		}

		/// <summary>
		/// Get connection status
		/// </summary>
		/// <param name="bShowConnString"></param>
		/// <param name="bShowExtraInfo"></param>
		/// <returns></returns>
		public string GetStatus(Info nfo)
		{
			StringBuilder sb = new StringBuilder();

			if( conn != null )
			{
				if( (nfo & Info.ConnectionString) != 0 )
				{
					sb.AppendLine($"STRINGA DI CONNESSIONE:{Environment.NewLine}{cstr.ToString().Trim()}");
				}

				if ( (nfo & Info.Status) != 0  )
				{
					sb.AppendLine($"Connessione: {conn.State.ToString().Trim()}");
				}

				if ( (nfo & Info.Schema) != 0  )
				{
					if(dtConn != null)
					{
						sb.AppendLine("--- SCHEMA ---");
						sb.AppendLine($"{Environment.NewLine}{DisplayDataTable(dtConn)}");
					}
				}

				if ( (nfo & Info.Functions) != 0  )
				{
					sb.AppendLine("--- FUNZIONI ---");
					sb.AppendLine(ExecuteSQLCommand("CALL ListaFunzioni();",SQLqueryType.Reader));
				}
				if ( (nfo & Info.Procedures) != 0  )
				{
					sb.AppendLine("--- PROCEDURE ---");
					sb.AppendLine(ExecuteSQLCommand("CALL ListaProcedure();",SQLqueryType.Reader));
				}
			}
			else
			{
				sb.AppendLine($"NON CONNESSO");
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

		/// <summary>
		/// Execute MySQL command (reader, non query or scalar)
		/// </summary>
		/// <param name="sql"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public string ExecuteSQLCommand(string sql, SQLqueryType type)
		{
			StringBuilder sb = new StringBuilder();
			MySqlDataReader? rdr = null;
			if(conn != null)
			{
				try
				{
					MySqlCommand cmd = new MySqlCommand(sql, conn);
					switch(type)
					{
						case SQLqueryType.NoQuery:			// ExecuteNonQuery 
						{
							int lines = cmd.ExecuteNonQuery();
							sb.AppendLine($"NonQuery():{lines} linee.");
						}
						break;
						case SQLqueryType.Scalar:			// ExecuteScalar 
						{
							string? s = string.Empty;
							object obj = cmd.ExecuteScalar();
							if(obj != null)
							{
								s = Convert.ToString(obj);
							}
							sb.AppendLine($"Scalar():{s}.");
						}
						break;
						case SQLqueryType.Reader:			// ExecuteReader 
						{
							sb.AppendLine($"Reader():");
							rdr = cmd.ExecuteReader();
							while(rdr.Read())
							{
								for(int i=0; i<rdr.FieldCount; i++)
								{
									sb.Append(rdr[i].ToString());
									if(i != rdr.FieldCount-1)	sb.Append(", ");
									
								}
								sb.Append(System.Environment.NewLine);
							}
							rdr.Close();
							rdr = null;
						}
						break;
					}
				}
				catch(Exception ex)
				{
					if(rdr != null)
					{
						rdr.Close();
						rdr = null;
					}
					sb.AppendLine(ex.ToString());
				}

			}
			return sb.ToString();
		}

	}
}
