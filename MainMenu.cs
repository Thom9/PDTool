using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace PDReports
{
	public partial class MainMenu : Form
	{
		public MainMenu()
		{
			InitializeComponent();
		}

		private void Button1_Click(object sender, EventArgs e)
		{
			Form crimReport = new CriminalReport();
			crimReport.Show();
		}

		private void Button2_Click(object sender, EventArgs e)
		{

		}

		public const int WM_NCLBUTTONDOWN = 0xA1;
		public const int HT_CAPTION = 0x2;

		[System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
		public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
		[System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
		public static extern bool ReleaseCapture();

		private void AllowMoveWindow(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				ReleaseCapture();
				SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
			}
		}

		private void Panel1_MouseDown(object sender, MouseEventArgs e)
		{
			AllowMoveWindow(e);
		}

		private void Label1_MouseDown(object sender, MouseEventArgs e)
		{
			AllowMoveWindow(e);
		}

		private void PictureBox1_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void PictureBox2_Click(object sender, EventArgs e)
		{
			this.WindowState = FormWindowState.Minimized;
		}

		private void Button2_Click_1(object sender, EventArgs e)
		{
			Form devDatasetTool = new CrimeCalc();
			devDatasetTool.Show();
		}

		private void MainMenu_Load(object sender, EventArgs e)
		{
			CrimeCalc.Crime.PopulateCrimeList();
		}

		private void Button2_Click_2(object sender, EventArgs e)
		{
			Form CrimeCalc = new CrimeCalc();
			CrimeCalc.Show();
		}

		private void Button3_Click(object sender, EventArgs e)
		{
		}
	}
}
