using CarsDemo.Models;

namespace CarsDemo.Interfaces
{
    public interface ICarRepository
    {
        Task<List<Car>> GetAllBCars();
        Task<Car> GetCarById(Guid id);
        Task<Car> CreateCar(Car car);
        Task<Car> UpdateCar(Car car);
        Task<bool> DeleteCar(Car car);
    }
}
