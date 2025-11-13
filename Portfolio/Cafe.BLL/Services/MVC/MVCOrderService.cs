using Cafe.Core.DTOs;
using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;
using Cafe.Core.Interfaces.Services;
using Cafe.Core.Interfaces.Services.MVC;
using Microsoft.Extensions.Logging;

namespace Cafe.BLL.Services.MVC
{
    public class MVCOrderService : IMVOrderService
    {
        private readonly ILogger _logger;
        private readonly IMenuRetrievalService _menuService;
        private readonly IShoppingBagService _shoppingBagService;
        private readonly IOrderRepository _orderRepository;

        public MVCOrderService(ILogger<MVCOrderService> logger, IMenuRetrievalService menuService, IShoppingBagService shoppingBagService, IOrderRepository orderRepository)
        {
            _logger = logger;
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
                    _logger.LogError($"Shopping Bag not found. Customer ID: {customerId}");
                    return ResultFactory.Fail<CafeOrder>("An error occurred. Please try again in a few minutes.");
                }

                var shoppingBag = shoppingBagResult.Data;
                var shoppingBagItems = shoppingBagResult.Data.Items;

                var orderItems = new List<OrderItem>();
                decimal subTotal = 0;
                const decimal taxRate = 0.04m;

                foreach (var item in shoppingBagItems)
                {
                    var itemPriceResult = await _menuService.GetItemPriceByItemIdAsync(item.ItemID);

                    if (!itemPriceResult.Ok || itemPriceResult.Data == null || itemPriceResult.Data.Price == null)
                    {
                        _logger.LogError($"Item not found with Item ID: {item.ItemID}");
                        return ResultFactory.Fail<CafeOrder>("An error occurred. Please try again in a few minutes.");
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
                _logger.LogError("An error occurred when attempting to create a new order.");
                return ResultFactory.Fail<CafeOrder>("An error occurred. Please contact our management team.");
            }
        }
    }
}