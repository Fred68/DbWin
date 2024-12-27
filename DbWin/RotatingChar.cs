using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbWin
{
	internal class RotatingChar
	{
		private char[] _ch = { '|','/','-','\\' };
		int _ich;
		ToolStripItem _itm;

		public RotatingChar(ToolStripItem item)
		{
			_itm = item;
			_ich = 0;
		}

		public void Invalidate()
		{
			_itm.Invalidate();
		}

		public void Update(bool next = false)
		{
			if(next)
			{
				_ich++;
				if(_ich >= _ch.Length)
					_ich = 0;
			}
			_itm.Text = _ch[_ich].ToString();
			_itm.Invalidate();
		}

	}
}
