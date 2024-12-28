using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using Fred68.CfgReader;
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
	}

	public static class CFG
	{
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
			//sb.AppendLine($"Configuration files:{Environment.NewLine}{_cfgFile}{Environment.NewLine}{_msgFile}");
			FieldInfo[] finfos;
			foreach(object obj in new object[] {Config, Msg })
				{
				Type t = obj.GetType();
				sb.AppendLine($"{Environment.NewLine}{t.Name}:");
				
				finfos = t.GetFields();			
				
				foreach(FieldInfo finfo in finfos)
					{
					sb.AppendLine($"{finfo.Name} = {finfo.GetValue(obj).ToString()}");
					}
			}
			return sb.ToString();
		}
	}
}
#pragma warning restore CS8602
#pragma warning restore CS8618
