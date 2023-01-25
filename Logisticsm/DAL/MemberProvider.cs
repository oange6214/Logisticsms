using Logisticsm.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Logisticsm.DAL
{
	public class MemberProvider : ProviderBase, IProvider<Member>
	{
		public int Delete(Member entity)
		{
			db.Entry(entity).State = EntityState.Deleted;
			return db.SaveChanges();
		}

		public List<Member> GetAll()
		{
			return db.Members.ToList();
		}

		public int Insert(Member entity)
		{
			db.Entry(entity).State = EntityState.Added;
			return db.SaveChanges();
		}

        public int Save()
        {
            return db.SaveChanges();
        }

        public int Update(Member entity)
		{
			db.Entry(entity).State = EntityState.Modified;
			return db.SaveChanges();
		}
	}
}
