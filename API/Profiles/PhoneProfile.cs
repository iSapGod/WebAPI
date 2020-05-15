using AutoMapper;
using DataAccessLayer.Entities;
using WebApiTestTask.Models;

namespace WebApiTestTask.Profiles
{
    public class PhoneProfile : Profile
    {
        public PhoneProfile()
        {
            CreateMap<Phone, PhoneModel>().ReverseMap();
        }
    }
}
