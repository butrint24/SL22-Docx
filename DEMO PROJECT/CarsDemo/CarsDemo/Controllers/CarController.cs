using CarsDemo.DTOS.Car.Requests;
using CarsDemo.DTOS.Car.Responses;
using CarsDemo.Interfaces;
using CarsDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarsDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet("getallcars/")]
        public async Task<ActionResult<List<Car>>> GetAllCars()
        {
            var result = await _carService.GetAllCars();
            return (result != null) ? Ok(result) : NotFound();
        }

        [HttpGet("getcar/{id}")]
        public async Task<ActionResult<Car>> GetCar(Guid id)
        {
            var result = await _carService.GetCarById(id);
            return (result != null) ? Ok(result) : NotFound();
        }

        [HttpPost("createcar/")]
        public async Task<ActionResult<CarCreateResponse>> CreateBrand([FromBody] CarCreateRequest request)
        {
            var result = await _carService.CreateCar(request);
            return Ok(result);
        }

        [HttpPut("updatecar/{id}")]
        public async Task<ActionResult<CarUpdateResponse>> UpdateBrand(Guid id, [FromBody] CarUpdateRequest request)
        {
            var result = await _carService.UpdateCar(id, request);
            return (result != null) ? Ok(result) : NotFound();
        }
    }
}
