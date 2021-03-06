﻿namespace SpeedHero.Data.Common.Repositories
{
    using System.Linq;

    public interface IDeletableEntityRepository<T> : IGenericRepository<T> where T : class
    {
        IQueryable<T> AllWithDeleted();

        void ActualDelete(T entity);

        void ActualDelete(int id);
    }
}
