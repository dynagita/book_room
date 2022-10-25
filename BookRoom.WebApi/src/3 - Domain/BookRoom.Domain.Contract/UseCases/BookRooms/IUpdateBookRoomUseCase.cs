using BookRoom.Domain.Contract.Requests.Commands.BookRooms;
using BookRoom.Domain.Contract.Responses.BookRoomsResponses;
using BookRoom.Domain.Contract.Responses;

namespace BookRoom.Domain.Contract.UseCases.BookRooms
{
    public interface IUpdateBookRoomUseCase : IUseCaseBase<UpdateBookRoomRequest, CommonResponse<BookRoomResponse>>
    {
    }
}
