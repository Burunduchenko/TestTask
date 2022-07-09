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
    public class ContactRepository : IRepository<Contact>
    {
        private IncidentDbContext _databaase;

        public ContactRepository(IncidentDbContext incidentDbContext)
        {
            _databaase = incidentDbContext;
        }
        public async Task AddAsync(Contact item)
        {
            await _databaase.Contacts.AddAsync(item);
            await _databaase.SaveChangesAsync();
        }

        public async Task DeleteAsync(Contact item)
        {
            await Task.Factory.StartNew(() => _databaase.Contacts.Remove(item));
            await _databaase.SaveChangesAsync();
        }

        public async Task<IEnumerable<Contact>> GetAllAsync()
        {
            var contacts = Task.Factory.StartNew(() => 
            _databaase.Contacts.Include(x => x.Account).AsEnumerable());
            return await contacts;
        }

        public async Task<Contact> GetAsync(string email)
        {
            var contact = Task.Factory.StartNew(() => 
            _databaase.Contacts.Include(x => x.Account).FirstOrDefault(x => x.Email == email));
            return await contact;
        }

        public async Task UpdateAsync(Contact item)
        {
            await Task.Factory.StartNew(() => _databaase.Contacts.Update(item));
            await _databaase.SaveChangesAsync();
        }
    }
}
