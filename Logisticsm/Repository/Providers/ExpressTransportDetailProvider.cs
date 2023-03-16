using Logisticsm.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace Logisticsm.Repository.Providers
{
    public class ExpressTransportDetailProvider : ProviderBase, IProvider<ExpressTransportDetail>
    {
        public int Delete(ExpressTransportDetail entity)
        {
            db.Entry(entity).State = EntityState.Deleted;
            return db.SaveChanges();
        }

        public List<ExpressTransportDetail> GetAll()
        {
            return db.ExpressTransportDetails.ToList();
        }

        public ExpressTransportDetail GetItemById(int id)
        {
            return db.ExpressTransportDetails.First(item => item.Id == id);
        }

        public int Insert(ExpressTransportDetail entity)
        {
            db.Entry(entity).State = EntityState.Added;
            return db.SaveChanges();
        }

        public int Save()
        {
            return db.SaveChanges();
        }

        public int Update(ExpressTransportDetail entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            return db.SaveChanges();
        }
    }
}
