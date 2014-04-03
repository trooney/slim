using System;

namespace Slim.Models
{
	public class GeoIp
	{
		public string Ip { get; set; }
		public string CountryCode { get; set; }
		public string CountryName { get; set; }
		public string RegionName { get; set; }
		public string City { get; set; }
		public string Zipcode { get; set; }
		public double? Latitude { get; set; }
		public double? Longitude { get; set; }
	}
}