using BLL.AddModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Astractions
{
    public interface IIncedentService
    {
        public Task AddAllRecords(AllEntitiesAddModel model);
    }
}
