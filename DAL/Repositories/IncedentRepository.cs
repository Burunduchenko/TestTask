using DAL.Abstractions;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class IncedentRepository : IRepository<Incedent>
    {
        private IncidentDbContext _databaase;

        public IncedentRepository(IncidentDbContext incidentDbContext)
        {
            _databaase = incidentDbContext;
        }
        public async Task AddAsync(Incedent item)
        {
            await _databaase.Incidents.AddAsync(item);
        }

        public async Task DeleteAsync(Incedent item)
        {
            await Task.Factory.StartNew(() => _databaase.Incidents.Remove(item));
        }

        public async Task<IEnumerable<Incedent>> GetAllAsync()
        {
            var incedents = Task.Factory.StartNew(() => 
            _databaase.Incidents.Include(x => x.Accounts).AsEnumerable());
            return await incedents;
        }

        public async Task<Incedent> GetAsync(string name)
        {
            var incedent = Task.Factory.StartNew(() => 
            _databaase.Incidents.Include(x => x.Accounts).FirstOrDefault(x => x.Name == name));
            return await incedent;
        }

        public async Task UpdateAsync(Incedent item)
        {
            await Task.Factory.StartNew(() => _databaase.Incidents.Update(item));
        }
    }
}
