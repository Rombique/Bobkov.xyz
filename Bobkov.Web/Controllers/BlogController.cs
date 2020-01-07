using Bobkov.BL.Interfaces;
using Bobkov.Web.Models.Blog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Bobkov.Web.Controllers
{
    public class BlogController : BaseController
    {
        private readonly IBlogService blogService;
        public BlogController(ILogger<BaseController> logger, IBlogService blogService) : base(logger)
        {
            this.blogService = blogService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult NewCategory(CategoryVM model)
        {
            if (ModelState.IsValid)
            {
                var result = blogService.AddNewCategory(model.Name);
                if (result.Succeedeed)
                {
                    return RedirectToAction("NewCategory");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.Message);
                }
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult NewCategory()
        {
            return View();
        }
    }
}