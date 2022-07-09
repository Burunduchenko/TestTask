using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Astractions
{
    public interface IService<T> : IBaseService<T> where T : class
    {
        public Task AddAsync(T item);
    }
}
