using AutoBogus;
using BookRoom.Domain.Entities;
using BookRoom.Domain.Validation.AuthValidations;
using FluentAssertions;

namespace BookRoom.Unit.Tests.Validation.AuthValidations
{
    public class AuthValidationTests
    {
        [Fact(DisplayName = "ShouldMatchPassword")]
        public async Task ShouldMatchPassword()
        {
            var user = new AutoFaker<User>().Generate();

            string pass = user.Password;

            var validation = new MatchPasswordValidation(pass);

            var result = await validation.ValidateAsync(user);

            result.IsValid
                .Should()
                .BeTrue();
        }

        [Fact(DisplayName = "ShouldNotMatchPassword")]
        public async Task ShouldNotMatchPassword()
        {
            var user = new AutoFaker<User>().Generate();

            string pass = "test";

            var validation = new MatchPasswordValidation(pass);

            var result = await validation.ValidateAsync(user);

            result.IsValid
                .Should()
                .BeFalse();
        }
    }    
}
