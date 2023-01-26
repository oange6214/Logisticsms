using Logisticsm.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace Logisticsm.Repository.Providers;

public class CustomerProvider : ProviderBase, IProvider<Customer>
{
    public int Delete(Customer entity)
    {
        db.Entry(entity).State = EntityState.Deleted;
        return db.SaveChanges();
    }

    public List<Customer> GetAll()
    {
        return db.Customers.ToList();
    }

    public int Insert(Customer entity)
    {
        db.Entry(entity).State = EntityState.Added;
        return db.SaveChanges();
    }

    public int Save()
    {
        return db.SaveChanges();
    }

    public int Update(Customer entity)
    {
        db.Entry(entity).State = EntityState.Modified;
        return db.SaveChanges();
    }
}
