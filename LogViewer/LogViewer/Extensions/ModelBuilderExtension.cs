using LogViewer.Extensions.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LogViewer.Extensions
{
    public static class ModelBuilderExtension
    {
        public static void MapEntity<T, TK>(this ModelBuilder builder) where TK : IMappingEntity<T>, new() where T : class
        {
            var mapper = new TK();
            mapper.Map(builder.Entity<T>());
        }
    }
}
