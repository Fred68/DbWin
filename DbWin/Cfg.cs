using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Fred68.CfgReader;

namespace DbWin
{

	/// <summary>
	/// DICHIARARE QUI LE VARIABILI DEL FILE DI CONFIGURAZIONE
	/// </summary>
	public class Cfg2 : CfgReader
	{
		public string CONN_server;	
		public string CONN_port;
		public string CONN_user;
		public string CONN_password;
		public string CONN_database;
	}

	public class Msg2 : CfgReader
	{
		public string MsgClosing;
		public string MsgDisconnecting;
		public string MsgDisconnected;
		public string MsgConnected;
		public string MsgNotConnected;
		
		public string MnuClosing;
		public string MnuDisconnecting;
		public string MnuStatus;	
		public string MnuConnecting;
		public string MnuConnect;
		public string MnuDisconnect;
		public string MnuParameters;
		public string MnuConnString;
		public string MnuSchema;
		public string MnuProcedures;
		public string MnuFunctions;
		public string MnuExit;
		public string MnuNfo;
		public string MnuDetails;

	}

	public static class Cfg 
	{
		public static Cfg2 Config;
		public static Msg2 Msg;

		static Cfg()
		{
			bool bCfg2, bMsg2;
			Config = new Cfg2();
			Config.CHR_ListSeparator = @";";
			Config.ReadConfiguration("DbWin.cfg");
			#if DEBUG
			MessageBox.Show(Config.DumpEntries());
			#endif
			Config.GetNames(true);

			Msg = new Msg2();
			Msg.CHR_ListSeparator = @";";
			Msg.ReadConfiguration("DbWin.msg");
			#if DEBUG
			MessageBox.Show(Msg.DumpEntries());
			#endif
			Msg.GetNames(true);
			
		}
	}
}
