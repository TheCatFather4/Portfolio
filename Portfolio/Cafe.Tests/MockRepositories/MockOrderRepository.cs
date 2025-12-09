using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;

namespace Cafe.Tests.MockRepositories
{
    public class MockOrderRepository : IOrderRepository
    {
        public async Task<CafeOrder> CreateOrderAsync(CafeOrder order, List<OrderItem> items)
        {
            var orders = new List<CafeOrder>();
            var orderItems = new List<OrderItem>();
            orders.Add(order);

            await Task.Delay(1000);
            return new CafeOrder
            {
                OrderID = 1,
                CustomerID = order.CustomerID,
                PaymentStatusID = order.PaymentStatusID,
                OrderDate = order.OrderDate,
                SubTotal = order.SubTotal,
                Tax = order.Tax,
                Tip = order.Tip,
                FinalTotal = order.FinalTotal,
                OrderItems = items
            };
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

            await Task.Delay(1000);
            return orders;
        }

        public async Task<CafeOrder?> GetOrderByIdAsync(int orderId)
        {
            if (orderId == 1)
            {
                await Task.Delay(1000);
                return new CafeOrder
                {
                    OrderID = 1,
                    CustomerID = 1,
                    PaymentTypeID = 1,
                    PaymentStatusID = 1,
                    OrderDate = DateTime.Today,
                    SubTotal = 10.00M,
                    Tax = 0.40M,
                    Tip = 1.50M,
                    FinalTotal = 10.90M
                };
            }

            return null;
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

            await Task.Delay(1000);
            return orderItems;
        }

        public async Task<List<CafeOrder>> GetOrdersByCustomerIdAsync(int customerId)
        {
            var orders = new List<CafeOrder>();

            var order1 = new CafeOrder
            {
                OrderID = 1,
                CustomerID = 1,
                PaymentTypeID = 1,
                PaymentStatusID = 1,
                OrderDate = DateTime.Today,
                SubTotal = 10.00M,
                Tax = 0.40M,
                Tip = 1.50M,
                FinalTotal = 10.90M
            };

            var order2 = new CafeOrder
            {
                OrderID = 2,
                CustomerID = 2,
                PaymentTypeID = 1,
                PaymentStatusID = 1,
                OrderDate = DateTime.Today,
                SubTotal = 20.00M,
                Tax = 0.80M,
                Tip = 3.00M,
                FinalTotal = 23.80M
            };

            var order3 = new CafeOrder
            {
                OrderID = 3,
                CustomerID = 1,
                PaymentTypeID = 1,
                PaymentStatusID = 1,
                OrderDate = DateTime.Today,
                SubTotal = 12.00M,
                Tax = 0.40M,
                Tip = 2.50M,
                FinalTotal = 14.90M
            };

            var order4 = new CafeOrder
            {
                OrderID = 4,
                CustomerID = 1,
                PaymentTypeID = 1,
                PaymentStatusID = 1,
                OrderDate = DateTime.Today,
                SubTotal = 30.00M,
                Tax = 1.40M,
                Tip = 4.00M,
                FinalTotal = 35.40M
            };

            orders.Add(order1);
            orders.Add(order2);
            orders.Add(order3);
            orders.Add(order4);

            var ordersReturned = new List<CafeOrder>();

            foreach (var order in orders)
            {
                if (customerId == order.CustomerID)
                {
                    ordersReturned.Add(order);
                }
            }

            await Task.Delay(1000);
            return ordersReturned;
        }

        public async Task UpdateOrderStatusAsync(CafeOrder order)
        {
            var existingOrder = new CafeOrder
            {
                OrderID = order.OrderID,
                PaymentStatusID = order.PaymentStatusID,
            };

            await Task.Delay(1000);
        }
    }
}