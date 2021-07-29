using EmailWhiteListEditor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailWhiteListEditor.Interfaces
{
	public interface IWhiteListIO
	{
		string[] ReadAllLines();

		void AppendAllLines(IEnumerable<Line> entries);

		void DeleteEntry(string key);

		string EditEntry(string key, Line entry);

	}
}
