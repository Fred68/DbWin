
using Fred68.GenDictionary;

namespace InputForm
{
	public class InputForm : Form
	{
		
		class Info
		{
			string _nome;
			string _label;
			bool _readonly;
			bool _aLato;
			bool _isDropList;
		}



		GenDictionary _values;


		public InputForm()
		{
			_values = new GenDictionary();

		}
	}
}
