using System;

namespace Slim.DTO
{
	public class ShortUrlCountryUsage
	{
		public string Hash { get; set; }
		public int Count { get; set; }
		public string CountryCode { get; set; }
		public string CountryName { get; set; }

		public override string ToString ()
		{
			return string.Format ("[ShortUrlCountries: Hash={0}, Count={1}, CountryCode={2}, CountryName={3}]", Hash, Count, CountryCode, CountryName);
		}
	}
}

