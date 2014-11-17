using Shop.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Shop.Data.Infrastructure
{
	public class EFRepository<T> : IRepository<T> where T : class
	{
		protected DbContext DbContext { get; set; }
		protected DbSet<T> DbSet { get; set; }

		//private IEnumerable<T> GetPagedQuery(Expression<Func<T, bool>> filter,
		//	Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, int skip, int take)
		//{
		//	IQueryable<T> query = DbSet;

		//	if (filter != null)
		//		query = query.Where(filter);

		//	return orderBy(query).Skip(skip).Take(take);
		//}

		public EFRepository(DbContext dbContext)
		{
			if (dbContext == null)
				throw new ArgumentNullException("dbContext");

			DbContext = dbContext;
			DbSet = DbContext.Set<T>();
		}

		public virtual T Get(int id)
		{
			return DbSet.Find(id);
		}

		public virtual IEnumerable<T> Get(int skip, int take)
		{
			return DbSet.Skip(skip).Take(take);
		}

		//public virtual IEnumerable<T> Get(Expression<Func<T, bool>> filter = null,
		//	Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
		//{
		//	IQueryable<T> query = DbSet;

		//	if (filter != null)
		//		query = query.Where(filter);

		//	return orderBy != null ? orderBy(query) : query;
		//}

		//public virtual IEnumerable<T> Get(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
		//	int skip, int take)
		//{
		//	return GetPagedQuery(null, orderBy, skip, take);
		//}

		//public virtual IEnumerable<T> Get(Expression<Func<T, bool>> filter,
		//	Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, int skip, int take)
		//{
		//	return GetPagedQuery(filter, orderBy, skip, take);
		//}

		public virtual IEnumerable<T> GetAll()
		{
			return DbSet;
		}

		public virtual void Add(T entity)
		{
			var dbEntityEntry = DbContext.Entry(entity);
			if (dbEntityEntry.State == EntityState.Detached)
				DbSet.Add(entity);
			else
				dbEntityEntry.State = EntityState.Added;
		}

		//public virtual void Update(T entity)
		//{
		//	var dbEntityEntry = DbContext.Entry(entity);
		//	if (dbEntityEntry.State == EntityState.Detached)
		//	{
		//		DbSet.Attach(entity);
		//	}
		//	dbEntityEntry.State = EntityState.Modified;
		//}

		//public virtual void Update(T entity, Expression<Func<T, bool>> filter)
		//{
		//	var dbEntityEntry = DbContext.Entry(entity);
		//	if (dbEntityEntry.State != EntityState.Detached)
		//		return;

		//	var set = DbContext.Set<T>();
		//	var attachedEntity = set.FirstOrDefault(filter);

		//	if (attachedEntity != null)
		//	{
		//		var attachedEntry = DbContext.Entry(attachedEntity);
		//		attachedEntry.CurrentValues.SetValues(entity);
		//	}
		//	else
		//	{
		//		dbEntityEntry.State = EntityState.Modified;
		//	}
		//}

		//public virtual void Delete(T entity)
		//{
		//	var dbEntityEntry = DbContext.Entry(entity);
		//	if (dbEntityEntry.State == EntityState.Deleted)
		//	{
		//		DbSet.Attach(entity);
		//		DbSet.Remove(entity);
		//	}
		//	else
		//		dbEntityEntry.State = EntityState.Deleted;
		//}

		//public virtual void Delete(int id)
		//{
		//	var entity = Get(id);
		//	if (entity == null)
		//		return;

		//	Delete(entity);
		//}
	}
}