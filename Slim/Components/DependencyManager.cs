using System;
using System.Collections.Generic;

using SQLite;
using Slim.Models;
using Slim.Services;
using Slim.Repositories;

namespace Slim.Components
{
	public class DependencyManager
	{
		protected SQLiteConnection db;

		public DependencyManager(SQLiteConnection db) 
		{
			this.db = db;
		}

		public T GetService<T>() where T : class
		{
			var type = typeof(T);

			if (type.IsSubclassOf(typeof(Service)) == false) {
				throw new ArgumentException(type.ToString() + " must extend Slim.Services.Service");
			}

			return Activator.CreateInstance(type, new[] { this }) as T;
		}

		public T GetRepository<T>() where T : class
		{
			var type = typeof(T);

			var rawGenericType = typeof(Repository<>);
			if (DependencyManager.IsSubclassOfRawGeneric(rawGenericType, type) == false) {
				throw new ArgumentException(type.ToString() + " must extend Slim.Repositories.Repository");
			}

			return Activator.CreateInstance(type, new[] { db }) as T;
		}

		static bool IsSubclassOfRawGeneric(Type generic, Type toCheck) {
			while (toCheck != null && toCheck != typeof(object)) {
				var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
				if (generic == cur) {
					return true;
				}
				toCheck = toCheck.BaseType;
			}
			return false;
		}

	}
}