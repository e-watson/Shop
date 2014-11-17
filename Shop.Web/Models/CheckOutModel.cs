using Shop.Data.Contexts;

namespace Shop.Web.Models
{
	public class CheckOutModel
	{
		public Customer Customer { get; set; }
		public string EncodedOrderLines { get; set; }
	}
}