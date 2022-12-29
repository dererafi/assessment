
using Microsoft.AspNetCore.Mvc;
using shared;

namespace WebApi.Controllers;

[Route("api/game")]
[ApiController]
[Produces("application/json")]
public class RouletteController : ControllerBase
{
    private readonly IBetMoveService _betMoveService;
    private readonly IMapper _mapper;

    public RouletteController(IBetMoveService betMoveService, IMapper mapper)
    {
        _betMoveService = betMoveService;
        _mapper = mapper;
    }
    
    [HttpPost("placeBet")]
    public async Task<IActionResult> PlaceBet([FromBody] PlayerBetViewModel playerBetViewModel)
    {
        var playerBetDto =  _betMoveService.GetBoardAreaBetDetails(playerBetViewModel.BoardArea, playerBetViewModel.PlayerId,  playerBetViewModel.Bet,  playerBetViewModel.Moves);
        var result = await _mapper.Save(new List<PlayerBetDto>() {playerBetDto});
        return Ok(result);
    }
    
    [HttpGet("payout")]
    public async Task<IActionResult> Payout(string winningNumber)
    {
        var playerBetDtos = await _mapper.GetSavedPlayerBetFromDb();
        playerBetDtos.ForEach(dto =>
        {
            dto.PayOut = _betMoveService.Payout(winningNumber, dto.Moves);
            dto.Winning = dto.PayOut.Count > 0;
        });
        
        return Ok(playerBetDtos);
    }
    
    [HttpGet("spin")]
    public async Task<IActionResult> Spin()
    {
        var result = await Task.Run(() =>
        {
            var result = _betMoveService.Spin();
            return new { data = result};
        });

        return Ok(result);
    }

    [HttpGet("history")]
    public async Task<IActionResult> ShowPreviousSpins()
    {
        var result = await _mapper.GetSavedPlayerBetFromDb();

        return Ok(result);
    }

}