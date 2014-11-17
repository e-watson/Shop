using Shop.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Shop.Data.Infrastructure
{
	public class XmlRepository<T> : IRepository<T> where T : class
	{
		private readonly string _entityName;
		private readonly XDocument _xDocument;
		private IEnumerable<XElement> _xElements;

		private IEnumerable<XElement> XElements
		{
			get { return _xElements ?? (_xElements = _xDocument.Descendants(_entityName)); }
		}

		private IEnumerable<T> XmlSet
		{
			get { return XElements.Select(x => (T)(dynamic)x); }
		}

		public XmlRepository(string fileName, string entityName)
		{
			_entityName = entityName;
			_xDocument = XDocument.Load(fileName);
		}

		public virtual T Get(int id)
		{
			var query = _xElements.Where(x => (int) x.Attribute("Id") == id);
			return (T) (dynamic) (query.FirstOrDefault());
		}

		public virtual IEnumerable<T> Get(int skip, int take)
		{
			return XmlSet.Skip(skip).Take(take);
		}

		public virtual IEnumerable<T> GetAll()
		{
			return XmlSet;
		}

		//public async Task<IEnumerable<T>> GetS()
		//{
		//	var entities = await XmlSet.AsQueryable().ToListAsync();
		//	return entities;
		//}

		#region Data manipulation methods

		public virtual void Add(T entity)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}