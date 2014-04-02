using System;

using SQLite;

namespace Slim.Models
{
	public class SlimGeoLog : IModel
	{
		public readonly string DefaultIp = "0.0.0.0";
		public readonly string DefaultCountryCode = "RD";
		public readonly string DefaultCountryName = "Reserved";

		[PrimaryKey, AutoIncrement]
		public int? Id { get; set; }
		public int? SlimActivityId { get; set; }
		public string Ip { get; set; }
		public string CountryCode { get; set; }
		public string CountryName { get; set; }
		public string RegionName { get; set; }
		public string City { get; set; }
		public string Zipcode { get; set; }
		public string Latitude { get; set; }
		public string Longitude { get; set; }
		public DateTime? CreatedDate { get; set; }

		public SlimGeoLog() {
			Ip = DefaultIp;
			CountryCode = DefaultCountryCode;
			CountryName = DefaultCountryName;
			CreatedDate = DateTime.Now;
		}

		public override string ToString ()
		{
			return string.Format ("[SlimGeoLog: Id={0}, SlimActivityId={1}, Ip={2}, CountryCode={3}, CountryName={4}, RegionName={5}, City={6}, Zipcode={7}, Latitude={8}, Longitude={9}, CreatedDate={10}]", Id, SlimActivityId, Ip, CountryCode, CountryName, RegionName, City, Zipcode, Latitude, Longitude, CreatedDate);
		}
	}
}

