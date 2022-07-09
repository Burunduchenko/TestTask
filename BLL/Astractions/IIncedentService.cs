using BLL.AddModels;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Astractions
{
    public interface IIncedentService : IBaseService<Incedent>
    {
        public Task AddAllRecords(IncedentAddModel model);
    }
}
