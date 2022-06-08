using CarsDemo.DTOS.Brand.Requests;
using CarsDemo.DTOS.Brand.Responses;
using CarsDemo.Interfaces;
using CarsDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarsDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet("getallbrands/")]
        public async Task<ActionResult<List<Brand>>> GetAllBrands()
        {
            var result = await _brandService.GetAllBrands();
            return (result != null) ? Ok(result) : NotFound();
        }

        [HttpGet("getbrand/{id}")]
        public async Task<ActionResult<Brand>> GetBrand(Guid id)
        {
            var result = await _brandService.GetBrandById(id);
            return (result != null) ? Ok(result) : NotFound();
        }

        [HttpPost("createbrand/")]
        public async Task<ActionResult<BrandCreateResponse>> CreateBrand([FromBody] BrandCreateRequest request)
        {
            var result = await _brandService.CreateBrand(request);
            return Ok(result);
        }

        [HttpPut("updatebrand/{id}")]
        public async Task<ActionResult<BrandUpdateResponse>> UpdateBrand(Guid id, [FromBody] BrandUpdateRequest request)
        {
            var result = await _brandService.UpdateBrand(id, request);
            return (result != null) ? Ok(result) : NotFound();
        }

        [HttpDelete("deletebrand/{id}")]
        public async Task<ActionResult<bool>> DeleteBet(Guid id)
        {
            var result = await _brandService.DeleteBrand(id);
            return (result) ? Ok(result) : NotFound(result);
        }
    }
}
