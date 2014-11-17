using Shop.Data.Contexts;

namespace Shop.Data.Interfaces
{
	public interface IUnitOfWork
	{
		IRepository<Customer> CustomerRepository { get; }
		IRepository<CustomerOrder> CustomerOrderRepository { get; }
		IRepository<Order> OrderRepository { get; }
		IRepository<OrderLine> OrderLineRepository { get; }

		Customer GetCustomer();
		CustomerOrder GetCustomerOrder();
		Order GetOrder();
		OrderLine GetOrderLine();

		void Commit();
	}
}