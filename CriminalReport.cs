using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using PDReports.Files;

namespace PDReports
{
	public partial class CriminalReport : Form
	{
		public static List<string> CrimeListContents = new List<string>();
		public List<string> RemovedCrimes = new List<string>();

		public List<string> ActiveCrimes = new List<string>();

		public CriminalReport()
		{
			InitializeComponent();
			FileLoader.GetTXTContent();
			InitializeCrimeBox();
		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}

		private void CrimReportClick(object sender, EventArgs e)
		{
			panel1.Visible = true;
		}

		private void EvidenceLogClick(object sender, EventArgs e)
		{
			//panel2.Visible = true;
		}

		private void CrimReportPanel(object sender, PaintEventArgs e)
		{

		}

		private void Button4_Click(object sender, EventArgs e)
		{
			panel1.Visible = false;
		}
		
		public string SelectedItem;

		private void ListBox1_DoubleClick(object sender, EventArgs e)
		{
			SelectedItem = listBox1.SelectedItem.ToString();
			int SelectedIndex = listBox1.SelectedIndex;
			panel2.Visible = false;
			listBox2.Items.Add(SelectedItem);
		}

		private void Button2_Click(object sender, EventArgs e)
		{
			panel2.Visible = true;
		}


		private void InitializeCrimeBox()
		{
			foreach(string item in CrimeListContents)
			{
				listBox1.Items.Add(item);
			}

			listBox2.Items.Insert(0, "");
			listBox2.Items.Clear();
			foreach(string item in ActiveCrimes)
			{
				listBox2.Items.Add(item);
			}
		}

		private void PopulateCrimesBox()
		{
			foreach(string item in RemovedCrimes)
			{
				listBox1.Items.Add(item);
			}
			RemovedCrimes.Clear();
		}

		private void PopulateCrimesBoxEx(string text)
		{
			for(int i = RemovedCrimes.Count - 1; i >= 0; i--)
			{
				if(RemovedCrimes[i].ToLower().Contains(text.ToLower()))
				{
					listBox1.Items.Add(RemovedCrimes[i]);
					RemovedCrimes.RemoveAt(i);
				}
			}
		}

		private void UpdateCrimeBox(string text)
		{
			for(int i = listBox1.Items.Count - 1; i >= 0; i--)
			{
				if(!listBox1.Items[i].ToString().ToLower().Contains(text))
				{
					RemovedCrimes.Add(listBox1.Items[i].ToString());
					listBox1.Items.RemoveAt(i);
					
				}
			}
		}

		public int TextLength;

		private void TextBox21_TextChanged(object sender, EventArgs e)
		{
			if(TextLength > textBox21.Text.Length) // if current text length is smaller than last recorded
				PopulateCrimesBoxEx(textBox21.Text);
				

			if(textBox21.Text == "")
				PopulateCrimesBox();
			else
				UpdateCrimeBox(textBox21.Text.ToLower());

			TextLength = textBox21.Text.Length;
		}

		private void Button6_Click(object sender, EventArgs e)
		{
			try
			{
				SelectedItem = listBox1.SelectedItem.ToString();
				listBox2.Items.Add(SelectedItem);
				ActiveCrimes.Add(SelectedItem);
			}
			catch (Exception)
			{
				
			}
		}

		private void Button7_Click(object sender, EventArgs e)
		{
			if(listBox2.SelectedItem != null)
			{
				string SelectedItem = listBox2.SelectedItem.ToString();
				listBox2.Items.Remove(SelectedItem);
				ActiveCrimes.Remove(SelectedItem);
			}
		}

		private void Button8_Click(object sender, EventArgs e)
		{
			
		}

		private void PictureBox1_Click(object sender, EventArgs e)
		{
			this.Visible = false;
		}

		public const int WM_NCLBUTTONDOWN = 0xA1;
		public const int HT_CAPTION = 0x2;

