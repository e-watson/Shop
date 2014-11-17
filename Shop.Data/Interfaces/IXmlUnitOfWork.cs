using Shop.Data.Models;

namespace Shop.Data.Interfaces
{
	public interface IXmlUnitOfWork
	{
		IRepository<Article> ArticleRepository { get; }
	}
}