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
	public class DeleteEntry 
	{
		EmailWhiteListEditor.Controllers.DefaultController _controller;


		[SetUp]
		public void Setup()
		{

		}

		

		//[Test(Description = "Format exception")]
		//public void GivenInValidFormatThenException()
		//{

		//	// Act & Assert
		//	Assert.Catch<EmailWhiteListEditor.Exceptions.EntryNotGoodFormatException>(() => _controller.AddEntry("only"));
		//}


		[Test(Description = "Deleted Success")]
		[TestCase("test2@yahoo.com.au")]
		public void GivenEntryThenDelete(string entry)
		{
			// Arrange
			var injector = new Mock.MockWhiteListIO();
			injector.Create(new string[] { "test2@yahoo.com.au\tOK\n" });
			_controller = new EmailWhiteListEditor.Controllers.DefaultController(injector);


			// Act
			var actual = _controller.DeleteEntry(entry);
			
			// Assert
			Assert.IsFalse(actual.ToList().Exists(x => Line.Parse(x).Entiry == entry));			
		}

		[Test(Description = "Deleted Success")]
		[TestCase("test2@yahoo.com.au")]
		public void GivenNotFoundEntryThenNothing(string entry)
		{
			// Arrange
			var injector = new Mock.MockWhiteListIO();
			injector.Create(new string[] { "test@yahoo.com.au\tOK\n" });
			_controller = new EmailWhiteListEditor.Controllers.DefaultController(injector);


			// Act
			var actual = _controller.DeleteEntry(entry);


			// Assert
			Assert.AreEqual(actual.Count(), 1);
		}

	}
}
