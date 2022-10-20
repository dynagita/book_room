using BookRoom.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookRoom.Infrastructure.Database.Context.DatabaseMapper
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("TB_User");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired();
            builder.Property(x => x.LastName)
                .IsRequired();
            builder.Property(x => x.Email)
                .IsRequired();
            builder.Property(x => x.Password)
                .IsRequired();
            builder.Property(x => x.BornDate)
                .IsRequired(false);
            builder.Property(x => x.DatAlt)
                .IsRequired();
            builder.Property(x => x.DatInc)
                .IsRequired();
        }
    }
}
