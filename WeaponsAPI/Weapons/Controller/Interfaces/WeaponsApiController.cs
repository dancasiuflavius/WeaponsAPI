using WeaponsAPI.Weapons.DTO;
using WeaponsAPI.Weapons.Model;
using Microsoft.AspNetCore.Mvc;

namespace WeaponsAPI.Weapons.Controller.Interfaces;

[ApiController]
[Route("api/v1/[controller]")]
public abstract class WeaponsApiController : ControllerBase
{
    [HttpGet("all")]
    [ProducesResponseType(statusCode: 200, type: typeof(List<Weapon>))]
    [ProducesResponseType(statusCode: 404, type: typeof(String))]
    public abstract Task<ActionResult<IEnumerable<Weapon>>> GetWeapons();

    [HttpPost("create")]
    [ProducesResponseType(statusCode: 200, type: typeof(Weapon))]
    [ProducesResponseType(statusCode: 400, type: typeof(String))]
    public abstract Task<ActionResult<Weapon>> CreateWeapon([FromBody] CreateWeaponRequest productRequest);

    [HttpPut("update")]
    [ProducesResponseType(statusCode: 200, type: typeof(Weapon))]
    [ProducesResponseType(statusCode: 400, type: typeof(String))]
    [ProducesResponseType(statusCode: 404, type: typeof(String))]
    public abstract Task<ActionResult<Weapon>> UpdateWeapon([FromQuery] int id, [FromBody] UpdateWeaponRequest productRequest);

    [HttpDelete("delete/{id}")]
    [ProducesResponseType(statusCode: 200, type: typeof(Weapon))]
    [ProducesResponseType(statusCode: 404, type: typeof(String))]
    public abstract Task<ActionResult<Weapon>> DeleteWeapon([FromRoute] int id);
}
