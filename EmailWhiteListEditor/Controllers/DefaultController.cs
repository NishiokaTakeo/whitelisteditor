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
		//IConfiguration _conf;
		Interfaces.IWhiteListIO _io;
		string _path = string.Empty;
		
		public DefaultController( /*IConfiguration configuration, */Interfaces.IWhiteListIO io)
		{
			//_conf = configuration;
			//_path = _conf["WhiteLIstPath"];

			_io = io;
		}

		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new string[] { "OK" };
		}

		[HttpPost("entries/{keyword}")]
		public IEnumerable<string> Entries(string keyword = "")
		{
			// var path = _conf["WhiteLIstPath"];
			
			//var lines = System.IO.File.ReadAllLines(path);

			var list = new EmailWhiteListEditor.Models.WhiteList(_io);

			var lines = list.Entries;


			var nameFiles = lines.Where(x => x.Entiry.Contains(keyword)).Select(x => x.Entiry);

			return nameFiles;
		}
		
		[HttpPost("add/{keyword}")]
		public string AddEntry(string keyword)
		{

			var line = new Models.Line() {  Entiry = keyword, Flag = "OK"};

			var list = new EmailWhiteListEditor.Models.WhiteList(_io);


			if (list.Exists(line))
			{
				throw new EntryExistsException(line);
			}
			
			if ( !line.FormatOK() )
			{
				throw new EntryNotGoodFormatException(line);
			}


			list.Appends(new Models.Line[] { line });


			return line.GetLineForFile();
		}
		
		[HttpPost("edit/{entry}")]
		public string EditEntry(string key, string entry)
		{
			var newentry = EmailWhiteListEditor.Models.Line.Parse(entry);
			
			var sotred = _io.EditEntry(key, newentry);
			
			return sotred;
		}
		
		[HttpPost("delete/{key}/{entry}")]
		public IEnumerable<string> DeleteEntry(string key)
		{
			//var line = new Models.Line() { Entiry = entry, Flag = "OK" };

			var list = new EmailWhiteListEditor.Models.WhiteList(_io);
			list.Delete(key);
			
			_io.DeleteEntry(key);

			return list.Entries.Select(x => x.Entiry);			
		}


	}
}
