using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Api.Controllers.Base
{
    public class BaseController :ControllerBase
    {
        protected string? GetUserId()
        {
            return User.Claims.FirstOrDefault(a=>a.Type=="Id")?.Value;
        }
    }
}
