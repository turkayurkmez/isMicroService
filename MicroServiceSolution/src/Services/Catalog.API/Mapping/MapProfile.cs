using AutoMapper;
using Catalog.API.DataTransferObjects;
using Catalog.API.Models;

namespace Catalog.API.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Product, ProductResponse>();
            CreateMap<AddProductRequest,Product>();
            CreateMap<UpdateProductRequest, Product>();
        }
    }
}
