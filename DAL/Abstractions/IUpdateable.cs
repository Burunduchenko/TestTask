using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstractions
{
    public interface IUpdateable<T> : IRepository<T> where T : class
    {
        public Task UpdateAsync(T item);
    }
}
