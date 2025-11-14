
using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;

namespace Cafe.Tests.MockRepositories
{
    public class MockOrderRepository : IOrderRepository
    {
        public Task<CafeOrder> CreateOrderAsync(CafeOrder order, List<OrderItem> items)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CafeOrder>> GetAllOrdersAsync()
        {
            var orders = new List<CafeOrder>();

            var order1 = new CafeOrder
            {
                OrderID = 1,
                ServerID = 1,
                CustomerID = 1,
                PaymentTypeID = 1,
                PaymentStatusID = 1,
                OrderDate = DateTime.Today,
                SubTotal = 5.00M,
                Tax = 0.50M,
                Tip = 1.00M,
                FinalTotal = 6.50M
            };

            var order2 = new CafeOrder
            {
                OrderID = 2,
                CustomerID = 2,
                PaymentTypeID = 2,
                PaymentStatusID = 2,
                OrderDate = DateTime.Today,
                SubTotal = 7.00M,
                Tax = 1.00M,
                Tip = 2.00M,
                FinalTotal = 10.00M
            };

            orders.Add(order1);
            orders.Add(order2);

            return orders;
        }

        public Task<CafeOrder> GetOrderByIdAsync(int orderId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<OrderItem>> GetOrderItemsByItemPriceIdAsync(int itemPriceId)
        {
            var orderItems = new List<OrderItem>();

            var orderItem1 = new OrderItem
            {
                OrderItemID = 1,
                OrderID = 1,
                ItemPriceID = 1,
                Quantity = 3,
                ExtendedPrice = 7.50M,
                CafeOrder = new CafeOrder()
            };

            var orderItem2 = new OrderItem
            {
                OrderItemID = 2,
                OrderID = 2,
                ItemPriceID = 2,
                Quantity = 6,
                ExtendedPrice = 20.00M,
                CafeOrder = new CafeOrder()
            };

            var orderItem3 = new OrderItem
            {
                OrderItemID = 3,
                OrderID = 3,
                ItemPriceID = 1,
                Quantity = 4,
                ExtendedPrice = 10.00M,
                CafeOrder = new CafeOrder()
            };

            orderItems.Add(orderItem1);
            orderItems.Add(orderItem2);
            orderItems.Add(orderItem3);

            return orderItems;
        }

        public Task<List<CafeOrder>> GetOrdersByCustomerIdAsync(int customerId)
        {
            throw new NotImplementedException();
        }
    }
}