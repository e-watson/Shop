using System;
using Shop.Data.Contexts;
using Shop.Data.Interfaces;

namespace Shop.Data.Infrastructure
{
	public class EFUnitOfWork : IUnitOfWork, IDisposable
	{
		private IRepository<Customer> _customerRepository;
		private IRepository<CustomerOrder> _customerOrderRepository;
		private IRepository<Order> _orderRepository;
		private IRepository<OrderLine> _orderLineRepository;

		private ShopDbContext DbContext { get; set; }

		public IRepository<Customer> CustomerRepository
		{
			get
			{
				return _customerRepository
					?? (_customerRepository = new EFRepository<Customer>(DbContext));
			}
		}

		public IRepository<CustomerOrder> CustomerOrderRepository
		{
			get
			{
				return _customerOrderRepository
					?? (_customerOrderRepository = new EFRepository<CustomerOrder>(DbContext));
			}
		}

		public IRepository<Order> OrderRepository
		{
			get
			{
				return _orderRepository
					?? (_orderRepository = new EFRepository<Order>(DbContext));
			}
		}

		public IRepository<OrderLine> OrderLineRepository
		{
			get
			{
				return _orderLineRepository
					?? (_orderLineRepository = new EFRepository<OrderLine>(DbContext));
			}
		}

		public EFUnitOfWork()
		{
			DbContext = new ShopDbContext();
			DbContext.Configuration.ProxyCreationEnabled = false;
		}

		public void Commit()
		{
			DbContext.SaveChanges();
		}

		public virtual Customer GetCustomer()
		{
			return new Customer();
		}

		public virtual CustomerOrder GetCustomerOrder()
		{
			return new CustomerOrder();
		}

		public virtual Order GetOrder()
		{
			return new Order { ActionDate = DateTime.Now };
		}

		public virtual OrderLine GetOrderLine()
		{
			return new OrderLine();
		}

		#region IDisposable methods

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!disposing)
				return;

			if (DbContext != null)
				DbContext.Dispose();
		}

		#endregion
	}
}