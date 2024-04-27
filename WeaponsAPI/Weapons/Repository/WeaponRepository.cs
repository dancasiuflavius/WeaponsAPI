using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WeaponsAPI.Data;
using WeaponsAPI.Weapons.DTO;
using WeaponsAPI.Weapons.Model;
using WeaponsAPI.Weapons.Repository.Interfaces;

namespace WeaponsAPI.Weapons.Repository
{
    public class WeaponRepository : IWeaponRepository
    {
        private readonly AppDbContext _context;

        private readonly IMapper _mapper;

        public WeaponRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<Weapon>> GetAllAsync()
        {
            return await _context.Weapons.ToListAsync();
        }
        public async Task<Weapon>GetByNameAsync(string name)
        {
            return await _context.Weapons.FirstOrDefaultAsync(weapon => weapon.Name.Equals(name));
        }
        public async Task<IEnumerable<Double>> GetAllAsyncPrice()
        {
            return await _context.Weapons.Select(weapon => weapon.price).ToListAsync();
        }
        public async Task<Weapon> CreateAsync(CreateWeaponRequest weaponRequest)
        {
            var weapon = _mapper.Map<Weapon>(weaponRequest);

            _context.Weapons.Add(weapon);

            await _context.SaveChangesAsync();

            return weapon;
        }

        public async Task<Weapon> GetByIdAsync(int id)
        {
            return await _context.Weapons.FindAsync(id);
        }
        public async Task<Weapon> UpdateAsync(int id, UpdateWeaponRequest request)
        {
            var weapon = await _context.Weapons.FindAsync(id);

            weapon.Name = request.Name ?? weapon.Name;
            weapon.Category = request.Category ?? weapon.Category;
            weapon.price = request.Price ?? weapon.price;
            


            _context.Weapons.Update(weapon);

            await _context.SaveChangesAsync();

            return weapon;
        }
        public async Task<Weapon> UpdateAsync(UpdateWeaponRequest productRequest)
        {
            var product = (await _context.Weapons.FindAsync(productRequest.Id))!;

            product.price = productRequest.Price ?? product.price;
            product.Name = productRequest.Name ?? product.Name;
            product.Category = productRequest.Category ?? product.Category;
           
            _context.Weapons.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }
        public async Task<Weapon> DeleteAsync(int id)
        {
            var weapon = await _context.Weapons.FindAsync(id);
            _context.Weapons.Remove(weapon);
            await _context.SaveChangesAsync();
            return weapon;
        }
    }
}
