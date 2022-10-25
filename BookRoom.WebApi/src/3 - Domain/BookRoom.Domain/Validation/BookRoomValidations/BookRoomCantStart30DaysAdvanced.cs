using BookRoom.Domain.Contract.Constants;
using BookRoom.Domain.Entities;
using FluentValidation;

namespace BookRoom.Domain.Validation.BookRoomValidations
{
    public class BookRoomCantStart30DaysAdvanced : AbstractValidator<BookRooms>
    {
        public BookRoomCantStart30DaysAdvanced()
        {
            RuleFor(x => x.StartDate)
                .LessThanOrEqualTo(DateTime.Now.AddDays(30))
                .WithMessage(ErrorMessages.BookRoomMessages.BOOK_CANT_START_30_DAYS_ADVANCED);
        }
    }
}
