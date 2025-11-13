using Cafe.Core.DTOs;
using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;
using Cafe.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Cafe.BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IShoppingBagService _shoppingBagService;
        private readonly IMenuRetrievalService _menuService;
        private readonly ILogger _logger;

        public OrderService(IOrderRepository orderRepository, IShoppingBagService shoppingBagService, IMenuRetrievalService menuService, ILogger logger)
        {
            _orderRepository = orderRepository;
            _shoppingBagService = shoppingBagService;
            _menuService = menuService;
            _logger = logger;
        }

        public Task<Result<CafeOrder>> CreateNewOrderAsync(int customerId, int paymentTypeId, decimal tip)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<CafeOrderResponse>> CreateOrderAsync(int customerId, int paymentTypeId, decimal tip)
        {
            try
            {
                var result = await _shoppingBagService.GetShoppingBagAsync(customerId);
                if (!result.Ok || result.Data == null || !result.Data.Items.Any())
                {
                    _logger.LogError("Shopping bag did not have any items in it.");
                    return ResultFactory.Fail<CafeOrderResponse>("An error occurred. Please try again in a few minutes.");
                }

                var shoppingBag = result.Data;
                var shoppingBagItems = shoppingBag.Items;

                var orderItems = new List<OrderItem>();
                decimal subTotal = 0;
                const decimal taxRate = 0.04m;

                foreach (var item in shoppingBagItems)
                {
                    var itemPriceResult = await _menuService.GetItemPriceByItemIdAsync(item.ItemID);
                    if (!itemPriceResult.Ok || itemPriceResult.Data == null || itemPriceResult.Data.Price == null)
                    {
                        _logger.LogError($"Could not find price for item with ID: {item.ItemID}");
                        return ResultFactory.Fail<CafeOrderResponse>("An error occurred. Please try again in a few minutes.");
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
                return ResultFactory.Fail<CafeOrderResponse>("An error occurred. Please contact our customer assistance team.");
            }
        }

        public async Task<Result<CafeOrderResponse>> GetOrderDetailsAsync(int orderId)
        {
            try
            {
                var order = await _orderRepository.GetOrderByIdAsync(orderId);

                if (order == null)
                {
                    _logger.LogError($"Order with ID {orderId} not found.");
                    return ResultFactory.Fail<CafeOrderResponse>("An error occurred. Please try again in a few minutes.");
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
                    OrderItems = new List<OrderItemResponse>()
                };

                foreach (var oi in order.OrderItems)
                {
                    var itemResponse = new OrderItemResponse
                    {
                        OrderItemID = oi.OrderItemID,
                        ItemPriceID = oi.ItemPriceID,
                        Quantity = oi.Quantity,
                        ExtendedPrice = oi.ExtendedPrice
                    };

                    response.OrderItems.Add(itemResponse);
                }

                return ResultFactory.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when attempting to retrieve order details for Order ID {orderId}: {ex.Message}");
                return ResultFactory.Fail<CafeOrderResponse>("An error occurred. Please contact the site administrator.");
            }
        }

        public async Task<Result<List<CafeOrderResponse>>> GetOrderHistoryAsync(int customerId)
        {
            try
            {
                var orderHistory = await _orderRepository.GetOrdersByCustomerIdAsync(customerId);

                if (orderHistory.Count() == 0)
                {
                    _logger.LogError($"Order history was not found for Customer ID:{customerId}.");
                    return ResultFactory.Fail<List<CafeOrderResponse>>("An error occured. Please try again in a few minutes.");
                }

                var orderResponses = new List<CafeOrderResponse>();

                foreach (var order in orderHistory)
                {
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
                        OrderItems = new List<OrderItemResponse>()
                    };

                    foreach (var item in order.OrderItems)
                    {
                        var orderItemResponse = new OrderItemResponse
                        {
                            OrderItemID = item.OrderItemID,
                            ItemPriceID = item.ItemPriceID,
                            Quantity = item.Quantity,
                            ExtendedPrice = item.ExtendedPrice
                        };

                        response.OrderItems.Add(orderItemResponse);
                    }

                    orderResponses.Add(response);
                }

                return ResultFactory.Success(orderResponses);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when attempting to retrieve Customer ID {customerId}'s order history: {ex.Message}");
                return ResultFactory.Fail<List<CafeOrderResponse>>("An error occurred. Please contact our site administrator.");
            }
        }
    }
}