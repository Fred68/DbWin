using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DbWin
{
	public partial class GridBox:Form
	{
		public GridBox(DataTable dt)
		{
			InitializeComponent();
			AdjustSize();
			dataGridView1.DataSource = dt;
		}

		void AdjustSize()
		{
			dataGridView1.Height = ClientSize.Height - flowLayoutPanel1.Height;
		}

		private void GridBox_ResizeEnd(object sender,EventArgs e)
		{
			AdjustSize();	
		}
	}
}
