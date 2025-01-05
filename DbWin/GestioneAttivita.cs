using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace DbWin
{

	

	/// <summary>
	/// Classe con coda di Task
	/// </summary>
	public class GestioneAttivita
	{


		/*******************************************/
		// class Attivita
		/*******************************************/

		/// <summary>
		/// Attività 
		/// </summary>
		public class Attivita
		{
			Task<DataTableInfo>		_task;				// Task
			Func<DataTableInfo>		_func;				// Funzione eseguita dal task
			DataTableInfo			_dtInfo;			// Risultato della funzione eseguita nel task
			
			Func<DataTable>			_dtFunc;			// Funzione che restituisce un DataTable, eseguita durante il Task
			DataTableFunc			_continueFunc;		// Funzione richiamata al completamento del task
			
			CancellationToken		_token;				// Cancellation token and...
			CancellationTokenSource _cts;				// ...source
			GestioneAttivita		_gst;				// Gestione attività

			/// <summary>
			/// Action per chiamata da Form.BeginInvoke() con funzione lambda new Action(() => action) con action(DataTable)
			/// </summary>
			public DataTableFunc	action
			{
				get { return _action; }
			}

			/// <summary>
			/// Ctor: crea ed avvia un task
			/// </summary>
			/// <param name="func">Func<DataTable>: funzione eseguita dal task</param>
			/// <param name="action">Action<Task<DataTable>>: funzione chiamata al completamento del task</param>
			/// <param name="gst">classe per la gestione dell'attività</param>
			public Attivita(Func<DataTable> dtFunc, DataTableFunc continueFunc, GestioneAttivita gst)
			{
				_cts = new CancellationTokenSource();	// Crea il token di cancellazione...
				_token = _cts.Token;					// ...anche se non è usato.
				_gst = gst;

				_dtFunc = dtFunc;
				_continueFunc = continueFunc;


				//_task = new Task<DataTableInfo>(_func, _cts.Token);		// Crea il task
				_task = Task<DataTableInfo>.Factory.StartNew(_func,_token);		// Crea e avvia il task	ERRORE: CREARE FUNZIONE CHE LAVORA SU DataTableInfo, chiamando _func
				_task.ContinueWith(_gst.AzioneAFineAttivita);
				//_task.Start();
			}




		}
	
		Queue<Attivita>		_codaAttivita;				// Coda delle attività
		Form				_form;						// Form chiamante (per BeginInvoke()
		bool				_busy;						// Indicatore se ci sono attività in corso non completate

		public bool isBusy {get { return _busy; } }

		/// <summary>
		/// CTOR
		/// </summary>
		/// <param name="form"></param>
		public GestioneAttivita(Form form)
		{
			_codaAttivita = new Queue<Attivita>();
			_form = form;
			_busy = false;
		}

		//public void AvviaAttività(Func<DataTableInfo> func, Action<DataTable>  action)
		public void AvviaAttività(Func<DataTableInfo> func, DataTableFunc action)
		{
			#warning	CREARE L'ATTIVITA'
			#warning	SE NON E' BUSY: LANCIARLA
			#warning	CREARE CICLO CON TIMER PER SVUOTARE LA CODA
			_busy = true;
			Attivita att = new Attivita(func, action, this);		// ERRORE: CREARE PRIMA UNA FUNZIONE
			_codaAttivita.Enqueue(att);
		}
		
		//DataTableInfo AzioneEsecuzioneAttivita()
		//{
		//	return
		//}

		void AzioneAFineAttivita(Task<DataTableInfo> t)
		{
			DataTableInfo dt = t.Result;
			_busy = false;
			if(dt.att.action != null)
			{
				_form.BeginInvoke(new Action(() => dt.att.action(dt.datatable)));
			}
		}
	}
}
