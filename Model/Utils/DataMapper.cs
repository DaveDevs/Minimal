using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace Model.Utils
{
    public class DataMapper
    {
        private MinimalDbContext DbContext { get; }

        public DataMapper(MinimalDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<T> Create<T>(T entity)
            where T : Entity
        {
            this.DbContext.Entry(entity).State = EntityState.Added;
            await this.Save();
            return entity;
        }

        public async Task<T> Delete<T>(T entity)
            where T : Entity
        {
            this.DbContext.Entry(entity).State = EntityState.Deleted;
            await this.Save();
            return entity;
        }

        public async Task<T> Update<T>(T entity)
            where T : Entity
        {
            this.DbContext.Entry(entity).State = EntityState.Modified;
            await this.Save();
            return entity;
        }

        public Task<T> GetById<T>(int id)
            where T: Entity
        {
            return DbContext.Set<T>().SingleAsync(x => x.Id == id);
        }

        public IQueryable<T> Set<T>()
            where T : Entity
        {
            return DbContext.Set<T>().AsQueryable();
        }

        private Task Save()
        {
            return DbContext.SaveChangesAsync();
        }

        public Task StartTransaction()
        {
            return DbContext.Database.BeginTransactionAsync();
        }

        public Task CommitTransaction()
        {
            return DbContext.Database.CommitTransactionAsync();
        }

        public Task RollbackTransaction()
        {
            return DbContext.Database.RollbackTransactionAsync();
        }
    }
}
