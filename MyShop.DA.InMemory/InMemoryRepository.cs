using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using MyShop.Core.Contracts;
using MyShop.Core.Models;

namespace MyShop.DA.InMemory
{
	public class InMemoryRepository<T> : IRepository<T> where T : BaseEntity
	{
		ObjectCache cache = MemoryCache.Default;
		List<T> genericList;
		string className;

		public InMemoryRepository() {
			className = typeof(T).Name;
			genericList = cache[className] as List<T>;
			if (genericList == null) {
				genericList = new List<T>();
			}
		}

		public void Commit() => cache[className] = genericList;

		public void Insert(T entity) => genericList.Add(entity);

		public void Update(T entity) {
			var t = genericList.Find(r => r.Id == entity.Id);
			if (t == null) {
				throw new Exception($"{className} not found");
			}
			t = entity;
		}

		public T Find(string id) {
			var t = genericList.Find(r => r.Id == id);
			if (t == null) {
				throw new Exception("Product not found");
			}
			return t;
		}

		public IQueryable<T> Collection() => genericList.AsQueryable();

		public void Delete(string id) {
			var t = genericList.FirstOrDefault(r => r.Id == id);
			if (t == null) {
				throw new Exception("Product not found");
			}
			genericList.Remove(t);
		}
	}
}