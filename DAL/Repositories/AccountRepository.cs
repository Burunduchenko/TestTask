﻿using DAL.Abstractions;
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
        private IncidentDbContext _databaase;

        public AccountRepository(IncidentDbContext incidentDbContext)
        {
            _databaase = incidentDbContext;
        }
        public async Task AddAsync(Account item)
        {
            await _databaase.Accounts.AddAsync(item);
        }

        public async Task DeleteAsync(Account item)
        {
            await Task.Factory.StartNew(() => _databaase.Accounts.Remove(item));
        }

        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            var accounts = Task.Factory.StartNew(() =>
            _databaase.Accounts.Include(x => x.Incident).Include(x => x.Contacts).AsEnumerable());
            return await accounts;
        }

        public async Task<Account> GetAsync(string name)
        {
            var account = Task.Factory.StartNew(() =>
            _databaase.Accounts.Include(x => x.Incident).Include(x => x.Contacts).FirstOrDefault(x => x.Name == name));
            return await account;
        }

        public async Task UpdateAsync(Account item)
        {
            await Task.Factory.StartNew(() => _databaase.Accounts.Update(item));
        }
    }
}
