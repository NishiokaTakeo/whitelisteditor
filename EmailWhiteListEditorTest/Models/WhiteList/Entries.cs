using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace EmailWhiteListEditorTest.Models
{
	[TestFixture]
	public class Entries
	{
		EmailWhiteListEditorTest.Mock.MockWhiteListIO _injector;
		EmailWhiteListEditor.Models.WhiteList _mgr;

		[SetUp]
		public void Setup()
		{
			_injector = new EmailWhiteListEditorTest.Mock.MockWhiteListIO();

			_mgr = new EmailWhiteListEditor.Models.WhiteList(_injector);
		}


		[Test]
		public void ShouldIgnoreInvalidItem()
		{
			// Arrange
			_injector.Create(new string[] {
				"#affinitywindows.com.au\tOK\n",
				" #yahoo.com.au\tOK\n",
				"",
				"\tOK\n",
				" ",
				"yahoo.com.au\tOK\n" });

			Assert.Greater(_mgr.Entries.Count(), 0);
			Assert.IsFalse(_mgr.Entries.Any(x => x.Entiry.Trim().Contains("#")));
			Assert.IsFalse(_mgr.Entries.Any(x => string.IsNullOrWhiteSpace( x.Entiry)));
		}

		[Test]
		public void ShouldSortedACS()
		{
			// Arrange
			var nonordered = new string[] {
				"z.com.au\tOK\n",
				"yahoo.com.au\tOK\n",
				"a.com.au\tOK\n",
				"0.com.au\tOK\n"
			 };
			_injector.Create(nonordered);
			Assert.AreEqual(nonordered.ToList().OrderBy(x => x).ToArray(), _mgr.Entries.Select(x => x.GetLineForFile()).ToArray());
			//CollectionAssert.IsOrdered(_mgr.Entries);
			//Assert.AreEqual(nonordered.ToList().OrderBy(x=>x), _mgr.Entries.Count(), 0);
			//Assert.IsFalse(_mgr.Entries.Any(x => x.Entiry.Trim().Contains("#")));
			//Assert.IsFalse(_mgr.Entries.Any(x => string.IsNullOrWhiteSpace(x.Entiry)));
		}


	}
}
