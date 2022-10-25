using BookRoom.Domain.Contract.Constants;
using BookRoom.Domain.Entities;
using BookRoom.Domain.Validation.BookRoomValidations;
using FluentAssertions;

namespace BookRoom.Unit.Tests.Validation.BookRoomValidations
{
    public class BookRoomCantStart30DaysAdvancedTest
    {
        [Fact(DisplayName = "ShouldBeValid")]
        public async Task ShouldBeValid()
        {
            var bookRoom = new BookRooms()
            {
                StartDate = DateTime.Today.AddDays(5)
            };
            
            var validation = new BookRoomCantStart30DaysAdvanced();

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
                StartDate = DateTime.Today.AddDays(31)
            };

            var validation = new BookRoomCantStart30DaysAdvanced();

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
