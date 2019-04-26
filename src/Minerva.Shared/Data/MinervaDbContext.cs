using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Minerva.Shared.Data.Entities;

namespace Minerva.Shared.Data
{
    public class MinervaDbContext : IdentityDbContext<UserEntity>
    {
        public MinervaDbContext(DbContextOptions<MinervaDbContext> dbContextOptions)
            : base(dbContextOptions)
        {

        }

        public DbSet<HistoryEntity> Histories { get; set; }
    }
}