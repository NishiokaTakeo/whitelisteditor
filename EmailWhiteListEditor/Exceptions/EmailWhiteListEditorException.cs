using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailWhiteListEditor.Exceptions
{
	public class EntryExistsException: Exception
	{
		public EntryExistsException(Models.Line line) : base($"Entry has already exists. {line.Entiry}")
		{

		}

	}

	public class EntryNotGoodFormatException : Exception
	{
		public EntryNotGoodFormatException(Models.Line line) : base($"Entry's format is matched neither email nor domain. {line.Entiry}")
		{

		}
	}

	public class EntryNotFoundException : Exception
	{
		public EntryNotFoundException(string entry) : base($"Entry not found. {entry}")
		{

		}
	}
}
