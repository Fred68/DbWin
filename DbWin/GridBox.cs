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
	public delegate void ShowCodeFunc(string cod, string mod);
	public partial class GridBox:Form
	{
		DataTable dtbl;
		ShowCodeFunc _shw;

		public GridBox(DataTable dt, ShowCodeFunc shw)
		{
			InitializeComponent();
			AdjustSize();
			dtbl = dt;
			dataGridView1.DataSource = dtbl;
			_shw = shw;
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

		private void dataGridView1_CellDoubleClick(object sender,DataGridViewCellEventArgs e)
		{
			int row = e.RowIndex;
			if(row != -1)
			{
				DataRow drow = dtbl.Rows[row];
				string tableName = dtbl.TableName;
				
				if((tableName == CFG.Msg.MnuViewCode) || (tableName == CFG.Msg.MnuViewCodes) || (tableName ==  CFG.Msg.MnuViewDescr))
				{
					string cod, mod;
					try
					{
						cod = (string)drow["CODICE"];
						mod = (string)drow["MODIFICA"];
						_shw(cod,mod);
					}
					catch
					{
						cod = mod = string.Empty;
					}
					#if DEBUG
					MessageBox.Show(dtbl.TableName + Environment.NewLine + cod+mod);
					#endif
				}

			}
		}
	}
}
