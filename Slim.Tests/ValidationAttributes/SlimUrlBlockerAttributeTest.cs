using NUnit.Framework;
using System;
using System.ComponentModel.DataAnnotations;

using Slim;
using Slim.ViewModels;

namespace Slim.Tests
{
	[TestFixture ()]
	public class SlimUrlBlockerAttributeTest
	{
		[Test ()]
		public void TestBlockingSlimUrls ()
		{
			var a = new SlimUrlBlockerAttribute();

			// Slim domain
			Assert.IsFalse(a.IsValid("http://slimurl.org"));

			// Slim redirect
			Assert.IsFalse(a.IsValid("http://slimurl.org/r/123"));
		}

		[Test ()]
		public void TestAllowingOtherUrls ()
		{
			var a = new SlimUrlBlockerAttribute();

			// Well formed
			Assert.IsTrue(a.IsValid("http://foo.bar"));
		}
	}
}