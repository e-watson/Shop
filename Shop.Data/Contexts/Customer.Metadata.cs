using Shop.Data.MetaData;
using System.ComponentModel.DataAnnotations;

namespace Shop.Data.Contexts
{
	[MetadataType(typeof(CustomerMetadata))]
	public partial class Customer
	{
	}
}