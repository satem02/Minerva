using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Minerva.Shared.Data;
using Minerva.Shared.Data.Entities;

namespace Minerva.Shared.Repositories.Implementations
{
    public class PostRepository : IPostRepository
    {
        private readonly MinervaDbContext _dbContext;
        private readonly ILogger<PostRepository> _logger;

        public PostRepository(MinervaDbContext dbContext, ILogger<PostRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<PostEntity> GetPostByUrlAsync(string url)
        {
            return await _dbContext.Posts.SingleOrDefaultAsync(historyEntity => historyEntity.Url == url);
        }

        public async Task<bool> IsExistsByUrlAsync(string url)
        {
            return await _dbContext.Posts.AnyAsync(historyEntity => historyEntity.Url == url);
        }

        public async Task<bool> AddAsync(PostEntity entity)
        {
            await _dbContext.Posts.AddAsync(entity);

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