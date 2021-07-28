using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailWhiteListEditor.Models
{
	public class Line
	{
		public string Entiry { get; set; } = "";

		public string Flag { get; set; } = "";


		public string GetLineForFile()
		{
			string str = $"{Entiry}\t{Flag}";

			return str;
		}
	}
}
