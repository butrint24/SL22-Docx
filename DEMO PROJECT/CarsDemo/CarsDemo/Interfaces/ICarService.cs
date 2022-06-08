using CarsDemo.DTOS.Car.Requests;
using CarsDemo.DTOS.Car.Responses;

namespace CarsDemo.Interfaces
{
    public interface ICarService
    {
        Task<List<CarReadResponse>> GetAllCars();
        Task<CarReadResponse> GetCarById(Guid id);
        Task<CarCreateResponse> CreateCar(CarCreateRequest car);
        Task<CarUpdateResponse> UpdateCar(Guid id, CarUpdateRequest car);
        Task<bool> DeleteCar(Guid id);
    }
}
