using Bobkov.BL.DTO;
using Bobkov.BL.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bobkov.BL.Interfaces
{
    public interface IBlogService
    {
        OperationDetails AddNewCategory(string categoryName);
        OperationDetails AddNewPost(PostDTO post);
        IEnumerable<CategoryDTO> GetCategories();
    }
}
