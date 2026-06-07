using Microsoft.AspNetCore.Mvc;
using TicTacToe.Api.DTOs;
using TicTacToe.Api.Services;

namespace TicTacToe.Api.Controllers;

[ApiController]
[Route("api/games")]
public class GamesController : ControllerBase
{
    private readonly IGameService _service;

    public GamesController(IGameService service)
    {
        _service = service;
    }

    [HttpPost]
    public IActionResult Create(
        CreateGameRequest request)
    {
        return Ok(
            _service.CreateGame(request));
    }

    [HttpGet("{id}")]
    public IActionResult Get(Guid id)
    {
        return Ok(
            _service.GetGame(id));
    }

    [HttpPost("{id}/moves")]
    public IActionResult Move(
        Guid id,
        MoveRequest request)
    {
        return Ok(
            _service.MakeMove(id, request));
    }

    [HttpPost("{id}/undo")]
    public IActionResult Undo(Guid id)
    {
        return Ok(
            _service.Undo(id));
    }

    [HttpPost("{id}/reset")]
    public IActionResult Reset(Guid id)
    {
        return Ok(
            _service.Reset(id));
    }
}