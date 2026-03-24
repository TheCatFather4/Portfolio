using Cafe.Core.Enums;

namespace Cafe.Core.Interfaces.Application
{
    public interface ICafeConfiguration
    {
        string GetConnectionString();
        DatabaseMode GetDatabaseMode();
    }
}
