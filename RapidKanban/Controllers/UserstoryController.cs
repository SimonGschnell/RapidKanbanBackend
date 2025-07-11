using Microsoft.AspNetCore.Mvc;
using RapidKanban.Application.DTO;
using RapidKanban.Application.Services;

namespace RapidKanban.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserstoryController: ControllerBase
{

    private readonly IUserstoryService UserstoryService;
    public UserstoryController(IUserstoryService userstoryService)
    {
        UserstoryService = userstoryService;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateUserstory([FromBody] CreateUserstoryDTO userstoryDTO)
    {
        var id = await UserstoryService.CreateUserstoryAsync(userstoryDTO);
        return Ok(id);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetUserstories()
    {
        var userStoriesDTO = await UserstoryService.GetAll();
        return Ok(userStoriesDTO);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetUserstories(int id)
    {
        var userStoryDTO = await UserstoryService.GetById(id);
        return Ok(userStoryDTO);
    }
}