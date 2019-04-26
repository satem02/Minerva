using System.Threading.Tasks;
using Minerva.Shared.Data.Entities;

namespace Minerva.Shared.Repositories
{
    public interface IHistoryRepository
    {
        Task<HistoryEntity> GetHistoryByUrlAsync(string url);
        Task<bool> IsExistsByUrlAsync(string url);
        Task<bool> AddAsync(HistoryEntity entity);
    }
}