using LogViewer.Extensions.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LogViewer.Entities.Mappings
{
    public class LogModelMap : IMappingEntity<LogModel>
    {
        public void Map(EntityTypeBuilder<LogModel> builder)
        {
            builder.ToTable("LogModels", "dbo");
            builder.HasKey(entity => entity.Id);

            builder
                .Property(entity => entity.Id)
                .HasColumnName("Id")
                .IsRequired();

            builder
                .Property(entity => entity.Date)
                .HasColumnName("Date");

            builder
                .Property(entity => entity.HostName)
                .HasColumnName("HostName");

            builder
                .Property(entity => entity.Route)
                .HasColumnName("Route");

            builder
                .Property(entity => entity.Params)
                .HasColumnName("Params");

            builder
                .Property(entity => entity.Status)
                .HasColumnName("Status");

            builder
                .Property(entity => entity.Size)
                .HasColumnName("Size");

            builder
                .Property(entity => entity.Country)
                .HasColumnName("Country");
        }
    }
}
