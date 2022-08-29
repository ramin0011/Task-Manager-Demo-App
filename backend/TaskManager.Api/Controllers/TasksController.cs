using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TaskManager.Api.Models;
using TaskManager.Application.Models;
using TaskManager.Application.Services.Interfaces;

namespace TaskManager.Api.Controllers
{
    [Route("[controller]/[action]")]
    public class TasksController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly ITaskManagementService _taskManagementService;
        private readonly ILogger<AuthenticationController> _logger;
        private IConfiguration _configuration;

        public TasksController(ILogger<AuthenticationController> logger, IConfiguration configuration, IUsersService usersService, ITaskManagementService taskManagementService)
        {
            _logger = logger;
            _configuration = configuration;
            _usersService = usersService;
            _taskManagementService = taskManagementService;
        }

        [Authorize(Roles = "worker,admin")]
        [HttpGet(Name = "get_tasks")]
        public async Task<IActionResult> GetTasks()
        {
            var tasks = await _taskManagementService.GetTasks();
            return Ok(tasks);
        }

        [Authorize(Roles = "admin")]
        [HttpPost(Name = "create_task")]
        public async Task<IActionResult> CreateTask(TaskModel model)
        {
            await _taskManagementService.CreateTask(model);
            return Ok(model);
        } 
        
        
        [HttpGet(Name = "claim_task")]
        [Authorize(Roles = "admin,worker")]
        public async Task<IActionResult> ClaimTask(string taskId, string userId)
        {
            await _taskManagementService.AssignTask(taskId,userId);
            return Ok();
        }
    }
}