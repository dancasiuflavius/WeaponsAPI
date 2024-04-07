using WeaponsAPI.Weapons.DTO;
using WeaponsAPI.Weapons.Model;
using WeaponsAPI.Weapons.Repository;
using WeaponsAPI.Weapons.Repository.Interfaces;
using WeaponsAPI.Weapons.Service.Interfaces;
using WeaponsAPI.System.Constants;
using WeaponsAPI.System.Exceptions;

namespace WeaponsAPI.Weapons.Service
{
    public class WeaponCommandService : IWeaponCommandService
    {
        private IWeaponRepository _repository;

        public WeaponCommandService(IWeaponRepository repository)
        {
            _repository = repository;
        }

        public async Task<Weapon> CreateWeapon(CreateWeaponRequest productRequest)
        {
            if(productRequest.Price < 0)
            {
                throw new InvalidPrice(Constants.INVALID_PRICE);
            }

            Weapon product = await _repository.GetByNameAsync(productRequest.Name);

            if(product !=null)
            {
                throw new ItemAlreadyExists(Constants.PRODUCT_ALREADY_EXISTS);
            }

            product = await _repository.CreateAsync(productRequest);
            return product;
        }
        public async Task<Weapon> UpdateWeapon(int id, UpdateWeaponRequest productRequest)
        {
            if (productRequest.Price < 0)
            {
                throw new InvalidPrice(Constants.INVALID_PRICE);
            }

            Weapon product = await _repository.GetByIdAsync(productRequest.Id);
            if (product == null)
            {
                throw new ItemDoesNotExist(Constants.PRODUCT_DOES_NOT_EXIST);
            }
            product = await _repository.UpdateAsync(id,productRequest);
            return product;
        }
        public async Task<Weapon> DeleteWeapon(int id)
        {
            Weapon product = await _repository.GetByIdAsync(id);
            if (product == null)
            {
                throw new ItemDoesNotExist(Constants.PRODUCT_DOES_NOT_EXIST);
            }

            await _repository.DeleteAsync(id);
            return product;
        }
    }
}
