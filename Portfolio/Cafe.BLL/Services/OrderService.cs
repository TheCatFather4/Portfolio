using Cafe.Core.DTOs;
using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;
using Cafe.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Cafe.BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly ILogger _logger;
        private readonly IMenuRetrievalRepository _menuRetrievalRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IShoppingBagRepository _shoppingBagRepository;

        public OrderService(ILogger logger, IMenuRetrievalRepository menuRetrievalRepository, IOrderRepository orderRepository, IShoppingBagRepository shoppingBagRepository)
        {
            _logger = logger;
            _menuRetrievalRepository = menuRetrievalRepository;
            _orderRepository = orderRepository;
            _shoppingBagRepository = shoppingBagRepository;
        }

        public async Task<Result<CafeOrderResponse>> CreateNewOrderAsync(OrderRequest dto)
        {
            try
            {
                var shoppingBagResult = await _shoppingBagRepository.GetShoppingBagAsync(dto.CustomerId);

                if (shoppingBagResult == null || shoppingBagResult.Items == null)
                {
                    _logger.LogError($"Shopping Bag and/or Items not found for Customer ID: {dto.CustomerId}");
                    return ResultFactory.Fail<CafeOrderResponse>("An error occurred. Please try again in a few minutes.");
                }

                var shoppingBag = shoppingBagResult;
                var shoppingBagItems = shoppingBagResult.Items;

                var orderItems = new List<OrderItem>();
                decimal subTotal = 0;
                const decimal taxRate = 0.04m;

                foreach (var item in shoppingBagItems)
                {
                    var itemPriceResult = await _menuRetrievalRepository.GetItemPriceByItemIdAsync(item.ItemID);

                    if (itemPriceResult == null || itemPriceResult.ItemPriceID == null || itemPriceResult.Price == null)
                    {
                        _logger.LogError($"Item price not found for Item ID: {item.ItemID}");
                        return ResultFactory.Fail<CafeOrderResponse>("An error occurred. Please try again in a few minutes.");
                    }

                    decimal extendedPrice = (decimal)(itemPriceResult.Price * item.Quantity);
                    subTotal += extendedPrice;

                    orderItems.Add(new OrderItem
                    {
                        ItemPriceID = (int)itemPriceResult.ItemPriceID,
                        Quantity = item.Quantity,
                        ExtendedPrice = extendedPrice
                    });
                }

                decimal tax = subTotal * taxRate;
                decimal finalTotal = subTotal + tax + dto.Tip;

                var order = new CafeOrder
                {
                    CustomerID = dto.CustomerId,
                    PaymentTypeID = dto.PaymentTypeId,
                    OrderDate = DateTime.Now,
                    SubTotal = subTotal,
                    Tax = tax,
                    Tip = dto.Tip,
                    FinalTotal = finalTotal,
                    PaymentStatusID = 2
                };

                var orderCreated = await _orderRepository.CreateOrderAsync(order, orderItems);
                await _shoppingBagRepository.ClearShoppingBagAsync(dto.CustomerId);

                var orderDto = new CafeOrderResponse
                {
                    CustomerID = dto.CustomerId,
                    PaymentTypeID = dto.PaymentTypeId,
                    OrderDate = DateTime.Now,
                    SubTotal = subTotal,
                    Tax = tax,
                    Tip = dto.Tip,
                    FinalTotal = finalTotal,
                    PaymentStatusID = 2,
                    OrderID = orderCreated.OrderID,
                    OrderItems = new List<OrderItemResponse>()
                };

                foreach (var item in orderCreated.OrderItems)
                {
                    var itemDto = new OrderItemResponse
                    {
                        OrderItemID = item.OrderItemID,
                        ItemPriceID = item.ItemPriceID,
                        Quantity = item.Quantity,
                        ExtendedPrice = item.ExtendedPrice
                    };

                    orderDto.OrderItems.Add(itemDto);
                }

                return ResultFactory.Success(orderDto);

            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when attempting to create a new order: {ex.Message}");
                return ResultFactory.Fail<CafeOrderResponse>("An error occurred. Please contact our management team.");
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