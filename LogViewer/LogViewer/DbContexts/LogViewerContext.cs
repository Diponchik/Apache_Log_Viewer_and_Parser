using LogViewer.Entities;
using LogViewer.Entities.Mappings;
using LogViewer.Extensions;
using Microsoft.EntityFrameworkCore;

namespace LogViewer.DbContexts
{
    public class LogViewerContext : DbContext
    {
        public LogViewerContext(DbContextOptions<LogViewerContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.MapEntity<LogModel, LogModelMap>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
