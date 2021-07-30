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

		public Line(string entry, string flag)
		{
			Entiry = entry.Trim();
			Flag = flag.Trim();
		}

		static public Line Parse(string raw)
		{
			raw = raw?.Trim();

			var line = new Line();
			//raw = (new Regex(@"(\s|\t|\r|\n)+")).Replace(raw, @"\t");

			var items = raw.Split('\t');

			line .Entiry = items.FirstOrDefault()?.Trim();

			line .Flag = items.Skip(1).FirstOrDefault()?.Trim().Replace("\n", "");

			return line;
		}

		public bool FormatOK()
		{

			//Regex regex = new Regex(@"^([\w\.\-0-9]+)@([\w\-]+)((\.(\w){2,3})+)$");
			//Match match = regex.Match(this.Entiry);

			this.Entiry = this.Entiry?.Trim();

			Regex regex2 = new Regex(@"^[^@\s]+\.[^@\s]+$");
			Match match2 = regex2.Match(this.Entiry);


			if (IsValidEmail(this.Entiry) || match2.Success)
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

		bool IsValidEmail(string email)
		{
			try
			{
				var addr = new System.Net.Mail.MailAddress(email);
				return addr.Address == email;
			}
			catch
			{
				return false;
			}
		}

	}
}
