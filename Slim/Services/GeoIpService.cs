using System;

using RestSharp;
using AutoMapper;

using Slim.Models;

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

		// Looks up GeoData service using t.Ip
		// Then maps the response back onto t
		public void GetGeoIpAsync(Tracking t, Action<Tracking> successCallback)
		{
			var client = CreateGeoIpClient();
			var request = CreateGeoIpRequest(t.Ip);

			client.ExecuteAsync<GeoIp>(request, response => {
				if (response.Data == null) {
					return;
				}

				Mapper.Map<GeoIp, Tracking>(response.Data, t);

				successCallback(t);
			});
		}
			
	}
}

