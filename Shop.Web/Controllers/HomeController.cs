using Shop.Data.Contexts;
using Shop.Data.Interfaces;
using Shop.Web.Helpers;
using Shop.Web.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Shop.Web.Controllers
{
	public class HomeController : Controller
	{
		private const int Success = 1;
		private const int Error = 0;

		private readonly IUnitOfWork _unitOfWork;
		private readonly IXmlUnitOfWork _xmlUnitOfWork;

		private int CreateOrder(string title, string encodedOrderLines)
		{
			var orderLines = OrderLinesHelper.DecodeOrderLines(encodedOrderLines);
			if (!orderLines.Any())
				return Error;

			var order = _unitOfWork.GetOrder();
			order.Code = string.Format("SH-{0}", title.Length > 20 ? title.Substring(0, 20) : title);

			_unitOfWork.OrderRepository.Add(order);
			_unitOfWork.Commit();

			if (order.Id <= 0)
				return Error;

			foreach (var line in orderLines)
			{
				var orderLine = _unitOfWork.GetOrderLine();

				orderLine.ArticleId = line.Id;
				orderLine.OrderId = order.Id;
				orderLine.Quantity = line.Quantity;

				_unitOfWork.OrderLineRepository.Add(orderLine);
			}

			_unitOfWork.Commit();

			return order.Id;
		}

		private int CreateCustomer(int orderId, CheckOutModel model)
		{
			var customer = _unitOfWork.GetCustomer();

			customer.Title = model.Customer.Title;
			customer.FirstName = model.Customer.FirstName;
			customer.LastName = model.Customer.LastName;
			customer.Address = model.Customer.Address;
			customer.HouseNumber = model.Customer.HouseNumber;
			customer.ZipCode = model.Customer.ZipCode;
			customer.City = model.Customer.City;
			customer.Email = model.Customer.Email;

			_unitOfWork.CustomerRepository.Add(customer);
			_unitOfWork.Commit();

			if (customer.Id <= 0)
				return Error;

			var customerOrder = _unitOfWork.GetCustomerOrder();

			customerOrder.CustomerId = customer.Id;
			customerOrder.OrderId = orderId;

			_unitOfWork.CustomerOrderRepository.Add(customerOrder);
			_unitOfWork.Commit();

			return customerOrder.Id <= 0 ? Error : customer.Id;
		}

		public HomeController(IUnitOfWork unitOfWork, IXmlUnitOfWork xmlUnitOfWork)
		{
			_unitOfWork = unitOfWork;
			_xmlUnitOfWork = xmlUnitOfWork;
		}

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Checkout()
		{
			var customer = _unitOfWork.GetCustomer();
			var checkOutModel = new CheckOutModel
			{
				Customer = customer
			};

			return View(checkOutModel);
		}

		[HttpPost]
		public ActionResult Checkout(CheckOutModel model)
		{
			try
			{
				if (!ModelState.IsValid)
					return RedirectToAction("Checkout");

				#region Business logic layer

				var orderId = CreateOrder(model.Customer.Title, model.EncodedOrderLines);
				if (orderId == Error)
					return RedirectToAction("Checkout");

				var customerId = CreateCustomer(orderId, model);
				if (customerId == Error)
					return RedirectToAction("Checkout");

				#endregion

				return RedirectToAction("ThankYou", new { id = Success });
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
				return RedirectToAction("Checkout");
			}
		}

		public ActionResult ThankYou(int id)
		{
			return View(id);
		}
	}
}