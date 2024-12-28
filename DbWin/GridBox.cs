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
		DataTable dtbl;
		public GridBox(DataTable dt)
		{
			InitializeComponent();
			AdjustSize();
			dtbl = dt;
			dataGridView1.DataSource = dtbl;
			this.Text = dtbl.TableName;
			#if DEBUG
			this.Text += $" {GetDataTypes()}";
			#endif
		}

		void AdjustSize()
		{
			dataGridView1.Height = ClientSize.Height - flowLayoutPanel1.Height;
		}

		private void GridBox_ResizeEnd(object sender,EventArgs e)
		{
			AdjustSize();
		}

		string GetDataTypes()
		{
			StringBuilder sb = new StringBuilder();
			if(dataGridView1.DataSource != null)
			{
				foreach(DataColumn dc in dtbl.Columns)
				{
					sb.Append($"[{dc.DataType.Name}] ");
				}
			}
			return sb.ToString();
		}

		private void btClose_Click(object sender,EventArgs e)
		{
			Close();
		}
	}
}
