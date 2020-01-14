using Bobkov.BL.DTO;
using Bobkov.BL.Interfaces;
using Bobkov.Web.Models;
using Bobkov.Web.Models.Blog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

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
                    AuthorId = GetCurrentUserId(),
                    CategoryId = model.SelectedCategoryId,
                    Title = model.Title,
                    Preview = model.Preview,
                    Content = model.Content
                };
                var result = blogService.AddNewPost(post);
                if (result.Succeedeed)
                {
                    return RedirectToAction("Posts");
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

        [HttpGet]
        public ActionResult PostDetails(int id)
        {
            PostDTO post = blogService.GetPostById(id);
            PostDetailsVM postDetails = new PostDetailsVM()
            {
                Author = post.Author,
                Category = post.Category,
                Content = post.Content,
                DateCreated = post.DateCreated,
                DateUpdated = post.DateUpdated,
                Preview = post.Preview,
                Title = post.Title
            };
            return View(postDetails);
        }

        [HttpGet]
        public IActionResult Posts(int? page)
        {
            var pageNumber = page ?? 1;
            var posts = blogService.GetPostsByPage(pageNumber, 2);
            var postsCount = blogService.GetPostsCount();
            PageInfo pageInfo = new PageInfo { PageNumber = pageNumber, PageSize = 2, TotalItems = postsCount };
            var allPosts = posts.Select(post => new PostDetailsVM
            {
                Title = post.Title,
                Id = post.Id,
                Author = post.Author,
                Category = post.Category,
                DateCreated = post.DateCreated,
                Preview = post.Preview,
            });

            return View(new AllPostsVM { Posts = allPosts, PageInfo = pageInfo });
        }
    }
}