using System;
using SQLite;

using Slim.Utils;

namespace Slim.Models
{
	public class SlimUrl
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }

		public string FullUrl { get; set; }

		public string Hash { get; set; }

		public int Count { get; set; }

		public DateTime CreatedDate { get; set; }

		public static string GenerateRandomHash()
		{
			int hashId = new Random().Next(100000000,999999999);
			return ShortUrl.Shrink(hashId);
		}

		public override string ToString ()
		{
			return string.Format ("[SlimUrl: Id={0}, FullUrl={1}, Hash={2}, Count={3}, CreatedDate={4}]", Id, FullUrl, Hash, Count, CreatedDate);
		}
	}
}

