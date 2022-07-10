using DAL.Abstractions;
using DAL.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IncidentDbContext _database;

        public UnitOfWork(IncidentDbContext incidentDbContext)
        {
            _database = incidentDbContext; 
        }

        private IRepository<Account> _AccountRepository;

        private IRepository<Contact> _ContactRepository;

        private IRepository<Incedent> _IncedentRepository;

        public IRepository<Account> AccountRepository
        {
            get
            {
                if( _AccountRepository == null )
                {
                    _AccountRepository = new AccountRepository(_database);
                }
                return _AccountRepository;
            }
        }

        public IRepository<Contact> ContactRepository
        {
            get
            {
                if (_ContactRepository == null)
                {
                    _ContactRepository = new ContactRepository(_database);
                }
                return _ContactRepository;
            }
        }

        public IRepository<Incedent> IncedentRepository
        {
            get
            {
                if (_IncedentRepository == null)
                {
                    _IncedentRepository = new IncedentRepository(_database);
                }
                return _IncedentRepository;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _database.SaveChangesAsync();
        }
    }
}
