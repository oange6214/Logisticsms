using Logisticsm.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace Logisticsm.Repository.Providers
{
    public class SeaTransportDetailProvider : ProviderBase, IProvider<SeaTransportDetail>
    {
        public int Delete(SeaTransportDetail entity)
        {
            db.Entry(entity).State = EntityState.Deleted;
            return db.SaveChanges();
        }

        public List<SeaTransportDetail> GetAll()
        {
            return db.SeaTransportDetails.ToList();
        }

        public SeaTransportDetail GetItemById(int id)
        {
            return db.SeaTransportDetails.First(item => item.Id == id);
        }

        public int Insert(SeaTransportDetail entity)
        {
            db.Entry(entity).State = EntityState.Added;
            return db.SaveChanges();
        }

        public int Save()
        {
            return db.SaveChanges();
        }

        public int Update(SeaTransportDetail entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            return db.SaveChanges();
        }
    }
}
