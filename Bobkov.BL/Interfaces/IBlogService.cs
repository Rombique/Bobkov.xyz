using Bobkov.BL.DTO;
using Bobkov.BL.Infrastructure;
using System.Collections.Generic;

namespace Bobkov.BL.Interfaces
{
    public interface IBlogService
    {
        OperationDetails AddNewCategory(string categoryName);
        OperationDetails AddNewPost(PostDTO post);
        IEnumerable<CategoryDTO> GetCategories();
        PostDTO GetPostById(int id);
        CategoryDTO GetCategoryById(int id);
        IEnumerable<PostDTO> GetPostsByPage(int page, int pageSize = 10);
        int GetPostsCount();
    }
}
