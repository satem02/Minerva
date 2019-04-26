using System.Threading.Tasks;
using Minerva.Shared.Data.Entities;

namespace Minerva.Shared.Repositories
{
    public interface IPostRepository
    {
        Task<PostEntity> GetPostByUrlAsync(string url);
        Task<bool> IsExistsByUrlAsync(string url);
        Task<bool> AddAsync(PostEntity entity);
    }
}