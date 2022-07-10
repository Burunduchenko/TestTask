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
        private IncidentDbContext _database;

        public IncedentRepository(IncidentDbContext incidentDbContext)
        {
            _database = incidentDbContext;
        }
        public async Task AddAsync(Incedent item)
        {
            await _database.Incidents.AddAsync(item);
        }

        public async Task DeleteAsync(Incedent item)
        {
            await Task.Factory.StartNew(() => _database.Incidents.Remove(item));
        }

        public async Task<IEnumerable<Incedent>> GetAllAsync()
        {
            var incedents = Task.Factory.StartNew(() => 
            _database.Incidents.Include(x => x.Accounts).AsEnumerable());
            return await incedents;
        }

        public async Task<Incedent> GetAsync(string name)
        {
            var incedent = await _database.Incidents.Include(x => x.Accounts).FirstOrDefaultAsync(x => x.Name == name);
            return incedent;
        }

        public async Task UpdateAsync(Incedent item)
        {
            await Task.Factory.StartNew(() => _database.Incidents.Update(item));
        }
    }
}
