using Microsoft.AspNetCore.Mvc;
using RapidKanban.Application.DTO;
using RapidKanban.Application.Services;

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
        return Ok(id);
    }
}