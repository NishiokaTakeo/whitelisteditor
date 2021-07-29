using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailWhiteListEditor.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using EmailWhiteListEditor;
using EmailWhiteListEditor.Interfaces;
using EmailWhiteListEditor.IO;
using EmailWhiteListEditor.Models;
using NUnit.Framework;

namespace EmailWhiteListEditorTest.Controllers
{
	[TestFixture]	
	public class EditEntry 
	{
		EmailWhiteListEditor.Controllers.DefaultController _controller;


		[SetUp]
		public void Setup()
		{
			var injector = new Mock.MockWhiteLIst();

			injector.Create(new string[] { "affinitywindows.com.au\tOK\n", "yahoo.com.au\tOK\n", "test@yahoo.com.au\tOK\n", "ng@ng.com.au\tOK\n" });
			
			_controller = new EmailWhiteListEditor.Controllers.DefaultController(injector);
		}

		
		[Test]
		public void GivenNotFoundKeyThenException()
		{

			// Act & Assert
			Assert.Catch<EmailWhiteListEditor.Exceptions.EntryNotFoundException>( () => _controller.EditEntry("notfound", "shouldnotbefound.com.au") );
		}


		[Test(Description = "Format exception")]
		public void GivenInValidFormatThenException()
		{

			// Act & Assert
			Assert.Catch<EmailWhiteListEditor.Exceptions.EntryNotGoodFormatException>(() => _controller.EditEntry("should raise format exception", "only"));
		}


	}
}
