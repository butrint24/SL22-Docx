using Bogus;
using Bogus.DataSets;
using CarsDemo.DTOS.Brand.Requests;
using CarsDemo.DTOS.Brand.Responses;
using CarsDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.BrandUnitTests.Helper
{
    public class BrandHelper
    {
        public static Brand BrandData(BrandCreateRequest brandRequest)
        {
            return new Faker<Brand>()
                .RuleFor(x => x.Id, Guid.NewGuid)
                .RuleFor(x => x.CompanyName, brandRequest.CompanyName)
                .RuleFor(x => x.Location, brandRequest.Location)
                .Generate();
        }

        public static Brand GetBrandRequest(Guid id, string companyName, string location)
        {
            return new Faker<Brand>()
                .RuleFor(x => x.Id, id)
                .RuleFor(x => x.CompanyName, companyName)
                .RuleFor(x => x.Location, location)
                .Generate();
        }

        public static BrandCreateResponse BrandCreateResponseData(Guid id, string companyName, string location)
        {
            return new Faker<BrandCreateResponse>()
                .RuleFor(x => x.Id, id)
                .RuleFor(x => x.CompanyName, companyName)
                .RuleFor(x => x.Location, location)
                .Generate();
        }

        public static BrandCreateRequest BrandCreateRequestData()
        {
            return new Faker<BrandCreateRequest>()
                .RuleFor(x => x.CompanyName, x => x.Company.CompanyName())
                .RuleFor(x => x.Location, x => x.Address.County())
                .Generate();
        }

        public static BrandReadResponse BrandReadResponseData(Brand brand)
        {
            return new BrandReadResponse()
            {
                Id = brand.Id,
                CompanyName = brand.CompanyName,
                Location = brand.Location
            };
        }

        public static List<Brand> BrandListData()
        {
            List<Brand> brandList = new();
            for (int i = 0; i < 10; i++)
            {
                BrandCreateRequest brandCreateRequest = BrandCreateRequestData();
                Brand brand = BrandData(brandCreateRequest);

                brandList.Add(brand);
            }
            return brandList;
        }

        public static List<BrandReadResponse> BrandReadResponseListData(List<Brand> brandList)
        {
            List<BrandReadResponse> brandReadResponseList = new();
            for (int i = 0; i < 10; i++)
            {
                BrandReadResponse brandReadRespons = BrandReadResponseData(brandList[i]);

                brandReadResponseList.Add(brandReadRespons);
            }
            return brandReadResponseList;
        }

        public static BrandUpdateRequest BrandUpdateRequestData(Brand brand)
        {
            return new Faker<BrandUpdateRequest>()
                .RuleFor(x => x.Id, brand.Id)
                .RuleFor(x => x.CompanyName, brand.CompanyName)
                .RuleFor(x => x.Location, brand.Location)
                .Generate();
        }

        public static BrandUpdateResponse BrandUpdateResponseData(Brand brand)
        {
            return new BrandUpdateResponse()
            {
                Id = brand.Id,
                CompanyName = brand.CompanyName,
                Location = brand.Location
            };
        }
    }
}
