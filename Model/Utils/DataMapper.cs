using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace Model.Utils
{
    public class DataMapper
    {
        public MinimalDbContext DbContext { get; }

        public DataMapper(MinimalDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<T> Create<T>(T entity)
            where T : Entity
        {
            var created = await DbContext.Set<T>().AddAsync(entity);
            await this.Save();
            return created.Entity;
        }

        public async Task<T> Delete<T>(T entity)
            where T : Entity
        {
            var deleted = DbContext.Set<T>().Remove(entity);
            await this.Save();
            return deleted.Entity;
        }

        public async Task Update<T>(T entity)
            where T : Entity
        {
            await this.Save();
            return;
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
