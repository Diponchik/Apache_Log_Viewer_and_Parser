using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LogViewer.Extensions.Interfaces
{
    public interface IMappingEntity<T> where T : class
    {
        void Map(EntityTypeBuilder<T> builder);
    }
}
