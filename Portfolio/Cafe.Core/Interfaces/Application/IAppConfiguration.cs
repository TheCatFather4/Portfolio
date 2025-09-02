using Cafe.Core.Enums;

namespace Cafe.Core.Interfaces.Application
{
    public interface IAppConfiguration
    {
        string GetConnectionString();
        DatabaseMode GetDatabaseMode();
    }
}
