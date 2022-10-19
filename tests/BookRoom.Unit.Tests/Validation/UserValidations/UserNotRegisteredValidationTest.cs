using AutoBogus;
using BookRoom.Domain.Entities;
using BookRoom.Domain.Validation.UserValidations;
using FluentAssertions;

namespace BookRoom.Unit.Tests.Validation.UserValidations
{
    public class UserNotRegisteredValidationTest
    {
        [Fact(DisplayName = "UserShouldNotBeRegistered")]
        public async Task UserShouldNotBeRegistered()
        {
            var user = new AutoFaker<User>().Generate();
            var validUser = new AutoFaker<User>().Generate();

            var validation = new UserNotRegisteredValidation(user);

            var result = await validation.ValidateAsync(validUser, new CancellationToken());

            result.IsValid
                .Should()
                .BeTrue();
        }

        [Fact(DisplayName = "UserShouldBeRegistered")]
        public async Task UserShouldBeRegistered()
        {
            var user = new AutoFaker<User>().Generate();

            var validation = new UserNotRegisteredValidation(user);

            var result = await validation.ValidateAsync(user, new CancellationToken());

            result.IsValid
                .Should()
                .BeFalse();
        }
    }
}
