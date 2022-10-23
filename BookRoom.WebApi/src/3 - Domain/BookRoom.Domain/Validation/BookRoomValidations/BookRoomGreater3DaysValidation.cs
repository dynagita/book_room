using BookRoom.Domain.Contract.Constants;
using BookRoom.Domain.Entities;
using FluentValidation;

namespace BookRoom.Domain.Validation.BookRoomValidations
{
    public class BookRoomGreater3DaysValidation : AbstractValidator<BookRooms>
    {
        public BookRoomGreater3DaysValidation()
        {

            RuleFor(x => x.EndDate.Subtract(x.StartDate).Days)
                .LessThanOrEqualTo(3)
                .WithMessage(ErrorMessages.BookRoomMessages.BOOK_GREATER_3_DAYS);

        }
    }
}
