using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbWin
{
	public class ConnectionString
	{
		public string srv;
		public string prt;
		public string usr;
		public string pwd;
		public string db;

		public ConnectionString(string server, string port, string user, string password, string database)
		{
			srv = server;
			prt = port;
			usr = user;
			pwd = password;
			db = database;
		}

		public ConnectionString(ConnectionString ocs)
		{
			srv = ocs.srv;
			prt = ocs.prt;
			usr	= ocs.usr;
			pwd	= ocs.pwd;
			db	= ocs.db;
		}

		public ConnectionString()
		{
			srv = prt = usr = pwd = db = string.Empty;
		}

		override public string ToString()
		{
			return $"server={srv};port={prt};uid={usr};pwd={pwd};database={db}";
		}

	}
}
