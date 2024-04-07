using WeaponsAPI.Weapons.Model;
using WeaponsAPI.Weapons.Repository;
using WeaponsAPI.Weapons.Repository.Interfaces;
using WeaponsAPI.Weapons.Service.Interfaces;
using WeaponsAPI.System.Constants;
using WeaponsAPI.System.Exceptions;

namespace WeaponsAPI.Weapons.Service;

public class WeaponQuerryService : IWeaponQuerryService
{
    private IWeaponRepository _repository;

    public WeaponQuerryService(IWeaponRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Weapon>> GetAllWeapons()
    {
        IEnumerable<Weapon> products = await _repository.GetAllAsync();

        if (products.Count() == 0)
        {
            throw new ItemsDoNotExist(Constants.NO_PRODUCTS_EXIST);
        }

        return products;
    }

    public async Task<IEnumerable<Weapon>> GetWeaponsWithCategory(string category)
    {
        IEnumerable<Weapon> products = (await _repository.GetAllAsync())
            .Where(product => product.Category.Equals(category));

        if (products.Count() == 0)
        {
            throw new ItemsDoNotExist(Constants.NO_PRODUCTS_EXIST);
        }

        return products;
    }

    public async Task<IEnumerable<Weapon>> GetWeaponsWithNoCategory()
    {
        IEnumerable<Weapon> products = (await _repository.GetAllAsync())
            .Where(product => product.Category == null!);

        if (products.Count() == 0)
        {
            throw new ItemsDoNotExist(Constants.NO_PRODUCTS_EXIST);
        }

        return products;
    }

    public async Task<IEnumerable<Weapon>> GetWeaponsInPriceRange(double min, double max)
    {
        IEnumerable<Weapon> products = (await _repository.GetAllAsync())
            .Where(product => product.price >= min && product.price <= max);

        if (products.Count() == 0)
        {
            throw new ItemsDoNotExist(Constants.NO_PRODUCTS_EXIST);
        }

        return products;
    }

    public async Task<Weapon> GetWeaponById(int id)
    {
        Weapon product = await _repository.GetByIdAsync(id);

        if (product == null)
        {
            throw new ItemDoesNotExist(Constants.PRODUCT_DOES_NOT_EXIST);
        }

        return product;
    }
}
