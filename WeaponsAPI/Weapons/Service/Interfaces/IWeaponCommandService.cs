using WeaponsAPI.Weapons.DTO;
using WeaponsAPI.Weapons.Model;


namespace WeaponsAPI.Weapons.Service.Interfaces;


public interface IWeaponCommandService
{
    Task<Weapon> CreateWeapon(CreateWeaponRequest weaponRequest);

    Task<Weapon> UpdateWeapon(int id, UpdateWeaponRequest weaponRequest);

    Task<Weapon> DeleteWeapon(int id);
}
