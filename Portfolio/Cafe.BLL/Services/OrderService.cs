using Cafe.Core.DTOs;
using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;
using Cafe.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Cafe.BLL.Services
{
    /// <summary>
    /// Handles the business logic concerning cafe orders.
    /// </summary>
    public class OrderService : IOrderService
    {
        private readonly ILogger _logger;
        private readonly IMenuRetrievalRepository _menuRetrievalRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IShoppingBagRepository _shoppingBagRepository;

        /// <summary>
        /// Constructs a service with the dependencies required for creating and accessing cafe orders.
        /// </summary>
        /// <param name="logger">A dependency used for logging error messages.</param>
        /// <param name="menuRetrievalRepository">A dependency used for retrieving item records.</param>
        /// <param name="orderRepository">A dependency used for creating and retrieving order records.</param>
        /// <param name="shoppingBagRepository">A dependency used for retrieving shopping bag records.</param>
        public OrderService(ILogger logger, IMenuRetrievalRepository menuRetrievalRepository, IOrderRepository orderRepository, IShoppingBagRepository shoppingBagRepository)
        {
            _logger = logger;
            _menuRetrievalRepository = menuRetrievalRepository;
            _orderRepository = orderRepository;
            _shoppingBagRepository = shoppingBagRepository;
        }

        /// <summary>
        /// The customer's shopping bag is retrieved. If successful, prices are retrieved 
        /// for each item and mapped to create a new CafeOrder record. The customer's 
        /// shopping bag is emptied and the new record is mapped to a response DTO.
        /// </summary>
        /// <param name="dto">A Request DTO used for retrieving and mapping customer data.</param>
        /// <returns>A Result DTO with a CafeOrderResponse DTO as its data.</returns>
        public async Task<Result<CafeOrderResponse>> CreateNewOrderAsync(OrderRequest dto)
        {
            try
            {
                var shoppingBagResult = await GetShoppingBagByCustomerIdAsync(dto.CustomerId);

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
                    var itemPriceResult = await GetItemPriceByItemIdAsync(item.ItemID);

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

        /// <summary>
        /// An ItemPrice record is retrieved.
        /// </summary>
        /// <param name="itemId">An ItemID used to retrieve an ItemPrice record.</param>
        /// <returns>An ItemPrice record.</returns>
        public async Task<ItemPrice?> GetItemPriceByItemIdAsync(int itemId)
        {
            ItemPrice? itemPrice = await _menuRetrievalRepository.GetItemPriceByItemIdAsync(itemId);

            if (itemPrice != null)
            {
                return itemPrice;
            }

            return null;
        }

        /// <summary>
        /// A CafeOrder is retrieved by OrderID. If found, a data is mapped to a response DTO for display.
        /// </summary>
        /// <param name="orderId">An OrderID used in retrieving a CafeOrder record.</param>
        /// <returns>A Result DTO with a response DTO as its data.</returns>
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

        /// <summary>
        /// A CustomerID is used to retrieve a list of CafeOrder records. 
        /// If successful, the data is mapped to a list of CafeOrderReponse DTOs for display.
        /// </summary>
        /// <param name="customerId">A CustomerID used for the retrieval of CafeOrder records.</param>
        /// <returns>A Result DTO with a list of response DTOs as its data.</returns>
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

        /// <summary>
        /// The subtotal of a customer's ShoppingBag is retrieved. 
        /// If found, the tax is calculated and added to the subtotal.
        /// The total price is then returned.
        /// </summary>
        /// <param name="customerId">A CustomerID used for retrieving the subtotal of a customer's ShoppingBag.</param>
        /// <returns>A Result DTO with a decimal as its data.</returns>
        public async Task<Result<decimal>> GetOrderTotalAsync(int customerId)
        {
            try
            {
                var total = await _shoppingBagRepository.GetShoppingBagTotalAsync(customerId);

                if (total == 0)
                {
                    _logger.LogError($"An error occurred when attempting to retrieve shopping bag total for Customer ID: {customerId}.");
                    return ResultFactory.Fail<decimal>("An error occurred. Please try again in a few minutes.");
                }

                decimal taxRate = 0.04M;
                var tax = total * taxRate;
                total += tax;

                return ResultFactory.Success(total);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when attempting to retrieve order total for Customer ID {customerId}: {ex.Message}");
                return ResultFactory.Fail<decimal>("An error occurred. Please contact our site administrator.");
            }
        }

        /// <summary>
        /// A ShoppingBag record is retrieved.
        /// </summary>
        /// <param name="customerId">A CustomerID used in the retrieval of a ShoppingBag record.</param>
        /// <returns>A ShoppingBag record.</returns>
        public async Task<ShoppingBag?> GetShoppingBagByCustomerIdAsync(int customerId)
        {
            ShoppingBag? sb = await _shoppingBagRepository.GetShoppingBagAsync(customerId);

            if (sb != null)
            {
                return sb;
            }

            return null;
        }
    }
}