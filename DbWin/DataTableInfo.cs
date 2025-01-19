﻿using System;
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

			/// <summary>
			/// Class DtiSingle
			/// Coppia DataTable (non nullable) + DataTableInfoFunc (nullable)
			/// </summary>
			class DtiSingle
				{
					public DataTable			_dt;				// DataTable
					public DataTableInfoFunc?	_dtiFunc;			// Funzione su DataTableInfo

					public DtiSingle(DataTable dt, DataTableInfoFunc? dtiFunc)
					{
						_dt = dt;
						_dtiFunc = dtiFunc;
					}
				}
			
			/*******************************************/
			// Static
			/*******************************************/

			static DtiSingle _dtisNullo;
			
			/// <summary>
			/// Ctor statico
			/// </summary>
			static DataTableInfo()
			{
				_dtisNullo = new DtiSingle(new DataTable(), null);		// Crea un elemento 'null' di riferimento per _primo
			}

			/*******************************************/
			// Private
			/*******************************************/

			Queue<DtiSingle> _dtQ;
			DtiSingle _primo;

			/*******************************************/
			// Proprietà
			/*******************************************/

			/// <summary>
			/// Numero di elementi
			/// </summary>
			public int Count { get { return _dtQ.Count; } }

			/// <summary>
			/// DataTable dell'elemento meno recente della coda
			/// </summary>
			public DataTable datatable
			{
				get
				{
					return _primo._dt;
				}
				set
				{
					if(_dtQ.Count == 0)
					{
						Accoda(value);			// Usa funzione privata Enqueue(DataTable datatable)
					}
					else
					{
						_dtQ.Peek()._dt = value;	
					}
				}
			}
			
			/// <summary>
			/// DataTableInfoFunc dell'elemento meno recente della coda
			/// </summary>
			public DataTableInfoFunc? dtFunc
			{
				get
				{
					return _primo._dtiFunc;
				}
			}

			/// <summary>
			/// Numero di righe del DataTable dell'elemento meno recente della coda
			/// </summary>
			public int Righe	{ get {return _primo._dt.Rows.Count;}}

			/// <summary>
			/// Numero di colonne del DataTable dell'elemento meno recente della coda
			/// </summary>
			public int Colonne	{ get {return _primo._dt.Columns.Count;}}


			/*******************************************/
			// Ctor e metodi
			/*******************************************/


			/// <summary>
			/// Ctor
			/// </summary>
			/// <param name="dtiFunc">Funzione su DataTableInfo</param>
			public DataTableInfo(DataTableInfoFunc dtiFunc)
			{
				_dtQ = new Queue<DtiSingle>();
				_primo = _dtisNullo;
				Accoda(dtiFunc);
			}

			/// <summary>
			/// Inserisce nella coda
			/// </summary>
			/// <param name="dtiFunc">DataTableInfoFunc</param>
			/// <param name="datatable"></param>
			public void Accoda(DataTableInfoFunc dtiFunc, DataTable datatable)
			{
				_dtQ.Enqueue(new DtiSingle(datatable,dtiFunc));
				_primo = _dtQ.Peek();
			}

			/// <summary>
			/// Inserisce nella coda
			/// </summary>
			/// <param name="dtiFunc">DataTableInfoFunc</param>
			public void Accoda(DataTableInfoFunc dtiFunc)
			{
				Accoda(dtiFunc, new DataTable());
			}

			/// <summary>
			/// Inserisce nella coda elemento con puntatore a funzione nullo
			/// </summary>
			/// <param name="datatable"></param>
			private void Accoda(DataTable datatable)
			{
				_dtQ.Enqueue(new DtiSingle(datatable,null));
				_primo = _dtQ.Peek();
			}

			/// <summary>
			/// Rimuove l'elemento più vecchio della coda
			/// </summary>
			/// <param name="dtiFunc"></param>
			/// <param name="datatable"></param>
			public void Rimuovi(out DataTableInfoFunc? dtiFunc, out DataTable? datatable)
			{
				if(_dtQ.Count == 0)
				{
					dtiFunc = null;
					datatable = null;
				}
				else
				{
					DtiSingle tmp = _dtQ.Dequeue();
					dtiFunc = tmp._dtiFunc;
					datatable = tmp._dt;
					_primo = (_dtQ.Count > 0) ? _dtQ.Peek() : _dtisNullo;
				}

			}

			/// <summary>
			/// Indice della colonna 
			/// </summary>
			/// <param name="nome">Nome della colonna</param>
			/// <returns>Indice della colomna, -1 se non trovata</returns>
			public int IndiceColonna(string nome)
			{
				int indx = -1;
				for(int i = 0; i < _primo._dt.Columns.Count; i++)
				{
					if(_primo._dt.Columns[i].ColumnName == nome)
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
					type = _primo._dt.Columns[indx].DataType;
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
				if( (indiceColonna>=0) && (indiceColonna<_primo._dt.Columns.Count) )
				{
					type = _primo._dt.Columns[indiceColonna].DataType;
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
				if( (riga >= 0) && (riga < _primo._dt.Rows.Count) )
				{
					row = _primo._dt.Rows[riga];
				}
				return row;
			}

			/// <summary>
			/// Accesso con indici di rigae di colonna
			/// </summary>
			/// <param name="riga">Indice della riga</param>
			/// <param name="colonna">Indice della colonna</param>
			/// <returns></returns>
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

			/// <summary>
			/// Aggiunge una colonna
			/// </summary>
			/// <param name="nome">Nome della colonna</param>
			/// <param name="tipo">Usare typeof(...)</param>
			/// <param name="x">Valore da inserire</param>
			/// <returns>Il numero della nuova colonna o -1 se errore</returns>
			/// <exception cref="Exception"></exception>
			public int AggiungeColonna(string nome, Type tipo, dynamic x)
			{
				int i = -1;
				if(IndiceColonna(nome) == -1)
				{
					_primo._dt.Columns.Add(nome, tipo);
					int iNuova = IndiceColonna(nome);
					if(iNuova != -1)
					{
						DataRow? row = Riga(0);
						if( (tipo != null) && (row != null) )
						{
							 row[iNuova] = x;
							 i = iNuova;

						}
						else
						{
							throw new Exception($"Errore durante la lettura della riga [0]");
						}
					}
					else
					{
						throw new Exception($"Errore durante l'aggiunta di una colonna");
					}

				}
				// else {}: colonna già esistente

				return i;
			}
	}


}
