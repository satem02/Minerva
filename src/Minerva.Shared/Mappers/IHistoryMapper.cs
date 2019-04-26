using Minerva.Shared.Contract.Models;
using Minerva.Shared.Contract.Response.History;
using Minerva.Shared.Data.Entities;

namespace Minerva.Shared.Mappers
{
    public interface IHistoryMapper : IMapper<HistoryEntity, HistoryModel>
    {
        HistoryEntity ToEntity(AddHistoryRequest request);
    }
}