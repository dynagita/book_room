using BookRoom.Application.Extensions;
using FluentAssertions;

namespace BookRoom.Unit.Tests.Extensions
{
    public class StringExtensionTest
    {
        [Fact(DisplayName = "ShouldComputeHash")]
        public async Task ShouldComputeHash()
        {
            string pass;
            string passHashed;

            pass = passHashed = "teste";

            passHashed = passHashed.ComputeHash();

            pass
                .Should()
                .NotBe(passHashed);

        }
    }
}
