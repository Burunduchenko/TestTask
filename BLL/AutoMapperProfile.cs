using AutoMapper;
using BLL.AddModels;
using BLL.ViewModels;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<Contact, ContactViewModel>().ReverseMap();
            CreateMap<Contact, ContactAddModel>().ReverseMap();
            CreateMap<Incedent, AccountViewModel>().ReverseMap();

            CreateMap<Account, AccountViewModel>().ReverseMap();
            CreateMap<Account, AccountAddModel>().ReverseMap();
            CreateMap<Contact, AccountAddModel>()
                .ForMember(x => x.ContactFirstName, y => y.MapFrom(x => x.FirstName))
                .ForMember(x => x.ContactLastName, y => y.MapFrom(x => x.LastName))
                .ForMember(x => x.ContactEmail, y => y.MapFrom(x => x.Email))
                .ReverseMap();



        }
    }
}
