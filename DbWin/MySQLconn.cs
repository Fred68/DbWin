using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;
using Org.BouncyCastle.Tls.Crypto;      // Per MySqlConnection
using System.Data.Common;

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
		// Tools
		/*******************************************/

		public static string ReplaceWildcards(string s)
		{
			return s.Replace('*','%').Replace('?','_');
		}

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
					#if DEBUG
					Thread.Sleep(1000);
					#endif
				}
				else
				{
					sb.AppendLine($"{CFG.Msg.MsgConnected}: {conn.State.ToString()}");
				}
			}
			catch(MySql.Data.MySqlClient.MySqlException ex)
			{
				sb.AppendLine($"{ex.Number}:{ex.Message}" + $"\n{CFG.Msg.MnuConnString}: {cstr.ToString()}");
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
				sb.AppendLine(CFG.Msg.MsgDisconnected);
			}
			else
			{
				sb.AppendLine(CFG.Msg.MsgNotConnected);
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
					sb.AppendLine($"{CFG.Msg.MnuConnString}:{Environment.NewLine}{cstr.ToString().Trim()}");
				}

				if ( (nfo & Info.Status) != 0  )
				{
					sb.AppendLine($"{CFG.Msg.MnuConnecting}: {conn.State.ToString().Trim()}");
				}

				if ( (nfo & Info.Schema) != 0  )
				{
					if(dtConn != null)
					{
						sb.AppendLine($"--- {CFG.Msg.MnuSchema} ---");
						sb.AppendLine($"{Environment.NewLine}{DataTableToString(dtConn)}");
						#if DEBUG
						Thread.Sleep(1000);
						#endif
					}
				}

				if ( (nfo & Info.Functions) != 0  )
				{
					sb.AppendLine($"--- {CFG.Msg.MnuFunctions} ---");
					sb.AppendLine(ExecuteSQLCommand("CALL ListaFunzioni();",SQLqueryType.Reader));
					#if DEBUG
					Thread.Sleep(1000);
					#endif
				}
				if ( (nfo & Info.Procedures) != 0  )
				{
					sb.AppendLine($"--- {CFG.Msg.MnuProcedures} ---");
					sb.AppendLine(ExecuteSQLCommand("CALL ListaProcedure();",SQLqueryType.Reader));
					#if DEBUG
					Thread.Sleep(1000);
					#endif
				}
				if ( (nfo & Info.User) != 0  )
				{
					sb.AppendLine($"--- {CFG.Msg.MnuUser} ---");
					sb.AppendLine(ExecuteSQLCommand("CALL NomeUtente();",SQLqueryType.Reader));
					#if DEBUG
					Thread.Sleep(1000);
					#endif
				}
			}
			else
			{
				sb.AppendLine($"{CFG.Msg.MsgNotConnected}");
			}

			return sb.ToString();
		}

		/// <summary>
		/// Display data table value
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		public string DataTableToString(DataTable dt)
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

		public DataTable EmptyDataTable(string columnName, string value)
		{
			DataTable dt = new DataTable();
			DataColumn dtC= new DataColumn();
			dtC.DataType = typeof(string);
			dtC.ColumnName = columnName;
			dtC.ReadOnly = false;
			dt.Columns.Add(dtC);
			DataSet ds = new DataSet();
			ds.Tables.Add(dt);
			DataRow dr = dt.NewRow();
			dr[columnName] = value;
			dt.Rows.Add(dr);
			return dt;
		}

