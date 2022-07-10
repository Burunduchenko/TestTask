using AutoMapper;
using BLL.AddModels;
using BLL.Astractions;
using BLL.ViewModels;
using DAL.Abstractions;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AccountService : IService<AccountViewModel, AccountAddModel>, IBaseService<AccountAddModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper; 

        public AccountService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddAsync(AccountAddModel item)
        {
            var dbaccount = await _unitOfWork.AccountRepository.GetAsync(item.Name);
            if(dbaccount is null)
            {
                await _unitOfWork.AccountRepository.AddAsync(_mapper.Map<Account>(item));
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

        public async Task<IEnumerable<AccountViewModel>> GetAllAsync()
        {
            var result = await _unitOfWork.AccountRepository.GetAllAsync();
            return result.Select(x => _mapper.Map<AccountViewModel>(x));
        }

        public async Task<AccountViewModel> GetAsync(string identifier)
        {
            var account = await _unitOfWork.AccountRepository.GetAsync(identifier);
            if (account is not null)
            {
                return _mapper.Map<AccountViewModel>(account);
            }
            else
            {
                throw new ArgumentException();
            }
        }

    }
}
