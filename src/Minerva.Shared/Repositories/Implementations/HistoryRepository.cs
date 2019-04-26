using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Minerva.Shared.Data;
using Minerva.Shared.Data.Entities;

namespace Minerva.Shared.Repositories.Implementations
{
    public class HistoryRepository : IHistoryRepository
    {
        private readonly MinervaDbContext _dbContext;
        private readonly ILogger<HistoryRepository> _logger;

        public HistoryRepository(MinervaDbContext dbContext, ILogger<HistoryRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<HistoryEntity> GetHistoryByUrlAsync(string url)
        {
            return await _dbContext.Histories.SingleOrDefaultAsync(historyEntity => historyEntity.Url == url);
        }

        public async Task<bool> IsExistsByUrlAsync(string url)
        {
            return await _dbContext.Histories.AnyAsync(historyEntity => historyEntity.Url == url);
        }

        public async Task<bool> AddAsync(HistoryEntity entity)
        {
            await _dbContext.Histories.AddAsync(entity);

            try
            {
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}