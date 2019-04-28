using System.Threading.Tasks;
using Minerva.Shared.Data.Entities;

namespace Minerva.Shared.Repositories
{
    public interface IBookmarkRepository
    {
        Task<bool> AddAsync(BookmarkEntity entity);
    }
}