using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Minerva.Shared.Data;
using Minerva.Shared.Data.Entities;

namespace Minerva.Shared.Repositories.Implementations
{
    public class BookmarkRepository : IBookmarkRepository
    {
        private readonly MinervaDbContext _dbContext;
        private readonly ILogger<BookmarkRepository> _logger;

        public BookmarkRepository(MinervaDbContext dbContext, ILogger<BookmarkRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<bool> AddAsync(BookmarkEntity entity)
        {
            await _dbContext.Bookmarks.AddAsync(entity);

            try
            {
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("Has Exception", e);
                return false;
            }
        }
    }
}