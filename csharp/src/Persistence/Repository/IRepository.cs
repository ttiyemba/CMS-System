
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;


namespace src.Persistence.Repository
{
    public interface IRepository<TEntity>
    {
        public Task<List<TEntity>> GetAllAsync();
        public Task<List<TEntity>> GetAllAsync<TProperty>(Expression<Func<TEntity, TProperty>> functionToInclude);

        public Task<TEntity> GetSingleAsync(int id);

        public Task<TEntity> GetSingleAsync<TProperty>(Expression<Func<TEntity, bool>> searchCondition, Expression<Func<TEntity, TProperty>> functionToInclude);

        public void Add(TEntity entity);
        public void Delete(TEntity entity);

        public void Update(TEntity entity);

        public Task<int> Save();





    }
}