		[System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
		public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
		[System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
		public static extern bool ReleaseCapture();

		/*
		private void Panel1_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				ReleaseCapture();
				SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
			}
		}
		*/

		private void AllowMoveWindow(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				ReleaseCapture();
				SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
			}
		}

		private void Panel3_MouseDown(object sender, MouseEventArgs e)
		{
			AllowMoveWindow(e);
		}

		private void Label19_MouseDown(object sender, MouseEventArgs e)
		{
			AllowMoveWindow(e);
		}

		private void PictureBox3_Click(object sender, EventArgs e)
		{
			string Forename = textBox1.Text;
			string Surname = textBox2.Text;
			string PhNum = textBox3.Text;
			string Address = textBox4.Text;
			string BusinessOne = textBox5.Text;
			string BusinessTwo = textBox6.Text;
			string BusinessThree = textBox7.Text;
			string MugshotURL = textBox8.Text;
			string VehModel = textBox9.Text;
			string VehPlate = textBox10.Text;
			string VehColour = textBox11.Text;

			string LStrikes = textBox13.Text;
			string ConfItems = textBox14.Text;
			string ImpoundedVehicles = textBox15.Text;
			string Officers = textBox16.Text;
			string CruiseNum = textBox17.Text;
			string EventSum = textBox18.Text;
			string DateTime = textBox19.Text;
			string AddNotes = textBox20.Text;

			string Crimes = "";
			foreach (string item in ActiveCrimes)
			{
				Crimes = Crimes + item + "\r\n";
			}

			List<string> CrimReport = new List<string> { };
			CrimReport.Add("[CENTER]");
			CrimReport.Add("[IMG]http://i.imgur.com/jUIyIJs.png[/IMG]");
			CrimReport.Add("[B][FONT=Arial]Criminal Resources Department[/B][/CENTER]");
			CrimReport.Add("[LEFT][FONT=Arial]");
			CrimReport.Add("[SIZE=4]");

			// Section 1

			CrimReport.Add("[B][U]Section 1 - Suspect Information[/U][/B][/SIZE]");

			CrimReport.Add("[SIZE=3]");
			CrimReport.Add("[B]Forename[/B]: " + Forename);
			CrimReport.Add("[B]Surname[/B]: " + Surname);

			CrimReport.Add("[B]Contact Details[/B][/SIZE][/FONT][/LEFT]");
			CrimReport.Add("[INDENT][LEFT][FONT=Arial][SIZE=3][B]Phone Number[/B]: " + PhNum);
			CrimReport.Add("[B]Home Address[/B]: " + Address);
			CrimReport.Add("[B]Known Businesses[/B]: \r\n" + "- " + BusinessOne + "\r\n" + "- " + BusinessTwo + "\r\n" + "- " + BusinessThree);
			CrimReport.Add("[/SIZE][/FONT][/LEFT][/INDENT]");


			CrimReport.Add("[FONT=Arial][SIZE=3][B]Vehicle Information[/B][/SIZE][/FONT]");
			CrimReport.Add("[INDENT][FONT=Arial][SIZE=3][B]Vehicle Type[/B]: " + VehModel);
			CrimReport.Add("[B]Vehicle Registration[/B]: " + VehPlate);
			CrimReport.Add("[B]Vehicle Colour[/B]: " + VehColour);
			CrimReport.Add("[/SIZE][/FONT][/INDENT]");

			CrimReport.Add("[FONT=Arial][B][SIZE=3]Mugshot[/SIZE][/B][SIZE=3]:[/SIZE] \r\n[IMG]" + MugshotURL + "[/IMG]");
			CrimReport.Add("linebreak");

			// Section 2

			CrimReport.Add("[B][U][SIZE=4]Section 2 - Crime Information[/SIZE][/U][/B]");



			CrimReport.Add("[B][SIZE=3]Crimes Committed[/SIZE][/B][SIZE=3]: " + Crimes + "[/SIZE]");

			CrimReport.Add("[B][SIZE=3]License Strikes Issued[/SIZE][/B][SIZE=3]: " + LStrikes + "[/SIZE] \r\n");

			CrimReport.Add("[B][SIZE=3]Items Confiscated[/SIZE][/B][SIZE=3]: \r\n" + ConfItems + "[/SIZE]");

			CrimReport.Add("[B][SIZE=3]Vehicle(s) Impounded[/SIZE][/B][SIZE=3]: \r\n" + ImpoundedVehicles + "[/SIZE]");


			// Section 3

			CrimReport.Add("[B][U][SIZE=4]Section 3 - Arrest Information[/SIZE][/U][/B]");
			CrimReport.Add("[SIZE=3][B]Arresting Officer(s)[/B]: \r\n" + Officers);
			CrimReport.Add("[B]Cruiser Number[/B]: " + CruiseNum + "\r\n");
			CrimReport.Add("[B]Summary of events[/B]: " + EventSum + "\r\n");
			CrimReport.Add("[B]Time and Date[/B]: " + DateTime);
			CrimReport.Add("[B]Associated Reports[/B]: ");
			CrimReport.Add("[B]Additional Notes[/B]: + " + AddNotes + "[/SIZE][/FONT]");
			CrimReport.Add("linebreak");
			CrimReport.Add("[/FONT]");


			string CrimFormat = "";

			foreach (var i in CrimReport)
			{
				string line = i;
				if (line == "linebreak")
					line = "\r\n";

				CrimFormat += line + "\r\n";
			}

			Clipboard.SetText(CrimFormat);
		}

		private void PictureBox3_Click_1(object sender, EventArgs e)
		{
			string Forename = textBox1.Text;
			string Surname = textBox2.Text;
			string PhNum = textBox3.Text;
			string Address = textBox4.Text;
			string BusinessOne = textBox5.Text;
			string BusinessTwo = textBox6.Text;
			string BusinessThree = textBox7.Text;
			string MugshotURL = textBox8.Text;
			string VehModel = textBox9.Text;
			string VehPlate = textBox10.Text;
			string VehColour = textBox11.Text;

			string LStrikes = textBox13.Text;
			string ConfItems = textBox14.Text;
			string ImpoundedVehicles = textBox15.Text;
			string Officers = textBox16.Text;
			string CruiseNum = textBox17.Text;
			string EventSum = textBox18.Text;
			string DateTime = textBox19.Text;
			string AddNotes = textBox20.Text;

			string Crimes = "";
			foreach (string item in ActiveCrimes)
			{
				Crimes = Crimes + item + "\r\n";
			}

			List<string> CrimReport = new List<string> { };
			CrimReport.Add("[CENTER]");
			CrimReport.Add("[IMG]http://i.imgur.com/jUIyIJs.png[/IMG]");
			CrimReport.Add("[B][FONT=Arial]Criminal Resources Department[/B][/CENTER]");
			CrimReport.Add("[LEFT][FONT=Arial]");
			CrimReport.Add("[SIZE=4]");

			// Section 1

			CrimReport.Add("[B][U]Section 1 - Suspect Information[/U][/B][/SIZE]");

			CrimReport.Add("[SIZE=3]");
			CrimReport.Add("[B]Forename[/B]: " + Forename);
			CrimReport.Add("[B]Surname[/B]: " + Surname);

			CrimReport.Add("[B]Contact Details[/B][/SIZE][/FONT][/LEFT]");
			CrimReport.Add("[INDENT][LEFT][FONT=Arial][SIZE=3][B]Phone Number[/B]: " + PhNum);
			CrimReport.Add("[B]Home Address[/B]: " + Address);
			CrimReport.Add("[B]Known Businesses[/B]: \r\n" + "- " + BusinessOne + "\r\n" + "- " + BusinessTwo + "\r\n" + "- " + BusinessThree);
			CrimReport.Add("[/SIZE][/FONT][/LEFT][/INDENT]");


			CrimReport.Add("[FONT=Arial][SIZE=3][B]Vehicle Information[/B][/SIZE][/FONT]");
			CrimReport.Add("[INDENT][FONT=Arial][SIZE=3][B]Vehicle Type[/B]: " + VehModel);
			CrimReport.Add("[B]Vehicle Registration[/B]: " + VehPlate);
			CrimReport.Add("[B]Vehicle Colour[/B]: " + VehColour);
			CrimReport.Add("[/SIZE][/FONT][/INDENT]");

			CrimReport.Add("[FONT=Arial][B][SIZE=3]Mugshot[/SIZE][/B][SIZE=3]:[/SIZE] \r\n[IMG]" + MugshotURL + "[/IMG]");
			CrimReport.Add("linebreak");

			// Section 2

			CrimReport.Add("[B][U][SIZE=4]Section 2 - Crime Information[/SIZE][/U][/B]");



			CrimReport.Add("[B][SIZE=3]Crimes Committed[/SIZE][/B][SIZE=3]: " + Crimes + "[/SIZE]");

			CrimReport.Add("[B][SIZE=3]License Strikes Issued[/SIZE][/B][SIZE=3]: " + LStrikes + "[/SIZE] \r\n");

			CrimReport.Add("[B][SIZE=3]Items Confiscated[/SIZE][/B][SIZE=3]: \r\n" + ConfItems + "[/SIZE]");

			CrimReport.Add("[B][SIZE=3]Vehicle(s) Impounded[/SIZE][/B][SIZE=3]: \r\n" + ImpoundedVehicles + "[/SIZE]");


			// Section 3

			CrimReport.Add("[B][U][SIZE=4]Section 3 - Arrest Information[/SIZE][/U][/B]");
			CrimReport.Add("[SIZE=3][B]Arresting Officer(s)[/B]: \r\n" + Officers);
			CrimReport.Add("[B]Cruiser Number[/B]: " + CruiseNum + "\r\n");
			CrimReport.Add("[B]Summary of events[/B]: " + EventSum + "\r\n");
			CrimReport.Add("[B]Time and Date[/B]: " + DateTime);
			CrimReport.Add("[B]Associated Reports[/B]: ");
			CrimReport.Add("[B]Additional Notes[/B]: + " + AddNotes + "[/SIZE][/FONT]");
			CrimReport.Add("linebreak");
			CrimReport.Add("[/FONT]");


			string CrimFormat = "";

			foreach (var i in CrimReport)
			{
				string line = i;
				if (line == "linebreak")
					line = "\r\n";

				CrimFormat += line + "\r\n";
			}

			Clipboard.SetText(CrimFormat);
		}

		private void Button1_Click(object sender, EventArgs e)
		{
			panel2.Visible = false;
		}
	}

}
