using BookRoom.Readness.Domain.Contract.Responses;
using BookRoom.Readness.Domain.Contract.Responses.BookRoomsResponses;
using MediatR;

namespace BookRoom.Readness.Domain.Contract.Requests.Queries.BookRoomsQueries
{
    public class ListBooksByUserRequest : IRequest<CommonResponse<List<BookRoomResponse>>>
    {
        public string Email { get; set; }
    }
}
