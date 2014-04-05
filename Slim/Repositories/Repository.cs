using System;

using Slim.Models;
using SQLite;

namespace Slim.Repositories
{
	public abstract class Repository<T> where T : IModel, new()
	{
		protected SQLiteConnection db;

//		public Repository(SQLiteConnection db)
//		{
//			this.db = db;
//		}

		public T GetById(int id)
		{
			return db.Table<T>().Where( r => r.Id.Equals(id) ).FirstOrDefault();
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

