using System;
using AutoMapper;
using CRMAPI.Domain.Models;
using CRMAPI.WebApi.DTO;

namespace CRMAPI.WebApi.Profiles
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<Contact, ContactDto>()
                .ReverseMap();
            CreateMap<Contact, ContactCreateDto>()
                .ReverseMap();
            CreateMap<Contact, ContactPutDto>()
                .ReverseMap();
        }
    }
}
