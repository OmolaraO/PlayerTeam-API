﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryBase<T>
    {
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
