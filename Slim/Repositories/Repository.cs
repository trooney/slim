using System;

using Slim.Models;
using SQLite;

namespace Slim.Repositories
{
	public class Repository<T> where T : Model, new()
	{
		protected SQLiteConnection db;

		public Repository(SQLiteConnection db)
		{
			this.db = db;
		}

		public T GetById(int id)
		{
			// db.Get<T>(id);
			return db.Table<T>().Where( r => r.Id.Equals(id) ).FirstOrDefault();
		}

		public void Save(T entity)
		{
			if (entity.Id == 0) {
				db.Insert(entity);
			} else {
				db.Update(entity);
			}
		}
	}
}

