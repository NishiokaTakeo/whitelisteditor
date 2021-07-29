using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EmailWhiteListEditor.Models
{
	public class Line
	{
		
		public string Entiry { get; set; } = "";

		public string Flag { get; set; } = "";

		string  _raw ;

		public Line()
		{

		}

		public Line(string entry)
		{
			_raw = entry;
		}


		static public Line Parse(string raw)
		{
			var line = new Line();
			var items = raw.Split('\t');

			line .Entiry = items.FirstOrDefault();

			line .Flag = items.Skip(1).FirstOrDefault()?.Replace("\n", "");

			return line;
		}

		public bool FormatOK()
		{

			Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
			Match match = regex.Match(this.Entiry);

			Regex regex2 = new Regex(@"^((?!-)[A-Za-z0-9-]{1, 63}(?<!-)\\.)+[A-Za-z]{2, 6}$");
			Match match2 = regex.Match(this.Entiry);


			if (match.Success || match2.Success)
			{
				return true;
			}
			else
			{
				return false;
			}

		}
		public string GetLineForFile()
		{
			string str = $"{Entiry}\t{Flag}\n";

			return str;
		}
	}
}
