namespace SpeedHero.Data.Common.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;

    using SpeedHero.Data.Common.Models;

    public class DeletableEntityRepository<T> : GenericRepository<T>, IDeletableEntityRepository<T>
        where T : class, IDeletableEntity
    {
        public DeletableEntityRepository(DbContext context)
            : base(context)
        {
        }

        public override IQueryable<T> All()
        {
            return base.All().Where(x => !x.IsDeleted);
        }

        public IQueryable<T> AllWithDeleted()
        {
            return base.All();
        }

        public override T GetById(int id)
        {
            T entity = base.GetById(id);
            if (entity.IsDeleted)
            {
                return null;
            }

            return entity;
        }

        // You don't actually delete anything from the db, but you have to check for the IsDeleted flag every time you get actual data.
        public override void Delete(T entity)
        {
            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.Now;
            DbEntityEntry entry = this.Context.Entry(entity);
            entry.State = EntityState.Modified;
        }

        public void ActualDelete(T entity)
        {
            base.Delete(entity);
        }

        public void ActualDelete(int id)
        {
            var entity = this.GetById(id);

            if (entity != null)
            {
                base.Delete(entity);
            }
        }
    }
}
