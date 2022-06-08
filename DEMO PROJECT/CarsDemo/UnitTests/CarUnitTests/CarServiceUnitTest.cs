using AutoMapper;
using Bogus;
using CarsDemo.DTOS.Car.Requests;
using CarsDemo.DTOS.Car.Responses;
using CarsDemo.Interfaces;
using CarsDemo.Models;
using CarsDemo.Services;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.CarUnitTests.Helper;
using Xunit;

namespace UnitTests.CarUnitTests
{
    public class CarServiceUnitTest
    {
        private static readonly List<Car> carList = CarHelper.CarListData();
        private static readonly List<CarReadResponse> carReadResponseList = CarHelper.CarReadResponseListData(carList);
        private readonly Mock<ICarRepository> _carRepositoryMock = new();
        private readonly Mock<ILogger<CarService>> _loggerMock = new();
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly CarService _sut;

        public CarServiceUnitTest()
        {
            _sut = new CarService(
                _carRepositoryMock.Object,
                _loggerMock.Object,
                _mapperMock.Object);
        }

        [Fact]
        public async Task CreateCar_ValidInfo_CarCreated()
        {
            //Arrange
            var car = CarHelper.GetCarRequest(Guid.NewGuid(), Guid.NewGuid(), "suv", "Electric");
            var carCreateResponse = CarHelper.CarCreateResponseData(Guid.NewGuid(), Guid.NewGuid(), "suv", "Electric");
            var carCreateRequest = CarHelper.CarCreateRequestData();

            _carRepositoryMock.Setup(x => x.CreateCar(It.IsAny<Car>())).ReturnsAsync(car);
            _mapperMock.Setup(x => x.Map<Car>(It.IsAny<CarCreateRequest>())).Returns(car);
            _mapperMock.Setup(x => x.Map<CarCreateResponse>(It.IsAny<Car>())).Returns(carCreateResponse);

            //Act
            var result = await _sut.CreateCar(carCreateRequest);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result, carCreateResponse);
        }

        [Fact]
        public async Task CreateCar_InValidInfo_CarWithoutBrand()
        {
            //Arrange
            var car = CarHelper.GetCarRequest(Guid.NewGuid(), Guid.Empty, "suv", "Electric");

            _mapperMock.Setup(x => x.Map<Car>(It.IsAny<CarCreateRequest>())).Returns(car);


            //Assert
            var result = await Assert.ThrowsAsync<ArgumentNullException>(async () => await _sut.CreateCar(new CarCreateRequest()));
            Assert.Equal("Value cannot be null.", result.Message);
        }

        [Fact]
        public async Task CreateCar_InValidInfo_CarWithoutType()
        {
            //Arrange
            var car = CarHelper.GetCarRequest(Guid.NewGuid(), Guid.NewGuid(), String.Empty, "Electric");
            
            _mapperMock.Setup(x => x.Map<Car>(It.IsAny<CarCreateRequest>())).Returns(car);

            //Act
            var result = await Assert.ThrowsAsync<ArgumentNullException>(async () => await _sut.CreateCar(new CarCreateRequest()));

            //Assert
            Assert.Equal("Value cannot be null.", result.Message);
        }

        [Fact]
        public async Task CreateCar_InValidInfo_CarWithoutEngineType()
        {
            //Arrange
            var car = CarHelper.GetCarRequest(Guid.NewGuid(), Guid.NewGuid(), "suv", String.Empty);
            
            _mapperMock.Setup(x => x.Map<Car>(It.IsAny<CarCreateRequest>())).Returns(car);

            //Act
            var result = await Assert.ThrowsAsync<ArgumentNullException>(async () => await _sut.CreateCar(new CarCreateRequest()));

            //Assert
            Assert.Equal("Value cannot be null.", result.Message);
        }

