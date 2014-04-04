using System;
using SQLite;

namespace Slim.Models
{
	public enum ActivityTypes { 
		Unknown = 0, 
		Created = 1, 
		Recreated = 2,
		Redirected = 3
	}

	public class Tracking : IModel
	{
		public readonly string DefaultIp = "0.0.0.0";
		public readonly string DefaultCountryCode = "RD";
		public readonly string DefaultCountryName = "Reserved";

		[PrimaryKey, AutoIncrement]
		public int? Id { get; set; }
		public int? SlimId { get; set; }
		public ActivityTypes Activity { get; set; }
		public string Ip { get; set; }
		public string CountryCode { get; set; }
		public string CountryName { get; set; }
		public string RegionName { get; set; }
		public string City { get; set; }
		public string Zipcode { get; set; }
		public double? Latitude { get; set; }
		public double? Longitude { get; set; }
		public DateTime? CreatedDate { get; set; }


		public Tracking()
		{
			Ip = DefaultIp;
			CountryCode = DefaultCountryCode;
			CountryName = DefaultCountryName;
			CreatedDate = DateTime.Now;
		}

		public override string ToString ()
		{
			return string.Format ("[Tracking: Id={0}, SlimId={1}, Activity={2}, Ip={3}, CountryCode={4}, CountryName={5}, RegionName={6}, City={7}, Zipcode={8}, Latitude={9}, Longitude={10}, CreatedDate={11}]", Id, SlimId, Activity, Ip, CountryCode, CountryName, RegionName, City, Zipcode, Latitude, Longitude, CreatedDate);
		}

	}
}

