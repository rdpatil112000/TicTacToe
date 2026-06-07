using Microsoft.AspNetCore.Mvc;
using TicTacToe.Api.Services;

namespace TicTacToe.Api.Controllers;

[ApiController]
[Route("api/scoreboard")]
public class ScoreboardController : ControllerBase
{
    private readonly IGameService _service;

    public ScoreboardController(
        IGameService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(
            _service.GetScoreboard());
    }

    [HttpPost("reset")]
    public IActionResult Reset()
    {
        _service.ResetScoreboard();

        return Ok();
    }
}