#if false		// MySQL async functions (non usate)
		public static async Task<string> ExecuteNonQuery(string sql, MySqlConnection conn, CancellationToken token)
		{
			int lines = 0;
			StringBuilder sb = new StringBuilder();
			if(conn != null)
			{
				try
				{
					MySqlCommand cmd = new MySqlCommand(sql, conn);
					lines = await cmd.ExecuteNonQueryAsync(token);
					sb.AppendLine($"NonQuery():{lines} linee.");

				}
				catch(Exception ex)
				{
					sb.AppendLine(ex.ToString());
				}
			}
			return sb.ToString();
		}
		public static async Task<string> ExecuteScalar(string sql, MySqlConnection conn, CancellationToken token)
		{
			object? obj = null;
			StringBuilder sb = new StringBuilder();
			if(conn != null)
			{
				try
				{
					MySqlCommand cmd = new MySqlCommand(sql, conn);
					obj = await cmd.ExecuteScalarAsync(token);
					string? s = string.Empty;
					if(obj != null)
					{
						s = Convert.ToString(obj);
					}
					sb.AppendLine($"Scalar():{s}.");
				}
				catch(Exception ex)
				{
					sb.AppendLine(ex.ToString());
				}
			}
			return sb.ToString();
		}
		public static async Task<string> ExecuteReader(string sql, MySqlConnection conn, CancellationToken token)
		{
			DbDataReader? rdr = null;		// Al posto di MySqlDataReader
			StringBuilder sb = new StringBuilder();
			if(conn != null)
			{
				try
				{
					MySqlCommand cmd = new MySqlCommand(sql, conn);
					sb.AppendLine($"Reader():");
					rdr = await cmd.ExecuteReaderAsync(token);
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
				catch(Exception ex)
				{
					sb.AppendLine(ex.ToString());
				}
			}
			return sb.ToString();
		}
		public async Task<string> ExecuteSQLCommandAsync(string sql, SQLqueryType type, CancellationToken token, CancellationTokenSource cts)
		{
			
			if( (token!=CancellationToken.None) && (cts!=null))
				 {
				 token.Register( () =>
					{
					MessageBox.Show("Task canceled");
					cts.Dispose();
					}
					);
				 }

			string result = "";
			if(conn != null)
			{
				switch(type)
				{
				
					case SQLqueryType.NoQuery:
					{
						result = await ExecuteNonQuery(sql, conn, token);
					}
					break;
					case SQLqueryType.Scalar:
					{
						result = await ExecuteScalar(sql, conn, token);
					}
					break;
					case SQLqueryType.Reader:
					{
						result = await ExecuteReader(sql, conn, token);
					}
					break;
				}
			}
			return result;
		}
		#endif

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
							
							for(int i=0; i<rdr.FieldCount; i++)
							{
								sb.Append(rdr.GetName(i).ToString());
								if(i != rdr.FieldCount-1)	sb.Append(", ");
							}
							sb.Append(System.Environment.NewLine);
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

		public void ExecuteSQLReadDataTable(string sql, ref DataTable dt)
		{
			if(conn != null)
			{
				try
				{
					MySqlCommand cmd = new MySqlCommand(sql, conn);
					using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
					{
						da.Fill(dt);
					}
				}
				catch
				{
					dt.Clear();
				}
			}
		}

		/*******************************************/
		// Query fuctions
		/*******************************************/

		/// <summary>
		/// Vedi codici (in una stringa unica)
		/// </summary>
		/// <param name="cod">Codice (con wildcard %)</param>
		/// <param name="mod">Modifica (con wildcard %)</param>
		/// <param name="limit">Numero massimo di righe</param>
		/// <returns></returns>
		public string VediCodiciString(string cod, string mod, int limit)
		{
			StringBuilder sb = new StringBuilder();
			if( conn != null )
			{
				string sql = $"CALL VediCodici({limit},\"{ReplaceWildcards(cod)}\",\"{ReplaceWildcards(mod)}\");";
				#if DEBUG
				MsgBox.Show(sql);
				#endif
				sb.AppendLine($"--- {"Vedi codici"} ---");
				sb.AppendLine(ExecuteSQLCommand(sql,SQLqueryType.Reader));
			}
			else
			{
				sb.AppendLine($"{CFG.Msg.MsgNotConnected}");
			}
			return sb.ToString();
		}

		public string EsplodiCodiceString(string cod, string mod, int limit)
		{
			StringBuilder sb = new StringBuilder();
			if( conn != null )
			{
				string sql = $"CALL Esplodi(\"{ReplaceWildcards(cod)}\",\"{ReplaceWildcards(mod)}\",{limit});";
				#if DEBUG
				MsgBox.Show(sql);
				#endif
				sb.AppendLine($"--- {"Esplodi codice"} ---");
				sb.AppendLine(ExecuteSQLCommand(sql,SQLqueryType.Reader));
			}
			else
			{
				sb.AppendLine($"{CFG.Msg.MsgNotConnected}");
			}
			return sb.ToString();
		}

		public DataTable EsplodiCodice(string cod, string mod, int limit)
		{
			DataTable dt = new DataTable();
			if( conn != null )
			{
				string sql = $"CALL Esplodi(\"{ReplaceWildcards(cod)}\",\"{ReplaceWildcards(mod)}\",{limit});";
				#if DEBUG
				MsgBox.Show(sql);
				#endif
				ExecuteSQLReadDataTable(sql, ref dt);
			}
			else
			{
				dt = EmptyDataTable("RESULT",CFG.Msg.MsgNotConnected);
			}
			dt.TableName = CFG.Msg.MnuExplode;
			return dt;
		}

		/// <summary>
		/// Vedi codici (in una tabella di dati)
		/// </summary>
		/// <param name="cod">Codice (con wildcard %)</param>
		/// <param name="mod">Modifica (con wildcard %)</param>
		/// <param name="limit">Numero massimo di righe</param>
		/// <returns></returns>
		public DataTable VediCodici(string cod, string mod, int limit)
		{
			DataTable dt = new DataTable();
			if(conn != null)
			{
				string sql = $"CALL VediCodici({limit},\"{ReplaceWildcards(cod)}\",\"{ReplaceWildcards(mod)}\");";
				#if DEBUG
				MsgBox.Show(sql);
				#endif
				ExecuteSQLReadDataTable(sql, ref dt);
			}
			else
			{
				dt = EmptyDataTable("RESULT",CFG.Msg.MsgNotConnected);
			}
			dt.TableName = CFG.Msg.MnuViewCodes;
			return dt;
		}

		/// <summary>
		/// Vedi descrizioni
		/// </summary>
		/// <param name="cod">Codice (con wildcard %)</param>
		/// <param name="mod">Modifica (con wildcard %)</param>
		/// <param name="limit">Numero massimo di righe</param>
		/// <returns></returns>
		public DataTable VediDescrizioni(string cod, string mod, int limit)
		{
			DataTable dt = new DataTable();
			if(conn != null)
			{
				string sql = $"CALL VediDescrizioni({limit},\"{ReplaceWildcards(cod)}\",\"{ReplaceWildcards(mod)}\");";
				#if DEBUG
				MsgBox.Show(sql);
				#endif
				ExecuteSQLReadDataTable(sql, ref dt);
			}
			else
			{
				dt = EmptyDataTable("RESULT",CFG.Msg.MsgNotConnected);
			}
			dt.TableName = CFG.Msg.MnuViewDescr;
			return dt;
		}

		/// <summary>
		/// Tutti i dati di un codice singolo
		/// </summary>
		/// <param name="cod">Codice (con wildcard %)</param>
		/// <param name="mod">Modifica (con wildcard %)</param>
		/// <returns></returns>
		public DataTable VediCodiceSingolo(string cod, string mod)
		{
			DataTable dt = new DataTable();
			if(conn != null)
			{
				string sql = $"CALL GetCode(\"{ReplaceWildcards(cod)}\",\"{ReplaceWildcards(mod)}\");";
				#if DEBUG
				MsgBox.Show(sql);
				#endif
				ExecuteSQLReadDataTable(sql, ref dt);
			}
			else
			{
				dt = EmptyDataTable("RESULT",CFG.Msg.MsgNotConnected);
			}
			dt.TableName = CFG.Msg.MnuViewCode;
			return dt;
		}


	}
}
