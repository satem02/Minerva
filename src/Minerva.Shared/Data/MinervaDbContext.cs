using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Minerva.Shared.Data.Entities;
using Minerva.Shared.Services;

namespace Minerva.Shared.Data
{
    public class MinervaDbContext : IdentityDbContext<UserEntity>
    {
        public MinervaDbContext(DbContextOptions<MinervaDbContext> dbContextOptions)
            : base(dbContextOptions)
        {

        }

        public DbSet<BookmarkEntity> Bookmarks { get; set; }
        public DbSet<PostEntity> Posts { get; set; }
    }
}