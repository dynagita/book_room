using AutoBogus;
using BookRoom.Domain.Validation.RoomValidations;
using FluentAssertions;

namespace BookRoom.Unit.Tests.Validation.RoomValidation
{
    public class RoomNumberExistsValidationTest
    {
        [Fact(DisplayName = "ShouldNotExist")]
        public async Task ShouldNotExist()
        {
            var room = new AutoFaker<Domain.Entities.Room>().Generate();
            var validation = new RoomNumberExistsValidation(null);
            
            var result = await validation.ValidateAsync(room);

            result.IsValid
                .Should()
                .BeTrue();
        }

        [Fact(DisplayName = "ShouldExist")]
        public async Task ShouldExist()
        {
            var room = new AutoFaker<Domain.Entities.Room>().Generate();
            var validation = new RoomNumberExistsValidation(room);

            var result = await validation.ValidateAsync(room);

            result.IsValid
                .Should()
                .BeFalse();
        }
    }
}
