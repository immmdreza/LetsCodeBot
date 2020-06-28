using LetsCodeApp.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LetsCodeApp.Repositories
{
    interface IRepository<T> where T : DbEntityBase
    {
        Task<T> GetById(long Id);

        Task<long> Create(T entity);
        Task Delete(T entity);
        Task Update(T entity);
    }
}
