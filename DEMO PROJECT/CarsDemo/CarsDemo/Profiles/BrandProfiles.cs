using AutoMapper;
using CarsDemo.DTOS.Brand.Requests;
using CarsDemo.DTOS.Brand.Responses;
using CarsDemo.Models;

namespace CarsDemo.Profiles
{
    public class BrandProfiles : Profile
    {
        public BrandProfiles()
        {
            CreateMap<Brand, BrandReadResponse>().ReverseMap();
            CreateMap<Brand, BrandUpdateResponse>().ReverseMap();
            CreateMap<Brand, BrandCreateResponse>().ReverseMap();
            CreateMap<BrandCreateRequest, Brand>();
            CreateMap<BrandUpdateRequest, Brand>();
        }
    }
}
