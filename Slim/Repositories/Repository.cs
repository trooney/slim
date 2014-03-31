using System;

using Slim.Models;
using SQLite;

namespace Slim.Repositories
{
	public class Repository<T> where T : Model, new()
	{
		protected SQLite.SQLiteConnection db;

		public Repository()
		{
			db = new SQLite.SQLiteConnection("App_Data/slim.sqlite");
		}

		public T GetById(int id)
		{
			return db.Get<T>(id);
		}

		public void Save(T entity)
		{
			if (entity.Id == null) {
				db.Insert(entity);
			} else {
				db.Update(entity);
			}


		}
	}
}

