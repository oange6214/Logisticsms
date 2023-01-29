using Logisticsm.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace Logisticsm.Repository.Providers;

public class AirTransportProvider : ProviderBase, IProvider<AirTransport>
{
    public int Delete(AirTransport entity)
    {
        db.Entry(entity).State = EntityState.Deleted;
        return db.SaveChanges();
    }

    public List<AirTransport> GetAll()
    {
        return db.AirTransports.OrderByDescending(t => t.Id).ToList();
    }

    public AirTransport GetItemById(int id)
    {
        return db.AirTransports.First(item => item.Id == id);
    }

    public int Insert(AirTransport entity)
    {
        db.Entry(entity).State = EntityState.Added;
        return db.SaveChanges();
    }

    public int Save()
    {
        return db.SaveChanges();
    }

    public int Update(AirTransport entity)
    {
        db.Entry(entity).State = EntityState.Modified;
        return db.SaveChanges();
    }
}
