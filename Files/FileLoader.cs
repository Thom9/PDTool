using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PDReports.Files
{
	class FileLoader
	{
		public static void GetTXTContent()
		{
			Assembly assembly = Assembly.GetExecutingAssembly();
			const string FileName = "PDReports.Files.Crimes.txt";

			using (Stream stream = assembly.GetManifestResourceStream(FileName))
			{
				using (StreamReader reader = new StreamReader(stream))
				{
					string line;
					while((line = reader.ReadLine()) != null)
					{
						CriminalReport.CrimeListContents.Add(line);
					}
				}
			}
		}
	}
}
