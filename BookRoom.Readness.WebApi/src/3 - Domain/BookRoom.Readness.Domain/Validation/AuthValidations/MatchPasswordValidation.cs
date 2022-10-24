using BookRoom.Readness.Domain.Contract.Constants;
using BookRoom.Readness.Domain.Entities;
using FluentValidation;

namespace BookRoom.Readness.Domain.Validation.AuthValidations
{
    public class MatchPasswordValidation : AbstractValidator<User>
    {
        private readonly string _password;
        public MatchPasswordValidation(string password)
        {
            _password = password;

            RuleFor(x => x).Custom((usr, context)=>
            {
                if (!usr.Password.Equals(_password))
                {
                    context.AddFailure(ErrorMessages.AuthMessages.PASSWORD_NOTMATCH);
                }
            });
        }
    }
}
