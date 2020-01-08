using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;

namespace Bobkov.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly ILogger<BaseController> logger;

        public BaseController(ILogger<BaseController> logger)
        {
            this.logger = logger;
        }

        protected int GetCurrentUserId() => Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
    }
}
