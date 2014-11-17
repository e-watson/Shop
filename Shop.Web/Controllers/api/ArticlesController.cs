using Shop.Data.Interfaces;
using Shop.Data.Models;
using Shop.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Shop.Web.Controllers.api
{
	public class ArticlesController : ApiController
	{
		private const string ImagePathFormat = "Content/Images/{0}";
		private readonly IXmlUnitOfWork _xmlUnitOfWork;

		public ArticlesController(IXmlUnitOfWork xmlUnitOfWork)
		{
			_xmlUnitOfWork = xmlUnitOfWork;
		}

		[HttpGet]
		[Route("article/t")]
		public int GetArticlesTotal()
		{
			var articleEntities = _xmlUnitOfWork.ArticleRepository.GetAll();
			return articleEntities.Count();
		}

		[HttpGet]
		[Route("article/{pageNumber:int}/{pageSize:int}")]
		public IEnumerable<ArticleListModel> GetArticlesForPage(int pageNumber, int pageSize)
		{
			IEnumerable<Article> articlesModel;

			if (pageSize == 0)
				articlesModel = _xmlUnitOfWork.ArticleRepository.GetAll();
			else
			{
				var skip = (pageNumber - 1) * pageSize;
				articlesModel = _xmlUnitOfWork.ArticleRepository.Get(skip, pageSize);
			}

			return articlesModel.Select(x => new ArticleListModel
			{
				Id = x.Id,
				Name = x.Name,
				Image = string.Format(ImagePathFormat, x.Image),
				Price = x.Price
			});
		}

		[HttpGet]
		[Route("article/{id:int}")]
		public ArticleDetailsModel GetArticleById(int id)
		{
			var article = _xmlUnitOfWork.ArticleRepository.Get(id);
			if (article == null)
				return null;

			return new ArticleDetailsModel
			{
				Id = article.Id,
				Name = article.Name,
				Description = article.Description,
				Image = string.Format(ImagePathFormat, article.Image),
				Price = article.Price
			};
		}

		[HttpPost]
		[Route("article")]
		public IEnumerable<ArticleListModel> PostArticle([FromBody]IEnumerable<int> idData)
		{
			var articles = _xmlUnitOfWork.ArticleRepository.GetAll();
			return articles.Where(x => idData.Contains(x.Id)).Select(x => new ArticleListModel
			{
				Id = x.Id,
				Name = x.Name,
				Image = string.Format(ImagePathFormat, x.Image),
				Price = x.Price
			});
		}
	}
}