using NUnit.Framework;
using System;

using Slim.Components;

namespace Slim.Tests
{
	[TestFixture ()]
	public class UrlMungerTest
	{
		[Test ()]
		public void MungeId ()
		{
			int expected = 1000;
			int result = UrlMunger.Expand(UrlMunger.Shrink(expected));
			Assert.AreEqual(expected, result);
		}
	}
}
