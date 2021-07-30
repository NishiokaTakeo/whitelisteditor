using System;
using System.Collections.Generic;
using EmailWhiteListEditor.Models;
using System.Linq;

namespace EmailWhiteListEditorTest.Mock
{
	internal class MockWhiteListIO : EmailWhiteListEditor.Interfaces.IWhiteListIO
	{
		List<string> _entries = new List<string>();

		public void Create(string[] entries)
		{
			_entries.AddRange(entries);
		}

		public void AppendAllLines(IEnumerable<Line> entries)
		{
			foreach (var entry in entries)
				_entries.Add(entry.GetLineForFile());
		}

		public void DeleteEntry(string key)
		{
			_entries = _entries.Where(x => Line.Parse(x).Entiry != key).ToList();
		}

		public string[] ReadAllLines()
		{
			return _entries.ToArray();
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
	}
}
