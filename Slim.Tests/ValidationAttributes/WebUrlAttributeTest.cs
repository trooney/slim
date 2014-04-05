using NUnit.Framework;
using System;
using System.ComponentModel.DataAnnotations;

using Slim;
using Slim.ViewModels;

namespace Slim.Tests
{
	[TestFixture ()]
	public class WebUrlAttributeTest
	{
		[Test ()]
		public void TestInvalidUrls ()
		{
			var a = new WebUrlAttribute();

			// Malformed
			Assert.IsFalse(a.IsValid("invalid_url"));

			// No TLD
			Assert.IsFalse(a.IsValid("http://localhost"));
		}

		[Test ()]
		public void TestValidUrls ()
		{
			var a = new WebUrlAttribute();

			// Well formed
			Assert.IsTrue(a.IsValid("http://foo.bar"));

			// Extended hostname
			Assert.IsTrue(a.IsValid("http://a.b.c.foo.bar"));

			// Query parameters
			Assert.IsTrue(a.IsValid("http://foo.bar?q="));
		}
	}
}
