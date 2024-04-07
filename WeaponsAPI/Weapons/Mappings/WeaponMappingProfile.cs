using AutoMapper;
using WeaponsAPI.Weapons.DTO;
using WeaponsAPI.Weapons.Model;

namespace WeaponsAPI.Weapons.Mappings
{
    public class WeaponMappingProfile : Profile
    {

        public WeaponMappingProfile()
        {

            CreateMap<CreateWeaponRequest, Weapon>();
        }

    }
}
