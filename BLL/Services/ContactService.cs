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
    public class ContactService : IService<Contact>, IBaseService<Contact>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ContactService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(Contact item)
        {
            var dbcontact = await _unitOfWork.ContactRepository.GetAsync(item.Email);
            if(dbcontact is null)
            {
                await _unitOfWork.ContactRepository.AddAsync(item);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public async Task DeleteAsync(Contact item)
        {
            var dbcontact = await _unitOfWork.ContactRepository.GetAsync(item.Email);
            if (dbcontact is not null)
            {
                await _unitOfWork.ContactRepository.DeleteAsync(item); ;
            }
            else
            {
                throw new ArgumentException();
            }
            
        }

        public async Task<IEnumerable<Contact>> GetAllAsync()
        {
            return await _unitOfWork.ContactRepository.GetAllAsync();
        }

        public async Task<Contact> GetAsync(string identifier)
        {
            return await _unitOfWork.ContactRepository.GetAsync(identifier);
        }

        public async Task UpdateAsync(Contact item)
        {
            await _unitOfWork.ContactRepository.UpdateAsync(item);
        }
    }
}
