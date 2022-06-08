using CarsDemo.DTOS.Brand.Requests;
using CarsDemo.DTOS.Brand.Responses;

namespace CarsDemo.Interfaces
{
    public interface IBrandService
    {
        Task<List<BrandReadResponse>> GetAllBrands();
        Task<BrandReadResponse> GetBrandById(Guid id);
        Task<BrandCreateResponse> CreateBrand(BrandCreateRequest brand);
        Task<BrandUpdateResponse> UpdateBrand(Guid id, BrandUpdateRequest brand);
        Task<bool> DeleteBrand(Guid id);
    }
}
