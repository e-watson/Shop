using Shop.Data.Interfaces;
using Shop.Data.Models;

namespace Shop.Data.Infrastructure
{
	public class XmlUnitOfWork : IXmlUnitOfWork
	{
		private const string FileName = @"D:\Projects\Shop\Shop.Web\App_Data\Articles.xml";
		private IRepository<Article> _articleRepository;

		public IRepository<Article> ArticleRepository
		{
			get
			{
				return _articleRepository
					?? (_articleRepository = new XmlRepository<Article>(FileName, "Article"));
			}
		}
	}
}