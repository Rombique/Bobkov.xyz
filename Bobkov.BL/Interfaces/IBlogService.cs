using Bobkov.BL.DTO;
using Bobkov.BL.Infrastructure;
using System.Threading.Tasks;

namespace Bobkov.BL.Interfaces
{
    public interface IBlogService
    {
        OperationDetails AddNewCategory(string categoryName);
        Task<OperationDetails> AddNewPost(PostDTO post);
    }
}
