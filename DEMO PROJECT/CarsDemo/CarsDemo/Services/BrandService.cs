using AutoMapper;
using CarsDemo.DTOS.Brand.Requests;
using CarsDemo.DTOS.Brand.Responses;
using CarsDemo.Interfaces;
using CarsDemo.Models;

namespace CarsDemo.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly ILogger<BrandService> _logger;
        private readonly IMapper _mapper;

        public BrandService(IBrandRepository brandRepository, ILogger<BrandService> logger, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<BrandCreateResponse> CreateBrand(BrandCreateRequest brand)
        {
            try
            {
                var mappedBrand = _mapper.Map<Brand>(brand);
                var result = await _brandRepository.CreateBrand(mappedBrand);

                if (result != null)
                    return _mapper.Map<BrandCreateResponse>(result);

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not create brand: ", ex.Message);
                throw;
            }
        }

        public async Task<bool> DeleteBrand(Guid id)
        {
            try
            {
                var brand = await _brandRepository.GetBrandById(id);
                
                if (brand != null)
                    return await _brandRepository.DeleteBrand(brand);
                
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not delete brand: ", ex.Message);
                throw;
            }
        }

        public async Task<List<BrandReadResponse>> GetAllBrands()
        {
            try
            {
                var brandList = await _brandRepository.GetAllBrands();

                if (brandList.Any())
                    return _mapper.Map<List<BrandReadResponse>>(brandList);

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get brands: ", ex.Message);
                throw;
            }
        }

        public async Task<BrandReadResponse> GetBrandById(Guid id)
        {
            try
            {
                var brand = await _brandRepository.GetBrandById(id);

                if (brand != null)
                    return _mapper.Map<BrandReadResponse>(brand);

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get brand: ", ex.Message);
                throw;
            }
        }

        public async Task<BrandUpdateResponse> UpdateBrand(Guid id, BrandUpdateRequest brand)
        {
            try
            {
                var updateBrand = await _brandRepository.GetBrandById(id);

                if (updateBrand !=null)
                {
                    if(id == updateBrand.Id)
                    {
                        var mappedBrand = _mapper.Map<Brand>(brand);
                        var result = await _brandRepository.UpdateBrand(mappedBrand);
                        return _mapper.Map<BrandUpdateResponse>(result);
                    }
                    return null;
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not update brand: ", ex.Message);
                throw;
            }
        }
    }
}
