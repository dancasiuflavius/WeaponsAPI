using Microsoft.AspNetCore.Mvc;
using WeaponsAPI.Weapons.Controller.Interfaces;

using WeaponsAPI.Weapons.DTO;
using WeaponsAPI.Weapons.Model;
using WeaponsAPI.Weapons.Repository.Interfaces;
using WeaponsAPI.Weapons.Service;
using WeaponsAPI.Weapons.Service.Interfaces;
using WeaponsAPI.System.Exceptions;


namespace WeaponsAPI.Weapons.Controller;



public class WeaponsController : WeaponsApiController
{

    private IWeaponQuerryService _productQueryService;
    private IWeaponCommandService _productCommandService;

    private readonly ILogger<WeaponsController> _logger;


    public WeaponsController(ILogger<WeaponsController> logger, IWeaponQuerryService productQueryService, IWeaponCommandService productCommandService)
    {
        _logger = logger;
        _productQueryService = productQueryService;
        _productCommandService = productCommandService;
    }



    [HttpGet("api/v1/all")]
    public override async Task<ActionResult<IEnumerable<Weapon>>> GetWeapons()
    {
        try
        {
            var products = await _productQueryService.GetAllWeapons();
            return Ok(products);
        }
        catch (ItemsDoNotExist ex)
        {
            return NotFound(ex.Message);
        }


    }


    public override async Task<ActionResult<Weapon>> CreateWeapon(CreateWeaponRequest productRequest)
    {
        _logger.LogInformation(message: $"Rest request: Create product with DTO:\n{productRequest}");
        try
        {
            var product = await _productCommandService.CreateWeapon(productRequest);

            return Ok(product);
        }
        catch (InvalidPrice ex)
        {
            _logger.LogWarning(ex.Message);
            return BadRequest(ex.Message);
        }
        catch (ItemAlreadyExists ex)
        {
            _logger.LogWarning(ex.Message);
            return BadRequest(ex.Message);
        }



    }
    // [HttpPut("api/v1/update")]
    public override async Task<ActionResult<Weapon>> UpdateWeapon([FromQuery] int id, [FromBody] UpdateWeaponRequest request)
    {
        _logger.LogInformation(message: $"Rest request: Create product with DTO:\n{request}");
        try
        {

            Weapon response = await _productCommandService.UpdateWeapon(id, request);

            return Accepted(response);
        }
        catch (InvalidPrice ex)
        {
            _logger.LogWarning(ex.Message);
            return BadRequest(ex.Message);
        }
        catch (ItemDoesNotExist ex)
        {
            _logger.LogWarning(ex.Message);
            return NotFound(ex.Message);
        }
    }

    //[HttpDelete("api/v1/delete")]
    public override async Task<ActionResult<Weapon>> DeleteWeapon([FromQuery] int id)
    {
        _logger.LogInformation(message: $"Rest request: Delete product with id:\n{id}");
        try
        {
            Weapon product = await _productCommandService.DeleteWeapon(id);

            return Ok(product);
        }
        catch (ItemDoesNotExist ex)
        {
            _logger.LogError(ex.Message + $"Error while trying to delete product: \n{id}");
            return NotFound(ex.Message);
        }
    }
}



