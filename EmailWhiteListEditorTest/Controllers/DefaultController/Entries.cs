using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace EmailWhiteListEditorTest.Controllers
{
	[TestFixture]	
	public class Entries
	{
		EmailWhiteListEditor.Controllers.DefaultController _controller;
		[SetUp]
		public void Setup()
		{
			var injector = new Mock.MockWhiteListIO();

			injector.Create( new string[] { "affinitywindows.com.au\tOK\n", "yahoo.com.au\tOK\n", "test@yahoo.com.au\tOK\n", "ng@ng.com.au\tOK\n" });

			_controller = new EmailWhiteListEditor.Controllers.DefaultController(injector);
		}

		
		[Test]
		public void GivenNothingThenReturnsAll()
		{

			IEnumerable<string> entries = _controller.Entries("");

			Assert.Greater( entries.Count() , 0);

		}

		[Test]
		public void GivenPartOfTextThenReturnsAll()
		{
			// Arrange
			var keyword = "yahoo";
			
			// Act
			IEnumerable<string> entries = _controller.Entries(keyword);

			// Assert
			Assert.Greater(entries.Count(), 0);
			var actual = entries.All(x => x.Contains(keyword));
			Assert.IsTrue(actual);

		}


	}
}
