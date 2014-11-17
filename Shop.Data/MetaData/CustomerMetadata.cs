using System.ComponentModel.DataAnnotations;

namespace Shop.Data.MetaData
{
	public class CustomerMetadata
	{
		[Required(ErrorMessage = "Title is required")]
		[StringLength(50)]
		[Display(Name = "Title (*)")]
		public string Title { get; set; }

		[Required(ErrorMessage = "First name is required")]
		[StringLength(50)]
		[Display(Name = "First name (*)")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Last name is required")]
		[StringLength(50)]
		[Display(Name = "Last name (*)")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Address is required")]
		[StringLength(100)]
		[Display(Name = "Address (*)")]
		public string Address { get; set; }

		[Required(ErrorMessage = "House number is required")]
		[StringLength(10)]
		[Display(Name = "House number (*)")]
		public string HouseNumber { get; set; }

		[Required(ErrorMessage = "Zip code is required")]
		[StringLength(10)]
		[Display(Name = "Zip code (*)")]
		public string ZipCode { get; set; }

		[Required(ErrorMessage = "City is required")]
		[StringLength(50)]
		[Display(Name = "City (*)")]
		public string City { get; set; }

		[Required(ErrorMessage = "Email is required")]
		[StringLength(50)]
		[Display(Name = "Email (*)")]
		[RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
		public string Email { get; set; }
	}
}