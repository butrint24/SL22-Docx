using CarsDemo.Data;
using CarsDemo.Interfaces;
using CarsDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace CarsDemo.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly DataContext _context;

        public CarRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Car> CreateCar(Car car)
        {
            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();
            
            return car;
        }

        public async Task<bool> DeleteCar(Car car)
        {
            _context.Remove(car);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Car>> GetAllBCars()
        {
            return await _context.Cars.ToListAsync();
        }

        public async Task<Car> GetCarById(Guid id)
        {
            var car = await _context.Cars.FindAsync(id);

            if(car != null)
            {
                _context.Entry(car).State = EntityState.Deleted;
                return car;
            }
            return car;
        }

        public async Task<Car> UpdateCar(Car car)
        {
            _context.Entry(car).State = EntityState.Modified;
            _context.Update(car);
            await _context.SaveChangesAsync();
            return car;
        }
    }
}
