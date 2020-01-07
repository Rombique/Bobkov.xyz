using Bobkov.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bobkov.Web.Controllers
{
    public class PostController : BaseController
    {
        public PostController(ILogger<BaseController> logger, UnitOfWork uow) : base(logger, uow)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}