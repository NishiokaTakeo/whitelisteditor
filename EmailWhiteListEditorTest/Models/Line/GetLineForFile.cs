using System;
using NUnit.Framework;

namespace EmailWhiteListEditorTest.Models
{
	[TestFixture]
	public class GetLineForFile
	{

		[Test]
		public void GivenTextThenReturns()
		{
			var line = new EmailWhiteListEditor.Models.Line() { Entiry = "test@test.com", Flag = "OK" };

			//Act 
			var actual = line.GetLineForFile();

			// Assert
			Assert.AreEqual("test@test.com\tOK\n", actual);
		
		}


	}
}
