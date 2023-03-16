using Logisticsm.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace Logisticsm.Repository.Providers
{
    public class ExpressTransportProvider : ProviderBase, IProvider<ExpressTransport>
    {
        public int Delete(ExpressTransport entity)
        {
            db.Entry(entity).State = EntityState.Deleted;
            return db.SaveChanges();
        }

        public List<ExpressTransport> GetAll()
        {
            return db.ExpressTransports.ToList();
        }

        public ExpressTransport GetItemById(int id)
        {
            return db.ExpressTransports.First(item => item.Id == id);
        }

        public int Insert(ExpressTransport entity)
        {
            db.Entry(entity).State = EntityState.Added;
            return db.SaveChanges();
        }

        public int Save()
        {
            return db.SaveChanges();
        }

        public int Update(ExpressTransport entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            return db.SaveChanges();
        }
    }
}
