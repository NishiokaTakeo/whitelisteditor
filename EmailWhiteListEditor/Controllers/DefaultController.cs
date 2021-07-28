using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace EmailWhiteListEditor.Controllers
{
	[Produces("application/json")]
	[Route("api/[controller]")]
	public class DefaultController : Controller
	{
		IConfiguration _conf;
		string _path = string.Empty;
		public DefaultController( IConfiguration configuration )
		{
			_conf = configuration;
			_path = _conf["WhiteLIstPath"];
		}

		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new string[] { "Didn't", "believe", "it", "right!??" };
		}

		[HttpPost("{entries}/{keyword}")]
		public IEnumerable<string> Entries(string keyword)
		{
			var path = _conf["WhiteLIstPath"];
			
			var lines = System.IO.File.ReadAllLines(path);


			var nameFiles = lines.Where(x => x.Contains(keyword));
			//var nameFiles = ret.Select( x => System.IO.Path.GetFileNameWithoutExtension(x) );

			return nameFiles;
		}
		
		[HttpPost("{add}/{keyword}")]
		public string AddEntry(string keyword)
		{

			var line = new Models.Line() {  Entiry = keyword, Flag = "OK"};

			System.IO.File.AppendAllText(_path, line.GetLineForFile());

			return line.GetLineForFile();
		}
		
		[HttpPost("{edit}/{keyword}")]
		public IEnumerable<string> EditEntry(string item)
		{
			return null;
			// var path = _conf["WhiteLIstPath"];
			
			// var lines = System.IO.File.ReadAllLines(path);


			// var nameFiles = lines.Where(x => x.Contains(keyword));
			// //var nameFiles = ret.Select( x => System.IO.Path.GetFileNameWithoutExtension(x) );

			// return nameFiles;
		}


	}
}
