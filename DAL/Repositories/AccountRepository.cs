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
    public class AccountRepository : IRepository<Account>
    {
        private IncidentDbContext _database;

        public AccountRepository(IncidentDbContext incidentDbContext)
        {
            _database = incidentDbContext;
        }
        public async Task AddAsync(Account item)
        {
            await _database.Accounts.AddAsync(item);
        }

        public async Task DeleteAsync(Account item)
        {
            await Task.Factory.StartNew(() => _database.Accounts.Remove(item));
        }

        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            var accounts = Task.Factory.StartNew(() =>
            _database.Accounts.Include(x => x.Incident).Include(x => x.Contacts).AsEnumerable());
            return await accounts;
        }

        public async Task<Account> GetAsync(string name)
        {
            var account = await _database.Accounts.Include(x => x.Incident).Include(x => x.Contacts).FirstOrDefaultAsync(x => x.Name == name);
            return account;
        }
    }
}
