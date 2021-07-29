using EmailWhiteListEditor.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EmailWhiteListEditor.Models
{
	public class WhiteList
	{

		//string _path;

		public IEnumerable<Line> Entries = new List<Line>();
		IWhiteListIO _io;

		public WhiteList(IWhiteListIO io )
		{
			//_path = path;
			_io = io;
			init();
		}

		void init()
		{
			var lines = _io.ReadAllLines();

			Entries = lines.Select(x =>
			{
				return Line.Parse(x);
			});
		}

		public bool Exists(Line line)
		{

			bool exists = Entries.ToList().Exists(x => x.Entiry == line.Entiry);

			return exists;

		}


		public void Appends(IEnumerable<Line> lines)
		{
			_io.AppendAllLines(lines);
		}

		public void Delete(string key)
		{
			_io.DeleteEntry(key);
		}

		public void EditEntry(string key, Line entry)
		{
			_io.EditEntry(key, entry);
		}

	}
}
