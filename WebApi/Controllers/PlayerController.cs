// /////////////////////////////////////////////////////////////////////////////
// YOU CAN FREELY MODIFY THE CODE BELOW IN ORDER TO COMPLETE THE TASK
// /////////////////////////////////////////////////////////////////////////////

namespace WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Helpers;
using WebApi.Entities;
using WebApi.Service.Contracts;
using WebApi.DTOs.Player;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
public class PlayerController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

  public PlayerController(IServiceManager serviceManager)
  {
   _serviceManager = serviceManager;
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<Player>>> GetAll()
  {
    //await Task.Run(() => Context.Players.FirstOrDefault( x => x.Id == 1));
    throw new NotImplementedException();
  }

  [HttpPost]
  public async Task<ActionResult<Player>> PostPlayer(PlayerForCreationDto request)
  {
        var response = await _serviceManager.PlayerService.CreatePlayerAsync(request, false);
        return Ok(response);
  }

  [HttpPut("{playerId:int}")]
  public async Task<IActionResult> PutPlayer(int playerId, PlayerForUpdateDto request)
  {
        var response = await _serviceManager.PlayerService.UpdatePlayerAsync(playerId,request, false);
        return Ok(response);
  }
  [Authorize]
  [HttpDelete("{playerId}")]
  public async Task<ActionResult<Player>> DeletePlayer([FromRoute]int playerId)
  {
        var response = await _serviceManager.PlayerService.DeletePlayerAsync(playerId, false);
        return Ok(response);
  }
}