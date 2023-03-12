using Logisticsm.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace Logisticsm.Repository.Providers
{
    public class SeaTransportProvider : ProviderBase, IProvider<SeaTransport>
    {
        public int Delete(SeaTransport entity)
        {
            db.Entry(entity).State = EntityState.Deleted;
            return db.SaveChanges();
        }

        public List<SeaTransport> GetAll()
        {
            return db.SeaTransports.ToList();
        }

        public SeaTransport GetItemById(int id)
        {
            return db.SeaTransports.First(item => item.Id == id);
        }

        public int Insert(SeaTransport entity)
        {
            db.Entry(entity).State = EntityState.Added;
            return db.SaveChanges();
        }

        public int Save()
        {
            return db.SaveChanges();
        }

        public int Update(SeaTransport entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            return db.SaveChanges();
        }
    }
}
