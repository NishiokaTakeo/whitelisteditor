using System;
using System.Collections.Generic;
using EmailWhiteListEditor.Models;
using System.Linq;

namespace EmailWhiteListEditorTest.Mock
{
	internal class MockWhiteLIst : EmailWhiteListEditor.Interfaces.IWhiteListIO
	{
		List<string> _entries = new List<string>();

		public void Create(string[] entries)
		{
			_entries.AddRange(entries);
		}

		public void AppendAllLines(IEnumerable<Line> entries)
		{
			throw new NotImplementedException();
		}

		public void DeleteEntry(string key)
		{
			throw new NotImplementedException();
		}

		public string[] ReadAllLines()
		{
			return _entries.ToArray();
		}

		public string EditEntry(string key, Line entry)
		{
			if (!ReadAllLines().ToList().Exists(x => Line.Parse(x).Entiry == key))
			{
				throw new EmailWhiteListEditor.Exceptions.EntryNotFoundException(key);
			}

			DeleteEntry(key);
			
			AppendAllLines(new Line[] { entry });

			return entry.GetLineForFile();

		}
	}
}
