using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




#warning	*** DA FARE ***

#warning	--->>> CAPIRE COME ACCODARE PIU' CONANDI ASINCRONI IN SEQUENZA E RICHIAMARLI. usando DataTableInfo

#warning	*** TABELLE CON I NOMI DI costruttori, materiali, prodotti.
//#warning	Lettura delle tabelle (funzioni MySQL).
#warning	Verifica se refresh necessario
#warning	Scrittura dopo refresh
#warning	Lettura e scrittura dei contenuti delle tabelle: costruttori, materiali, prodotti.

#warning	*** CODICI ***

#warning	*** FORM DI INPUT ***
#warning	Form dinamico, con:
//#warning	- tipo di dato o lista valori, label, readonly, allinea sotto o a lato...
#warning	- pulsanti extra (con delegate)
#warning	- Eventuale posizionamento

#warning	-->>> DAR FARE !!! Funzione per la lettura dei valori di un DataTableInfo. Preparare la DataTableInfo con gli array dei campi letti, numero di righe ecc...
#warning	Form per input, leggendo campi e valori da datatable (e configurazione)

#warning	*** GUI ***
#warning	Icona da disegnare

#warning	*** RICERCA ***

#warning	Rivedere Busy necessario ? Assicurarsi che i task in sequenza vengano avviati con il flag Wait.
//#warning	Provare a lanciare due task, aumentando il ritardo a 10 secondi. Vedere se MySql è multithread oppure no.
#warning	Assicurarsi che tutte le chiamate allochino oggetti DataTableInfo distinti.
#warning	Controllare il flag busy per tutti i comandi. Ma imporre busy solo per quelli di modifica o inserimento di codici.

namespace DbWin	{}
