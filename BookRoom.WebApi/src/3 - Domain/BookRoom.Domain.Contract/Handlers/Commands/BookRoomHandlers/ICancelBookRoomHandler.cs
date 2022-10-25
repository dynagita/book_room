using BookRoom.Domain.Contract.Requests.Commands.BookRooms;
using BookRoom.Domain.Contract.Responses;
using BookRoom.Domain.Contract.Responses.BookRoomsResponses;
using MediatR;

namespace BookRoom.Domain.Contract.Handlers.Commands.BookRoomHandlers
{
    public interface ICancelBookRoomHandler : IRequestHandler<CancelBookRoomRequest, CommonResponse<BookRoomResponse>>
    {
    }
}
