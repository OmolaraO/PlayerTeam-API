using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApi.Contracts;
using WebApi.Helpers;

namespace WebApi.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected DataContext DataContext;
        public RepositoryBase(DataContext dataContext)
        => DataContext = dataContext;


        public IQueryable<T> FindAll(bool trackChanges) =>  !trackChanges ? DataContext.Set<T>()
        .AsNoTracking() :
        DataContext.Set<T>();

        public void Create(T entity) => DataContext.Set<T>().Add(entity);

        public void BulkCreate(List<T> entity) => DataContext.Set<T>().AddRange(entity);
       

        public void Delete(T entity) => DataContext.Set<T>().Remove(entity);
      
        public void Update(T entity) => DataContext.Set<T>().Update(entity);

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
            !trackChanges ? DataContext.Set<T>().Where(expression).AsNoTracking(): DataContext.Set<T>().Where(expression);
                
    }
}
