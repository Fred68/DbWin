﻿using Org.BouncyCastle.Tls.Crypto;
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
			bool visib = false;

			#if DEBUG
			visib = true;
			#endif

			tbSvr.Visible = tbPrt.Visible = tbDb.Visible = visib;
			label1.Visible = label2.Visible = label5.Visible = visib;
			if(!visib)
			{
				this.Height -= tbDb.Bottom - tbPwd.Bottom;
			}
			
			this.StartPosition = FormStartPosition.CenterScreen;
			this.TopMost = true;
			this.FormBorderStyle = FormBorderStyle.Fixed3D;

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
