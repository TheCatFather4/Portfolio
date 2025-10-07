using Cafe.Core.DTOs;
using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;
using Cafe.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Cafe.BLL.Services.API
{
    public class APIOrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IShoppingBagService _shoppingBagService;
        private readonly IMenuService _menuService;
        private readonly ILogger _logger;

        public APIOrderService(IOrderRepository orderRepository, IShoppingBagService shoppingBagService, IMenuService menuService, ILogger logger)
        {
            _orderRepository = orderRepository;
            _shoppingBagService = shoppingBagService;
            _menuService = menuService;
            _logger = logger;
        }

        public async Task<Result<CafeOrderResponse>> CreateOrderAsync(int customerId, int paymentTypeId, decimal tip)
        {
            try
            {
                var result = await _shoppingBagService.GetShoppingBagAsync(customerId);
                if (!result.Ok || result.Data == null || !result.Data.Items.Any())
                {
                    return ResultFactory.Fail<CafeOrderResponse>("Shopping bag is empty or could not be found.");
                }

                var shoppingBag = result.Data;
                var shoppingBagItems = shoppingBag.Items;

                var orderItems = new List<OrderItem>();
                decimal subTotal = 0;
                const decimal taxRate = 0.04m;

                foreach (var item in shoppingBagItems)
                {
                    var itemPriceResult = await _menuService.GetItemPriceByIdAsync(item.ItemID);
                    if (!itemPriceResult.Ok || itemPriceResult.Data == null || itemPriceResult.Data.Price == null)
                    {
                        return ResultFactory.Fail<CafeOrderResponse>($"Could not find price for item with ID: {item.ItemID}");
                    }

                    decimal extendedPrice = (decimal)(itemPriceResult.Data.Price * item.Quantity);
                    subTotal += extendedPrice;

                    orderItems.Add(new OrderItem
                    {
                        ItemPriceID = (int)itemPriceResult.Data.ItemPriceID,
                        Quantity = item.Quantity,
                        ExtendedPrice = extendedPrice,
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

                var createdOrder = await _orderRepository.CreateOrderAsync(order, orderItems);

                await _shoppingBagService.ClearShoppingBagAsync(customerId);

                _logger.LogInformation($"Order {createdOrder.OrderID} created successfully for customer {customerId}.");

                var response = new CafeOrderResponse
                {
                    OrderID = createdOrder.OrderID,
                    CustomerID = createdOrder.CustomerID,
                    PaymentTypeID = createdOrder.PaymentTypeID,
                    PaymentStatusID = createdOrder.PaymentStatusID,
                    OrderDate = createdOrder.OrderDate,
                    SubTotal = createdOrder.SubTotal,
                    Tax = createdOrder.Tax,
                    Tip = createdOrder.Tip,
                    FinalTotal = createdOrder.FinalTotal,
                    ServerID = createdOrder.ServerID,
                    OrderItems = createdOrder.OrderItems?
                                         .Select(oi => new OrderItemResponse 
                                         { 
                                            OrderItemID = oi.OrderItemID, 
                                            ItemPriceID = oi.ItemPriceID, 
                                            Quantity = oi.Quantity, 
                                            ExtendedPrice = oi.ExtendedPrice                       
                                         })
                                         .ToList() ?? new List<OrderItemResponse>()
                };

                return ResultFactory.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while creating an order for customer {customerId}");
                return ResultFactory.Fail<CafeOrderResponse>("An unexpected error occurred while creating the order.");
            }
        }

        public async Task<Result<CafeOrderResponse>> GetOrderDetailsAsync(int orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                return ResultFactory.Fail<CafeOrderResponse>($"Order with ID {orderId} not found.");
            }

            var response = new CafeOrderResponse
            {
                OrderID = order.OrderID,
                CustomerID = order.CustomerID,
                PaymentTypeID = order.PaymentTypeID,
                PaymentStatusID = order.PaymentStatusID,
                OrderDate = order.OrderDate,
                SubTotal = order.SubTotal,
                Tax = order.Tax,
                Tip = order.Tip,
                FinalTotal = order.FinalTotal,
                ServerID = order.ServerID,
                OrderItems = order.OrderItems?
                                .Select(oi => new OrderItemResponse
                                {
                                    OrderItemID = oi.OrderItemID,
                                    ItemPriceID = oi.ItemPriceID,
                                    Quantity = oi.Quantity,
                                    ExtendedPrice = oi.ExtendedPrice
                                })
                                .ToList() ?? new List<OrderItemResponse>()
            };

            return ResultFactory.Success(response);
        }

        public async Task<Result<List<CafeOrderResponse>>> GetOrdersAsync(int customerId)
        {
            var orders = await _orderRepository.GetOrdersByCustomerIdAsync(customerId);
            if (orders == null || !orders.Any())
            {
                return ResultFactory.Fail<List<CafeOrderResponse>>($"No orders found for customer {customerId}.");
            }

            var response = orders.Select(order => new CafeOrderResponse
            {
                OrderID = order.OrderID,
                CustomerID = order.CustomerID,
                PaymentTypeID = order.PaymentTypeID,
                PaymentStatusID = order.PaymentStatusID,
                OrderDate = order.OrderDate,
                SubTotal = order.SubTotal,
                Tax = order.Tax,
                Tip = order.Tip,
                FinalTotal = order.FinalTotal,
                ServerID = order.ServerID,
                OrderItems = order.OrderItems?
                                .Select(oi => new OrderItemResponse
                                {
                                    OrderItemID= oi.OrderItemID,
                                    ItemPriceID= oi.ItemPriceID,
                                    Quantity = oi.Quantity,
                                    ExtendedPrice = oi.ExtendedPrice
                                })
                                .ToList() ?? new List<OrderItemResponse>()
            }).ToList();

            return ResultFactory.Success(response);
        }
    }
}
