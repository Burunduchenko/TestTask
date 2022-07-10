using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstractions
{
    public interface IUnitOfWork
    {
        IRepository<Account> AccountRepository { get; }
        IUpdateable<Contact> ContactRepository { get; }
        IRepository<Incedent> IncedentRepository { get; }

        Task<int> SaveChangesAsync();
     }
}
