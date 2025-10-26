using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;

namespace Cafe.Tests.MockRepositories
{
    public class MockAccountantRepository : IAccountantRepository
    {
        public List<OrderItem> GetOrderItemsByItemPriceId(int itemPriceId)
        {
            var items = new List<OrderItem>();
            items.Add(new OrderItem());

            return items;
        }

        public List<CafeOrder> GetOrders()
        {
            var orders = new List<CafeOrder>();
            orders.Add(new CafeOrder());

            return orders;
        }
    }
}