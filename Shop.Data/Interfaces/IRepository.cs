using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Shop.Data.Interfaces
{
	public interface IRepository<T> where T : class
	{
		T Get(int id);

		//IEnumerable<T> Get(Expression<Func<T, bool>> filter = null,
		//	Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
		//IEnumerable<T> Get(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
		//	int skip, int take);
		//IEnumerable<T> Get(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
		//	int skip, int take);

		IEnumerable<T> Get(int skip, int take);
		IEnumerable<T> GetAll();

		void Add(T entity);
		//void Update(T entity);
		//void Delete(T entity);
		//void Delete(int id);
	}
}