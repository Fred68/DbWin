using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace DbWin
{

	#warning	AGGIUNGERE COMMENTI E SPIEGAZIONI !!!!

	public class GestioneAttivita
	{
		public class DataTableInfo
		{
			DataTable _dt;
			Task<DataTableInfo> _task;
			Attivita _att;

			public DataTable datatable
			{
				get
				{
					return _dt;
				}
			}
			public Task<DataTableInfo> task
			{
				get
				{
					return _task;
				}
			}
			public Attivita att
			{
				get
				{
					return _att;
				}
			}

			public DataTableInfo(DataTable datatable,Task<DataTableInfo> task, Attivita att)
			{
				_dt = datatable;
				_task = task;
				_att = att;
			}
		}

		public class Attivita
		{
			Task<DataTableInfo>		_task;				// Task
			Func<DataTableInfo>		_func;				// Funzione eseguita dal task
			DataTable?				_dt;				// Risultato della funzione eseguita nel task
			Action<DataTable>?		_action;			// Funzione richiamata dalla funzione chiamata al completamento del task
			CancellationToken		_token;				// Cancellation token and...
			CancellationTokenSource _cts;				// ...source
			GestioneAttivita		_gst;				// Gestione attività

			public Action<DataTable>	action
			{
				get { return _action; }
			}

			/// <summary>
			/// Ctor: crea ed avvia un task
			/// </summary>
			/// <param name="func">Func<DataTable>: funzione eseguita dal task</param>
			/// <param name="action">Action<Task<DataTable>>: funzione chiamata al completamento del task</param>
			public Attivita(Func<DataTableInfo> func, Action<DataTable> action, GestioneAttivita gst)
			{
				_cts = new CancellationTokenSource();					// Crea il token di cancellazione
				_token = _cts.Token;
				_func = func;
				_action = action;										// Funzione richiamata dalla funzione chiamata al completamento del task
				_gst = gst;
				_task = Task<DataTableInfo>.Factory.StartNew(_func,_token);
				_task.ContinueWith(_gst.AzioneAFineAttivita);
			}
		}
	
		Queue<Attivita>		_codaAttivita;
		Form				_form;
		Busy				busy; 

		/// <summary>
		/// CTOR
		/// </summary>
		/// <param name="form"></param>
		public GestioneAttivita(Form form)
		{
			_codaAttivita = new Queue<Attivita>();
			_form = form;
		}

		public void AvviaAttività(Func<DataTableInfo> func, Action<DataTable>  action)
		{
			#warning	CREARE L'ATTIVITA'
			#warning	SE NON E' BUSY: LANCIARLA
			#warning	CREARE CICLO CON TIMER PER SVUOTARE LA CODA
			busy.busy = true;
			Attivita att = new Attivita(func, action, this);
			_codaAttivita.Enqueue(att);
		}

		void AzioneAFineAttivita(Task<DataTableInfo> t)
		{
			DataTableInfo dt = t.Result;
			busy.busy = false;
			if(dt.att.action != null)
			{
				_form.BeginInvoke(new Action(() => dt.att.action(dt.datatable)));
			}
		}



	}
}
