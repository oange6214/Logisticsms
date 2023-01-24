using Logisticsm.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Logisticsm.DAL
{
	public class AirTransportDetailProvider : ProviderBase, IProvider<AirTransportDetail>
	{
		public int Delete(AirTransportDetail entity)
		{
			db.Entry(entity).State = EntityState.Deleted;
			return db.SaveChanges();
		}

		public List<AirTransportDetail> GetAll()
		{
			return db.AirTransportDetails.ToList();
		}

		public int Insert(AirTransportDetail entity)
		{
			db.Entry(entity).State = EntityState.Added;
			return db.SaveChanges();
		}

		public int Update(AirTransportDetail entity)
		{
			db.Entry(entity).State = EntityState.Modified;
			return db.SaveChanges();
		}
	}
}
