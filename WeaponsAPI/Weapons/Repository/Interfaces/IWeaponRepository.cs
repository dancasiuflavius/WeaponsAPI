using WeaponsAPI.Weapons.DTO;
using WeaponsAPI.Weapons.Model;

namespace WeaponsAPI.Weapons.Repository.Interfaces
{
    public interface IWeaponRepository
    {
        Task<IEnumerable<Weapon>> GetAllAsync();
        Task<Weapon> GetByNameAsync(string name);

        Task<IEnumerable<Double>> GetAllAsyncPrice();
        Task<Weapon> GetByIdAsync(int id);
        Task<Weapon> CreateAsync(CreateWeaponRequest weaponRequest);
        Task<Weapon> UpdateAsync(int id, UpdateWeaponRequest weaponRequest);
        Task<Weapon> UpdateAsync(UpdateWeaponRequest productRequest);
        Task<Weapon> DeleteAsync(int id);
    }
}
