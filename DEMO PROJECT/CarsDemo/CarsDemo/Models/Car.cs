using System.ComponentModel.DataAnnotations;

namespace CarsDemo.Models
{
    public class Car
    {
        [Required]
        public Guid Id { get; set; }
        
        [Required]
        public Guid BrandId { get; set; }
        public Brand Brand { get; set; }
        
        [Required]
        public string Type { get; set; }
        
        [Required]
        public string EngineType { get; set; }
    }
}
