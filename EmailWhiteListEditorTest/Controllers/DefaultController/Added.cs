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
	public class AddEntry
	{
		EmailWhiteListEditor.Controllers.DefaultController _controller;

		Mock.MockWhiteListIO _injector;
		[SetUp]
		public void Setup()
		{
			_injector = new Mock.MockWhiteListIO();
			
			_controller = new EmailWhiteListEditor.Controllers.DefaultController(_injector);
		}

		

		[Test(Description = "Format exception")]
		[TestCase("doubleAt@@yahoo.com.au")]
		[TestCase("nodomain@")]
		[TestCase(".au")]
		[TestCase("dodomain")]
		public void GivenInValidFormatThenException(string item)
		{

			// Act & Assert
			Assert.Catch<EmailWhiteListEditor.Exceptions.EntryNotGoodFormatException>(() => _controller.AddEntry(item));
		}


		[Test(Description = "Add Success")]
		[TestCase("test@yahoo.com.au")]
		[TestCase("yahoo.com.au")]
		public void GivenEntryThenAdded(string item)
		{
			// Arrange
			var expect = $"{item}\tOK\n";
			//_injector.Create(new string[] { $"{item}\tOK\n" });


			// Act
			var actual = _controller.AddEntry(item);

			// Assert
			Assert.AreEqual(expect, actual);
			Assert.AreEqual(expect,_injector.ReadAllLines()[0] );
		}


		[Test(Description = "Add Success")]
		[TestCase("test@yahoo.com.au")]
		[TestCase("yahoo.com.au")]
		public void GivenDuplicatedEntryThenAdded(string item)
		{
			// Arrange
			var expect = $"{item}\tOK\n";
			_injector.Create(new string[] { $"{item}\tOK\n" });


			// Act
			Assert.Catch<EmailWhiteListEditor.Exceptions.EntryExistsException>(() => _controller.AddEntry(item));


		}


	}
}
