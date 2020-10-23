using Microsoft.EntityFrameworkCore;

namespace RepordDbPopulater.DataBase
{
    public class DBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Properties.Resources.ConnectionString);
        }

        public DbSet<CreditDataReport> Reports { get; set; }
        public DbSet<ApiUser> ApiUsers { get; set; }

        /// <summary>
        /// Creat CreditDataDb database if it does not exists
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

    }
}
 