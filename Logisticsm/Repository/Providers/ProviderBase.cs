using Logisticsm.Repository.Data;

namespace Logisticsm.Repository.Providers;

public abstract class ProviderBase
{
    protected OrderDbContext db = null!;

    public ProviderBase()
    {
        db = new OrderDbContext();
    }
}

