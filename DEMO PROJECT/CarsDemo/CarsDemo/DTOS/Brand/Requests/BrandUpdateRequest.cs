namespace CarsDemo.DTOS.Brand.Requests
{
    public class BrandUpdateRequest
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
        public string Location { get; set; }
    }
}
