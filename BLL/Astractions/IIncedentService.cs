using BLL.AddModels;
using BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Astractions
{
    public interface IIncedentService : IService<IncedentViewModel, IncedentAddModel>
    {
        public Task AddAllRecords(IncedentAddModel model);
    }
}
