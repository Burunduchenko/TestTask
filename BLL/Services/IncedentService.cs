using AutoMapper;
using BLL.AddModels;
using BLL.Astractions;
using BLL.ViewModels;
using DAL.Abstractions;
using DAL.Entities;


namespace BLL.Services
{
    public class IncedentService : IService<IncedentViewModel, IncedentAddModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public IncedentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddAsync(IncedentAddModel model)
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

        public async Task DeleteAsync(string email)
        {
            var dbcontact = await _unitOfWork.ContactRepository.GetAsync(email);
            if (dbcontact is null)
            {
                throw new ArgumentException();
            }
            await _unitOfWork.ContactRepository.DeleteAsync(dbcontact);

        }

        public async Task<IEnumerable<IncedentViewModel>> GetAllAsync()
        {
            var result = await _unitOfWork.IncedentRepository.GetAllAsync();
            return result.Select(x => _mapper.Map<IncedentViewModel>(x));
        }

        public async Task<IncedentViewModel> GetAsync(string identifier)
        {
            var result = await _unitOfWork.IncedentRepository.GetAsync(identifier);
            if (result is not null)
            {
                return _mapper.Map<IncedentViewModel>(result);
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}
