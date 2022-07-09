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
    public class AccountService : IService<Account>, IBaseService<Account>
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

        public async Task DeleteAsync(Account item)
        {
            var dbaccount = await _unitOfWork.AccountRepository.GetAsync(item.Name);
            if (dbaccount is not null)
            {
                await _unitOfWork.AccountRepository.DeleteAsync(item);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            return await _unitOfWork.AccountRepository.GetAllAsync();
        }

        public async Task<Account> GetAsync(string identifier)
        {
            return await _unitOfWork.AccountRepository.GetAsync(identifier);
        }

        public async Task UpdateAsync(Account item)
        {
            await _unitOfWork.AccountRepository.UpdateAsync(item);
        }
    }
}
