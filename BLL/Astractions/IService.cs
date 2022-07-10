using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Astractions
{
    public interface IService<View, Add> : IBaseService<Add>
    {
        
        public Task<View> GetAsync(string identifier);
        public Task<IEnumerable<View>> GetAllAsync();
        public Task DeleteAsync(string identifire);
    }
}
