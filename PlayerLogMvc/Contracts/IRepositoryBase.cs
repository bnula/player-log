using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayerLogMvc.Contracts
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<bool> SaveAsync();
        Task<IList<T>> FindAllAsync();
        Task<T> FindByIdAsync(int id);
        Task<bool> CreateAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
    }
}
