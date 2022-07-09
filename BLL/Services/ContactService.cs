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
    public class ContactService : IBaseService<Contact>, IService<Contact>
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

        public async Task DeleteAsync(string email)
        {
            var dbcontact = await _unitOfWork.ContactRepository.GetAsync(email);
            if (dbcontact is null)
            {
                throw new ArgumentException();
            }
            await _unitOfWork.ContactRepository.DeleteAsync(dbcontact);

        }

        public async Task<IEnumerable<Contact>> GetAllAsync()
        {
            return await _unitOfWork.ContactRepository.GetAllAsync();
        }

        public async Task<Contact> GetAsync(string identifier)
        {
            var result =  await _unitOfWork.ContactRepository.GetAsync(identifier);
            if (result is not null)
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
