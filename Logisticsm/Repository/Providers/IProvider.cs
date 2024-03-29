﻿namespace Logisticsm.Repository.Providers;

public interface IProvider<T> where T : class
{
    int Insert(T entity);
    int Update(T entity);
    int Delete(T entity);
    List<T> GetAll();
    int Save();
    T GetItemById(int id);
}
