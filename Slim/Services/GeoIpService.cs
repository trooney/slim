using System;

using RestSharp;

using Slim.Components;
using Slim.Models;
using Slim.Repositories;

namespace Slim.Services
{
	public class GeoIpService
	{
		protected string ApiBaseUrl = "http://freegeoip.net";

		protected string ApiEndpoint = "json/{ip}";

		protected string ApiSegment = "ip";

		protected string LocalhostIp = "127.0.0.1";

		protected RestClient CreateGeoIpClient()
		{
			return new RestClient(ApiBaseUrl);
		}

		protected RestRequest CreateGeoIpRequest(string ip)
		{
			var request = new RestRequest(ApiEndpoint, Method.GET);
			request.AddUrlSegment(ApiSegment, ip);
			return request;
		}

		public GeoIp GetGeoIp(string ip)
		{
			var client = CreateGeoIpClient();
			var request = CreateGeoIpRequest(ip);

			var response = (RestResponse<GeoIp>)client.Execute<GeoIp>(request);

			client.ExecuteAsync(request, r => {
				Console.WriteLine("this is the async request");
				Console.WriteLine(r.Content);
			});

			if (response.ErrorException != null) {
				return null;
			}

			return response.Data;
		}

		public void BindGeoIpData(Tracking t, string ip)
		{
			GeoIp g = GetGeoIp(ip);

			if (g == null) {
				return;
			}

			t.Ip = g.Ip;
			t.CountryCode = g.CountryCode;
			t.CountryName = g.CountryName;
			t.RegionName = g.RegionName;
			t.City = g.City;
			t.Zipcode = g.Zipcode;
			t.Latitude = g.Latitude;
			t.Longitude = g.Longitude;
		}

		public static void BindGeoIpData(GeoIp g, Tracking t)
		{
			t.Ip = g.Ip;
			t.CountryCode = g.CountryCode;
			t.CountryName = g.CountryName;
			t.RegionName = g.RegionName;
			t.City = g.City;
			t.Zipcode = g.Zipcode;
			t.Latitude = g.Latitude;
			t.Longitude = g.Longitude;
		}

		public void BindGeoIpDataUsingAsyncThenSave(TrackingService trackingService, Tracking t, string ip)
		{
			var client = CreateGeoIpClient();
			var request = CreateGeoIpRequest(ip);

			client.ExecuteAsync<GeoIp>(request, response => {
				Console.WriteLine("ASYNC REQUEST COMPLETED");
				if (response.Data == null) {
					return;
				}
				GeoIpService.BindGeoIpData(response.Data, t);

				Console.WriteLine("SAVE COMPLETED");
				trackingService.Save(t);
			});
		}
	}
}

