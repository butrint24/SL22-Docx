using AutoMapper;
using CarsDemo.DTOS.Brand.Requests;
using CarsDemo.DTOS.Brand.Responses;
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
using UnitTests.BrandUnitTests.Helper;
using Xunit;

namespace UnitTests.BrandUnitTests
{
    public class BrandServiceUnitTest
    {
        private readonly Mock<IBrandRepository> _brandRepositoryMock = new();
        private readonly Mock<ILogger<BrandService>> _loggerMock = new();
        private readonly Mock<IMapper> _mapperMock = new();
        private static readonly List<Brand> brandList = BrandHelper.BrandListData();
        private static readonly List<BrandReadResponse> brandReadResponseList = BrandHelper.BrandReadResponseListData(brandList);
        private readonly BrandService _sut;

        public BrandServiceUnitTest()
        {
            _sut = new BrandService(
                _brandRepositoryMock.Object, 
                _loggerMock.Object, 
                _mapperMock.Object);
        }

        [Fact]
        public async Task CreateBrand_ValidInfo_BrandCreated()
        {
            //Arrange
            var brand = BrandHelper.GetBrandRequest(Guid.NewGuid(), "Lucky Inc.", "Bronx");
            var brandCreateResponse = BrandHelper.BrandCreateResponseData(Guid.NewGuid(), "Lucky Inc.", "Bronx");
            var brandCreateRequest = BrandHelper.BrandCreateRequestData();

            _brandRepositoryMock.Setup(x => x.CreateBrand(It.IsAny<Brand>())).ReturnsAsync(brand);
            _mapperMock.Setup(x => x.Map<Brand>(It.IsAny<BrandCreateRequest>())).Returns(brand);
            _mapperMock.Setup(x => x.Map<BrandCreateResponse>(It.IsAny<Brand>())).Returns(brandCreateResponse);

            //Act
            var result = await _sut.CreateBrand(brandCreateRequest);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result, brandCreateResponse);
        }

