using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbWin
{

	[Flags]
	public enum Info
	{
		None				= 0,
		Status				= 1 << 0,
		ConnectionString	= 1 << 1,
		Schema				= 1 << 2,
		Procedures			= 1 << 3,
		Functions			= 1 << 4,
		All					= -1
	}

	public enum SQLqueryType
	{
		Reader,
		NoQuery,
		Scalar
	}

	internal class Messages
	{

		public static class Msg
		{
			public static string Closing =			"Uscire dal programma ?";
			public static string Disconnecting =	"Disconnettersi ?";
			public static string Disconnected =		"Disconnesso";
			public static string NotConnected =		"Non connesso";

			
		}
		public static class Titles
		{
			public static string Closing =			"Uscita";
			public static string Disconnecting =	"Disconnessione";
			public static string Status =			"Stato della connessione";
		}
	}
}

