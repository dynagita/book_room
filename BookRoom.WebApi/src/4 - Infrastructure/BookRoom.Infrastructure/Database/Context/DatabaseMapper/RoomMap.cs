using BookRoom.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookRoom.Infrastructure.Database.Context.DatabaseMapper
{
    public class RoomMap : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.ToTable("TB_Room");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.DatAlt)
                .IsRequired();
            builder.Property(x => x.DatInc)
                .IsRequired();
            builder.Property(x => x.Active)
                .IsRequired();
            builder.Property(x => x.Title)
                .IsRequired();
            builder.Property(x => x.Description)
                .IsRequired();
            builder.Property(x => x.Number)
                .IsRequired();

            builder.HasMany(x => x.Books)
                .WithOne(x => x.Room);
        }
    }
}
