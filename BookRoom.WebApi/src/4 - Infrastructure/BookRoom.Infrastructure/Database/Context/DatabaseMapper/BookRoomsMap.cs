using BookRoom.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookRoom.Infrastructure.Database.Context.DatabaseMapper
{
    public class BookRoomsMap : IEntityTypeConfiguration<BookRooms>
    {
        public void Configure(EntityTypeBuilder<BookRooms> builder)
        {
            builder.ToTable("TB_BookRooms");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.DatAlt)
                .IsRequired();
            builder.Property(x => x.DatInc)
                .IsRequired();
            builder.Property(x => x.Active)
                .IsRequired();
            builder.Property(x => x.StartDate)
                .IsRequired();
            builder.Property(x => x.EndDate)
                .IsRequired();            
            builder.Property(x => x.Status)
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithMany(x => x.Books)
                .IsRequired();
            builder.HasOne(x => x.Room)
                .WithMany(x => x.Books)
                .IsRequired();
        }
    }
}
