using Cafe.Core.DTOs;
using Cafe.Core.Entities;

namespace Cafe.Core.Interfaces.Services
{
    public interface IMenuManagerService
    {
        Result<List<Item>> FilterMenu(MenuFilter dto);
    }
}