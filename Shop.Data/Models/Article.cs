using System.Xml.Linq;

namespace Shop.Data.Models
{
	public class Article
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Image { get; set; }
		public decimal Price { get; set; }

		public static explicit operator Article(XElement xElement)
		{
			if (xElement == null)
				return null;

			return new Article
			{
				Id = (int)xElement.Attribute("Id"),
				Name = (string)xElement.Attribute("Name"),
				Description = (string)xElement.Attribute("Description"),
				Image = (string)xElement.Attribute("Image"),
				Price = (decimal)xElement.Attribute("Price")
			};
		}
	}
}