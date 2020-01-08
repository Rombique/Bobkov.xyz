using Bobkov.BL.DTO;
using Bobkov.BL.Infrastructure;
using Bobkov.BL.Interfaces;
using Bobkov.DAL.Entities;
using Bobkov.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bobkov.BL.Services
{
    public class BlogService : IBlogService
    {
        IUnitOfWork UnitOfWork { get; set; }

        public BlogService(IUnitOfWork uow)
        {
            UnitOfWork = uow;
        }

        public OperationDetails AddNewPost(PostDTO post)
        {
            var newPost = UnitOfWork.Repository<Post>().GetFirst(p => p.Title == post.Title, null, true);
            if (newPost == null)
            {
                newPost = new Post()
                {
                    AuthorId = post.AuthorId,
                    CategoryId = post.CategoryId,
                    Content = post.Content,
                    DateCreated = DateTime.Now,
                    Preview = post.Preview,
                    Title = post.Title
                };
                try
                {
                    UnitOfWork.Repository<Post>().AddNew(newPost);
                    UnitOfWork.Commit();
                    return new OperationDetails(true, $"Пост успешно добавлен!", "BlogService.AddNewPost");
                }
                catch (DbUpdateException ex)
                {
                    return new OperationDetails(false, ex.Message, "BlogService.AddNewPost");
                }
            }
            else
            {
                return new OperationDetails(false, $"Пост с заголовком \"{post.Title}\" уже существует!", "BlogService.AddNewPost");
            }
        }

        public OperationDetails AddNewCategory(string categoryName)
        {
            var category = UnitOfWork.Repository<Category>().GetFirst(c => c.Name == categoryName, null, true);
            if (category == null)
            {
                category = new Category { Name = categoryName };
                try
                {
                    UnitOfWork.Repository<Category>().AddNew(category);
                    UnitOfWork.Commit();
                    return new OperationDetails(true, $"Категория \"{categoryName}\" успешно добавлена!", "BlogService.CreateNewCategory");
                }
                catch(DbUpdateException ex)
                {
                    return new OperationDetails(false, ex.Message, "BlogService.CreateNewCategory");
                }
            }
            else
            {
                return new OperationDetails(false, $"Категория \"{categoryName}\" уже существует!", "BlogService.CreateNewCategory");
            }
        }

        public IEnumerable<CategoryDTO> GetCategories()
        {
            var allCategories = UnitOfWork.Repository<Category>().GetAll(true);
            return allCategories.Select(c => new CategoryDTO() { Id = c.Id, Name = c.Name });
        }

        public PostDTO GetPostById(int id)
        {
            var post = UnitOfWork.Repository<Post>().GetFirst(p => p.Id == id, null, true, i => i.Author, i => i.Category);
            if (post == null)
                return null;

            return new PostDTO() //TODO: либо автомеппер либо напиши собственные мепперы
            {
                AuthorId = post.AuthorId,
                Author = post.Author.UserName,
                Category = post.Category.Name,
                CategoryId = post.CategoryId,
                Id = post.Id,
                Content = post.Content,
                DateCreated = post.DateCreated,
                DateUpdated = post.DateUpdated,
                Preview = post.Preview,
                Title = post.Title
            };
        }

        public IEnumerable<PostDTO> GetPostsByPage(int page, int pageSize = 10)
        {
            var posts = UnitOfWork.PostsRepository.GetPaged(page, pageSize, true);
            if (posts == null)
                return null;

            return posts.Select(post => new PostDTO
            {
                AuthorId = post.AuthorId,
                Author = post.Author.UserName,
                CategoryId = post.CategoryId,
                Category = post.Category.Name,
                Id = post.Id,
                Content = post.Content,
                DateCreated = post.DateCreated,
                DateUpdated = post.DateUpdated,
                Preview = post.Preview,
                Title = post.Title
            });
        }

        public CategoryDTO GetCategoryById(int id)
        {
            var category = UnitOfWork.Repository<Category>().GetFirst(c => c.Id == id);
            if (category == null)
                return null;
            return new CategoryDTO() { Id = category.Id, Name = category.Name };
        }

        public int GetPostsCount()
        {
            return UnitOfWork.PostsRepository.GetCount(null, true);
        }
    }
}
