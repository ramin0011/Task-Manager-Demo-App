using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Api.Controllers.Base
{
    public class BaseController :ControllerBase
    {
        protected string? GetUserId()
        {
            return User.Claims.FirstOrDefault(a=>a.Type=="Id")?.Value;
        }  
        protected List<string>? GetUserRole()
        {
            return User.Claims.Where(a=>a.Type== ClaimTypes.Role)?.Select(a=>a.Value).ToList();
        }  
        protected bool IsAdmin()
        {
            return GetUserRole().Contains("admin");
        }
    }
}
