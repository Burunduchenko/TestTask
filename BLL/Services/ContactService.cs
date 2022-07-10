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
    public class ContactService : IService<ContactViewModel, ContactAddModel>, IBaseService<ContactAddModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ContactService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddAsync(ContactAddModel item)
        {
            var dbcontact = await _unitOfWork.ContactRepository.GetAsync(item.Email);
            if(dbcontact is null)
            {
                await _unitOfWork.ContactRepository.AddAsync(_mapper.Map<Contact>(item));
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

        public async Task<IEnumerable<ContactViewModel>> GetAllAsync()
        {
            var result = await _unitOfWork.ContactRepository.GetAllAsync();
            return result.Select(x => _mapper.Map<ContactViewModel>(x));
        }

        public async Task<ContactViewModel> GetAsync(string identifier)
        {
            var result = await _unitOfWork.ContactRepository.GetAsync(identifier);
            if (result is not null)
            {
                return _mapper.Map<ContactViewModel>(result);
            }
            else
            {
                throw new ArgumentException();
            } 
        }

    }
}
