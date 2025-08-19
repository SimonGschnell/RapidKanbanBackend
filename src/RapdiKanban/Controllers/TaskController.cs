using Microsoft.AspNetCore.Mvc;
using RapidKanban.Application.DTO;
using RapidKanban.Application.Services;
using RapidKanban.Application.Services.Tasks;
using RapidKanban.Infrastructure.Repositories;

namespace RapidKanban.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController: ControllerBase
{

    private readonly ITaskService TaskService;
    public TaskController(ITaskService taskService)
    {
        TaskService = taskService;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskDTO taskDTO)
    {
        var id = await TaskService.CreateTaskAsync(taskDTO);
        return CreatedAtAction(nameof(GetById), new {id}, taskDTO);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var taskDTO = await TaskService.GetById(id);
        return Ok(taskDTO);
    }
    
    [HttpPatch("{taskId:int}")]
    public async Task<IActionResult> PatchTask(int taskId, [FromBody]PatchTaskDTO patchTaskDTO)
    {
        var taskDTO = await TaskService.UpdateTask(taskId, patchTaskDTO);
        return Ok(taskDTO);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        await TaskService.DeleteTask(id);
        return NoContent();
    }
}