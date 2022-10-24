using BookRoom.Domain.Contract.Constants;
using BookRoom.Domain.Entities;
using BookRoom.Domain.Validation.BookRoomValidations;
using FluentAssertions;
using FluentValidation;

namespace BookRoom.Unit.Tests.Validation.BookRoomValidations
{
    public class BookRoomStartsAtLeast1DayAfterBookingTest 
    {
        [Fact(DisplayName = "ShouldBeValid")]
        public async Task ShouldBeValid()
        {
            var bookRoom = new BookRooms()
            {
                StartDate = DateTime.Today.AddDays(1),
                EndDate = DateTime.Today.AddDays(4),
                DatAlt = DateTime.Today
            };

            var validation = new BookRoomStartsAtLeast1DayAfterBooking();

            var result = validation.Validate(bookRoom);

            result.IsValid
                .Should()
                .BeTrue();
        }

        [Fact(DisplayName = "ShouldNotBeValid")]
        public async Task ShouldNotBeValid()
        {
            var bookRoom = new BookRooms()
            {
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(3),
                DatAlt = DateTime.Today
            };

            var validation = new BookRoomStartsAtLeast1DayAfterBooking();

            var result = validation.Validate(bookRoom);

            result.IsValid
                .Should()
                .BeFalse();
            result.Errors
                .Should()
                .HaveCount(1);
        }
    }
}
