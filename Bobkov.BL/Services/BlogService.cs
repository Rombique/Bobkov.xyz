using Bobkov.BL.DTO;
using Bobkov.BL.Infrastructure;
using Bobkov.BL.Interfaces;
using Bobkov.DAL.Entities;
using Bobkov.DAL.Interfaces;
using System;
using System.Threading.Tasks;

namespace Bobkov.BL.Services
{
    public class BlogService : IBlogService
    {
        IUnitOfWork UnitOfWork { get; set; }

        public BlogService(IUnitOfWork uow)
        {
            UnitOfWork = uow;
        }

        public Task<OperationDetails> AddNewPost(PostDTO post)
        {
            throw new NotImplementedException();
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
                catch(Exception ex)
                {
                    return new OperationDetails(false, ex.Message, "BlogService.CreateNewCategory");
                }
            }
            else
            {
                return new OperationDetails(false, $"Категория \"{categoryName}\" уже существует!", "BlogService.CreateNewCategory");
            }
        }
    }
}