        [Fact]
        public async Task DeleteCar_ValidInfo_CarDeleted()
        {
            //Arrange
            var car = CarHelper.GetCarRequest(Guid.NewGuid(), Guid.NewGuid(), "suv", "Electric");
            
            _carRepositoryMock.Setup(x => x.DeleteCar(It.IsAny<Car>())).ReturnsAsync(true);
            _carRepositoryMock.Setup(x => x.GetCarById(It.IsAny<Guid>())).ReturnsAsync(car);

            //Act
            var result = await _sut.DeleteCar(car.Id);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteCar_InValidInfo_CouldNotFindCar()
        {
            //Arrange
            var car = CarHelper.GetCarRequest(Guid.Empty, Guid.Empty, null, null);
            
            _carRepositoryMock.Setup(x => x.GetCarById(It.IsAny<Guid>())).ReturnsAsync(() => null);

            //Act
            var result = await _sut.GetCarById(car.Id);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllCars_Cars_ListCars()
        {
            //Arrange
            _mapperMock.Setup(x => x.Map<List<CarReadResponse>>(It.IsAny<List<Car>>())).Returns(carReadResponseList);
            _carRepositoryMock.Setup(x => x.GetAllBCars()).ReturnsAsync(carList);

            //Act
            var result = await _sut.GetAllCars();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result, carReadResponseList);
        }

        [Fact]
        public async Task GetAllCars_InValidData_NoListOfCars()
        {
            //Arrange
            _carRepositoryMock.Setup(x => x.GetAllBCars()).ReturnsAsync(new List<Car>());

            //Act
            var result = await _sut.GetAllCars();

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetCarById_ValidData_Car()
        {
            //Arrange
            var car = CarHelper.GetCarRequest(Guid.NewGuid(), Guid.NewGuid(), "Suv", "Gasoline");
            var carReadResponse = CarHelper.CarReadResponse(Guid.NewGuid(), Guid.NewGuid(), "Suv", "Diesel");
            
            _carRepositoryMock.Setup(x => x.GetCarById(It.IsAny<Guid>())).ReturnsAsync(car);
            _mapperMock.Setup(x => x.Map<CarReadResponse>(It.IsAny<Car>())).Returns(carReadResponse);

            //Act
            var result = await _sut.GetCarById(Guid.NewGuid());

            //Assert
            Assert.NotNull(result);
            Assert.Equal(carReadResponse, result);
        }

        [Fact]
        public async Task GetCarById_InValidData_NoCarReturned()
        {
            //Arrange
            var carReadResponse = CarHelper.CarReadResponse(Guid.NewGuid(), Guid.NewGuid(), "Suv", "Diesel");
            
            _carRepositoryMock.Setup(x => x.GetCarById(It.IsAny<Guid>()));
            _mapperMock.Setup(x => x.Map<CarReadResponse>(It.IsAny<Car>())).Returns(carReadResponse);

            //Act
            var result = await _sut.GetCarById(Guid.Empty);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateCar_ValidData_CarUpdated()
        {
            //Arrange
            Car newCar = new Car();
            newCar.Id = Guid.NewGuid();
            newCar.BrandId = Guid.NewGuid();
            newCar.Type = "Sudan";
            newCar.EngineType = "Electric";
            
            var car = CarHelper.GetCarRequest(newCar.Id, Guid.NewGuid(), "Suv", "Gasoline");
            var carUpdateResponsee = CarHelper.CarUpdateResponseData(car);
            var carUpdateRequest = CarHelper.CarUpdateRequestData(car);
            
            _carRepositoryMock.Setup(x => x.GetCarById(It.IsAny<Guid>())).ReturnsAsync(newCar);
            _carRepositoryMock.Setup(x => x.UpdateCar(It.IsAny<Car>())).ReturnsAsync(car);

            CarUpdateResponse carUpdateResponse = CarHelper.CarUpdateResponseData(newCar);
            _mapperMock.Setup(x => x.Map<Car>(It.IsAny<CarUpdateRequest>())).Returns(newCar);
            _mapperMock.Setup(x => x.Map<CarUpdateResponse>(It.IsAny<Car>())).Returns(carUpdateResponse);

            //Act
            var result = await _sut.UpdateCar(car.Id, carUpdateRequest);

            //Assert
            Assert.NotEqual(result.EngineType, car.EngineType);
        }

        [Fact]
        public async Task UpdateCar_InValidData_CarNotFound()
        {
            //Arrange
            var car = CarHelper.GetCarRequest(Guid.Empty, Guid.Empty, null, null);
            var carUpdateRequest = CarHelper.CarUpdateRequestData(car);
            
            _carRepositoryMock.Setup(x => x.GetCarById(It.IsAny<Guid>())).ReturnsAsync(() => null);

            //Act
            var result = await _sut.UpdateCar(car.Id, carUpdateRequest);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateCar_InValidData_IdMissMatch()
        {
            //Arrange
            var car = CarHelper.GetCarRequest(Guid.Empty, Guid.Empty, null, null);
            
            CarCreateRequest carCreateRequest = CarHelper.CarCreateRequestData();
            Car randomCar = CarHelper.CarData(carCreateRequest);
            CarUpdateRequest carUpdateRequest = CarHelper.CarUpdateRequestData(randomCar);
            _carRepositoryMock.Setup(x => x.GetCarById(It.IsAny<Guid>())).ReturnsAsync(randomCar);
            
            //Act
            var result = await _sut.UpdateCar(car.Id, carUpdateRequest);

            //Assert
            Assert.Null(result);
        }
    }
}
