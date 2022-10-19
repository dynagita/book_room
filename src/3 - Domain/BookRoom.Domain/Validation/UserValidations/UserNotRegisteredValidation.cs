using BookRoom.Domain.Contract.Constants;
using BookRoom.Domain.Entities;
using FluentValidation;

namespace BookRoom.Domain.Validation.UserValidations
{
    public class UserNotRegisteredValidation : AbstractValidator<User>
    {
        private readonly User _userDb;
        public UserNotRegisteredValidation(User userDb)
        {
            _userDb = userDb;

            RuleFor(x => x).Custom((usr, context) =>
            {
                if (_userDb == null) {
                    return;
                }
                if (usr.Email.Trim().ToLower().Equals(_userDb.Email.Trim().ToLower()))
                {
                    context.AddFailure(ErrorMessages.UserMessages.USER_REGISTERED);
                }

            });

        }
    }
}
