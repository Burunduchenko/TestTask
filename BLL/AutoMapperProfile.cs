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
            CreateMap<Account, AccountViewModel>().ReverseMap();
            CreateMap<Account, AccountAddModel>().ReverseMap();
            CreateMap<Contact, ContactViewModel>().ReverseMap();
            CreateMap<Contact, ContactAddModel>().ReverseMap();
            CreateMap<Incedent, AccountViewModel>().ReverseMap();

        }
    }
}
