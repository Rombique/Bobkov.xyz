using Bobkov.BL.DTO;
using Bobkov.BL.Interfaces;
using Bobkov.Web.Models.Blog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult NewPost(NewPostVM model)
        {
            if (ModelState.IsValid)
            {
                PostDTO post = new PostDTO()
                {
                    AuthorId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value), //TODO: отрефакторить этот щит
                    CategoryId = model.SelectedCategoryId,
                    Title = model.Title,
                    Preview = model.Preview,
                    Content = model.Content
                };
                var result = blogService.AddNewPost(post);
                if (result.Succeedeed)
                {
                    return RedirectToAction("NewCategory");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.Message);
                }
            }
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult NewPost()
        {
            List<CategoryVM> categoriesModels = blogService.GetCategories()
                .Select(c => new CategoryVM { Id = c.Id, Name = c.Name })
                .ToList();
            NewPostVM newPost = new NewPostVM() { Categories = categoriesModels };
            return View(newPost);
        }
    }
}