        [Fact]
        public async Task CreateBrand_InValidInfo_Null()
        {
            //Arrange
            var brandCreateRequest = BrandHelper.BrandCreateRequestData();

            _brandRepositoryMock.Setup(x => x.CreateBrand(It.IsAny<Brand>())).ReturnsAsync(() => null!);

            //Act
            var result = await _sut.CreateBrand(brandCreateRequest);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteBrand_ValidInfo_BrandDeleted()
        {
            //Arrange
            var brand = BrandHelper.GetBrandRequest(Guid.NewGuid(), "Lucky Inc.", "Bronx");

            _brandRepositoryMock.Setup(x => x.DeleteBrand(It.IsAny<Brand>())).ReturnsAsync(true);
            _brandRepositoryMock.Setup(x => x.GetBrandById(It.IsAny<Guid>())).ReturnsAsync(brand);

            //Act
            var result = await _sut.DeleteBrand(brand.Id);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteBrand_InValidInfo_CouldNotFindBrand()
        {
            //Arrange
            var brand = BrandHelper.GetBrandRequest(Guid.Empty, "Lucky Inc.", "Bronx");

            _brandRepositoryMock.Setup(x => x.GetBrandById(It.IsAny<Guid>())).ReturnsAsync(() => null);

            //Act
            var result = await _sut.GetBrandById(brand.Id);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllBrands_Brands_ListBrands()
        {
            //Arrange
            _mapperMock.Setup(x => x.Map<List<BrandReadResponse>>(It.IsAny<List<Brand>>())).Returns(brandReadResponseList);
            _brandRepositoryMock.Setup(x => x.GetAllBrands()).ReturnsAsync(brandList);

            //Act
            var result = await _sut.GetAllBrands();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result, brandReadResponseList);
        }

        [Fact]
        public async Task GetAllBrands_InValidData_NoListOfBrands()
        {
            //Arrange
            _brandRepositoryMock.Setup(x => x.GetAllBrands()).ReturnsAsync(new List<Brand>());

            //Act
            var result = await _sut.GetAllBrands();

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetBrandById_ValidData_Brand()
        {
            //Arrange
            var brand = BrandHelper.GetBrandRequest(Guid.NewGuid(), "Lucky Inc.", "Bronx");
            var brandReadResponse = BrandHelper.BrandReadResponseData(brand);

            _brandRepositoryMock.Setup(x => x.GetBrandById(It.IsAny<Guid>())).ReturnsAsync(brand);
            _mapperMock.Setup(x => x.Map<BrandReadResponse>(It.IsAny<Brand>())).Returns(brandReadResponse);

            //Act
            var result = await _sut.GetBrandById(brand.Id);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result, brandReadResponse);
        }

        [Fact]
        public async Task GetBrandById_InValidData_NoBrandReturned()
        {
            //Arrange
            var brand = BrandHelper.GetBrandRequest(Guid.Empty, "Lucky Inc.", "Bronx");
            var brandReadResponse = BrandHelper.BrandReadResponseData(brand);

            _brandRepositoryMock.Setup(x => x.GetBrandById(It.IsAny<Guid>()));
            _mapperMock.Setup(x => x.Map<BrandReadResponse>(It.IsAny<Brand>())).Returns(brandReadResponse);

            //Act
            var result = await _sut.GetBrandById(Guid.Empty);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task BrandUpdate_VaildData_BrandUpdated()
        {
            //Arrange
            var brand = BrandHelper.GetBrandRequest(Guid.Empty, "Lucky Inc.", "Bronx");

            Brand newBrand = new Brand();
            newBrand.Id = brand.Id;
            newBrand.CompanyName = "bs";
            newBrand.Location = brand.Location;

            var brandUpdateResponsee = BrandHelper.BrandUpdateResponseData(brand);
            var brandUpdateRequest = BrandHelper.BrandUpdateRequestData(brand);
            _brandRepositoryMock.Setup(x => x.GetBrandById(It.IsAny<Guid>())).ReturnsAsync(newBrand);
            _brandRepositoryMock.Setup(x => x.UpdateBrand(It.IsAny<Brand>())).ReturnsAsync(brand);

            BrandUpdateResponse brandUpdateResponse = BrandHelper.BrandUpdateResponseData(newBrand);
            _mapperMock.Setup(x => x.Map<Brand>(It.IsAny<BrandUpdateRequest>())).Returns(newBrand);
            _mapperMock.Setup(x => x.Map<BrandUpdateResponse>(It.IsAny<Brand>())).Returns(brandUpdateResponse);

            //Act
            var result = await _sut.UpdateBrand(brand.Id, brandUpdateRequest);

            //Assert
            Assert.NotEqual(result.CompanyName, brand.CompanyName);
        }

        [Fact]
        public async Task BrandUpdate_InVaildData_BrandNotFound()
        {
            //Arrange
            var brand = BrandHelper.GetBrandRequest(Guid.Empty, "Lucky Inc.", "Bronx");
            var brandUpdateRequest = BrandHelper.BrandUpdateRequestData(brand);

            _brandRepositoryMock.Setup(x => x.GetBrandById(It.IsAny<Guid>())).ReturnsAsync(() => null);

            //Act
            var result = await _sut.UpdateBrand(brand.Id, brandUpdateRequest);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task BrandUpdate_InVaildData_IdMissMatch()
        {
            //Arrange
            var brand = BrandHelper.GetBrandRequest(Guid.Empty, "Lucky Inc.", "Bronx");

            BrandCreateRequest brandCreateRequest = BrandHelper.BrandCreateRequestData();
            Brand randomBrand = BrandHelper.BrandData(brandCreateRequest);
            BrandUpdateRequest brandUpdateRequest = BrandHelper.BrandUpdateRequestData(randomBrand);
            _brandRepositoryMock.Setup(x => x.GetBrandById(It.IsAny<Guid>())).ReturnsAsync(randomBrand);

            //Act
            var result = await _sut.UpdateBrand(brand.Id, brandUpdateRequest);

            //Assert
            Assert.Null(result);
        }
    }
}
