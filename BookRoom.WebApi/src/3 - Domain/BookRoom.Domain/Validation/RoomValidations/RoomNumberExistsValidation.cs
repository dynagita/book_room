using BookRoom.Domain.Contract.Constants;
using BookRoom.Domain.Entities;
using FluentValidation;

namespace BookRoom.Domain.Validation.RoomValidations
{
    public class RoomNumberExistsValidation : AbstractValidator<Room>
    {
        private readonly Room _roomDb;
        public RoomNumberExistsValidation(Room roomDb)
        {
            _roomDb = roomDb;

            RuleFor(x => x).Custom((room, context) => {
                if (_roomDb is not null) { 
                    context.AddFailure(ErrorMessages.RoomMessages.ROOM_EXISTS);
                    return;
                }
            });
        }
    }
}
