using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;

namespace Cafe.Data.Repositories.TrainingRepository
{
    public class TrainingManagementRepository : IManagementRepository
    {
        public void AddItem(Item item)
        {
            var lastItem = FakeCafeDb.Items.LastOrDefault();
            item.ItemID = lastItem.ItemID + 1;

            var lastPrice = FakeCafeDb.Prices.LastOrDefault();
            item.Prices[0].ItemPriceID = lastPrice.ItemPriceID + 1;
            item.Prices[0].ItemID = lastItem.ItemID + 1;

            FakeCafeDb.Items.Add(item);
            FakeCafeDb.Prices.Add(item.Prices[0]);
        }

        public void AddServer(Server server)
        {
            var lastServer = FakeCafeDb.Servers.LastOrDefault();
            server.ServerID = lastServer.ServerID + 1;
            FakeCafeDb.Servers.Add(server);
        }

        public Item GetMenuItemById(int itemID)
        {
            var item = FakeCafeDb.Items
                .FirstOrDefault(i => i.ItemID == itemID);

            item.Prices = FakeCafeDb.Prices
                .Where(ip => ip.ItemID == item.ItemID)
                .ToList();

            return item;
        }

        public Server GetServerById(int serverID)
        {
            var server = FakeCafeDb.Servers
                .FirstOrDefault(s => s.ServerID == serverID);

            return server;
        }

        public List<Server> GetServers()
        {
            var servers = new List<Server>();

            servers = FakeCafeDb.Servers
                .ToList();

            return servers;
        }

        public bool IsDuplicateItem(string itemName)
        {
            throw new NotImplementedException();
        }

        public void UpdateMenu(Item item)
        {
            var existingItem = FakeCafeDb.Items
                .FirstOrDefault(i => i.ItemID == item.ItemID);

            existingItem.ItemName = item.ItemName;
            existingItem.ItemDescription = item.ItemDescription;

            var currentPricesInDb = FakeCafeDb.Prices
                .Where(ip => ip.ItemID == item.ItemID)
                .ToList();

            var incomingPrices = item.Prices;

            foreach (var incomingPrice in incomingPrices)
            {
                var priceInDb = currentPricesInDb.FirstOrDefault(p => p.ItemPriceID == incomingPrice.ItemPriceID);

                if (priceInDb != null)
                {
                    priceInDb.ItemID = incomingPrice.ItemID;
                    priceInDb.TimeOfDayID = incomingPrice.TimeOfDayID;
                    priceInDb.Price = incomingPrice.Price;
                    priceInDb.StartDate = incomingPrice.StartDate;
                    priceInDb.EndDate = incomingPrice.EndDate;
                }
            }
        }

        public void UpdateServer(Server server)
        {
            var s1 = FakeCafeDb.Servers.FirstOrDefault(s => s.ServerID == server.ServerID);
            s1.FirstName = server.FirstName;
            s1.LastName = server.LastName;
            s1.HireDate = server.HireDate;
            s1.TermDate = server.TermDate;
            s1.DoB = server.DoB;
        }
    }
}
