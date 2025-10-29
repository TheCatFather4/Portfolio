using Cafe.Core.DTOs;
using Cafe.Core.Entities;

namespace Cafe.Core.Interfaces.Services
{
    public interface IMenuManagerService
    {
        Result AddNewItem(Item item);
        Result<List<Item>> FilterMenu(MenuFilter dto);
        Result UpdateItem(Item item);
    }
}