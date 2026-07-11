using AutoMapper;
using Cashing.Dto;
using Cashing.Models;

namespace Cashing.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductResponse>();
        }
    }
}
