using System.ComponentModel.DataAnnotations;

namespace CarsDemo.DTOS.Car.Requests
{
    public class CarUpdateRequest
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid BrandId { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string EngineType { get; set; }
    }
}
