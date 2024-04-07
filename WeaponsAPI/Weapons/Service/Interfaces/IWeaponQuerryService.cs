using WeaponsAPI.Weapons.Model;

namespace WeaponsAPI.Weapons.Service.Interfaces
{
    public interface IWeaponQuerryService
    {
        Task<IEnumerable<Weapon>> GetAllWeapons();
        Task<IEnumerable<Weapon>> GetWeaponsWithCategory(string category);
        Task<IEnumerable<Weapon>> GetWeaponsWithNoCategory();
        Task<IEnumerable<Weapon>> GetWeaponsInPriceRange(double min, double max);
        Task<Weapon> GetWeaponById(int id);
    }
}
