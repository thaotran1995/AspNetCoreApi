using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace AspNetCoreApi
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        { 
            CreateMap<Owner, OwnerDTO>();    
        }
    }
}
