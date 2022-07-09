using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstractions
{
    public interface IRepository<T> where T : class
    {
        public Task AddAsync(T item);
        public Task<T> GetAsync(string identifier);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task UpdateAsync(T item);
        public Task DeleteAsync(T item);
    }
}
