using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbWin
{
	
		/*******************************************/
		// class DataTableInfo
		/*******************************************/

		/// <summary>
		/// Classe con DataTable restituita dalle query
		/// insieme con informazioni aggiuntive sul task
		/// </summary>
		public class DataTableInfo
		{
			DataTable		_dt;
			DataTableFunc	_dtFunc;

			/// <summary>
			/// DataTable
			/// </summary>
			public DataTable datatable
			{
				get
				{
					return _dt;
				}
				set
				{
					_dt = value;
				}
			}
			
			/// <summary>
			/// DataTableFunc
			/// </summary>
			public DataTableFunc dtFunc
			{
				get
				{
					return _dtFunc;
				}
			}
			
			///// <summary>
			///// CTOR
			///// </summary>
			///// <param name="datatable"></param>
			///// <param name="task">Task<DataTableInfo></param> da eseguire
			///// <param name="att">Attivita (class) a cui appartiene il l'oggetto DataTableInfo</param>
			///// 

			/// <summary>
			/// CTOR
			/// </summary>
			/// <param name="datatable">DataTable con i valori restituiti dalla query</param>
			/// <param name="dtFunc">delegate void DataTableFunc(DataTable dt) con operazioni da eseguire al completamento del task</param>
			public DataTableInfo(DataTable datatable, DataTableFunc dtFunc)
			{
				_dt = datatable;
				_dtFunc = dtFunc;
			}
		}


}
