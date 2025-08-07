using AutoMapper;
using BusinessObject.Models.Dto;
using BusinessObject.Models.Entity;

namespace ServiceObject.Configurations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Manager, AccountData>();
        }
    }
}