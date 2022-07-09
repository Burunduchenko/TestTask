using BLL.AddModels;
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
    public class IncedentService : IIncedentService, IService<Incedent>
    {
        private readonly IUnitOfWork _unitOfWork;

        public IncedentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddAllRecords(AllEntitiesAddModel model)
        {
            var dbaccount = await _unitOfWork.AccountRepository.GetAsync(model.AccountName);
            if(dbaccount is null)
            {
                throw new ArgumentException();
            }

            var dbcontact = await _unitOfWork.ContactRepository.GetAsync(model.ContactEmail);
            if(dbaccount is not null)
            {
                await _unitOfWork.ContactRepository.UpdateAsync(new Contact() 
                {   Account = dbaccount, 
                    Email = model.ContactEmail,
                    FirstName = model.ContactFirstName, 
                    LastName = model.ContactLastName
                });
            } 
            else
            {
                await _unitOfWork.ContactRepository.AddAsync(new Contact()
                {
                    Account = dbaccount,
                    Email = model.ContactEmail,
                    FirstName = model.ContactFirstName,
                    LastName = model.ContactLastName
                });
            }

            await _unitOfWork.IncedentRepository.AddAsync(new Incedent()
            {
                Description = model.IncedentDescription,
                Accounts = new List<Account>() { dbaccount }
            }) ;

        }

        public async Task DeleteAsync(Incedent item)
        {
            await _unitOfWork.IncedentRepository.DeleteAsync(item);
        }

        public async Task<IEnumerable<Incedent>> GetAllAsync()
        {
            return await _unitOfWork.IncedentRepository.GetAllAsync();
        }

        public async Task<Incedent> GetAsync(string identifier)
        {
            return await _unitOfWork.IncedentRepository.GetAsync(identifier);
        }
    }
}
