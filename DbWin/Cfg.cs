using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using Fred68.CfgReader;
using Org.BouncyCastle.Math.EC.Multiplier;
using static Fred68.CfgReader.CfgReader;


#pragma warning disable CS8602   // Dereferenziamento di un riferimento eventualmente Null.                                                            
#pragma warning disable CS8618   // La variabile che non ammette i valori Null deve contenere un valore non Null quando si esce dal costruttore.                                                                                                                 

namespace DbWin
{

	/// <summary>
	/// DICHIARARE QUI LE VARIABILI DEL FILE DI CONFIGURAZIONE
	/// </summary>
	public class Cfg:CfgReader
	{
		public string	CONN_server;
		public string	CONN_port;
		public string	CONN_user;
		public string	CONN_password;
		public string	CONN_database;
		public bool		INI_quick;
		public DateTime	INI_dt;
		public List<string>	P_show;
		public List<string>	P_rdonly;
		public List<string>	P_drop;
		public List<string>	C_show;
		public List<string>	C_rdonly;
		public List<string>	C_drop;
		public List<string>	A_show;
		public List<string>	A_rdonly;
		public List<string>	A_drop;
		public List<string>	S_show;
		public List<string>	S_rdonly;
		public List<string>	S_drop;
	}

	public class Msg:CfgReader
	{
		public string	MsgClosing;
		public string	MsgDisconnecting;
		public string	MsgDisconnected;
		public string	MsgConnected;
		public string	MsgNotConnected;

		public string	MnuClosing;
		public string	MnuDisconnecting;
		public string	MnuStatus;
		public string	MnuConnecting;
		public string	MnuConnect;
		public string	MnuDisconnect;
		public string	MnuParameters;
		public string	MnuConnString;
		public string	MnuSchema;
		public string	MnuProcedures;
		public string	MnuFunctions;
		public string	MnuExit;
		public string	MnuNfo;
		public string	MnuDetails;
		public string	MnuServer;
		public string	MnuPort;
		public string	MnuUser;
		public string	MnuPassword;
		public string	MnuDatabase;
		public string	MnuCancel;
		public string	MnuOK;
		public string	MnuViewCodes;
		public string	MnuQuery;
		public string	MnuConfig;
		public string	MnuViewCode;
		public string	MnuViewDescr;
		public string	MnuExplode;
	}

	public static class CFG
	{
		public enum ListType
		{
			Show,
			Readonly,
			Dropdown
		}
		public readonly static string _cfgFile = "DbWin.cfg";
		public readonly static string _msgFile = "DbWin.msg";
		
		public static Cfg Config;
		public static Msg Msg;

		static CFG()
		{
			Config = new Cfg();
			Config.CHR_ListSeparator = @";";
			Config.ReadConfiguration(_cfgFile);
#if false
			MsgBox.Show(Config.DumpEntries());
#endif
			Config.GetNames(true);
			ClearEmpyStringsInLists();

			Msg = new Msg();
			Msg.CHR_ListSeparator = @";";
			Msg.ReadConfiguration(_msgFile);
#if false
			MsgBox.Show(Msg.DumpEntries());
#endif
			Msg.GetNames(true);

		}
	
		public static string DumpEntries()
		{
			StringBuilder sb = new StringBuilder();

			FieldInfo[] finfos;
			foreach(object obj in new object[] {Config, Msg })
			{
				Type t = obj.GetType();
				sb.AppendLine($"{Environment.NewLine}{t.Name}:");
				finfos = t.GetFields();			
				foreach(FieldInfo finfo in finfos)
				{
					string name = finfo.Name;
					var prop = finfo.GetValue(obj);
					if(prop is IEnumerable<string>)
					{
						sb.Append(finfo.Name+" = {"); 
						foreach (var listitem in prop as IEnumerable<string>)	// Solo List<string>; per altri: aggiungere else if...!
						{
							sb.Append(listitem.ToString()+";");
						}
						sb.AppendLine("}");
					}
					else 
					{
						if(prop != null)
						{
							sb.AppendLine($"{finfo.Name} = {prop.ToString()}");	
						}
					}
				}
			}
			return sb.ToString();
		}
		
		public static List<string> GetList(ListType lst, string? tipo)
		{
			List<string> list = new List<string>();
			switch(tipo)
			{
				case "P":
				{
					return SelectList(lst, Config.P_show, Config.P_rdonly, Config.P_drop);
				}
				case "A":
				{
					return SelectList(lst, Config.A_show, Config.A_rdonly, Config.A_drop);
				}
				case "C":
				{
					return SelectList(lst, Config.C_show, Config.C_rdonly, Config.C_drop);
				}
				case "S":
				{
					return SelectList(lst, Config.S_show, Config.S_rdonly, Config.S_drop);
				}

			}
			return list;
		}

		private static List<string> SelectList(ListType lt, List<string> show, List<string> rdonly, List<string> dropdwn)
		{
			switch(lt)
			{
				case ListType.Show:
					return show;
				case ListType.Readonly:
					return rdonly;
				case ListType.Dropdown:
					return dropdwn;
				default:
					return new List<string>();
			}
		}
		private static void ClearEmpyStringsInLists()
		{
			List<string>[] lstl = {Config.P_show, Config.P_rdonly, Config.P_drop,Config.A_show, Config.A_rdonly, Config.A_drop,Config.C_show, Config.C_rdonly, Config.C_drop,Config.S_show, Config.S_rdonly, Config.S_drop,};
			foreach(List<string> lst in lstl)
			{
				lst.RemoveAll(s => string.IsNullOrWhiteSpace(s));
			}
		}
	}
}
#pragma warning restore CS8602
#pragma warning restore CS8618
