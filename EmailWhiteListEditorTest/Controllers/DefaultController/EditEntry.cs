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
			var injector = new Mock.MockWhiteListIO();

			injector.Create(new string[] { "affinitywindows.com.au\tOK\n", "yahoo.com.au\tOK\n", "test@yahoo.com.au\tOK\n", "notgoodformat\tOK\n", "ng@ng.com.au\tOK\n" });
			
			_controller = new EmailWhiteListEditor.Controllers.DefaultController(injector);
		}

		


		[Test(Description = "Format exception")]
		public void GivenInValidFormatThenException()
		{

			// Act & Assert
			Assert.Catch<EmailWhiteListEditor.Exceptions.EntryNotGoodFormatException>(() => _controller.EditEntry("should raise format exception", "only"));
		}

		[Test(Description = "Edit Success")]
		[TestCase("test2@yahoo.com.au")]
		public void GivenEntryThenEdited(string entry)
		{
			// Arrange

			// Act
			var actual = _controller.EditEntry("test@yahoo.com.au", entry);

			// Assert
			Assert.AreEqual(new Line(entry, "OK").GetLineForFile(), actual);
			
		}


	}
}
