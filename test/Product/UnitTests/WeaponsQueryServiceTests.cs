using WeaponsAPI.Weapons.DTO;
using WeaponsAPI.Weapons.Model;
//using ProductsCrudApi.Products.Model.Comparers;
using WeaponsAPI.Weapons.Repository;
using WeaponsAPI.Weapons.Service.Interfaces;
using WeaponsAPI.Weapons.Service;
using WeaponsAPI.System.Constants;
using WeaponsAPI.System.Exceptions;
using Moq;
using test.Products.Helper;
using WeaponsAPI.Weapons.Repository.Interfaces;
using WeaponsAPI.Weapons.Service;

namespace tests.Products.UnitTests;

public class WeaponsQueryServiceTests
{
    private readonly Mock<IWeaponRepository> _mockRepo;
    private readonly IWeaponQuerryService _service;

    public WeaponsQueryServiceTests()
    {
        _mockRepo = new Mock<IWeaponRepository>();
        _service = new WeaponQuerryService(_mockRepo.Object);
    }

    [Fact]
    public async Task GetAllProducts_NoProductsExist_ThrowItemsDoNotExistException()
    {
        _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Weapon>());

        var exception = await Assert.ThrowsAsync<ItemsDoNotExist>(() => _service.GetAllWeapons());

        Assert.Equal(Constants.NO_PRODUCTS_EXIST, exception.Message);
    }

    [Fact]
    public async Task GetAllProducts_ProductsExist_ReturnsAllProducts()
    {
        var products = TestProductFactory.CreateProducts(3);

        _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(products);
        var result = await _service.GetAllWeapons();

        Assert.NotNull(result);
        Assert.Equal(3, result.Count());
        Assert.Contains(products[0], result);
        Assert.Contains(products[1], result);
    }

    [Fact]
    public async Task GetProductsWithCategory_NoProducts_ThrowItemsDoNotExistException()
    {
        _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Weapon>());

        var exception = await Assert.ThrowsAsync<ItemsDoNotExist>(() => _service.GetAllWeapons());

        Assert.Equal(Constants.NO_PRODUCTS_EXIST, exception.Message);
    }

    [Fact]
    public async Task GetProductsWithCategory_NoProductsWithCategory_ThrowItemsDoNotExistException()
    {
        var products = TestProductFactory.CreateProducts(3);
        products.RemoveAt(0);
        _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(products);

        var exception = await Assert.ThrowsAsync<ItemsDoNotExist>(() => _service.GetWeaponsWithCategory("Category 2"));

        Assert.Equal(Constants.NO_PRODUCTS_EXIST, exception.Message);
    }

    [Fact]
    public async Task GetProductsWithCategory_ProductsFound_ReturnsAllFoundProducts()
    {
        var products = TestProductFactory.CreateProducts(3);

        _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(products);
        var result = await _service.GetWeaponsWithCategory("Category 2");

        Assert.Single(result);
        Assert.Contains(products[0], result);
        Assert.DoesNotContain(products[1], result);
        Assert.DoesNotContain(products[2], result);
    }

    [Fact]
    public async Task GetProductsWithNoCategory_NoProducts_ThrowItemsDoNotExistException()
    {
        _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Weapon>());

        var exception = await Assert.ThrowsAsync<ItemsDoNotExist>(() => _service.GetAllWeapons());

        Assert.Equal(Constants.NO_PRODUCTS_EXIST, exception.Message);
    }

    [Fact]
    public async Task GetProductsWithNoCategory_NoProductsWithoutCategory_ThrowItemsDoNotExistException()
    {
        var products = TestProductFactory.CreateProducts(3);
        _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(products);

        var exception = await Assert.ThrowsAsync<ItemsDoNotExist>(() => _service.GetWeaponsWithNoCategory());

        Assert.Equal(Constants.NO_PRODUCTS_EXIST, exception.Message);
    }

    [Fact]
    public async Task GetProductsWithNoCategory_ProductsFound_ReturnsAllFoundProducts()
    {
        var products = TestProductFactory.CreateProducts(4);
        products.Add(TestProductFactory.CreateProductWithNoCategory(5));
        products.Add(TestProductFactory.CreateProductWithNoCategory(6));

        _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(products);
        var result = await _service.GetWeaponsWithNoCategory();

        Assert.NotNull(result);
        Assert.Equal(2, result.Count());

        for (int i = 0; i < 4; i++)
        {
            Assert.DoesNotContain(products[i], result);
        }

        Assert.Contains(products[4], result);
        Assert.Contains(products[5], result);
    }

    [Fact]
    public async Task GetProductsInPriceRange_NoProducts_ThrowItemsDoNotExistException()
    {
        _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Weapon>());

        var exception = await Assert.ThrowsAsync<ItemsDoNotExist>(() => _service.GetWeaponsInPriceRange(600, 1000));

        Assert.Equal(Constants.NO_PRODUCTS_EXIST, exception.Message);
    }

    [Fact]
    public async Task GetProductsInPriceRange_NoProductsInPriceRange_ThrowItemsDoNotExistException()
    {
        var products = TestProductFactory.CreateProducts(3);
        _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(products);

        var exception = await Assert.ThrowsAsync<ItemsDoNotExist>(() => _service.GetWeaponsInPriceRange(600, 1000));

        Assert.Equal(Constants.NO_PRODUCTS_EXIST, exception.Message);
    }

    [Fact]
    public async Task GetProductsInPriceRange_ProductsFound_ReturnsAllFoundProducts()
    {
        var products = TestProductFactory.CreateProducts(4);
        TestProductFactory.CreateProductsInPriceRange(600, 1000, 2)
            .ForEach(product => products.Add(product));

        _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(products);
        var result = await _service.GetWeaponsInPriceRange(600, 1000);

        Assert.NotNull(result);
        Assert.Equal(2, result.Count());

        for (int i = 0; i < 4; i++)
        {
            Assert.DoesNotContain(products[i], result);
        }

        Assert.Contains(products[4], result);
        Assert.Contains(products[5], result);
    }

    [Fact]
    public async Task GetProductById_ProductNotFound_ThrowItemDoesNotExistException()
    {
        _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Weapon)null);

        var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetWeaponById(1));

        Assert.Equal(Constants.PRODUCT_DOES_NOT_EXIST, exception.Message);
    }

    [Fact]
    public async Task GetProductById_ProductFound_ReturnsProduct()
    {
        var product = TestProductFactory.CreateProduct(1);

        _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(product);
        var result = await _service.GetWeaponById(1);

        Assert.NotNull(result);
        Assert.Equal(product, result);
    }
}