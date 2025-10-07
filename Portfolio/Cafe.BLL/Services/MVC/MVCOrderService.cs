using Cafe.Core.DTOs;
using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;
using Cafe.Core.Interfaces.Services;
using Cafe.Core.Interfaces.Services.MVC;

namespace Cafe.BLL.Services.MVC
{
    public class MVCOrderService : IMVOrderService
    {
        private IMenuService _menuService;
        private IShoppingBagService _shoppingBagService;
        private IOrderRepository _orderRepository;

        public MVCOrderService(IMenuService menuService, IShoppingBagService shoppingBagService, IOrderRepository orderRepository)
        {
            _menuService = menuService;
            _shoppingBagService = shoppingBagService;
            _orderRepository = orderRepository;
        }

        public async Task<Result<CafeOrder>> CreateNewOrderAsync(int customerId, int paymentTypeId, decimal tip)
        {
            try
            {
                var shoppingBagResult = await _shoppingBagService.GetShoppingBagAsync(customerId);

                if (!shoppingBagResult.Ok || shoppingBagResult == null || !shoppingBagResult.Data.Items.Any())
                {
                    return ResultFactory.Fail<CafeOrder>("An error occurred. Please contact our management team.");
                }

                var shoppingBag = shoppingBagResult.Data;
                var shoppingBagItems = shoppingBagResult.Data.Items;

                var orderItems = new List<OrderItem>();
                decimal subTotal = 0;
                const decimal taxRate = 0.04m;

                foreach (var item in shoppingBagItems)
                {
                    var itemPriceResult = await _menuService.GetItemPriceByIdAsync(item.ItemID);

                    if (!itemPriceResult.Ok || itemPriceResult.Data == null || itemPriceResult.Data.Price == null)
                    {
                        return ResultFactory.Fail<CafeOrder>("An error occurred");
                    }

                    decimal extendedPrice = (decimal)(itemPriceResult.Data.Price * item.Quantity);
                    subTotal += extendedPrice;

                    orderItems.Add(new OrderItem
                    {
                        ItemPriceID = (int)itemPriceResult.Data.ItemPriceID,
                        Quantity = item.Quantity,
                        ExtendedPrice = extendedPrice
                    });
                }

                decimal tax = subTotal * taxRate;
                decimal finalTotal = subTotal + tax + tip;

                var order = new CafeOrder
                {
                    CustomerID = customerId,
                    PaymentTypeID = paymentTypeId,
                    OrderDate = DateTime.Now,
                    SubTotal = subTotal,
                    Tax = tax,
                    Tip = tip,
                    FinalTotal = finalTotal,
                    PaymentStatusID = 2
                };

                await _orderRepository.CreateOrderAsync(order, orderItems);
                await _shoppingBagService.ClearShoppingBagAsync(customerId);

                return ResultFactory.Success(order);
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<CafeOrder>("An error occurred. Please contact our management team.");
            }
        }

        public async Task<Result<CafeOrder>> GetOrderByIdAsync(int orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);

            if (order == null)
            {
                return ResultFactory.Fail<CafeOrder>("Order not found.");
            }

            return ResultFactory.Success(order);
        }

        public async Task<Result<List<CafeOrder>>> GetOrderHistoryAsync(int customerId)
        {
            var orders = await _orderRepository.GetOrdersByCustomerIdAsync(customerId);

            if (orders == null || !orders.Any())
            {
                return ResultFactory.Fail<List<CafeOrder>>("No orders found.");
            }

            return ResultFactory.Success(orders);
        }
    }
}