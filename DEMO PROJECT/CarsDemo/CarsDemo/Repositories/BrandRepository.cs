using CarsDemo.Data;
using CarsDemo.Interfaces;
using CarsDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace CarsDemo.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly DataContext _context;

        public BrandRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Brand>> GetAllBrands()
        {
            return await _context.Brands.ToListAsync();
        }

        public async Task<Brand> GetBrandById(Guid id)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand != null)
            {
                _context.Entry(brand).State = EntityState.Detached;
                return brand;
            }
            return brand;
        }

        public async Task<Brand> CreateBrand(Brand brand)
        {
            await _context.AddAsync(brand);
            await _context.SaveChangesAsync();
            return brand;
        }

        public async Task<Brand> UpdateBrand(Brand brand)
        {
            _context.Entry(brand).State = EntityState.Modified;
            _context.Update(brand);
            await _context.SaveChangesAsync();
            return brand;
        }

        public async Task<bool> DeleteBrand(Brand brand)
        {
            _context.Remove(brand);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
