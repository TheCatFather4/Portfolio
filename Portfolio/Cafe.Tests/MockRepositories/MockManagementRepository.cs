using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;

namespace Cafe.Tests.MockRepositories
{
    public class MockManagementRepository : IManagementRepository
    {
        public void AddItem(Item item)
        {
            throw new NotImplementedException();
        }

        public void AddServer(Server server)
        {
            throw new NotImplementedException();
        }

        public Item GetMenuItemById(int itemID)
        {
            if (itemID == 3)
            {
                return null;
            }

            return new Item()
            {
                ItemID = 1,
                CategoryID = 1,
                ItemName = "Food",
                ItemDescription = "Yummy",
                ItemStatusID = 1
            };
        }

        public Server GetServerById(int serverID)
        {
            if (serverID == 4)
            {
                return null;
            }

            return new Server
            {
                ServerID = 1,
                FirstName = "John",
                LastName = "Smith",
                HireDate = DateTime.Now,
                DoB = DateTime.Today.AddYears(-25)
            };
        }

        public List<Server> GetServers()
        {
            throw new NotImplementedException();
        }

        public bool IsDuplicateItem(string itemName)
        {
            if (itemName == "Tuna Sub")
            {
                return true;
            }

            return false;
        }

        public void UpdateMenu(Item item)
        {
            throw new NotImplementedException();
        }

        public void UpdateServer(Server server)
        {
            throw new NotImplementedException();
        }
    }
}
