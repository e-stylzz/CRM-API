using System;
using AutoMapper;
using CRMAPI.WebApi.DTO;

namespace CRMAPI.WebApi.Profiles
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<ContactProfile, ContactDto>()
                .ReverseMap();
        }
    }
}
