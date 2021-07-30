using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailWhiteListEditor.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace EmailWhiteListEditor.Controllers
{
	[Produces("application/json")]
	[Route("api/[controller]")]
	public class DefaultController : Controller
	{
		Interfaces.IWhiteListIO _io;
		string _path = string.Empty;
		EmailWhiteListEditor.Models.WhiteList _whitelist;

		public DefaultController( /*IConfiguration configuration, */Interfaces.IWhiteListIO io)
		{

			_io = io;
			_whitelist = new EmailWhiteListEditor.Models.WhiteList(_io);


		}

		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new string[] { "OK" };
		}

		[HttpPost("entries/all")]
		public IEnumerable<string> Entries()
		{

			var lines = _whitelist.Entries;


			var nameFiles = lines.Select(x => x.Entiry);

			return nameFiles;
		}

		[HttpPost("entries/{keyword}")]
		public IEnumerable<string> Entries(string keyword = "")
		{
			
			var lines = _whitelist.Entries;


			var nameFiles = lines.Where(x => x.Entiry.Contains(keyword)).Select(x => x.Entiry);

			return nameFiles;
		}
		
		[HttpPost("add/{keyword}")]
		public string AddEntry(string keyword)
		{

			var line = new Models.Line() {  Entiry = keyword, Flag = "OK"};
			
			if (_whitelist.Exists(line))
			{
				throw new EntryExistsException(line);
			}
			
			if ( !line.FormatOK() )
			{
				throw new EntryNotGoodFormatException(line);
			}


			_whitelist.Appends(new Models.Line[] { line });


			return line.GetLineForFile();
		}

		//[HttpPost("edit/{entry}")]
		[HttpPost("edit/{key}/{item}")]
		public string EditEntry(string key, string item)
		{
			var newentry = new Models.Line() { Entiry = item, Flag = "OK" };

			var sotred = _whitelist.EditEntry(key, newentry);
			
			return sotred;
		}
		
		[HttpPost("delete/{key}")]
		public IEnumerable<string> DeleteEntry(string key)
		{
			_whitelist.Delete(key);

			return _whitelist.Entries.Select(x => x.Entiry);			
		}


	}
}
