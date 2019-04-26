using System.Threading.Tasks;
using Minerva.Shared.Contract.Request.History;
using Minerva.Shared.Contract.Response.History;

namespace Minerva.Shared.Services
{
    public interface IHistoryService
    {
        Task<GetHistoryResponse> GetHistoryAsync(GetHistoryRequest request);
        Task<AddHistoryResponse> AddHistoryAsync(AddHistoryRequest request);
    }
}