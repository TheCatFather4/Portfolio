using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;

namespace Cafe.Tests.MockRepositories
{
    public class MockManagementRepository : IManagementRepository
    {
        public void AddItem(Item item)
        {
            List<Item> items = new List<Item>();
            items.Add(item);
        }

        public void AddServer(Server server)
        {
            List<Server> servers = new List<Server>();
            servers.Add(server);
        }

        public bool IsDuplicateItem(string itemName)
        {
            if (itemName == "Tuna Sub")
            {
                return true;
            }

            return false;
        }

        public Item GetMenuItemById(int itemID)
        {
            if (itemID != 1)
            {
                return new Item();
            }

            return new Item
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
            if (serverID != 1)
            {
                return new Server();
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
            var servers = new List<Server>();
            servers.Add(new Server());
            return servers;
        }

        public void UpdateMenu(Item item)
        {
            var item2 = new Item();
            item2 = item;
        }

        public void UpdateServer(Server server)
        {
            var server2 = new Server();
            server2 = server;
        }
    }
}
