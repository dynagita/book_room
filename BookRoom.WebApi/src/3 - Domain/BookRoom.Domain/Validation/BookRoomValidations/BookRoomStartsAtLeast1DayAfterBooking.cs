using BookRoom.Domain.Contract.Constants;
using BookRoom.Domain.Entities;
using FluentValidation;

namespace BookRoom.Domain.Validation.BookRoomValidations
{
    public class BookRoomStartsAtLeast1DayAfterBooking : AbstractValidator<BookRooms>
    {
        public BookRoomStartsAtLeast1DayAfterBooking()
        {
            RuleFor(x => x.StartDate.Subtract(x.DatAlt).Days)
                .GreaterThanOrEqualTo(1)
                .WithMessage(ErrorMessages.BookRoomMessages.BOOK_STARTS_AT_LEAST_1_DAY_BOOKING);
        }
    }
}
