using System;
using SQLite;

namespace Slim.Models
{
	public enum DocketTypes { 
		Unknown = 0, 
		Created = 1, 
		Recreated = 2,
		Redirected = 3
	}

	public class Docket : IModel
	{
		public readonly string DefaultIp = "0.0.0.0";
		public readonly string DefaultCity = "Reserved";
		public readonly string DefaultCountryCode = "RD";
		public readonly string DefaultCountryName = "Reserved";

		[PrimaryKey, AutoIncrement]
		public int? Id { get; set; }
		[Indexed("IX_ShortUrl", 1)]
		public int? ShortUrlId { get; set; }
		public DocketTypes Activity { get; set; }
		public string Ip { get; set; }
		[Indexed("IX_CountryCode", 2)]
		public string CountryCode { get; set; }
		public string CountryName { get; set; }
		public string RegionName { get; set; }
		[Indexed("IX_City", 3)]
		public string City { get; set; }
		public string Zipcode { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
		public DateTime CreatedDate { get; set; }


		public Docket()
		{
			Ip = DefaultIp;
			City = DefaultCity;
			CountryCode = DefaultCountryCode;
			CountryName = DefaultCountryName;
			CreatedDate = DateTime.Now;
		}

		public override string ToString ()
		{
			return string.Format ("[Docket: Id={0}, ShortUrlId={1}, Activity={2}, Ip={3}, CountryCode={4}, CountryName={5}, RegionName={6}, City={7}, Zipcode={8}, Latitude={9}, Longitude={10}, CreatedDate={11}]", Id, ShortUrlId, Activity, Ip, CountryCode, CountryName, RegionName, City, Zipcode, Latitude, Longitude, CreatedDate);
		}

	}
}

