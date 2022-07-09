using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Astractions
{
    public interface IBaseService<T> where T : class
    {
        
        public Task<T> GetAsync(string identifier);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task DeleteAsync(string identifier);
    }
}
