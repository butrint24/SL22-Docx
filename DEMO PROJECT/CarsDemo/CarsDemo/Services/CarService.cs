using AutoMapper;
using CarsDemo.DTOS.Car.Requests;
using CarsDemo.DTOS.Car.Responses;
using CarsDemo.Interfaces;
using CarsDemo.Models;

namespace CarsDemo.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly ILogger<CarService> _logger;
        private readonly IMapper _mapper;

        public CarService(ICarRepository carRepository, ILogger<CarService> logger, IMapper mapper)
        {
            _carRepository = carRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<CarCreateResponse> CreateCar(CarCreateRequest car)
        {
            try
            {
                var mappedCar = _mapper.Map<Car>(car);

                if (mappedCar.BrandId == Guid.Empty)
                    throw new ArgumentNullException();
                
                if (mappedCar.Type == String.Empty)
                    throw new ArgumentNullException();

                if (mappedCar.EngineType == String.Empty)
                    throw new ArgumentNullException();

                var result = await _carRepository.CreateCar(mappedCar);

                if (!(result is null))
                {
                    return _mapper.Map<CarCreateResponse>(result);
                }

                return null;
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError("Could not create car: ", ex.Message);
                throw;
            }
        }

        public async Task<bool> DeleteCar(Guid id)
        {
            try
            {
                var car = await _carRepository.GetCarById(id);

                if (!(car is null))
                    return await _carRepository.DeleteCar(car);

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not delete car: ", ex.Message);
                throw;
            }
        }

        public async Task<List<CarReadResponse>> GetAllCars()
        {
            try
            {
                var carList = await _carRepository.GetAllBCars();
                if (carList.Any())
                    return _mapper.Map<List<CarReadResponse>>(carList);

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get cars: ", ex.Message);
                throw;
            }
        }

        public async Task<CarReadResponse> GetCarById(Guid id)
        {
            try
            {
                var car = await _carRepository.GetCarById(id);

                if (!(car is null))
                    return _mapper.Map<CarReadResponse>(car);

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not find car: ", ex.Message);
                throw;
            }
        }

        public async Task<CarUpdateResponse> UpdateCar(Guid id, CarUpdateRequest car)
        {
            var updateCar = await _carRepository.GetCarById(id);

            if (!(updateCar is null))
            {
                if(id == updateCar.Id)
                {
                    var mappedCar = _mapper.Map<Car>(car);
                    var result = await _carRepository.UpdateCar(mappedCar);

                    if (result.BrandId == Guid.Empty)
                        throw new ArgumentNullException("Error");

                    return _mapper.Map<CarUpdateResponse>(result);
                }
                return null;
            }
            return null;
        }
    }
}
