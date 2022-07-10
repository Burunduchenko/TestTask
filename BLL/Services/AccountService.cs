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
                var dbcontact = await _unitOfWork.ContactRepository.GetAsync(item.ContactEmail);
                if(dbcontact is null)
                {
                    await AddAccountAndContactAsync(item);
                }
                else
                {
                    await AddAccountUpdateContact(item);
                }
                await _unitOfWork.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException();
            }
        }

        private async Task AddAccountUpdateContact(AccountAddModel item)
        {
            var contact = _mapper.Map<Contact>(item);
            var account = new Account()
            {
                Name = item.Name,
                Contacts = new List<Contact>() { contact }
            };
            contact.Account = account;

            await _unitOfWork.ContactRepository.UpdateAsync(contact);
            await _unitOfWork.AccountRepository.AddAsync(account);
        }

        private async Task AddAccountAndContactAsync(AccountAddModel item)
        {
            var contact = _mapper.Map<Contact>(item);
            var account = new Account()
            {
                Name = item.Name,
                Contacts = new List<Contact>() { contact }
            };
            contact.Account = account;
            await _unitOfWork.ContactRepository.AddAsync(contact);
            await _unitOfWork.AccountRepository.AddAsync(account);
        }

        public async Task DeleteAsync(string name)
        {
            var dbaccount = await _unitOfWork.AccountRepository.GetAsync(name);
            if (dbaccount is null)
            {
                throw new ArgumentException();
            }
            await _unitOfWork.AccountRepository.DeleteAsync(dbaccount);
            await _unitOfWork.SaveChangesAsync();

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
