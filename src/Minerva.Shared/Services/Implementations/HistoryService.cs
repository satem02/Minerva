using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Minerva.Shared.Contract.Request.History;
using Minerva.Shared.Contract.Response.History;
using Minerva.Shared.Mappers;
using Minerva.Shared.Repositories;

namespace Minerva.Shared.Services.Implementations
{
    public class HistoryService : IHistoryService
    {
        private readonly IHistoryRepository _historyRepository;
        private readonly IHistoryMapper _historyMapper;
        private readonly ILogger<HistoryService> _logger;
        public HistoryService(IHistoryRepository historyRepository, IHistoryMapper historyMapper, ILogger<HistoryService> logger)
        {
            _historyRepository = historyRepository;
            _historyMapper = historyMapper;
            _logger = logger;
        }

        public async Task<GetHistoryResponse> GetHistoryAsync(GetHistoryRequest request)
        {
            var response = new GetHistoryResponse();

            var entity = await _historyRepository.GetHistoryByUrlAsync(request.Url);
            if (entity == null)
            {
                response.StatusCode = (int) HttpStatusCode.NotFound;
                return response;
            }

            response.History = _historyMapper.ToModel(entity);
            return response;
        }

        public async Task<AddHistoryResponse> AddHistoryAsync(AddHistoryRequest request)
        {
            var response = new AddHistoryResponse();

            bool isExists = await _historyRepository.IsExistsByUrlAsync(request.Url);
            if (isExists)
            {
                response.StatusCode = (int) HttpStatusCode.Conflict;
                return response;
            }

            var entity = _historyMapper.ToEntity(request);
            bool isSuccess = await _historyRepository.AddAsync(entity);

            if (!isSuccess)
            {
                response.StatusCode = (int) HttpStatusCode.InternalServerError;
                return response;
            }

            response.StatusCode = (int) HttpStatusCode.Created;
            return response;
        }
    }
}