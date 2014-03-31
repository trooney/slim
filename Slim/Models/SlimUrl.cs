using System;
using System.ComponentModel.DataAnnotations;
using SQLite;

namespace Slim.Models
{
	public class SlimUrl
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }

		public string ShortUrl { get; set; }

		[StringLength(2048)]
		public string FullUrl { get; set; }

		public string Hash { get; set; }

		public DateTime CreatedDate { get; set; }

		public override string ToString ()
		{
			return string.Format ("[SlimUrl: Id={0}, ShortUrl={1}, FullUrl={2}, Hash={3}, CreatedDate={4}]", Id, ShortUrl, FullUrl, Hash, CreatedDate);
		}
	}
}

