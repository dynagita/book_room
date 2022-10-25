using BookRoom.Domain.Entities;
using BookRoom.Infrastructure.Database.Context.DatabaseMapper;
using Microsoft.EntityFrameworkCore;

namespace BookRoom.Infrastructure.Database.Context
{
    public class BookRoomDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<BookRooms> Books { get; set; }

        public BookRoomDbContext(DbContextOptions<BookRoomDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new RoomMap());
            modelBuilder.ApplyConfiguration(new BookRoomsMap());
        }
    }
}
