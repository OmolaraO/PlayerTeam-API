using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Helpers;

namespace Repository
{

    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected DataContext DataContext;

        public RepositoryBase(DataContext repositoryContext)
        => DataContext = repositoryContext;
        public void Create(T entity) => DataContext.Set<T>().Add(entity);
        
        public void Delete(T entity) => DataContext.Set<T>().Remove(entity);
        

        public void Update(T entity) => DataContext.Set<T>().Update(entity);
       
    }
}
