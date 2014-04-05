using System;

namespace Slim.DTO
{
	public class ShortUrlUsage
	{
		public string Hash { get; set; }
		public int Count { get; set; }
		public string CountryCode { get; set; }
		public string CountryName { get; set; }
		public string City { get; set; }

		public override string ToString ()
		{
			return string.Format ("[ShortUrlUsage: Hash={0}, Count={1}, CountryCode={2}, CountryName={3}, City={4}]", Hash, Count, CountryCode, CountryName, City);
		}
	}
}

