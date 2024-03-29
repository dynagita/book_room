﻿using BookRoom.Domain.Contract.Enums;
using BookRoom.Domain.Entities;
using BookRoom.Domain.Repositories.EntityFramework;
using BookRoom.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace BookRoom.Infrastructure.Database.Repositories
{
    public class BookRoomsRepository : RepositoryBase<BookRooms>, IBookRoomsRepository
    {
        public BookRoomsRepository(BookRoomDbContext context) : base(context)
        {
        }

        public async Task<BookRooms> GetBookRoomByPeriod(int roomNumber, DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
        {
            return await _dbSet.Where(x => (
            (
                (startDate >= x.StartDate && startDate <= x.EndDate) ||
                (endDate >= x.StartDate && endDate <= x.EndDate)
            ) && x.Active == true &&
            x.Room.Number == roomNumber &&
            x.Status == BookStatusRoom.Confirmed)).FirstOrDefaultAsync();
        }

        public async Task<BookRooms> GetByRoomAsync(int roomId, CancellationToken cancellationToken)
        {
            return await _dbSet.Where(X => X.Room.Id == roomId)
                .FirstOrDefaultAsync();
        }
    }
}
