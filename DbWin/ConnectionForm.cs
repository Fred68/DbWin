using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DbWin
{
	public partial class ConnectionForm:Form
	{
		public ConnectionString cs;

		public ConnectionForm(ConnectionString cstr)
		{
			InitializeComponent();
			cs = new ConnectionString(cstr);
			UpdateContent();
		}

		private void button1_Click(object sender,EventArgs e)
		{
			cs.srv = tbSvr.Text;
			cs.prt = tbPrt.Text;
			cs.usr = tbUsr.Text;
			cs.pwd = tbPwd.Text;
			cs.db  = tbDb.Text;	
		}

		void UpdateContent()
		{
			tbSvr.Text = cs.srv;
			tbPrt.Text = cs.prt;
			tbUsr.Text = cs.usr;
			tbPwd.Text = cs.pwd;
			tbDb.Text = cs.db;
		}
	}
}
