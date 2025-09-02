using Cafe.Core.DTOs;
using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;
using Cafe.Core.Interfaces.Services;

namespace Cafe.BLL.Services
{
    public class AccountantService : IAccountantService
    {
        private IAccountantRepository _accountantRepository;

        public AccountantService(IAccountantRepository accountantRepository)
        {
            _accountantRepository = accountantRepository;
        }

        public Result<List<ItemPrice>> GetItemPrices()
        {
            try
            {
                var orderItems = _accountantRepository.GetItemPrices();

                if (orderItems.Count() == 0)
                {
                    return ResultFactory.Fail<List<ItemPrice>>("Error getting item prices.");
                }

                return ResultFactory.Success(orderItems);
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<List<ItemPrice>>(ex.Message);
            }
        }

        public Result<List<Item>> GetItemsByCategoryID(int categoryID)
        {
            try
            {
                var items = _accountantRepository.GetItemsByCategoryID(categoryID);

                if (items.Count() == 0)
                {
                    return ResultFactory.Fail<List<Item>>("Error getting items.");
                }

                return ResultFactory.Success(items);
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<List<Item>>(ex.Message);
            }
        }

        public Result<List<CafeOrder>> GetOrders()
        {
            try
            {
                var orders = _accountantRepository.GetOrders();

                if (orders.Count() == 0)
                {
                    return ResultFactory.Fail<List<CafeOrder>>("Orders not found.");
                }

                return ResultFactory.Success(orders);
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<List<CafeOrder>>(ex.Message);
            }
        }
    }
}
