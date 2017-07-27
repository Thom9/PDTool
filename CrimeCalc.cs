using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Reflection;

namespace PDReports
{
	public partial class CrimeCalc : Form
	{
		public CrimeCalc()
		{
			InitializeComponent();
			InitializeCrimeBox();
		}

		public class Crime
		{
			public int cID;
			public string cName;
			public int cTime;
			public int cFine;
			public int cStrikes;
			public string cAddNotes;

			public static List<Crime> CrimeList = new List<Crime>{};

			public static void PopulateCrimeList()
			{
				Assembly assembly = Assembly.GetExecutingAssembly();
				const string FileName = "PDReports.Files.CrimeData.txt";
				using (Stream stream = assembly.GetManifestResourceStream(FileName))
				{
					using (StreamReader reader = new StreamReader(stream))
					{
						while(!reader.EndOfStream)
						{
							String[] values = reader.ReadLine().Split(',');
							values[0] = Regex.Replace(values[0], "\"", string.Empty);
							values[1] = Regex.Replace(values[1], "\"", string.Empty);
							values[2] = Regex.Replace(values[2], "\"", string.Empty);
							values[3] = Regex.Replace(values[3], "\"", string.Empty);
							values[4] = Regex.Replace(values[4], "\"", string.Empty);
							values[5] = Regex.Replace(values[5], "\"", string.Empty);

							Crime crime = new Crime{};
							crime.cID = Convert.ToInt16(values[0]);
							crime.cName = values[1];
							crime.cTime = Convert.ToInt32(values[2]);
							crime.cFine = Convert.ToInt32(values[3]);
							crime.cStrikes = Convert.ToInt32(values[4]);
							crime.cAddNotes = values[5];
							CrimeList.Add(crime);

						}
					}
				}
			}

		}

		public List<string> RemovedCrimeData = new List<string> { };

		private void UpdateCrimeBox(string text)
		{
			for (int i = listBox1.Items.Count - 1; i >= 0; i--)
			{
				if (!listBox1.Items[i].ToString().ToLower().Contains(text.ToLower()))
				{
					RemovedCrimeData.Add(listBox1.Items[i].ToString());
					listBox1.Items.RemoveAt(i);
				}
			}
		}

		private void UpdateCrimeBoxEx(string text)
		{
			for (int i = RemovedCrimeData.Count - 1; i >= 0; i--)
			{
				if (RemovedCrimeData[i].ToLower().Contains(text.ToLower()))
				{
					listBox1.Items.Add(RemovedCrimeData[i]);
					RemovedCrimeData.RemoveAt(i);
				}
			}
		}

		private void InitializeCrimeBox()
		{
			listBox1.Items.Clear();
			foreach (var item in Crime.CrimeList)
			{
				listBox1.Items.Add(item.cName);
			}
		}

		int TextLength;
		private void TextBox1_TextChanged(object sender, EventArgs e)
		{
			if (TextLength > textBox1.Text.Length) // if current text length is smaller than last recorded
				UpdateCrimeBoxEx(textBox1.Text);


			if (textBox1.Text == "")
				InitializeCrimeBox();
			else
				UpdateCrimeBox(textBox1.Text);

			TextLength = textBox1.Text.Length;
		}

		private void PictureBox1_Click(object sender, EventArgs e)
		{
			this.WindowState = FormWindowState.Minimized;
		}

		private void PictureBox2_Click(object sender, EventArgs e)
		{
			this.Visible = false;
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

		private void DisplayCrimeInfo(string crime)
		{
			foreach(var c in Crime.CrimeList)
			{
				if(c.cName == crime)
				{
					label7.Text = crime;
					label8.Text = c.cTime.ToString() + " minutes.";
					label9.Text = "$" + c.cFine.ToString() + ".";
					label10.Text = c.cStrikes.ToString() + " license strikes.";
					label11.Text = c.cAddNotes;
				}
			}
		}

		string SelectedItem = "";

		private void ListBox1_DoubleClick(object sender, EventArgs e)
		{
			if (listBox1.SelectedItem != null)
			{
				SelectedItem = listBox1.SelectedItem.ToString();
				DisplayCrimeInfo(SelectedItem);
			}
		}
		private void ListBox1_KeyDown(object sender, KeyEventArgs e)
		{
			switch(e.KeyCode)
			{
				case Keys.Enter:
				{
					if(listBox1.SelectedItem != null)
					{
						SelectedItem = listBox1.SelectedItem.ToString();
						DisplayCrimeInfo(SelectedItem);
					}
					break;
				}
			}
		}
	}
}
