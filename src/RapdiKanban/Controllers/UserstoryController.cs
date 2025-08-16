using Microsoft.AspNetCore.Mvc;
using RapidKanban.Application.DTO;
using RapidKanban.Application.Services;
using RapidKanban.CQRS;

namespace RapidKanban.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserstoryController: ControllerBase
{

    private readonly IUserstoryService UserstoryService;
    private readonly ICommandDispatcher _dispatcher;
    public UserstoryController(IUserstoryService userstoryService, ICommandDispatcher dispatcher)
    {
        UserstoryService = userstoryService;
        _dispatcher = dispatcher;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateUserstory([FromBody] CreateUserstoryDTO userstoryDTO)
    {
        var id = await UserstoryService.CreateUserstoryAsync(userstoryDTO);
        return CreatedAtAction(nameof(GetUserstoryById), new { id }, userstoryDTO);
    }
    
    [HttpPost("CQRS")]
    public async Task<IActionResult> CreateUserstoryQCRS([FromBody] CreateUserstoryCommand command)
    {
        var userstoryDTO = await _dispatcher.Send<UserstoryDTO>(command);
        return CreatedAtAction(nameof(GetUserstoryById), new { userstoryDTO.Id }, userstoryDTO);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetUserstories()
    {
        var userStoriesDTO = await UserstoryService.GetAll();
        return Ok(userStoriesDTO);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetUserstoryById(int id)
    {
        var userStoryDTO = await UserstoryService.GetById(id);
        return Ok(userStoryDTO);
    }
    
    [HttpGet("basic")]
    public async Task<IActionResult> GetUserstoriesBasic()
    {
        var userStoriesBasicDTO = await UserstoryService.GetAllBasic();
        return Ok(userStoriesBasicDTO);
    }

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> PatchUserstory(int id, [FromBody] PatchUserstoryDTO patchUserstoryDTO)
    {
        var userstoryDto = await UserstoryService.PatchUserstory(id, patchUserstoryDTO);
        return Ok(userstoryDto);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteUserstory(int id)
    {
        await UserstoryService.DeleteUserstory(id);
        return NoContent();
    }
}