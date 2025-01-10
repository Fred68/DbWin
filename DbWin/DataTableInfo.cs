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
			DataTable			_dt;				// DataTable
			DataTableInfoFunc	_dtiFunc;			// Funzione su DataTableInfo

			/// <summary>
			/// DataTable property
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
			/// DataTableInfoFunc
			/// </summary>
			public DataTableInfoFunc dtFunc
			{
				get
				{
					return _dtiFunc;
				}
			}

			/// <summary>
			/// Numero di righe del DataTable
			/// </summary>
			public int Righe	{ get {return _dt.Rows.Count;}}
			/// <summary>
			/// Numero di colonne del DataTable
			/// </summary>
			public int Colonne	{ get {return _dt.Columns.Count;}}

			

			/// <summary>
			/// Ctor
			/// </summary>
			/// <param name="dtiFunc">Funzione su DataTableInfo</param>
			public DataTableInfo(DataTableInfoFunc dtiFunc)
			{
				_dt = new DataTable();
				_dtiFunc = dtiFunc;
			}

			/// <summary>
			/// Indice della colonna 
			/// </summary>
			/// <param name="nome">Nome della colonna</param>
			/// <returns>Indice della colomna, -1 se non trovata</returns>
			public int IndiceColonna(string nome)
			{
				int indx = -1;
				for(int i = 0; i < _dt.Columns.Count; i++)
				{
					if(_dt.Columns[i].ColumnName == nome)
						{
						indx = i;
						break;
						}
				}
				return indx;
			}

			/// <summary>
			/// Tipo di dati della colonna 
			/// </summary>
			/// <param name="nome">Nome della colonna</param>
			/// <returns>Type? oppure null se colonna non trovata</returns>
			public Type? TipoColonna(string nome)
			{
				Type? type = null;
				int indx = IndiceColonna(nome);
				if(indx != -1)
				{
					type = _dt.Columns[indx].DataType;
				}
				return type;
			}
			/// <summary>
			/// Tipo di dati della colonna 
			/// </summary>
			/// <param name="indiceColonna">Indice della colonna, da 0 a (numero colonne -1)</param>
			/// <returns>Type? oppure null se colonna non trovata</returns>
			public Type? TipoColonna(int indiceColonna)
			{
				Type? type = null;
				if( (indiceColonna>=0) && (indiceColonna<_dt.Columns.Count) )
				{
					type = _dt.Columns[indiceColonna].DataType;
				}
				return type;
			}

			/// <summary>
			/// Riga completa
			/// </summary>
			/// <param name="riga">Indice della riga</param>
			/// <returns>DataRow? oppure null se non trovata</returns>
			DataRow? Riga(int riga)
			{
				DataRow? row = null;
				if( (riga >= 0) && (riga < _dt.Rows.Count) )
				{
					row = _dt.Rows[riga];
				}
				return row;
			}

			public dynamic? this[int riga,int colonna]
			{
				get
				{
					Type? tp = TipoColonna(colonna);
					dynamic? x = null;
					DataRow? row = Riga(riga);
					if( (tp != null) && (row != null) )
					{
						x = row[colonna];
					}
					return x;
				}
			}

			public int AggiungeColonna(string nome, Type tipo, dynamic x)
			{
				int i = -1;
				if(IndiceColonna(nome) == -1)
				{


				}
				// else {}: colonna già esistente

				return i;
			}
	}


}
