using BookRoom.Domain.Contract.Responses;
using BookRoom.Domain.Contract.Responses.UserResponses;
using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace BookRoom.Domain.Contract.Requests.Commands.UserCommands
{
    [ExcludeFromCodeCoverage]
    public class UserCreateRequest : IRequest<CommonResponse<UserResponse>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime BornDate { get; set; }
        public int Reference { get; set; }
    }
}
