using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using EmailWhiteListEditor.Models;
using EmailWhiteListEditor.Exceptions;

namespace EmailWhiteListEditor.IO
{
	public class WhiteListIO : EmailWhiteListEditor.Interfaces.IWhiteListIO
	{
		string _path;
		public WhiteListIO(string path)
		{
			_path = path;
		}

		public void AppendAllLines(IEnumerable<Line> entries)
		{
			File.AppendAllLines(_path, entries.Select(x => x.GetLineForFile()));

		}

		public void DeleteEntry(string key)
		{
			key = key?.Trim();
			
			var exits = ReadAllLines().ToList().Exists(x => Line.Parse(x).Entiry == key);

			if (!exits )
			{
				throw new EntryNotFoundException(key);
			}

			// Replace entry
			var deleted = ReadAllLines().Where(x => Line.Parse(x).Entiry != key);
			
			File.WriteAllLines(_path, deleted);
		}

		public string EditEntry(string key, Line entry)
		{

			if (!entry.FormatOK())
			{
				throw new EmailWhiteListEditor.Exceptions.EntryNotGoodFormatException(entry);
			}

			DeleteEntry(key);

			AppendAllLines(new Line[] { entry });

			return entry.GetLineForFile();
		}

		public string[] ReadAllLines()
		{
			return File.ReadAllLines(_path);
		}
	}
}
