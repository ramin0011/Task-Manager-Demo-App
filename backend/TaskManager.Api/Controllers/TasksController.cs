using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using TaskManager.Api.Controllers.Base;
using TaskManager.Api.MessageBroker;
using TaskManager.Api.Models;
using TaskManager.Application.Exception;
using TaskManager.Application.Models;
using TaskManager.Application.Services.Interfaces;

namespace TaskManager.Api.Controllers
{
    [Route("[controller]/[action]")]
    public class TasksController : BaseController
    {
        private readonly IUsersService _usersService;
        private readonly ITaskManagementService _taskManagementService;
        private readonly ILogger<AuthenticationController> _logger;
        private IConfiguration _configuration;
        private readonly IHubContext<SignalRManager> _hubContext;
        public TasksController(ILogger<AuthenticationController> logger, IConfiguration configuration, IUsersService usersService, ITaskManagementService taskManagementService, IHubContext<SignalRManager> hubContext)
        {
            _logger = logger;
            _configuration = configuration;
            _usersService = usersService;
            _taskManagementService = taskManagementService;
            _hubContext=hubContext;
        }

        [Authorize(Roles = "worker,admin")]
        [HttpGet(Name = "get_tasks")]
        public async Task<IActionResult> GetTasks()
        {
            var tasks = await _taskManagementService.GetTasks(IsAdmin());
            return Ok(tasks);
        } 
        
        [Authorize(Roles = "worker,admin")]
        [HttpGet(Name = "get_my_tasks")]
        public async Task<IActionResult> GetMyTasks()
        {
            var tasks = await _taskManagementService.GetMyTasks(GetUserId());
            return Ok(tasks);
        }

        [Authorize(Roles = "admin")]
        [HttpPost(Name = "create_task")]
        public async Task<IActionResult> CreateTask([FromBody]TaskModel model)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveMessage");
            await _taskManagementService.CreateTask(model);
            return Ok(model);
        } 
        
        
        [HttpGet(Name = "claim_task")]
        [Authorize(Roles = "admin,worker")]
        public async Task<IActionResult> ClaimTask(string taskId)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveMessage");
            try
            {
                await _taskManagementService.AssignTask(taskId, GetUserId());
            }
            catch (AppException e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }
    }
}