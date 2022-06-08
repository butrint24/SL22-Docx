using System.ComponentModel.DataAnnotations;

namespace CarsDemo.DTOS.Car.Requests
{
    public class CarCreateRequest
    {
        [Required]
        public Guid BrandId { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string EngineType { get; set; }
    }
}
