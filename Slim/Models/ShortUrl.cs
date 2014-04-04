using System;
using SQLite;

using Slim.Components;

namespace Slim.Models
{
	public class ShortUrl : IShortUrl, IModel
	{
		[PrimaryKey, AutoIncrement]
		public int? Id { get; set; }
		public string FullUrl { get; set; }
		public string Hash { get; set; }
		public int Count { get; set; }
		public DateTime? CreatedDate { get; set; }

		public ShortUrl()
		{
			CreatedDate = DateTime.Now;
		}

		public static string GenerateRandomHash()
		{
			int hashId = new Random().Next(100000000,999999999);
			return UrlMunger.Shrink(hashId);
		}

		public override string ToString ()
		{
			return string.Format ("[ShortUrl: Id={0}, FullUrl={1}, Hash={2}, Count={3}, CreatedDate={4}]", Id, FullUrl, Hash, Count, CreatedDate);
		}
	}
}

