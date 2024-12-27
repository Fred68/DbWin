using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbWin
{
	internal class Busy
	{
		bool _busy;
		ToolStripItem _itm;
		string[] txt;

		public bool busy
		{
			get
			{
				return _busy;
			}
			set
			{
				_busy = value;
				_itm.Text = txt[busy ? 0 : 1];
				_itm.Invalidate();
			}

		}

		public void Invalidate()
		{
			_itm.Invalidate();
		}

		public Busy(ToolStripItem item,string busy,string not_busy)
		{
			txt = new string[2];
			txt[0] = busy;
			txt[1] = not_busy;
			_busy = false;
			_itm = item;
		}

	}
}
