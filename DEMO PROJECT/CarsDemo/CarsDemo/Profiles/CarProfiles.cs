using AutoMapper;
using CarsDemo.DTOS.Car.Requests;
using CarsDemo.DTOS.Car.Responses;
using CarsDemo.Models;

namespace CarsDemo.Profiles
{
    public class CarProfiles : Profile
    {
        public CarProfiles()
        {
            CreateMap<Car, CarReadResponse>().ReverseMap();
            CreateMap<Car, CarUpdateResponse>().ReverseMap();
            CreateMap<Car, CarCreateResponse>().ReverseMap();
            CreateMap<CarCreateRequest, Car>();
            CreateMap<CarUpdateRequest, Car>();
        }
    }
}
