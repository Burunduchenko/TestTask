using BLL.Astractions;
using DAL.Abstractions;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AccountService : IBaseService<Account>, IService<Account>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(Account item)
        {
            var dbaccount = await _unitOfWork.AccountRepository.GetAsync(item.Name);
            if(dbaccount is null)
            {
                await _unitOfWork.AccountRepository.AddAsync(item);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public async Task DeleteAsync(string name)
        {
            var dbaccount = await _unitOfWork.AccountRepository.GetAsync(name);
            if (dbaccount is not null)
            {
                throw new ArgumentException();
            }
            await _unitOfWork.AccountRepository.DeleteAsync(dbaccount);

        }

        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            return await _unitOfWork.AccountRepository.GetAllAsync();
        }

        public async Task<Account> GetAsync(string identifier)
        {
            var result = await _unitOfWork.AccountRepository.GetAsync(identifier);
            if(result is not null)
            {
                return result;
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}
