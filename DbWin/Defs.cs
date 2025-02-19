﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbWin
{
	public delegate void CodRevFunc(string cod, string mod);		// Delegate per operazioni su codice e revisione
	public delegate void DataTableInfoFunc(DataTableInfo dti);		// Delegate per operazioni su DataTableInfo


	/// <summary>
	/// Connection information 
	/// </summary>
	[Flags]
	public enum Info
	{
		None				= 0,
		/// <summary>
		/// Connection status
		/// </summary>
		Status				= 1 << 0,
		/// <summary>
		/// Connection string
		/// </summary>
		ConnectionString	= 1 << 1,
		/// <summary>
		/// Database schema
		/// </summary>
		Schema				= 1 << 2,
		/// <summary>
		/// Database procedures
		/// </summary>
		Procedures			= 1 << 3,
		/// <summary>
		/// Database functions
		/// </summary>
		Functions			= 1 << 4,
		/// <summary>
		/// Current user
		/// </summary>
		User				= 1 << 5,
		All					= -1
	}

	/// <summary>
	/// MySQL query type
	/// </summary>
	public enum SQLqueryType
	{
		Reader,
		NoQuery,
		Scalar
	}

	/// <summary>
	/// Task execution options
	/// </summary>
	[Flags]
	public enum TaskOptions
	{
		/// <summary>
		/// Do not execute any function after task
		/// </summary>
		None						=	0,
		/// <summary>
		/// Start task and continue without waiting task completion
		/// </summary>
		DoNotWait					=	1 << 0,
		/// <summary>
		/// Execute function after task completion
		/// </summary>
		ExecAfterTask				=	1 << 1,
		Default						=	DoNotWait | ExecAfterTask
	}
}

