using System;

using RestSharp;

using Slim.Components;
using Slim.Models;
using Slim.Repositories;

namespace Slim.Services
{
	// Geodata courtesty freegeoip.net
	public class SlimGeoLogService
	{
		protected SlimGeoLogRepository repository;
		protected string BaseUrl = "http://freegeoip.net";

		protected string LocalhostIp = "127.0.0.1";

		public SlimGeoLogService(SlimGeoLogRepository repository)
		{
			this.repository = repository;
		}

		public void Save(SlimGeoLog g)
		{
			repository.Save(g);
		}
			
		public SlimGeoLog GetGeoIp(string ip)
		{
			// Don't lookup localhost
			if (ip == LocalhostIp) {
				return new SlimGeoLog { 
					Ip = LocalhostIp
				};
			}

			var client = new RestClient(this.BaseUrl);

			var request = new RestRequest("json/{ip}", Method.GET);
			request.AddUrlSegment("ip", ip);

			var response = (RestResponse<SlimGeoLog>)client.Execute<SlimGeoLog>(request);

			if (response.ErrorException != null) {
				string message = "Failed to lookup GeoIp: " + response.ErrorMessage;
				throw new ApplicationException(message, response.ErrorException);
			}

			return response.Data;
		}
	}
}

