using System.Data.Entity;
using DataBase.Utils.Entities;

namespace DataBase.Utils
{
    public class LogDbContext: DbContext
    {
        public LogDbContext() : base("DefaultConnection")
        { }

        public DbSet<LogModel> Logs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LogModel>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
