using AutoMapper;
using CoreTestFramework.Northwind.Entities.Concrate;
using CoreTestFramework.Northwind.Entities.DTO;

namespace CoreTestFramework.Northwind.WebMvcUI.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>()
            .ForMember(dto => dto.supplier, entity => entity.MapFrom(p => p.supplierName()))
            .ForMember(dto => dto.category, entity => entity.MapFrom(p => p.categoryName()));
            
        }
    }
}