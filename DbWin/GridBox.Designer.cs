namespace DbWin
{
	partial class GridBox
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if(disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			dataGridView1 = new DataGridView();
			flowLayoutPanel1 = new FlowLayoutPanel();
			btClose = new Button();
			((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
			flowLayoutPanel1.SuspendLayout();
			SuspendLayout();
			// 
			// dataGridView1
			// 
			dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
			dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridView1.Dock = DockStyle.Top;
			dataGridView1.Location = new Point(0,0);
			dataGridView1.Name = "dataGridView1";
			dataGridView1.ReadOnly = true;
			dataGridView1.Size = new Size(800,377);
			dataGridView1.TabIndex = 0;
			// 
			// flowLayoutPanel1
			// 
			flowLayoutPanel1.AutoSize = true;
			flowLayoutPanel1.Controls.Add(btClose);
			flowLayoutPanel1.Dock = DockStyle.Bottom;
			flowLayoutPanel1.FlowDirection = FlowDirection.RightToLeft;
			flowLayoutPanel1.Location = new Point(0,421);
			flowLayoutPanel1.Name = "flowLayoutPanel1";
			flowLayoutPanel1.Size = new Size(800,29);
			flowLayoutPanel1.TabIndex = 1;
			// 
			// btClose
			// 
			btClose.DialogResult = DialogResult.OK;
			btClose.Location = new Point(722,3);
			btClose.Name = "btClose";
			btClose.Size = new Size(75,23);
			btClose.TabIndex = 0;
			btClose.Text = "Close";
			btClose.UseVisualStyleBackColor = true;
			btClose.Click += btClose_Click;
			// 
			// GridBox
			// 
			AutoScaleDimensions = new SizeF(7F,15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800,450);
			Controls.Add(flowLayoutPanel1);
			Controls.Add(dataGridView1);
			Name = "GridBox";
			Text = "GridBox";
			ResizeEnd += GridBox_ResizeEnd;
			((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
			flowLayoutPanel1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private DataGridView dataGridView1;
		private FlowLayoutPanel flowLayoutPanel1;
		private Button btClose;
	}
}