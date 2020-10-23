using Microsoft.EntityFrameworkCore;
using System.IO;

namespace RepordDbPopulater.DataBase
{
    public class DBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string ConnectionString = @"";

            string[] lines = File.ReadAllLines(@"./../../../config.txt");

            foreach (string line in lines)
            {

                string[] info = line.Split('>');
                if (info[0] == "ConnectionString")
                {
                    ConnectionString += info[1];

                }

            }
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        public DbSet<CreditDataReport> Reports { get; set; }
        public DbSet<ApiUser> ApiUsers { get; set; }



    }
}
 