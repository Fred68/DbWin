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
		User				= 1 << 5,
		All					= -1
	}

	public enum SQLqueryType
	{
		Reader,
		NoQuery,
		Scalar
	}

}

