using System;
using System.Collections.Generic;
using System.Linq;
using MyShop.Core.Contracts;
using MyShop.Core.Models;

namespace MyShop.UI.Tests.Mocks
{
	public class MockContext<T> : IRepository<T> where T : BaseEntity
	{
		List<T> items;
		string className;

		public MockContext() {
			items = new List<T>();
		}

		public IQueryable<T> Collection() => items.AsQueryable();

		public void Commit() {
			return;
		}

		public void Delete(string id) {
			var t = items.FirstOrDefault(r => r.Id == id);
			if (t == null) {
				throw new Exception("Product not found");
			}
			items.Remove(t);
		}

		public T Find(string id) {
			var t = items.Find(r => r.Id == id);
			if (t == null) {
				throw new Exception("Product not found");
			}
			return t;
		}

		public void Insert(T entity) => items.Add(entity);

		public void Update(T entity) {
			var t = items.Find(r => r.Id == entity.Id);
			if (t == null) {
				throw new Exception($"{className} not found");
			}
			t = entity;
		}
	}
}