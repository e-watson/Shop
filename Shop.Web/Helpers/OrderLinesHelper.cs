using Shop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Web.Helpers
{
	public class OrderLinesHelper
	{
		private const int ItemLen = 2;

		public static IEnumerable<OrderLineModel> DecodeOrderLines(string encodedLines)
		{
			if (string.IsNullOrEmpty(encodedLines.Trim()))
				return Enumerable.Empty<OrderLineModel>();

			var encodedItems = encodedLines.Split(',').Where(x => !string.IsNullOrEmpty(x)).ToArray();

			if (!encodedItems.Any())
				return Enumerable.Empty<OrderLineModel>();

			var lines = new List<OrderLineModel>();

			foreach(var item in encodedItems)
			{
				var parts = item.Split('-').Where(x => !string.IsNullOrEmpty(x)).ToArray();
				if (parts.Length == ItemLen)
				{
					try
					{
						lines.Add(new OrderLineModel
						{
							Id = Convert.ToInt32(parts[0]),
							Quantity = Convert.ToInt32(parts[1])
						});
					}
					catch
					{
						throw new Exception("Invalid data!");
					}
				}
			}

			return lines;
		}
	}
}