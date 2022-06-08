using Bogus;
using CarsDemo.DTOS.Car.Requests;
using CarsDemo.DTOS.Car.Responses;
using CarsDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.CarUnitTests.Helper
{
    public class CarHelper
    {
        public static Car CarData(CarCreateRequest carRequest)
        {
            return new Faker<Car>()
                .RuleFor(x => x.Id, Guid.NewGuid)
                .RuleFor(x => x.BrandId, carRequest.BrandId)
                .RuleFor(x => x.Type, carRequest.Type)
                .RuleFor(x => x.EngineType, carRequest.EngineType)
                .Generate();
        }

        public static Car CarDataWithoutBrand(CarCreateRequest carRequest)
        {
            return new Faker<Car>()
                .RuleFor(x => x.Id, Guid.NewGuid)
                .RuleFor(x => x.BrandId, Guid.Empty)
                .RuleFor(x => x.Type, carRequest.Type)
                .RuleFor(x => x.EngineType, carRequest.EngineType)
                .Generate();
        }

        public static Car GetCarRequest(Guid id, Guid brandId, string type, string engineType)
        {
            return new Faker<Car>()
                .RuleFor(x => x.Id, id)
                .RuleFor(x => x.BrandId, brandId)
                .RuleFor(x => x.Type, type)
                .RuleFor(x => x.EngineType, engineType)
                .Generate();
        }

        public static CarCreateResponse CarCreateResponseData(Guid id, Guid brandId, string type, string engineType)
        {
            return new Faker<CarCreateResponse>()
                .RuleFor(x => x.Id, id)
                .RuleFor(x => x.BrandId, brandId)
                .RuleFor(x => x.Type, type)
                .RuleFor(x => x.EngineType, engineType)
                .Generate();
        }

        public static CarCreateRequest CarCreateRequestData()
        {
            return new Faker<CarCreateRequest>()
                .RuleFor(x => x.BrandId, Guid.NewGuid())
                .RuleFor(x => x.Type, x => x.Vehicle.Type())
                .RuleFor(x => x.EngineType, x => x.Vehicle.Fuel())
                .Generate();
        }

        public static CarReadResponse CarReadResponseData(Car car)
        {
            return new CarReadResponse()
            {
                Id = car.Id,
                BrandId = car.BrandId,
                Type = car.Type,
                EngineType = car.EngineType
            };
        }

        public static CarReadResponse CarReadResponse(Guid id, Guid brandId, string type, string engineType)
        {
            return new Faker<CarReadResponse>()
                .RuleFor(x => x.Id, id)
                .RuleFor(x => x.BrandId, brandId)
                .RuleFor(x => x.Type, type)
                .RuleFor(x => x.EngineType, engineType)
                .Generate();
        }

        public static List<Car> CarListData()
        {
            List<Car> carList = new();
            for (int i = 0; i < 10; i++)
            {
                CarCreateRequest carCreateRequest = CarCreateRequestData();
                Car car = CarData (carCreateRequest);

                carList.Add(car);
            }
            return carList;
        }

        public static List<CarReadResponse> CarReadResponseListData(List<Car> carList)
        {
            List<CarReadResponse> carReadResponseList = new();
            for (int i = 0; i < 10; i++)
            {
                CarReadResponse carReadRespons = CarReadResponseData(carList[i]);

                carReadResponseList.Add(carReadRespons);
            }
            return carReadResponseList;
        }

        public static CarUpdateRequest CarUpdateRequestData(Car car)
        {
            return new Faker<CarUpdateRequest>()
                .RuleFor(x => x.Id, car.Id)
                .RuleFor(x => x.BrandId, car.BrandId)
                .RuleFor(x => x.Type, car.Type)
                .RuleFor(x => x.EngineType, car.EngineType)
                .Generate();
        }

        public static CarUpdateResponse CarUpdateResponseData(Car car)
        {
            return new CarUpdateResponse()
            {
                Id = car.Id,
                BrandId = car.BrandId,
                Type = car.Type,
                EngineType = car.EngineType
            };
        }
    }
}
