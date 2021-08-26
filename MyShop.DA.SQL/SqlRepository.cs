using System.Data.Entity;
using System.Linq;
using MyShop.Core.Contracts;
using MyShop.Core.Models;

namespace MyShop.DA.SQL
{
	public class SqlRepository<T> : IRepository<T> where T : BaseEntity
	{
		private DataContext context;
		private DbSet<T> dbSet;

		public SqlRepository(DataContext context) {
			this.context = context;
			this.dbSet = context.Set<T>();
		}

		public IQueryable<T> Collection() => dbSet;

		public void Commit() => context.SaveChanges();

		public void Delete(string id) {
			var toRemove = Find(id);
			if (context.Entry(toRemove).State == EntityState.Deleted)
				dbSet.Attach(toRemove);
			dbSet.Remove(toRemove);
		}

		public T Find(string id) => dbSet.Find(id);

		public void Insert(T entity) => dbSet.Add(entity);

		public void Update(T entity) {
			dbSet.Attach(entity);
			context.Entry(entity).State = EntityState.Modified;
		}
	}
}