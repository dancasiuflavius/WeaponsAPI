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

namespace test.Products.UnitTests;

public class WeaponsCommandServiceTests
{
    private readonly Mock<IWeaponRepository> _mockRepo;
    private readonly IWeaponCommandService _service;

    public WeaponsCommandServiceTests()
    {
        _mockRepo = new Mock<IWeaponRepository>();
        _service = new WeaponCommandService(_mockRepo.Object);
    }

    [Fact]
    public async Task CreateProduct_InvalidPrice_ThrowsInvalidPriceException()
    {
        var createRequest = new CreateWeaponRequest
        {
            Name = "New Product",
            Price = -100,
            Category = "Test Category",
            
        };

        var expectedProduct = TestProductFactory.CreateProduct(1);
        expectedProduct.Name = createRequest.Name;
        expectedProduct.price = createRequest.Price;

        _mockRepo.Setup(repo => repo.GetByNameAsync(createRequest.Name)).ReturnsAsync((Weapon)null);

        var exception = await Assert.ThrowsAsync<InvalidPrice>(() => _service.CreateWeapon(createRequest));

        Assert.Equal(Constants.INVALID_PRICE, exception.Message);
    }
    [Fact]
    public async Task CreateProduct_ProductWithSameNameAlreadyExists_ThrowsItemAlreadyExistsException()
    {
        var createRequest = new CreateWeaponRequest
        {
            Name = "New Product",
            Price = 100,
            Category = "Test Category",
            
        };

        var expectedProduct = TestProductFactory.CreateProduct(1);
        expectedProduct.Name = createRequest.Name;
        expectedProduct.price = createRequest.Price;

        var existingProduct = TestProductFactory.CreateProduct(2);
        existingProduct.Name = createRequest.Name;

        _mockRepo.Setup(repo => repo.GetByNameAsync(createRequest.Name)).ReturnsAsync(existingProduct);

        var exception = await Assert.ThrowsAsync<ItemAlreadyExists>(() => _service.CreateWeapon(createRequest));

        Assert.Equal(Constants.PRODUCT_ALREADY_EXISTS, exception.Message);
    }

    [Fact]
    public async Task CreateProduct_ValidData_ReturnsCreatedProduct()
    {
        var createRequest = new CreateWeaponRequest
        {
            Name = "New Product",
            Price = 100,
            Category = "Test Category",
            
        };

        var expectedProduct = TestProductFactory.CreateProduct(1);
        expectedProduct.Name = createRequest.Name;

        _mockRepo.Setup(repo => repo.GetByNameAsync(It.IsAny<string>())).ReturnsAsync((Weapon)null!);
        _mockRepo.Setup(repo => repo.CreateAsync(It.IsAny<CreateWeaponRequest>())).ReturnsAsync(expectedProduct);

        var result = await _service.CreateWeapon(createRequest);

        Assert.NotNull(result);
        Assert.Equal(createRequest.Name, result.Name);
    }

    [Fact]
    public async Task UpdateProduct_InvalidPrice_ThrowsInvalidPriceException()
    {
        var updateRequest = new UpdateWeaponRequest
        {
            Id = 1,
            Name = "New Product",
            Price = -100,
            Category = "Test Category",
            
        };

        var unmodifiedProduct = TestProductFactory.CreateProduct(1);

        var expectedProduct = TestProductFactory.CreateProduct(1);
        expectedProduct.price = -100;

        _mockRepo.Setup(repo => repo.GetByIdAsync(updateRequest.Id)).ReturnsAsync(unmodifiedProduct);

        var exception = await Assert.ThrowsAsync<InvalidPrice>(() => _service.UpdateWeapon(updateRequest));

        Assert.Equal(Constants.INVALID_PRICE, exception.Message);
    }

    [Fact]
    public async Task UpdateProduct_ProductDoesNotExist_ThrowsItemDoesNotExistException()
    {
        var updateRequest = new UpdateWeaponRequest
        {
            Id = 1,
            Name = "New Product",
            Price = 100,
            Category = "Test Category",
            
        };

        var expectedProduct = TestProductFactory.CreateProduct(1);
        expectedProduct.price = 100;

        _mockRepo.Setup(repo => repo.GetByIdAsync(updateRequest.Id)).ReturnsAsync((Weapon)null!);

        var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.UpdateWeapon(updateRequest));

        Assert.Equal(Constants.PRODUCT_DOES_NOT_EXIST, exception.Message);
    }

    [Fact]
    public async Task UpdateProduct_ValidData_ReturnsUpdatedProduct()
    {
        DateTime time = DateTime.Now;
        var updateRequest = new UpdateWeaponRequest
        {
            Id = 1,
            Name = "New Product",
            Price = 100,
            Category = "Test Category",
           
        };

        var unmodifiedProduct = TestProductFactory.CreateProduct(1);

        var expectedProduct = TestProductFactory.CreateProduct(1);
        expectedProduct.Name = updateRequest.Name;
        expectedProduct.price = 100;
        expectedProduct.Category = "Test Category";
        

        _mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(unmodifiedProduct);
        _mockRepo.Setup(repo => repo.UpdateAsync(It.IsAny<UpdateWeaponRequest>())).ReturnsAsync(expectedProduct);

        var result = await _service.UpdateWeapon(updateRequest);

        Assert.NotNull(result);
        //Assert.Equal(expectedProduct, result, new ProductEqualityComparer()); ProductEqualityComparer?
    }

    [Fact]
    public async Task DeleteProduct_ProductDoesNotExist_ThrowsItemDoesNotExistException()
    {
        _mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Weapon)null!);

        var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.DeleteWeapon(1));

        Assert.Equal(Constants.PRODUCT_DOES_NOT_EXIST, exception.Message);
    }


}
